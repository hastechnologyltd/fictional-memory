using System;
using System.Collections.Generic;
using EnvDTE;

namespace FictionalMemory.Reflektor
{
    internal class ReflektorProjectCodeFiles : IReflektorProjectCodeFiles
    {
        private readonly IReflektorProject _reflektorProject;

        public ReflektorProjectCodeFiles(IReflektorProject reflektorProject)
        {
            _reflektorProject = reflektorProject;
        }

        public void CompileChildren(ProjectItems elementProjectItems, List<IReflektorCodeFile> document, string filterName = null)
        {
            for (int k = 1; k <= elementProjectItems.Count; ++k)
            {
                var elementProjectItem = elementProjectItems.Item(k);
                var childItems = elementProjectItem.ProjectItems;
                if (childItems.Count > 0)
                {
                    CompileChildren(childItems, document, filterName);
                }
                else if (elementProjectItem.Name == $"{filterName}.cs" || (String.IsNullOrEmpty(filterName) && elementProjectItem.Name.EndsWith(".cs")))
                {
                    var className = elementProjectItem.Name.Replace(".cs", "");
                    var fullPath = elementProjectItem.FileNames[0];
                    document.Add(new ReflektorCodeFile(className, fullPath, _reflektorProject, elementProjectItem));
                }
            }
        }

        public List<IReflektorCodeFile> GetAll(string filterName = null)
        {
            var document = new List<IReflektorCodeFile>();
            CompileChildren(_reflektorProject.DteProject.ProjectItems, document, filterName);
            return document;
        }

        public IReflektorCodeFile AddFromTemplateName(ProjectItem parentItem, string fileName, string templateName)
        {
            throw new NotImplementedException();
        }
    }
}
