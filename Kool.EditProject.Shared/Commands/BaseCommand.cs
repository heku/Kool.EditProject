using EnvDTE;
using Kool.EditProject.Models;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using static Kool.EditProject.EditProjectPackage;

namespace Kool.EditProject.Commands
{
    internal abstract class BaseCommand : OleMenuCommand
    {
        private static void OnBaseBeforeQueryStatus(object sender, EventArgs e) => (sender as BaseCommand).OnBeforeQueryStatus();

        private static void OnBaseCommandEventHandler(object sender, EventArgs e) => (sender as BaseCommand).OnExecute();

        protected static void Open(string file)
        {
            if (file is not null)
            {
                var editor = FileEditorFactory.GetEditor();
                if (Options.OpenProjitems && file.EndsWith(".shproj", StringComparison.InvariantCultureIgnoreCase))
                {
                    editor.OpenFile(Path.ChangeExtension(file, ".projitems"));
                }
                editor.OpenFile(file);
            }
        }

        protected BaseCommand(int cmdId) : base(OnBaseCommandEventHandler, null, OnBaseBeforeQueryStatus, new CommandID(Guid.Parse(Ids.CMD_SET), cmdId))
        {
        }

        protected IEnumerable<Project> SelectedProjects => VS.SelectedItems.OfType<SelectedItem>().Where(x => x.Project != null).Select(x => x.Project);

        protected virtual void OnBeforeQueryStatus()
        {
        }

        protected abstract void OnExecute();
    }
}