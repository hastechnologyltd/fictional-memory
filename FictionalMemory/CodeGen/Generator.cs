using FictionalMemory.Reflection;
using System.Threading.Tasks;

namespace FictionalMemory.CodeGen
{
    internal class Generator
    {
        public async Task<bool> ExecuteAsync(SelectedItem selectedItem)
        {
            IsGenerating = true;

            //var rootPath = System.IO.Path.GetDirectoryName(filePath);
            //var fileName = System.IO.Path.GetFileName(filePath);




            IsGenerating = false;
            return true;
        }

        public bool IsGenerating { get; private set; }
    }
}
