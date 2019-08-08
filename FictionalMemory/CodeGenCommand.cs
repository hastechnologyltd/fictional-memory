using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using EnvDTE;
using EnvDTE80;
using FictionalMemory.CodeGen;
using FictionalMemory.CodeGen.Generation;
using FictionalMemory.Reflection;
using FictionalMemory.Reflektor;
using FictionalMemory.Reflektor.Helpers;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Design;
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
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            IEnumerable<Type> types = assemblies.SelectMany(x => GetLoadableTypes(x));

            var service = (DTE)Package.GetGlobalService(typeof(SDTE));
            var projectItem = service.Solution.FindProjectItem("Jeff.cs");


            var m = projectItem.FileCodeModel;


            var jeff = new List<string>();

            FileCodeModel fileCodeModel = projectItem.FileCodeModel;

            for (int i = 1; i <= fileCodeModel.CodeElements.Count; ++i)
            {
                CodeElement fileCodeElement = fileCodeModel.CodeElements.Item(i);
                if (fileCodeElement.Kind == vsCMElement.vsCMElementNamespace)
                {
                    CodeNamespace fileNamespace = (CodeNamespace) fileCodeElement;
                    for (int j = 1; j <= fileNamespace.Members.Count; ++j)
                    {
                        CodeElement fileNamespaceElement = fileNamespace.Members.Item(j);

                        jeff.Add(fileNamespaceElement.FullName);
                        jeff.Add(fileNamespaceElement.Name);
                        jeff.Add(fileNamespaceElement.Kind.ToString());

                        if (fileNamespaceElement.Kind == vsCMElement.vsCMElementClass)
                        {
                            CodeType classElement = (CodeType) fileNamespaceElement;
                            for (int k = 1; k <= classElement.Members.Count; ++k)
                            {
                                CodeElement classMember = classElement.Members.Item(k);
                                jeff.Add(classMember.Kind.ToString());
                                if (classMember.Kind == vsCMElement.vsCMElementProperty)
                                {
                                    var codeProperty = (CodeProperty2) classMember;
                                    jeff.Add(codeProperty.Name);
                                    jeff.Add(codeProperty.Type.AsString);

                                }
                            }
                        }


                        var cls = (CodeClass)fileNamespaceElement;

                        var genClass = new GenClass(cls);
                        genClass.AddPublicProperty("Address", "Address");
                        genClass.AddPublicProperty("Client", "string");
                    }
                }
            }

            var solution = new ReflektorSolution(service.Solution);

            var classes = solution.Projects.SelectMany(x => x.GetClasses());

            var filteredClasses = solution.Projects.SelectMany(x => x.FilteredClasses("Jeff"));

            solution.Projects.First().AddClass("Susie", "");
        }
    }
}
