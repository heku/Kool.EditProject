using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using static Kool.EditProject.Package;

namespace Kool.EditProject.Models;

public static class MessageBox
{
    public static void Info(string title, string message)
        => Show(title, message, OLEMSGICON.OLEMSGICON_INFO, OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

    public static void Warning(string title, string message)
        => Show(title, message, OLEMSGICON.OLEMSGICON_WARNING, OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

    public static void Error(string title, string message)
        => Show(title, message, OLEMSGICON.OLEMSGICON_CRITICAL, OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

    public static void Show(string title, string message, OLEMSGICON icon, OLEMSGBUTTON button, OLEMSGDEFBUTTON defaultButton)
        => ErrorHandler.ThrowOnFailure(VsShellUtilities.ShowMessageBox(Instance, message, title, icon, button, defaultButton));
}