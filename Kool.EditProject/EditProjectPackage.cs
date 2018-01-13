using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;

namespace Kool.EditProject
{
    [Guid(Ids.PACKAGE)]
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", Vsix.VERSION, IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideAutoLoad(Microsoft.VisualStudio.VSConstants.UICONTEXT.ShellInitialized_string)]
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