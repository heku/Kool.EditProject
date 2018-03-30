using System.IO;
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

        // https://stackoverflow.com/questions/45795759/detect-a-dotnet-core-project-from-envdte-project-api
        // https://www.codeproject.com/reference/720512/list-of-visual-studio-project-type-guids
        private const string CS_CORE_PROJECT_KIND = "{9A19103F-16F7-4668-BE54-9A1E7A4F7556}";
        private const string FS_CORE_PROJECT_KIND = "{6EC3EE1D-3C4E-46DD-8F32-0CC8E7565705}";
        private const string VB_CORE_PROJECT_KIND = "{778DAE3C-4631-46EA-AA77-85C1314464D9}";

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
                        var fileName = Path.GetFileName(_projectFile);
                        Visible = true;
                        Text = string.Format(VSPackage.EditMenuPattern, fileName);
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