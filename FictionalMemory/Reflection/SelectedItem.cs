using EnvDTE;

namespace FictionalMemory.Reflection
{
    internal class SelectedItem
    {
        public SelectedItem(string selectedFileName)
        {
            Name = selectedFileName;
        }

        public string Name { get; }
        public bool IsSelectedAnOpenApiDocItem => Name.StartsWith("OpenApiDocument");
    }
}
