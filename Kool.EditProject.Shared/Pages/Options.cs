using Kool.EditProject.Models;
using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;
using UIElement = System.Windows.UIElement;

namespace Kool.EditProject.Pages
{
    [Guid(Ids.OPTIONS)]
    internal sealed class Options : UIElementDialogPage
    {
        private OptionsPage _page;

        public Options() => SetDefaults();

        public bool UseCustomEditor { get; set; }
        public string EditorExe { get; set; }
        public string EditorArg { get; set; }
        public bool OpenProjitems { get; set; }

        protected override UIElement Child => _page ??= new OptionsPage(this);

        protected override void OnApply(PageApplyEventArgs e)
        {
            FileEditorFactory.ClearCache();
            _page?.UpdateDefaultStyle(); // Ensure VS Environment Font Settings are applied.
            base.OnApply(e);
        }

        public override void ResetSettings()
        {
            SetDefaults();
            base.ResetSettings();
        }

        private void SetDefaults()
        {
            UseCustomEditor = false;
            EditorExe = "notepad.exe";
            EditorArg = "$FILE";
            OpenProjitems = false;
        }
    }
}