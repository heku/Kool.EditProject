using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;

namespace Kool.EditProject
{
    internal sealed class EditProjectCommand
    {
        // Editing File - Source File
        private static readonly Dictionary<string, string> EditingFileMap = new Dictionary<string, string>();

        public static EditProjectCommand Instance { get; private set; }

        public static void Initialize(EditProjectPackage package) => Instance = new EditProjectCommand(package);

        // https://stackoverflow.com/questions/45795759/detect-a-dotnet-core-project-from-envdte-project-api
        // https://www.codeproject.com/reference/720512/list-of-visual-studio-project-type-guids
        private const string CS_CORE_PROJECT_KIND = "{9A19103F-16F7-4668-BE54-9A1E7A4F7556}";
        private const string FS_CORE_PROJECT_KIND = "{6EC3EE1D-3C4E-46DD-8F32-0CC8E7565705}";
        private const string VB_CORE_PROJECT_KIND = "{778DAE3C-4631-46EA-AA77-85C1314464D9}";

        private readonly EditProjectPackage _package;
        private Events _dteEvents;
        private DocumentEvents _documentEvents;

        private EditProjectCommand(EditProjectPackage package)
        {
            _package = package;
            var menuCommandID = new CommandID(Guid.Parse(Ids.CMD_SET), Ids.EDIT_PROJECT_MENU_COMMAND_ID);
            var menuCommand = new OleMenuCommand(ExecuteCommand, null, BeforeQueryStatus, menuCommandID);
            _package.CommandService.AddCommand(menuCommand);
        }

        private void BeforeQueryStatus(object sender, EventArgs e)
        {
            var command = sender as OleMenuCommand;
            var project = _package.DTE.SelectedItems.Item(1).Project;

            switch (project.Kind)
            {
                case CS_CORE_PROJECT_KIND:
                case FS_CORE_PROJECT_KIND:
                case VB_CORE_PROJECT_KIND:
                    command.Visible = false;
                    break;

                default:
                    var projectName = Path.GetFileName(project.FullName);
                    command.Visible = true;
                    command.Text = string.Format(Resources.EditMenuPattern, projectName);
                    break;
            }
        }

        private void ExecuteCommand(object sender, EventArgs e)
        {
            var selectedFile = _package.DTE.SelectedItems.Item(1).Project.FullName;

            if (EditingFileMap.ContainsValue(selectedFile))
            {
                ActiveDocument(selectedFile);
                return;
            }

            if (EditingFileMap.Count == 0)
            {
                RegisterListeners();
            }

            var editingFile = CreateFileForEditing(selectedFile);
            EditingFileMap.Add(editingFile, selectedFile);
            OpenDocument(editingFile);
        }

        private string CreateFileForEditing(string selectedFile)
        {
            var path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(path);    // Ensure temp path exists.
            var file = Path.Combine(path, Path.GetFileName(selectedFile));
            File.Copy(selectedFile, file, true);
            return file;
        }

        private void OpenDocument(string file) => VsShellUtilities.OpenDocument(_package, file);

        private void ActiveDocument(string file)
        {
            var editingFile = EditingFileMap.Single(x => x.Value == file).Key;

            foreach (Document document in _package.DTE.Documents)
            {
                if (document.FullName == editingFile)
                {
                    document.Activate();
                }
            }
        }

        private void RegisterListeners()
        {
            _dteEvents = _package.DTE.Events;               // Prevent it from GC.
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

        private void DocumentEvents_DocumentSaved(Document document)
        {
            var savedFile = document.FullName;

            if (EditingFileMap.TryGetValue(savedFile, out var sourceFile))
            {
                File.Copy(savedFile, sourceFile, true);
            }
        }
    }
}