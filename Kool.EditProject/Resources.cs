using System.Reflection;
using System.Resources;

namespace Kool.EditProject
{
    internal static class Resources
    {
        private const string PACKAGE_RESX_FILE_NAME = "VSPackage";
        private static readonly ResourceManager Resx = new ResourceManager(PACKAGE_RESX_FILE_NAME, Assembly.GetExecutingAssembly());

        public static string EditMenuPattern { get; } = Resx.GetString(nameof(EditMenuPattern));
    }
}