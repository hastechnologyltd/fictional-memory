using System.Collections.Generic;
using System.Linq;
using EnvDTE;
using RocketReflektor.Dte;

namespace FictionalMemory.RocketReflektor
{
    internal class DteSolution : IDteSolution
    {
        private readonly Solution _dteSolution;
        private IEnumerable<IDteProject> _projects;
        private IEnumerable<Project> _dteProjects()
        {
            var projects = new List<Project>();
            for (var i = 1; i < _dteSolution.Projects.Count + 1; i++)
            {
                projects.Add(_dteSolution.Projects.Item(i));
            }
            return projects;
        }

        public DteSolution(Solution dteSolution)
        {
            _dteSolution = dteSolution;
        }

        public IEnumerable<IDteProject> Projects => _projects ?? (_projects = _dteProjects().Select(p => new DteProject(p)));
    }
}
