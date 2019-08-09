namespace FictionalMemory.Reflektor
{
    internal interface IReflektorClass
    {
        IReflektorProject ParentProject { get; }
        string LocalPath { get; }
        string Name { get; }
        string NameSpace { get; }

        void DefinePublicProperty(string propertyName, string typeName);
    }
}
