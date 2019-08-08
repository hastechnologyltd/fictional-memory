using System;
using System.Collections.Generic;
using EnvDTE;

namespace FictionalMemory.Reflektor.Helpers
{
    internal static class ProjectExtensions
    {
        public static List<IReflektorClass> GetClasses(this IReflektorProject rootProject, string filterName = null)
        {
            var document = new List<IReflektorClass>();
            rootProject.DteProject.ProjectItems.GetClasses(rootProject, document, filterName);
            return document;
        }

        private static void GetClasses(this ProjectItems rootProjectItems, IReflektorProject reflektorProject, List<IReflektorClass> document, string filterName = null)
        {
            for (int k = 1; k <= rootProjectItems.Count; ++k)
            {
                var item = rootProjectItems.Item(k);
                var items = item.ProjectItems;
                if (items.Count > 0)
                {
                    items.GetClasses(reflektorProject, document, filterName);
                }
                else if (item.Name == $"{filterName}.cs" || (String.IsNullOrEmpty(filterName) && item.Name.EndsWith(".cs")))
                {
                    var className = item.Name.Replace(".cs", "");
                    var fullPath = item.FileNames[0];
                    document.Add(new ReflektorClass(className, fullPath, reflektorProject));
                }
            }
        }
    }
}
