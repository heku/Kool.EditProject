using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Kool.EditProject.Package;

namespace Kool.EditProject.Models;

internal sealed class VsEditor : IFileEditor
{
    // Editing File - Project File Watcher
    private static readonly Dictionary<string, FileSystemWatcher> EditingFileMap = new();

    private static string GetWatchingFile(FileSystemWatcher watcher) => Path.Combine(watcher.Path, watcher.Filter);
    private static string FindEditingFile(string projectFile) => EditingFileMap.SingleOrDefault(x => GetWatchingFile(x.Value) == projectFile).Key;
    private static bool IsEditing(string projectFile) => FindEditingFile(projectFile) != null;

    private Events _dteEvents;
    private DocumentEvents _documentEvents;
    private bool _detectDocumentSavedEvent = true;

    public void OpenFile(string projectFile)
    {
        if (IsEditing(projectFile))
        {
            ActiveDocument(projectFile);
        }
        else
        {
            if (EditingFileMap.Count == 0)
            {
                RegisterListeners();
            }
            var projectFileWatcher = new FileSystemWatcher(Path.GetDirectoryName(projectFile), Path.GetFileName(projectFile))
            {
                EnableRaisingEvents = true,
                NotifyFilter = NotifyFilters.LastWrite
            };
            var editingFile = TempFileHelper.CreateTempFile(projectFile);
            EditingFileMap.Add(editingFile, projectFileWatcher);
            VsShellUtilities.OpenDocument(Instance, editingFile);
            projectFileWatcher.Changed += OnProjectFileChanged;
        }
    }

    private void OnProjectFileChanged(object sender, FileSystemEventArgs e)
    {
        var projectFile = e.FullPath;
        var document = FindDocument(projectFile);
        if (document is null)
        {
            return;
        }
        var editingFile = document.FullName;
        var watcher = EditingFileMap[editingFile];

        // Try preventing this handler be called multiple times
        watcher.EnableRaisingEvents = false;

        try
        {
            var textDocument = document.Object("TextDocument") as TextDocument;
            var projectFileContent = File.ReadAllText(projectFile);
            var wholeTextEditPoint = textDocument.CreateEditPoint(textDocument.StartPoint);
            wholeTextEditPoint.ReplaceText(textDocument.EndPoint, projectFileContent, (int)vsFindOptions.vsFindOptionsNone);
            _detectDocumentSavedEvent = false;
            document.Save();
        }
        finally
        {
            _detectDocumentSavedEvent = true;
            watcher.EnableRaisingEvents = true;
        }
    }

    private void ActiveDocument(string projectFile) => FindDocument(projectFile)?.Activate();

    private Document FindDocument(string projectFile)
    {
        var editingFile = FindEditingFile(projectFile);

        foreach (Document document in IDE.Documents)
        {
            if (document.FullName == editingFile)
            {
                return document;
            }
        }

        return null;
    }

    private void RegisterListeners()
    {
        _dteEvents = IDE.Events;                        // Prevent it from GC.
        _documentEvents = _dteEvents.DocumentEvents;    // Prevent it from GC.
        _documentEvents.DocumentSaved += DocumentEvents_DocumentSaved;
        _documentEvents.DocumentClosing += DocumentEvents_DocumentClosing;
    }

    private void RemoveListeners()
    {
        _documentEvents.DocumentSaved -= DocumentEvents_DocumentSaved;
        _documentEvents.DocumentClosing -= DocumentEvents_DocumentClosing;
        _dteEvents = null;
        _documentEvents = null;
    }

    private void DocumentEvents_DocumentSaved(Document document)
    {
        if (_detectDocumentSavedEvent)
        {
            var savedFile = document.FullName;

            if (EditingFileMap.TryGetValue(savedFile, out var watcher))
            {
                try
                {
                    watcher.EnableRaisingEvents = false;
                    var projectFile = GetWatchingFile(watcher);
                    File.Copy(savedFile, projectFile, true);
                }
                finally
                {
                    watcher.EnableRaisingEvents = true;
                }
            }
        }
    }

    private void DocumentEvents_DocumentClosing(Document document)
    {
        var closingFile = document.FullName;

        if (EditingFileMap.TryGetValue(closingFile, out var watcher))
        {
            watcher.Changed -= OnProjectFileChanged;
            watcher.Dispose();
            EditingFileMap.Remove(closingFile);
            if (EditingFileMap.Count == 0)
            {
                RemoveListeners();
            }
            TempFileHelper.RemoveTempFile(closingFile);
        }
    }
}