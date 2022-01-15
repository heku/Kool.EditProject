using EnvDTE;
using EnvDTE80;
using Kool.EditProject.Commands;
using Kool.EditProject.Pages;
using Microsoft;
using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Threading;
using Tasks = System.Threading.Tasks;
using UIContextGuids = Microsoft.VisualStudio.Shell.Interop.UIContextGuids;

namespace Kool.EditProject;

[Guid(Ids.PACKAGE)]
[PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
[InstalledProductRegistration("#110", "#112", Vsix.VERSION, IconResourceID = 400)]
[ProvideMenuResource("Menus.ctmenu", 1)]
[ProvideOptionPage(typeof(Options), Vsix.PRODUCT, Vsix.PACKAGE, 0, 0, true, Sort = 200)]
[ProvideAutoLoad(Ids.AUTO_LOAD_CONTEXT, PackageAutoLoadFlags.BackgroundLoad)]
[ProvideUIContextRule(Ids.AUTO_LOAD_CONTEXT,
    "HasProject",
    "SingleProject | MultipleProjects",
    new[] { "SingleProject", "MultipleProjects" },
    new[] { UIContextGuids.SolutionHasSingleProject, UIContextGuids.SolutionHasMultipleProjects },
    delay: 1000)]
public sealed class EditProjectPackage : AsyncPackage
{
    internal static DTE2 IDE { get; private set; }
    internal static Options Options { get; private set; }
    internal static EditProjectPackage Instance { get; private set; }

    protected override async Tasks.Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> _)
    {
        Instance = this;

        await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

        IDE = await GetServiceAsync(typeof(DTE)) as DTE2;
        Options = (Options)GetDialogPage(typeof(Options));

        var cmds = await GetServiceAsync(typeof(IMenuCommandService)) as IMenuCommandService;
        Assumes.Present(cmds);
        cmds.AddCommand(EditProjectCommand.Instance);
        cmds.AddCommand(EditProjectsCommand.Instance);
        cmds.AddCommand(EditSolutionCommand.Instance);
    }
}