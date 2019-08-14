using System.Collections.Generic;
using RocketReflektor.Injected;

namespace RocketReflektor.CodeFiles
{
    public interface IDiscover
    {
        IEnumerable<CodeFile> GetFiles(IDteProjectItems projectItems, string filterName = null);
    }
}
