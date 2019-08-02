using FictionalMemory.Driver.Types;
using System;
using System.IO;

namespace FictionalMemory.Driver.Files.StreamReaders
{
    internal class ComponentFileStreamReader : IFileStreamReader<ComponentType>
    {
        public ComponentType Read(StreamReader input)
        {
            var componentType = new ComponentType();

            string line;
            while ((line = input.ReadLine()) != null)
            {
                if (line.Contains("namespace"))
                {
                    componentType.Namespace = line.Substring(10, line.Length - 10);
                }

                if (line.Contains("public class"))
                {
                    var commandIndex = line.IndexOf("public class");
                    componentType.Name = line.Substring(commandIndex + 13, line.Length - (commandIndex + 13));
                }

                if (line.Contains("{ get; set; }"))
                {
                }
            }
            return componentType;
        }
    }
}
