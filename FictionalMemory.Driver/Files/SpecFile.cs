using Microsoft.OpenApi.Readers;
using System.IO;
using MicrosoftOpenApiDoc = Microsoft.OpenApi.Models.OpenApiDocument;

namespace FictionalMemory.Driver.Files
{
    internal class SpecFile : IReadFile<MicrosoftOpenApiDoc>
    {
        public MicrosoftOpenApiDoc Read(string fileName)
        {
            MicrosoftOpenApiDoc openApiDocument;
            using (var stream = new StreamReader(fileName))
            {
                openApiDocument = new OpenApiStreamReader().Read(stream.BaseStream, out var diagnostic);
            }
            return openApiDocument;
        }
    }
}
