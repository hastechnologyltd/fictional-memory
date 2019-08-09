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

        public IReflektorClass AddClass(string className, string nameSpace)
        {
            var location = $"{LocalPath}{nameSpace.Substring(Name.Length + (Name == nameSpace ? 0 : 1)).Replace(".", "\\")}\\{className}.cs";
            var codeNameSpace = DteProject.CodeModel.AddNamespace(nameSpace, location);
            var codeClass = codeNameSpace.AddClass(className, 0, null, null, vsCMAccess.vsCMAccessPublic);
            return new ReflektorClass(className, nameSpace, location, this, codeClass.ProjectItem);
        }

        public IReflektorClass AddComponent(string componentName, string nameSpace, IDictionary<string, string> properties)
        {
            var componentClass = AddClass(componentName, nameSpace);
            foreach(var property in properties)
            {
                componentClass.DefinePublicProperty(property.Key, property.Value);
            }
            return componentClass;
        }
    }
}
