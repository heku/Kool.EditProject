using EnvDTE80;
using Kool.EditProject.Commands;
using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Threading;
using Tasks = System.Threading.Tasks;
using UIContextGuids = Microsoft.VisualStudio.Shell.Interop.UIContextGuids;

namespace Kool.EditProject
{
    [Guid(Ids.PACKAGE)]
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("#110", "#112", Vsix.VERSION, IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideAutoLoad(Ids.AUTO_LOAD_CONTEXT, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideUIContextRule(Ids.AUTO_LOAD_CONTEXT,
        "HasProject",
        "SingleProject | MultipleProjects",
        new[] { "SingleProject", "MultipleProjects" },
        new[] { UIContextGuids.SolutionHasSingleProject, UIContextGuids.SolutionHasMultipleProjects },
        delay: 1000)]
    public sealed class EditProjectPackage : AsyncPackage
    {
        internal DTE2 DTE { get; private set; }

        internal OleMenuCommandService CommandService { get; private set; }

        protected override async Tasks.Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            DTE = await GetServiceAsync(typeof(EnvDTE.DTE)) as DTE2;
            CommandService = await GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;

            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

            EditProjectCommand.Initialize(this);
            EditProjectsCommand.Initialize(this);
        }
    }

    // References
    // https://github.com/Microsoft/VSSDK-Extensibility-Samples/tree/master/VisibilityConstraints
    // https://github.com/madskristensen/ImageOptimizer/blob/master/src/Helpers/ProjectHelpers.cs
}