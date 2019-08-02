using System.IO;

namespace FictionalMemory.Driver.Files.StreamReaders
{
    internal class FileStream : IFileStream
    {
        public StreamReader Reader(string fileName)
        {
            return new StreamReader(fileName);
        }
    }
}
