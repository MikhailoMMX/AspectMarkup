using System;
using System.Text;
using System.Windows.Forms;

namespace AspectCore.Helpers
{
    public class ExceptionInfoHelper
    {
        public static void ShowInfo(Exception e)
        {
            GlobalData.traceAction(e.ToString());
            //MessageBox.Show(e.ToString());
        }
    }
}
