using System.Collections.Generic;
using System.Linq;
using RocketReflektor.Dte;

namespace RocketReflektor.CodeFiles
{
    public class Discover : IDiscover
    {

        public IEnumerable<CodeFile> GetFiles(IDteProjectItems projectItems, string filterName = null)
        {
            var childItems = projectItems.Where(x => x.HasChildren).SelectMany(s => GetFiles(s.ChildItems, filterName));
            var items = projectItems.Where(x => !x.HasChildren && x.NameFilter(filterName) && x.IsClassFile).Select(s => new CodeFile(s.Name));
            return items.Concat(childItems);
        }
    }
}
