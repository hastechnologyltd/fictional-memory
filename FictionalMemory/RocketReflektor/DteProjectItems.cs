using System.Collections;
using System.Collections.Generic;
using EnvDTE;
using RocketReflektor.Injected;

namespace FictionalMemory.RocketReflektor
{
    internal class DteProjectItems : IDteProjectItems
    {
        private readonly ProjectItems _projectItems;

        public DteProjectItems(ProjectItems projectItems)
        {
            _projectItems = projectItems;
        }
        public IEnumerator<IDteProjectItem> GetEnumerator()
        {
            var items = new List<IDteProjectItem>();
            for (var i = 1; i < _projectItems.Count + 1; i++)
            {
                var item = _projectItems.Item(i);
                items.Add(new DteProjectItem(item));
            }
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }
    }
}
