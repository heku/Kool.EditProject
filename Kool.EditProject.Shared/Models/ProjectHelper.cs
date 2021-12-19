using EnvDTE;
using Microsoft;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using static Kool.EditProject.Models.ProjectKinds;

namespace Kool.EditProject.Models
{
    internal static class ProjectHelper
    {
        public static bool IsDotNetCoreProject(Project project)
        {
            switch (project.Kind)
            {
                case CS_CORE_PROJECT_KIND:
                case FS_CORE_PROJECT_KIND:
                case VB_CORE_PROJECT_KIND:
                    return true;

                default:
                    // Shared project is CPS capable but has no built-in Edit Project menu
                    if (project.FullName.EndsWith(".shproj", StringComparison.InvariantCultureIgnoreCase))
                    {
                        return false;
                    }
                    var solution = Package.GetGlobalService(typeof(SVsSolution)) as IVsSolution;
                    if (solution.GetProjectOfUniqueName(project.UniqueName, out var hierarchy) == 0)
                    {
                        return IsCpsProject(hierarchy);
                    }

                    return false;
            }
        }

        // https://developercommunity.visualstudio.com/content/problem/312523/envdteprojectkind-no-longer-differentiates-between.html
        // https://github.com/Microsoft/VSProjectSystem/blob/master/doc/automation/detect_whether_a_project_is_a_CPS_project.md
        // https://www.mztools.com/articles/2014/MZ2014006.aspx
        private static bool IsCpsProject(IVsHierarchy hierarchy)
        {
            Requires.NotNull(hierarchy, nameof(hierarchy));
            return hierarchy.IsCapabilityMatch("CPS");
        }
    }
}