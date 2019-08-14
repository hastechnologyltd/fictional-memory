using System.Collections.Generic;

namespace RocketReflektor.Injected
{
    public interface IDteProjectItem
    {
        string Name { get; }
        bool HasChildren { get; }
        bool IsClassFile { get; }
        bool NameFilter(string filterName);
        IDteProjectItems ChildItems { get; }
    }
}
