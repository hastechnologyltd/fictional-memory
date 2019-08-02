using System;
using System.ComponentModel.Design;
using FictionalMemory.CodeGen;
using FictionalMemory.Reflection;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Task = System.Threading.Tasks.Task;

namespace FictionalMemory
{
    internal sealed class CodeGenCommand
    {
        public const int CommandId = 0x0100;
        private readonly AsyncPackage package;
        private Generator _generator;

        private CodeGenCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new OleMenuCommand(this.Execute, menuCommandID);
            menuItem.BeforeQueryStatus += BeforeQueryStatus;
            commandService.AddCommand(menuItem);
        }

        private void BeforeQueryStatus(object sender, EventArgs e)
        {
            ((OleMenuCommand)sender).Visible = false;
            if(VsSolution.SolutionExplorer.IsAnOpenApiDocItemSelected)
            {
                ((OleMenuCommand)sender).Visible = true;
            }
        }

        private void Execute(object sender, EventArgs e)
        {
            if (!Generator.IsGenerating)
            {
                _ = Task.Run(async () =>
                {
                    if(await Generator.ExecuteAsync(VsSolution.SolutionExplorer.CurrentSelectedOpenApiDocItem))
                    {
                        DisplayMessage("Code generator successful.");
                    }
                    else
                    {
                        DisplayMessage("Code generator failed.");
                    }
                });  
            }
            else
            {
                DisplayMessage("Code generator is still executing, please wait.");
            }
        }

        private void DisplayMessage(string message)
        {
            VsShellUtilities.ShowMessageBox(
                this.package,
                message,
                "Code Generator",
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }

        private Generator Generator => _generator ?? (_generator = new Generator());

        public static readonly Guid CommandSet = new Guid("c734bda7-680d-414d-8af1-3ccd141fbabd");
        public static CodeGenCommand Instance { get; private set; }
        public static async Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new CodeGenCommand(package, commandService);
        }
    }
}
