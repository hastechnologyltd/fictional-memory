using System.Collections.Generic;
using EnvDTE;
using FictionalMemory.Reflektor.Helpers;

namespace FictionalMemory.Reflektor
{
    internal class ReflektorProject : IReflektorProject
    {
        private readonly IReflektorSolution _parentSolution;
        public ReflektorProject(Project dteProject, IReflektorSolution parentSolution)
        {
            DteProject = dteProject;
            _parentSolution = parentSolution;
            Name = dteProject.Name;
            LocalPath = dteProject.FullName.Replace($"{Name}.csproj", "");
        }

        public Project DteProject { get; }
        public string LocalPath { get; }
        public string Name { get; }
        public IEnumerable<IReflektorClass> AllClasses => this.GetClasses();
        public IEnumerable<IReflektorClass> FilteredClasses(string filter) => this.GetClasses(filter);

        public void AddClass(string className, string nameSpace)
        {
            DteProject.CodeModel.AddClass(className, @"C:\Users\jeff.brannon\source\repos\TestApp\Susie.cs", 0, 0, 0, vsCMAccess.vsCMAccessPublic);

        }
    }
}
