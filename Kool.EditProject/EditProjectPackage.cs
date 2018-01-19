using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using UIContextGuids = Microsoft.VisualStudio.Shell.Interop.UIContextGuids;

namespace Kool.EditProject
{
    [Guid(Ids.PACKAGE)]
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", Vsix.VERSION, IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideAutoLoad(Ids.AUTO_LOAD_CONTEXT)]
    [ProvideUIContextRule(Ids.AUTO_LOAD_CONTEXT,
        "HasProject",
        "SingleProject | MultipleProjects",
        new[] { "SingleProject", "MultipleProjects" },
        new[] { UIContextGuids.SolutionHasSingleProject, UIContextGuids.SolutionHasMultipleProjects })]
    public sealed class EditProjectPackage : Package
    {
        internal DTE2 DTE { get; private set; }

        internal OleMenuCommandService CommandService { get; private set; }

        protected override void Initialize()
        {
            base.Initialize();

            DTE = GetService(typeof(EnvDTE.DTE)) as DTE2;
            CommandService = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;

            EditProjectCommand.Initialize(this);
        }
    }
}