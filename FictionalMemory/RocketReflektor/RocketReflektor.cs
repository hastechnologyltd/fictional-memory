using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using RocketReflektor;

namespace FictionalMemory.RocketReflektor
{
    public class RocketReflektor : IRocketReflektor
    {
        private DTE2 _dte;
        private DTE2 Dte => _dte ?? (_dte = (DTE2)Package.GetGlobalService(typeof(SDTE)));

        public RocketReflektor()
        {
        }
    }
}
