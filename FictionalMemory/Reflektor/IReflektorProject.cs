using System.Collections.Generic;
using EnvDTE;

namespace FictionalMemory.Reflektor
{
    internal interface IReflektorProject
    {
        Project DteProject { get; }
        string LocalPath { get; }
        string Name { get; }

        IEnumerable<IReflektorCodeFile> AllCodeFiles { get; }
        IEnumerable<IReflektorCodeFile> FilteredCodeFiles(string filter);
        IReflektorCodeFile CreateCodeFile(string fullFileName);


        //IReflektorClass AddClass(string className, string nameSpace);
        //IReflektorClass AddComponent(string componentName, string nameSpace, IDictionary<string, string> properties);
    }
}
