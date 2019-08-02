using System.IO;

namespace FictionalMemory.Driver.Files.StreamReaders
{
    internal interface IFileStream
    {
        StreamReader Reader(string fileName);
    }
}
