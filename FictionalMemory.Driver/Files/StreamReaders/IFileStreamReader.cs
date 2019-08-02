using System.IO;

namespace FictionalMemory.Driver.Files.StreamReaders
{
    internal interface IFileStreamReader<TOutput>
    {
        TOutput Read(StreamReader input);
    }
}
