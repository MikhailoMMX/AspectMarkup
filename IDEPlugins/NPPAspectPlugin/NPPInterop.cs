using System;
using System.Collections.Generic;
using System.Text;
using AspectCore;
using NppPluginNET;
using System.Runtime.InteropServices;

namespace NPPAspectPlugin
{
    class NPPInterop : IDEInterop
    {
        private const int _charLen = 2;
        private IntPtr _nppHandle;
        private Encoding _NppEncoding = Encoding.Unicode;
        private Encoding _SciEncoding = Encoding.UTF8;
        private int tabSize = 1;

        private IntPtr _sciHandle
        {
            get 
            {
                int s = 0;
                Win32.SendMessage(_nppHandle, NppMsg.NPPM_GETCURRENTSCINTILLA, 0, out s);
                if (s == 0)
                    return PluginBase.nppData._scintillaMainHandle;
                else
                    return PluginBase.nppData._scintillaSecondHandle;
            }   
        }

        public NPPInterop()
        {
            _nppHandle = PluginBase.nppData._nppHandle;
            tabSize = Win32.SendMessage(PluginBase.GetCurrentScintilla(), SciMsg.SCI_GETTABWIDTH, 0, 0).ToInt32();
        }
        public override bool IsDocumentOpen()
        {
            //TODO
            StringBuilder buffer = new StringBuilder(Win32.MAX_PATH);
            NppPluginNET.Win32.SendMessage(_nppHandle, NppMsg.NPPM_GETFULLCURRENTPATH, Win32.MAX_PATH, buffer);
            return buffer.ToString().Contains("\\");
        }
        public override bool IsDocumentOpen(string FileName)
        {
            bool result = false;
            int ret = Win32.SendMessage(_nppHandle, NppMsg.NPPM_GETNBOPENFILES, 0, (int)NppMsg.ALL_OPEN_FILES).ToInt32();
            if (ret <= 0)
                return false;
            IntPtr[] str = new IntPtr[ret];
            for (int i = 0; i < ret; ++i)
                str[i] = Marshal.AllocHGlobal(Win32.MAX_PATH * _charLen);
            IntPtr buf = Marshal.AllocHGlobal(ret * IntPtr.Size);
            Marshal.Copy(str, 0, buf, ret);

            int ret2 = Win32.SendMessage(_nppHandle, NppMsg.NPPM_GETOPENFILENAMES, buf, ret).ToInt32();

            for (int i = 0; i< ret2; ++i)
            {
                byte[] c = new byte[Win32.MAX_PATH*2];
                Marshal.Copy(str[i], c, 0, Win32.MAX_PATH * _charLen);
                int len = 0;
                for (; len < Win32.MAX_PATH * _charLen; len += _charLen)
                    if (c[len] == 0 && (_charLen == 2 && c[len + 1] == 0))
                        break;
                string s = _NppEncoding.GetString(c, 0, len);
                if (s.Equals(FileName, StringComparison.CurrentCultureIgnoreCase))
                {
                    result = true;
                    break;
                }
            }

            for (int i = 0; i < str.Length; ++i)
                Marshal.FreeHGlobal(str[i]);
            Marshal.FreeHGlobal(buf);
            return result;
        }
        public override string GetCurrentDocumentFileName()
        {
            StringBuilder buffer = new StringBuilder(Win32.MAX_PATH);
            Win32.SendMessage(_nppHandle, NppMsg.NPPM_GETFULLCURRENTPATH, Win32.MAX_PATH, buffer);
            return buffer.ToString();
        }
        public override string GetCurrentTextDocument()
        {
            int length = Win32.SendMessage(_sciHandle, SciMsg.SCI_GETTEXTLENGTH, 0, 0).ToInt32();
            IntPtr buf = Marshal.AllocHGlobal((length + 1) * _charLen);
            int nTempLength = Win32.SendMessage(_sciHandle, SciMsg.SCI_GETTEXT, length + 1, buf).ToInt32();

            byte[] chars = new byte[nTempLength * _charLen];
            Marshal.Copy(buf, chars, 0, nTempLength * _charLen);
            string res = _SciEncoding.GetString(chars);

            Marshal.FreeHGlobal(buf);
            return res;
        }
        public override string GetDocument(string FileName)
        {
            if (IsDocumentOpen(FileName))
            {
                SwitchToFile(FileName);
                return GetCurrentTextDocument();
            }
            else
                return base.GetDocument(FileName);
        }
        public override string GetLine(int lineIndex)
        {
            int nChars = Win32.SendMessage(_sciHandle, SciMsg.SCI_LINELENGTH, lineIndex - 1, 0).ToInt32();
            IntPtr buf = Marshal.AllocHGlobal(nChars * _charLen);
            int len = Win32.SendMessage(_sciHandle, SciMsg.SCI_GETLINE, lineIndex-1, buf).ToInt32();
            byte[] bytes = new byte[len];
            Marshal.Copy(buf, bytes, 0, bytes.Length);
            Marshal.FreeHGlobal(buf);
            return _SciEncoding.GetString(bytes, 0, len);
        }
        public override string GetCurrentLine()
        {
            return GetLine(GetCursorPosition().StartLine);
        }
        public override QUT.Gppg.LexLocation GetCursorPosition()
        {
            //TODO Selection
            int Line = Win32.SendMessage(_nppHandle, NppMsg.NPPM_GETCURRENTLINE, 0, 0).ToInt32()+1;
            int Col = Win32.SendMessage(_nppHandle, NppMsg.NPPM_GETCURRENTCOLUMN, 0, 0).ToInt32();
            if (tabSize != 1)
            {
                string lineText = GetLine(Line);
                Col = GetActualColumn(lineText, Col);
            }

            return new QUT.Gppg.LexLocation(Line, Col, Line, Col);
        }
        public override void NavigateToFileAndPosition(string file, int line, int col, int lineEnd = 0, int columnEnd = 0)
        {
            //TODO Selection
            SwitchToFile(file);

            int posStart = Win32.SendMessage(_sciHandle, SciMsg.SCI_POSITIONFROMLINE, line-1, 0).ToInt32() + col;
            Win32.SendMessage(_sciHandle, SciMsg.SCI_GOTOPOS, posStart, 0);
        }
        private void SwitchToFile(string FileName)
        {
            IntPtr buf = Marshal.AllocHGlobal((FileName.Length + 1) * _charLen);
            byte[] bytes = new byte[(FileName.Length+1)*_charLen];
            _NppEncoding.GetBytes(FileName).CopyTo(bytes, 0);
            for (int i = FileName.Length * _charLen; i < FileName.Length * _charLen + _charLen; ++i)
                bytes[i] = 0;
            Marshal.Copy(bytes, 0, buf, bytes.Length);
            if (IsDocumentOpen(FileName))
                Win32.SendMessage(_nppHandle, NppMsg.NPPM_SWITCHTOFILE, 0, buf);
            else
                Win32.SendMessage(_nppHandle, NppMsg.NPPM_DOOPEN, 0, buf);
            Marshal.FreeHGlobal(buf);
        }
        private int GetActualColumn(string text, int Column)
        {
            for (int i = 0; i < Column; ++i)
                if (text[i] == '\t')
                    Column -= tabSize-1;
            return Column;
        }
    }
}
