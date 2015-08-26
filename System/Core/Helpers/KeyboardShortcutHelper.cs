using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AspectCore.Helpers
{
    public class KeyboardShortcutHelper
    {
        private struct MSG
        {
            public IntPtr hwnd;
            public int message;
            public IntPtr wParam;
            public IntPtr lParam;
            public int time;
            public int pt_x;
            public int pt_y;
        }
        public static AspectWindowPane control;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool PeekMessage([In, Out] ref MSG msg,
            HandleRef hwnd, int msgMin, int msgMax, int remove);


        public static void ProcessKeyPreview(ref Message m, Keys ModifierKeys)
        {
            const int WM_KEYDOWN = 0x100;
            //const int WM_KEYUP = 0x101;
            const int WM_CHAR = 0x102;
            const int WM_SYSCHAR = 0x106;
            const int WM_SYSKEYDOWN = 0x104;
            //const int WM_SYSKEYUP = 0x105;
            const int WM_IME_CHAR = 0x286;

            KeyEventArgs e = null;

            if ((m.Msg != WM_CHAR) && (m.Msg != WM_SYSCHAR) && (m.Msg != WM_IME_CHAR))
            {
                e = new KeyEventArgs(((Keys)((int)((long)m.WParam))) | ModifierKeys);
                if ((m.Msg == WM_KEYDOWN) || (m.Msg == WM_SYSKEYDOWN))
                    control.TrappedKeyDown(e);

                // Remove any WM_CHAR type messages if supresskeypress is true.
                if (e.SuppressKeyPress)
                {
                    RemovePendingMessages(WM_CHAR, WM_CHAR);
                    RemovePendingMessages(WM_SYSCHAR, WM_SYSCHAR);
                    RemovePendingMessages(WM_IME_CHAR, WM_IME_CHAR);
                }
            }
        }

        private static void RemovePendingMessages(int msgMin, int msgMax)
        {
            if (!control.IsDisposed)
            {
                MSG msg = new MSG();
                IntPtr handle = control.Handle;
                while (PeekMessage(ref msg, new HandleRef(control, handle), msgMin, msgMax, 1))
                {
                }
            }

        }
    }
}
