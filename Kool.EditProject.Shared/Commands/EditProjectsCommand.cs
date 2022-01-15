using Kool.EditProject.Models;
using System;

namespace Kool.EditProject.Commands;

internal sealed class EditProjectsCommand : BaseCommand
{
    public static EditProjectsCommand Instance { get; } = new();

    public EditProjectsCommand() : base(Ids.EDIT_PROJECTS_MENU_COMMAND_ID)
    {
    }

    protected override void OnExecute()
    {
        try
        {
            foreach (var project in SelectedProjects)
            {
                Open(project.FullName);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Error(VSPackage.ErrorMessageTitle, ex.Message);
        }
    }
}