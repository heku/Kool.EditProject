using static Kool.EditProject.EditProjectPackage;

namespace Kool.EditProject.Commands
{
    internal sealed class EditSolutionCommand : BaseCommand
    {
        public static EditSolutionCommand Instance { get; } = new();

        private string _solutionFile;

        private EditSolutionCommand() : base(Ids.EDIT_SOLUTION_MENU_COMMAND_ID)
        {
        }

        protected override void OnBeforeQueryStatus() => _solutionFile = VS.Solution.FullName;

        protected override void OnExecute() => Open(_solutionFile);
    }
}