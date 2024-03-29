﻿using EnvDTE;
using RocketReflektor.Dte;

namespace FictionalMemory.RocketReflektor
{
    internal class DteProject : IDteProject
    {
        private readonly Project _dteProject;

        public DteProject(Project dteProject)
        {
            _dteProject = dteProject;
        }

        public IDteProjectItems ProjectItems => new DteProjectItems(_dteProject.ProjectItems);
    }
}
