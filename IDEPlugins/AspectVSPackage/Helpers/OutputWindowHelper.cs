using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio;

namespace MMX.AspectVSPackage.Helpers
{
    class OutputWindowHelper
    {
        private static IVsOutputWindowPane pane;
        public static void WriteOutput(string Message)
        {
            if (pane == null)
            {
                IVsOutputWindow outputWindow = AspectPackage.GetGlobalService(typeof(SVsOutputWindow)) as IVsOutputWindow;

                Guid guidGeneral = Microsoft.VisualStudio.VSConstants.OutputWindowPaneGuid.GeneralPane_guid;
                outputWindow.CreatePane(guidGeneral, AspectCore.Strings.ToolWindowTitle, 1, 0);
                outputWindow.GetPane(guidGeneral, out pane);
            }
            pane.OutputString(Message+Environment.NewLine);
        }
    }
}
