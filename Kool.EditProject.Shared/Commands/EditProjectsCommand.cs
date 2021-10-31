namespace Kool.EditProject.Commands
{
    internal sealed class EditProjectsCommand : BaseCommand
    {
        public static EditProjectsCommand Instance { get; } = new();

        public EditProjectsCommand() : base(Ids.EDIT_PROJECTS_MENU_COMMAND_ID)
        {
        }

        protected override void OnExecute()
        {
            foreach (var project in SelectedProjects)
            {
                Open(project.FullName);
            }
        }
    }
}