namespace Kool.EditProject.Commands
{
    internal sealed class EditSolutionCommand : BaseCommand
    {
        public static EditSolutionCommand Instance { get; private set; }

        public static void Initialize(EditProjectPackage package)
        {
            Instance = new EditSolutionCommand(package);
            package.CommandService.AddCommand(Instance);
        }

        private string _solutionFile;

        private EditSolutionCommand(EditProjectPackage package)
            : base(package, Ids.CMD_SET, Ids.EDIT_SOLUTION_MENU_COMMAND_ID)
        {
        }

        protected override void OnBeforeQueryStatus()
        {
            _solutionFile = Package.DTE.Solution.FullName;
        }

        protected override void OnExecute()
        {
            if (IsEditing(_solutionFile))
            {
                ActiveDocument(_solutionFile);
            }
            else
            {
                OpenDocument(_solutionFile);
            }
        }
    }
}