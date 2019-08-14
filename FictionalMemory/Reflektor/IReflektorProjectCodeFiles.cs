using System.Collections.Generic;
using EnvDTE;

namespace FictionalMemory.Reflektor
{
    internal interface IReflektorProjectCodeFiles
    {
        List<IReflektorCodeFile> GetAll(string filterName = null);
        void CompileChildren(ProjectItems rootProjectItems, List<IReflektorCodeFile> document, string filterName = null);
        IReflektorCodeFile AddFromTemplateName(ProjectItem parentItem, string fileName, string templateName);
    }
}
