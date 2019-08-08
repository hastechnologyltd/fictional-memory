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
        void AddClass(string className, string nameSpace);
    }
}
