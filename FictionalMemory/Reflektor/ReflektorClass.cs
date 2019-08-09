using EnvDTE;

namespace FictionalMemory.Reflektor
{
    internal class ReflektorClass : IReflektorClass
    {
        private readonly ProjectItem _projectItem;

        public ReflektorClass(string className, string fullPath, IReflektorProject parentProject, ProjectItem projectItem) : this(className, null, fullPath, parentProject, projectItem)
        {
        }

        public ReflektorClass(string className, string nameSpace, string fullPath, IReflektorProject parentProject, ProjectItem projectItem)
        {
            Name = className;
            LocalPath = fullPath;
            ParentProject = parentProject;
            _projectItem = projectItem;
            NameSpace = nameSpace ?? GetNameSpace();
        }

        public IReflektorProject ParentProject { get; }
        public string LocalPath { get; }

        public string Name { get; }

        public string NameSpace { get; }

        private string GetNameSpace()
        {
            var relativeNameSpace = LocalPath.Replace(ParentProject.LocalPath, ".").Replace($"{Name}.cs", "").Replace("\\", ".");
            if (relativeNameSpace.EndsWith("."))
            {
                relativeNameSpace = relativeNameSpace.Substring(0, relativeNameSpace.Length - 1);
            }
            return ParentProject.Name + relativeNameSpace;
        }

        public void DefinePublicProperty(string propertyName, string typeName)
        {
            FileCodeModel fileCodeModel = _projectItem.FileCodeModel;

            CodeElement fileCodeElement = fileCodeModel.CodeElements.Item(1);
            CodeNamespace fileNamespace = (CodeNamespace)fileCodeElement;
            CodeElement fileNamespaceElement = fileNamespace.Members.Item(1);
            CodeClass codeClass = (CodeClass)fileNamespaceElement;

            var l = codeClass.Language;

            codeClass.AddProperty(propertyName, propertyName, typeName, 0, vsCMAccess.vsCMAccessPublic);

            codeClass.AddVariable(propertyName, typeName, -1, vsCMAccess.vsCMAccessPublic);

            

            //fileCodeModel.AddVariable(propertyName, typeName, -1, vsCMAccess.vsCMAccessPublic);





            //CodeElement fileCodeElement = fileCodeModel.CodeElements.Item(1);
            //CodeNamespace fileNamespace = (CodeNamespace)fileCodeElement;
            //CodeElement fileNamespaceElement = fileNamespace.Members.Item(1);
            //CodeClass codeClass = (CodeClass)fileNamespaceElement;


            //var prop = codeClass.AddVariable(propertyName, typeName, -1, vsCMAccess.vsCMAccessPublic);
            //var startPoint = prop.StartPoint;
            //int linelength = startPoint.LineLength;
            //var edit = startPoint.CreateEditPoint();
            //var currentLine = edit.GetLines(edit.Line, edit.Line + 1).TrimStart();
            //var newLine = currentLine.Replace(";", " { get; set; }");
            //edit.Delete(currentLine.Length);
            //edit.Insert(newLine);
        }
    }
}
