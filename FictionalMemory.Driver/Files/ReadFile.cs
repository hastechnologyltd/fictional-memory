using FictionalMemory.Driver.Files.StreamReaders;
using System;

namespace FictionalMemory.Driver.Files
{
    internal class ReadFile<TFileType> : IReadFile<TFileType>
    {
        private readonly IFileStream _fileStream;
        private readonly IFileStreamReader<TFileType> _fileStreamReader;

        public ReadFile(IFileStream fileStream, IFileStreamReader<TFileType> fileStreamReader = null)
        {
            _fileStream = fileStream;
            _fileStreamReader = fileStreamReader ?? (IFileStreamReader<TFileType>)Activator.CreateInstance(typeof(IFileStreamReader<TFileType>));
        }

        public TFileType Read(string fileName)
        {
            TFileType fileType;
            using (var stream = _fileStream.Reader(fileName))
            {
                fileType = _fileStreamReader.Read(stream);
            }
            return fileType;
        }
    }
}
