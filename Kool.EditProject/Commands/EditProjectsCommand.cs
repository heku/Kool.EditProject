namespace Kool.EditProject.Commands
{
    internal sealed class EditProjectsCommand : BaseCommand
    {
        public static EditProjectsCommand Instance { get; private set; }

        public static void Initialize(EditProjectPackage package)
        {
            Instance = new EditProjectsCommand(package);
            package.CommandService.AddCommand(Instance);
        }

        public EditProjectsCommand(EditProjectPackage package)
            : base(package, Ids.CMD_SET, Ids.EDIT_PROJECTS_MENU_COMMAND_ID)
        {
        }

        protected override void OnExecute()
        {
            bool opened = false;
            string projectFile = null;

            foreach (var project in SelectedProjects)
            {
                projectFile = project.FullName;

                if (!IsEditing(projectFile))
                {
                    opened = true;
                    OpenDocument(projectFile);
                }
            }

            if (!opened && projectFile != null)
            {
                ActiveDocument(projectFile);
            }
        }
    }
}