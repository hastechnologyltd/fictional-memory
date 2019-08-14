using System;
using EnvDTE;
using RocketReflektor.Injected;

namespace FictionalMemory.RocketReflektor
{
    internal class DteProjectItem : IDteProjectItem
    {
        private readonly ProjectItem _projectItem;

        public DteProjectItem(ProjectItem projectItem)
        {
            _projectItem = projectItem;
        }

        public string Name => _projectItem.Name;
        public bool HasChildren => _projectItem.ProjectItems.Count > 0;

        public bool IsClassFile => _projectItem.Name.EndsWith(".cs");

        public IDteProjectItems ChildItems => new DteProjectItems(_projectItem.ProjectItems);

        public bool NameFilter(string filterName) => Name == $"{filterName}.cs" || (String.IsNullOrEmpty(filterName) && IsClassFile);
    }
}
