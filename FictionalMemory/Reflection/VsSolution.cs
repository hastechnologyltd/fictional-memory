using EnvDTE80;

namespace FictionalMemory.Reflection
{
    internal class VsSolution
    {
        public static DTE2 ApplicationObject { get; set; }
        public static SolutionExplorer SolutionExplorer => new SolutionExplorer(ApplicationObject.ToolWindows.SolutionExplorer);
    }
}
