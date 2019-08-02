using EnvDTE;
using System.Collections.Generic;
using System.Linq;

namespace FictionalMemory.Reflection
{
    internal class SolutionExplorer
    {
        private UIHierarchy _solutionExplorer;

        public SolutionExplorer(UIHierarchy solutionExplorer)
        {
            _solutionExplorer = solutionExplorer;
        } 

        private UIHierarchyItem[] HierarchyItems => (UIHierarchyItem[])_solutionExplorer.SelectedItems;
        public IEnumerable<SelectedItem> CurrentSelectedItems => HierarchyItems.Select(x => new SelectedItem(x.Name));
        public SelectedItem CurrentSelectedOpenApiDocItem => CurrentSelectedItems.FirstOrDefault(x => x.IsSelectedAnOpenApiDocItem);
        public bool IsAnOpenApiDocItemSelected => CurrentSelectedOpenApiDocItem != null;
    }
}
