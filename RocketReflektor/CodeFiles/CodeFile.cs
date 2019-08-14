namespace RocketReflektor.CodeFiles
{
    public class CodeFile : ICodeFile
    {
        public CodeFile(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
