using System.Collections.Generic;
using EnvDTE;

namespace FictionalMemory.Reflektor
{
    internal class ReflektorSolution : IReflektorSolution
    {
        private IEnumerable<IReflektorProject> _projects;

        public ReflektorSolution(Solution dteSolution)
        {
            DteSolution = dteSolution;
        }

        public Solution DteSolution { get; }
        public IEnumerable<IReflektorProject> Projects => _projects ?? (_projects = GetProjects());

        private IEnumerable<IReflektorProject> GetProjects()
        {
            var list = new List<IReflektorProject>();
            foreach (Project project in DteSolution.Projects)
            {
                list.Add(new ReflektorProject(project, this));
            }
            return list;
        }
    }
}
