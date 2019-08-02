using FictionalMemory.Driver.Types;
using System;
using System.IO;

namespace FictionalMemory.Driver.Files.StreamReaders
{
    internal class OperationFileStreamReader : IFileStreamReader<OperationType>
    {
        public OperationType Read(StreamReader input)
        {
            throw new NotImplementedException();
        }
    }
}