using Kool.EditProject.Models;
using System;
using static Kool.EditProject.Package;

namespace Kool.EditProject.Commands;

internal sealed class EditSolutionCommand : BaseCommand
{
    public EditSolutionCommand() : base(Ids.EDIT_SOLUTION_MENU_COMMAND_ID)
    {
    }

    protected override void OnExecute()
    {
        try
        {
            Open(IDE.Solution.FullName);
        }
        catch (Exception ex)
        {
            MessageBox.Error(I18n.ErrorMessageTitle, ex.Message);
        }
    }
}