using EnvDTE;

namespace FictionalMemory.CodeGen.Generation
{
    internal class GenClass : IGenClass
    {
        private readonly CodeClass _codeClass;

        public GenClass(CodeClass codeClass)
        {
            _codeClass = codeClass;
        }

        public void AddPublicProperty(string propertyName, string typeName)
        {
            var prop = _codeClass.AddVariable(propertyName, typeName, -1, vsCMAccess.vsCMAccessPublic);
            var startPoint = prop.StartPoint;
            int linelength = startPoint.LineLength;
            var edit = startPoint.CreateEditPoint();
            var currentLine = edit.GetLines(edit.Line, edit.Line + 1).TrimStart();
            var newLine = currentLine.Replace(";", " { get; set; }");
            edit.Delete(currentLine.Length);
            edit.Insert(newLine);
        }
    }
}
