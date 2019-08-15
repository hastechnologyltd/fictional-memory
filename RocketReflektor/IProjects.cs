using System.Collections.Generic;

namespace RocketReflektor
{
    public interface IProjects : IEnumerable<IProject>
    {
        void AddProject(string name, string templateName);
    }
}
