using EnvDTE;

namespace FictionalMemory.Reflektor
{
    internal class ReflektorCodeFile : IReflektorCodeFile
    {
        public ReflektorCodeFile(string className, string fullPath, IReflektorProject parentProject, ProjectItem projectItem) : this(className, null, fullPath, parentProject, projectItem)
        {
        }
        public ReflektorCodeFile(string className, string nameSpace, string fullPath, IReflektorProject parentProject, ProjectItem projectItem)
        {
            //Name = className;
            //LocalPath = fullPath;
            //ParentProject = parentProject;
            //_projectItem = projectItem;
            //NameSpace = nameSpace ?? GetNameSpace();
        }
    }
}
