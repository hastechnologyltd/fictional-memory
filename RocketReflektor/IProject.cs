using System.Collections.Generic;
using RocketReflektor.CodeFiles;

namespace RocketReflektor
{
    public interface IProject
    {
        string Namespace { get; }
        string Name { get; }
        ILocation Location { get; }
        IEnumerable<CodeFile> GetFiles(string filter = null);
        CodeFile CreateFileFromTemplate(string name, string templateName, ILocation location = null);
    }
}
