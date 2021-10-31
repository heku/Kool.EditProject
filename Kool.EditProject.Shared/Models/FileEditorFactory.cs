using static Kool.EditProject.EditProjectPackage;

namespace Kool.EditProject.Models
{
    internal static class FileEditorFactory
    {
        private static IFileEditor CachedEditor;

        public static IFileEditor GetEditor()
        {
            return CachedEditor ??= CreateEditor();

            static IFileEditor CreateEditor()
            {
                return Options.UseCustomEditor
                    ? new CustomEditor(Options.EditorExe, Options.EditorArg)
                    : new VsEditor();
            }
        }

        public static void ClearCache() => CachedEditor = null;
    }
}