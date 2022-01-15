using static Kool.EditProject.EditProjectPackage;

namespace Kool.EditProject.Models;

internal static class FileEditorFactory
{
    private static IFileEditor CachedEditor;

    public static IFileEditor GetEditor() => CachedEditor ??= Options.UseCustomEditor ? new CustomEditor() : new VsEditor();

    public static void ClearCache() => CachedEditor = null;
}