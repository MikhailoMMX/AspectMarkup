using System;
using System.Collections.Generic;
using System.Text;
using VisualPascalABCPlugins;
using VisualPascalABC;
using AspectCore;

namespace PascalABCAspects
{
    public class PABCInterop : IDEInterop
    {
        IVisualEnvironmentCompiler _compiler;
        IWorkbench _wb;

        public PABCInterop(IWorkbench Workbench)
        {
            _wb = Workbench;
            _compiler = _wb.VisualEnvironmentCompiler;
        }

        public override string GetCurrentDocumentFileName()
        {
            return (string)_compiler.ExecuteAction(VisualEnvironmentCompilerAction.GetCurrentSourceFileName, null);
        }
        public override string GetCurrentLine()
        {
            return GetLine(GetCursorPosition().StartLine);
        }
        public override string GetCurrentTextDocument()
        {
            ICodeFileDocument doc = (_wb as IWorkbenchDocumentService).CurrentCodeFileDocument;
            return doc.TextEditor.Text;
        }
        public override QUT.Gppg.LexLocation GetCursorPosition()
        {
            ICodeFileDocument doc = (_wb as IWorkbenchDocumentService).CurrentCodeFileDocument;
            List<ICSharpCode.TextEditor.Document.ISelection> sel = doc.TextEditor.ActiveTextAreaControl.SelectionManager.SelectionCollection;
            if (sel.Count>0)
                return new QUT.Gppg.LexLocation(sel[0].StartPosition.Line+1, sel[0].StartPosition.Column+1, sel[0].EndPosition.Line+1, sel[0].EndPosition.Column+1);
            ICSharpCode.TextEditor.Caret caret = doc.TextEditor.ActiveTextAreaControl.Caret;
            return new QUT.Gppg.LexLocation(caret.Line+1, caret.Column, caret.Line+1, caret.Column);
        }
        public override string GetDocument(string FileName)
        {
            IWorkbenchDocumentService DS = _wb as IWorkbenchDocumentService;
            if (DS.ContainsTab(FileName))
                return DS.GetDocument(FileName).TextEditor.Text;
            //((DS as Form1).FindTab(FileName) as ICodeFileDocument).TextEditor.Text; // можно было так обойтись без GetDocument 
            else
                return base.GetDocument(FileName);
        }
        public override string GetLine(int lineIndex)
        {
            ICodeFileDocument doc = (_wb as IWorkbenchDocumentService).CurrentCodeFileDocument;
            string text = doc.TextEditor.Text;
            text = text.Split('\n')[lineIndex-1];
            return text;
        }
        public override bool IsDocumentOpen()
        {
            return true;
        }
        public override bool IsDocumentOpen(string FileName)
        {
            return (_wb as IWorkbenchDocumentService).ContainsTab(FileName);
        }
        public override void NavigateToFileAndPosition(string file, int line, int col, int lineEnd = 0, int columnEnd = 0)
        {
            if (lineEnd == 0)
                lineEnd = line;
            if (columnEnd == 0)
                columnEnd = col;
            PascalABCCompiler.SourceLocation sl = new PascalABCCompiler.SourceLocation(file, line, col + 1, lineEnd, columnEnd);
            _wb.VisualEnvironmentCompiler.ExecuteSourceLocationAction(sl, SourceLocationAction.SelectAndGotoBeg);
        }
        public override void UpdateSubAspectProperties(PointOfInterest point, System.Windows.Forms.TreeNode node)
        {  }
    }
}