using System.Collections.Generic;

namespace RocketReflektor.Dte
{
    public interface IDteSolution
    {
        IEnumerable<IDteProject> Projects { get; }
    }
}
