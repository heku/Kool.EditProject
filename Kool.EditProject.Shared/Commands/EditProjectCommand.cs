using Kool.EditProject.Models;
using Microsoft.VisualStudio;
using System;
using System.Linq;

namespace Kool.EditProject.Commands;

internal sealed class EditProjectCommand : BaseCommand
{
    public static EditProjectCommand Instance { get; } = new();

    private readonly uint _uiContextCookie;

    private EditProjectCommand() : base(Ids.EDIT_PROJECT_MENU_COMMAND_ID)
    {
        var result = Package.Selection.GetCmdUIContextCookie(Guid.Parse(Ids.EDIT_PROJECT_UI_CONTEXT), out _uiContextCookie);
        ErrorHandler.ThrowOnFailure(result);
    }

    // After package loaded, the VisibilityConstraints way doesn't work anymore, we need to check it ourselves.
    private bool IsUIContextActive()
    {
        var result = Package.Selection.IsCmdUIContextActive(_uiContextCookie, out var isActive);
        ErrorHandler.ThrowOnFailure(result);
        return isActive == 1;
    }

    protected override void OnBeforeQueryStatus()
    {
        Visible = IsUIContextActive();
    }

    protected override void OnExecute()
    {
        try
        {
            Open(SelectedProjects.Single().FullName); // must be single, right?
        }
        catch (Exception ex)
        {
            MessageBox.Error(VSPackage.ErrorMessageTitle, ex.Message);
        }
    }
}