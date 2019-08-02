using System;
using System.IO;
using MicrosoftOpenApiDoc = Microsoft.OpenApi.Models.OpenApiDocument;
using Microsoft.OpenApi.Readers;
using FictionalMemory.Driver.Files;

namespace FictionalMemory.Driver.Microsoft.OpenApi
{
    internal class Specification
    {
        private static MicrosoftOpenApiDoc _openApiDoc;
        public static MicrosoftOpenApiDoc Document => _openApiDoc ?? (_openApiDoc = new SpecFile().Read(""));
    }
}
