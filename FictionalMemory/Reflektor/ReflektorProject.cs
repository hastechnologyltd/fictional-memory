using System.Collections.Generic;
using EnvDTE;

namespace FictionalMemory.Reflektor
{
    internal class ReflektorProject : IReflektorProject
    {
        private readonly IReflektorSolution _parentSolution;
        private readonly IReflektorProjectCodeFiles _projectCodeFiles;

        public ReflektorProject(Project dteProject, IReflektorSolution parentSolution)
        {
            DteProject = dteProject;
            _parentSolution = parentSolution;
            Name = dteProject.Name;
            LocalPath = dteProject.FullName.Replace($"{Name}.csproj", "");
            _projectCodeFiles = new ReflektorProjectCodeFiles(this);
        }

        public Project DteProject { get; }
        public string LocalPath { get; }
        public string Name { get; }
        public IEnumerable<IReflektorCodeFile> AllCodeFiles => _projectCodeFiles.GetAll();
        public IEnumerable<IReflektorCodeFile> FilteredCodeFiles(string filter) => _projectCodeFiles.GetAll(filter);
        public IReflektorCodeFile CreateCodeFile(string fullFileName)
        {
            throw new System.NotImplementedException();
        }

        //public IReflektorClass AddClass(string className, string nameSpace)
        //{
        //    var location = $"{LocalPath}{nameSpace.Substring(Name.Length + (Name == nameSpace ? 0 : 1)).Replace(".", "\\")}\\{className}.cs";
        //    var codeNameSpace = DteProject.CodeModel.AddNamespace(nameSpace, location);
        //    var codeClass = codeNameSpace.AddClass(className, 0, null, null, vsCMAccess.vsCMAccessPublic);
        //    return new ReflektorClass(className, nameSpace, location, this, codeClass.ProjectItem);
        //}

        //public IReflektorClass AddComponent(string componentName, string nameSpace, IDictionary<string, string> properties)
        //{
        //    var componentClass = AddClass(componentName, nameSpace);
        //    foreach(var property in properties)
        //    {
        //        componentClass.DefinePublicProperty(property.Key, property.Value);
        //    }
        //    return componentClass;
        //}
    }
}
