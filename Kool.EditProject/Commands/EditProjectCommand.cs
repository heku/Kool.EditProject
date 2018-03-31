using System.IO;
using System.Linq;
using static Kool.EditProject.Models.ProjectKinds;

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
                switch (project.Kind)
                {
                    case CS_CORE_PROJECT_KIND:
                    case FS_CORE_PROJECT_KIND:
                    case VB_CORE_PROJECT_KIND:
                        Visible = false;
                        break;

                    default:
                        _projectFile = project.FullName;
                        Text = string.Format(VSPackage.EditMenuPattern, Path.GetFileName(_projectFile));
                        Visible = true;
                        break;
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