using System.Collections.Generic;
using EnvDTE;

namespace FictionalMemory.Reflektor
{
    internal interface IReflektorProject
    {
        Project DteProject { get; }
        string LocalPath { get; }
        string Name { get; }

        IEnumerable<IReflektorClass> AllClasses { get; }
        IEnumerable<IReflektorClass> FilteredClasses(string filter);
        IReflektorClass AddClass(string className, string nameSpace);
        IReflektorClass AddComponent(string componentName, string nameSpace, IDictionary<string, string> properties);
    }
}
