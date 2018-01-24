using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;

namespace Kool.EditProject.Commands
{
    internal abstract class BaseCommand : OleMenuCommand
    {
        // Editing File - Project File
        private static readonly Dictionary<string, string> EditingFileMap = new Dictionary<string, string>();

        private Events _dteEvents;
        private DocumentEvents _documentEvents;

        protected BaseCommand(EditProjectPackage package, string cmdSet, int cmdId)
            : base(OnBaseCommandEventHandler, null, OnBaseBeforeQueryStatus, new CommandID(Guid.Parse(cmdSet), cmdId))
        {
            Package = package;
        }

        protected EditProjectPackage Package { get; }

        protected IEnumerable<Project> SelectedProjects => Package.DTE.SelectedItems.OfType<SelectedItem>().Select(x => x.Project);

        private static void OnBaseBeforeQueryStatus(object sender, EventArgs e) => (sender as BaseCommand).OnBeforeQueryStatus();

        private static void OnBaseCommandEventHandler(object sender, EventArgs e) => (sender as BaseCommand).OnExecute();

        protected static bool IsEditing(string projectFile) => EditingFileMap.ContainsValue(projectFile);

        private static string CreateFileForEditing(string projectFile)
        {
            var path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(path);    // Ensure temp path exists.
            var file = Path.Combine(path, Path.GetFileName(projectFile));
            File.Copy(projectFile, file, true);
            return file;
        }

        protected virtual void OnBeforeQueryStatus()
        {
        }

        protected abstract void OnExecute();

        protected void OpenDocument(string projectFile)
        {
            if (EditingFileMap.Count == 0)
            {
                RegisterListeners();
            }

            var editingFile = CreateFileForEditing(projectFile);
            EditingFileMap.Add(editingFile, projectFile);
            VsShellUtilities.OpenDocument(Package, editingFile);
        }

        protected void ActiveDocument(string projectFile)
        {
            var editingFile = EditingFileMap.Single(x => x.Value == projectFile).Key;

            foreach (Document document in Package.DTE.Documents)
            {
                if (document.FullName == editingFile)
                {
                    document.Activate();
                }
            }
        }

        private void RegisterListeners()
        {
            _dteEvents = Package.DTE.Events;                // Prevent it from GC.
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
            var savedFile = document.FullName;

            if (EditingFileMap.TryGetValue(savedFile, out var projectFile))
            {
                File.Copy(savedFile, projectFile, true);
            }
        }

        private void DocumentEvents_DocumentClosing(Document document)
        {
            var closingFile = document.FullName;

            if (EditingFileMap.Remove(closingFile))
            {
                if (EditingFileMap.Count == 0)
                {
                    RemoveListeners();
                }
                Directory.Delete(Path.GetDirectoryName(closingFile), true);
            }
        }
    }
}