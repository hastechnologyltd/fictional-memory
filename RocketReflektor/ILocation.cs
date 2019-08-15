namespace RocketReflektor
{
    public interface ILocation
    {
        string AbsolutePath { get; }
        string RelativePath { get; }
        string[] Namespace { get; }
    }
}
