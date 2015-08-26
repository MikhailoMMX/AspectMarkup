using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspectCore;
using EnvDTE;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio;

namespace MMX.AspectVSPackage
{
    public class VSInterop : IDEInterop
    {
        private AspectWindow aw;
        private EnvDTE.DTE _dte;
        private EnvDTE.DTE dte {
            get 
            {
                if (_dte == null)
                    _dte = (EnvDTE.DTE)AspectPackage.dte;
                return _dte;
            } 
        }
        public VSInterop(AspectWindow aw)
        {
            this.aw = aw;
        }

        private TextDocument GetCurrentVSTextDocument()
        {
            if (dte.ActiveDocument != null)
                return (TextDocument)dte.ActiveDocument.Object("TextDocument"); //string.Empty тоже можно
            else return null;
        }
        public override string GetCurrentDocumentFileName()
        {
            if (dte != null && dte.ActiveDocument != null)
                return dte.ActiveDocument.FullName;
            else
                return "";
        }

        public override string GetCurrentLine()
        {
            if (dte == null || dte.ActiveDocument == null)
                return "";

            EnvDTE.TextDocument objTextDoc = GetCurrentVSTextDocument();
            EditPoint ep = objTextDoc.StartPoint.CreateEditPoint();
            ep.LineDown(objTextDoc.Selection.TopPoint.Line - 1);
            EditPoint ep2 = ep.CreateEditPoint();
            ep2.EndOfLine();
            return ep.GetText(ep2);
        }

        public override string GetLine(int lineIndex)
        {
            if (dte == null || dte.ActiveDocument == null)
                return "";

            EnvDTE.TextDocument objTextDoc = GetCurrentVSTextDocument();
            EditPoint ep = objTextDoc.StartPoint.CreateEditPoint();
            ep.LineDown(lineIndex - 1);
            EditPoint ep2 = ep.CreateEditPoint();
            ep2.EndOfLine();
            return ep.GetText(ep2);
        }

        public override bool IsDocumentOpen()
        {
            return GetCurrentTextDocument() != null;
        }

        public override bool IsDocumentOpen(string FileName)
        {
            for (int i = 1; i <= dte.Documents.Count; ++i) //нумерация с 1
                if (dte.Documents.Item(i).FullName.Equals(FileName, StringComparison.CurrentCultureIgnoreCase))
                    return true;
            return false;
        }

        public override QUT.Gppg.LexLocation GetCursorPosition()
        {
            if (dte == null || dte.ActiveDocument == null)
                return new QUT.Gppg.LexLocation();
            
            TextSelection ts = GetCurrentVSTextDocument().Selection;
            return new QUT.Gppg.LexLocation(ts.TopPoint.Line, ts.TopPoint.LineCharOffset, ts.BottomPoint.Line, ts.BottomPoint.LineCharOffset);
        }

        public override string GetCurrentTextDocument()
        {
            TextDocument td = GetCurrentVSTextDocument();
            if (td == null)
                return "";
            EditPoint ep = td.StartPoint.CreateEditPoint();
            return ep.GetText(td.EndPoint.CreateEditPoint());
        }

        public override string GetDocument(string FileName)
        {
            for (int i = 1; i <= dte.Documents.Count; ++i) //нумерация с 1
                if (dte.Documents.Item(i).FullName.Equals(FileName, StringComparison.CurrentCultureIgnoreCase))
                {
                    TextDocument textDoc = dte.Documents.Item(i).Object() as TextDocument;
                    if (textDoc != null)
                        return textDoc.CreateEditPoint(textDoc.StartPoint).GetText(textDoc.EndPoint);
                }
            return "";
        }

        public override void NavigateToFileAndPosition(string file, int line, int col, int lineEnd = 0, int columnEnd = 0)
        {
            IVsUIShellOpenDocument openDoc = AspectPackage.GetGlobalService(typeof(IVsUIShellOpenDocument)) as IVsUIShellOpenDocument;

            IVsWindowFrame frame;
            Microsoft.VisualStudio.OLE.Interop.IServiceProvider sp;
            IVsUIHierarchy hier;
            uint itemid;
            Guid logicalView = VSConstants.LOGVIEWID_Code;
            if (ErrorHandler.Failed(openDoc.OpenDocumentViaProject(file, ref logicalView, out sp, out hier, out itemid, out frame)) || frame == null)
            {
                return;
            }
            object docData;
            frame.GetProperty((int)__VSFPROPID.VSFPROPID_DocData, out docData);

            // Get the VsTextBuffer  
            VsTextBuffer buffer = docData as VsTextBuffer;
            if (buffer == null)
            {
                IVsTextBufferProvider bufferProvider = docData as IVsTextBufferProvider;
                if (bufferProvider != null)
                {
                    IVsTextLines lines;
                    ErrorHandler.ThrowOnFailure(bufferProvider.GetTextBuffer(out lines));
                    buffer = lines as VsTextBuffer;
                    if (buffer == null)
                        return;
                }
            }
            IVsTextManager mgr = AspectPackage.GetGlobalService(typeof(VsTextManagerClass)) as IVsTextManager;
            if (lineEnd == 0)
            {
                lineEnd = line;
                columnEnd = col;
            }
            mgr.NavigateToLineAndColumn(buffer, ref logicalView, line - 1, col, lineEnd - 1, columnEnd);
        }

        public override void UpdateSubAspectProperties(PointOfInterest point, System.Windows.Forms.TreeNode node)
        {
            Helpers.AspectPropertiesHelper.UpdateSubAspectProperties(point, node, aw.GetITrackSelectionService());
        }
    }
}
