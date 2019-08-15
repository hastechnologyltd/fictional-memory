using System.Collections;
using System.Collections.Generic;
using RocketReflektor.Dte;

namespace RocketReflektor
{
    public class Projects : IProjects
    {
        public void AddProject(string name, string templateName)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator<IProject> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
