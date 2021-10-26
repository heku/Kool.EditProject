﻿using Kool.EditProject.Models;
using System.Linq;

namespace Kool.EditProject.Commands
{
    internal sealed class EditProjectCommand : BaseCommand
    {
        public static EditProjectCommand Instance { get; private set; }

        public static void Initialize(EditProjectPackage package)
        {
            Instance = new EditProjectCommand(package);
            package.CommandService.AddCommand(Instance);
        }

        private string _projectFile;

        private EditProjectCommand(EditProjectPackage package)
            : base(package, Ids.CMD_SET, Ids.EDIT_PROJECT_MENU_COMMAND_ID)
        {
        }

        protected override void OnBeforeQueryStatus()
        {
            var projects = SelectedProjects.ToArray();
            if (projects.Length == 1)
            {
                var project = projects[0];
                if (ProjectHelper.IsDotNetCoreProject(project))
                {
                    Visible = false;
                }
                else
                {
                    _projectFile = project.FullName;
                    Visible = true;
                }
            }
            else
            {
                Visible = false;
            }
        }

        protected override void OnExecute()
        {
            if (IsEditing(_projectFile))
            {
                ActiveDocument(_projectFile);
            }
            else
            {
                OpenDocument(_projectFile);
            }
        }
    }
}