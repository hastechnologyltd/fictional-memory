namespace FictionalMemory.Driver.Files
{
    internal interface IReadFile<TFileData>
    {
        TFileData Read(string fileName);
    }
}
