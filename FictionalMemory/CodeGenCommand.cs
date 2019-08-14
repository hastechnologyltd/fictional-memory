using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using EnvDTE80;
using FictionalMemory.CodeGen;
using FictionalMemory.Reflection;
using FictionalMemory.RocketReflektor;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using RocketReflektor.CodeFiles;
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
            //((OleMenuCommand)sender).Visible = false;
            //if(VsSolution.SolutionExplorer.IsAnOpenApiDocItemSelected)
            //{
            //    ((OleMenuCommand)sender).Visible = true;
            //}
        }

        private void Execute(object sender, EventArgs e)
        {
            if (!Generator.IsGenerating)
            {
                _ = Task.Run(async () =>
                {
                    Search();
                    if (await Generator.ExecuteAsync(VsSolution.SolutionExplorer.CurrentSelectedOpenApiDocItem))
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




        private static IEnumerable<Type> GetLoadableTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null);
            }
        }

        public void Search()
        {
            //var service = (DTE2)Package.GetGlobalService(typeof(SDTE));

            //var solution = new ReflektorSolution(service.Solution);

            //var dte2Solution = (EnvDTE80.Solution2)service.Application.Solution;

            //var projectItemTemplate = dte2Solution.GetProjectItemTemplate("Class", "CSharp");



            //var folder = service.Solution.Projects.Item(1).ProjectItems.AddFolder("Jeff");
            //var componentItemTemplate = dte2Solution.GetProjectItemTemplate("OpenApiComponentTemplate", "CSharp");
            //var componentItem = folder.ProjectItems.AddFromTemplate(componentItemTemplate, "SusieComponent.cs");
            //var controllerItemTemplate = dte2Solution.GetProjectItemTemplate("OpenApiControllerTemplate", "CSharp");
            //var controllerItem = folder.ProjectItems.AddFromTemplate(controllerItemTemplate, "SusieController.cs");

            //FileCodeModel fileCodeModel = componentItem.FileCodeModel;

            //CodeElements nameSpace = null;
            //for (var i = 1; i < fileCodeModel.CodeElements.Count + 1; i++)
            //{
            //    CodeElement namespaceCodeElement = fileCodeModel.CodeElements.Item(i);
            //    if (namespaceCodeElement.Kind == vsCMElement.vsCMElementNamespace)
            //    {
            //        nameSpace = namespaceCodeElement.Children;
            //        break;
            //    }
            //}

            //CodeClass codeClass = null;
            //for (var i = 1; i < nameSpace.Count + 1; i++)
            //{
            //    CodeElement classCodeElement = nameSpace.Item(i);
            //    if (classCodeElement.Kind == vsCMElement.vsCMElementClass)
            //    {
            //        codeClass = (CodeClass)classCodeElement;
            //        break;
            //    }
            //}


            //codeClass.AddVariable("PropertyOne", "string", -1, vsCMAccess.vsCMAccessPublic);







            var service = (DTE2)Package.GetGlobalService(typeof(SDTE));

            var solution = new DteSolution(service.Solution);
            var discover = new Discover();

            foreach (var project in solution.Projects)
            {
                var d = discover.GetFiles(project.ProjectItems);
            }

            //var project = service.Solution.Projects.Item(1);

            //var discover = new Discover();

            //var d = discover.GetFiles(new DteProjectItems(project.ProjectItems));











            //var classes = solution.Projects.SelectMany(x => x.GetClasses());

            //var filteredClasses = solution.Projects.SelectMany(x => x.FilteredClasses("Status"));

            //var statusClass = filteredClasses.First();
            //var a = statusClass.Name;
            //var b = statusClass.NameSpace;
            //var c = statusClass.LocalPath;




            //var susieClass = statusClass.ParentProject.AddClass("Susie", b);
            //susieClass.DefinePublicProperty("Address", "string");
        }
    }
}
