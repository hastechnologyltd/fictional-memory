namespace FictionalMemory.Reflektor
{
    internal class ReflektorClass : IReflektorClass
    {
        private readonly IReflektorProject _parentProject;
        private string _nameSpace;

        public ReflektorClass(string className, string fullPath, IReflektorProject parentProject)
        {
            Name = className;
            LocalPath = fullPath;
            _parentProject = parentProject;
        }

        public string LocalPath { get; }

        public string Name { get; }

        public string NameSpace => _nameSpace ?? (_nameSpace = GetNameSpace());

        private string GetNameSpace()
        {
            var relativeNameSpace = LocalPath.Replace(_parentProject.LocalPath, ".").Replace($"{Name}.cs", "").Replace("\\", ".");
            if (relativeNameSpace.EndsWith("."))
            {
                relativeNameSpace = relativeNameSpace.Substring(0, relativeNameSpace.Length - 1);
            }
            return _parentProject.Name + relativeNameSpace;
        }

        public void DefinePublicProperty(string propertyName, string typeName)
        {
            throw new System.NotImplementedException();
        }
    }
}
