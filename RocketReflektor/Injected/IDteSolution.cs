using System.Collections.Generic;

namespace RocketReflektor.Injected
{
    public interface IDteSolution
    {
        IEnumerable<IDteProject> Projects { get; }
    }
}
