using System.Collections.Generic;
using System.Linq;
using EnvDTE;

namespace FictionalMemory.Reflektor
{
    internal class ReflektorSolution : IReflektorSolution
    {
        private IEnumerable<IReflektorProject> _projects;
        private IEnumerable<Project> _dteProjects => (IEnumerable<Project>)DteSolution.Projects.GetEnumerator();

        public ReflektorSolution(Solution dteSolution)
        {
            DteSolution = dteSolution;
        }

        public Solution DteSolution { get; }
        public IEnumerable<IReflektorProject> Projects => _projects ?? (_projects = _dteProjects.Select(x => new ReflektorProject(x, this)));
    }
}
