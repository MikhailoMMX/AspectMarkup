using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using AspectCore;
using System.IO;

namespace DLLWrapper
{
    public partial class Form1 : Form
    {
        AspectCore.AspectManager AspectManager = new AspectCore.AspectManager();
        //ParserWrapper AspectParser = new ParserWrapper(Environment.CurrentDirectory);
        TreeManager treeManager = new TreeManager(Environment.CurrentDirectory);
        TreeViewAdapter tvAdapter;
        string CurrentFile;
        PointOfInterest ParsedPoints;

        private void UpdateStatus(string newText)
        {
            statusLabel.Text = newText;
        }

        public Form1()
        {
            try
            {
                InitializeComponent();
                SetDialogFilter();

                if (treeManager.GetParsersCount() == 0)
                    treeManager = new TreeManager();

                UpdateStatus("Загружено парсеров " + treeManager.GetParsersCount() + ": " +string.Join(", ", treeManager.GetParserIDs()));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void SetDialogFilter()
        {
            List<string> SL = new List<string>();
            foreach (string str in treeManager.GetParserIDs())
            {
                if (str == "*")
                    continue;
                SL.Add(str.ToUpper() + "-файл");
                SL.Add("*." + str);
            }
            SL.Add("Все файлы");
            SL.Add("*.*");
            openFileDialog1.Filter = string.Join("|", SL);
        }

        private void SetNotes(PointOfInterest point)
        {
            if (point == null)
                return;
            if (point.Context != null && point.Context.Count != 0)
                point.Note = point.Context[0].Type;
            foreach (PointOfInterest p in point.Items)
                SetNotes(p);
        }

        private void RebuildParsedTree()
        {
            tvParsedPoints.Nodes.Clear();
            SetNotes(ParsedPoints);
            AspectManager.WorkingAspect = ParsedPoints;
            tvAdapter = new TreeViewAdapter(AspectManager, tvParsedPoints);
            tvAdapter.RebuildTree();
        }

        private string ConvertNewLines(string text)
        {
            StringBuilder sb = new StringBuilder();
            bool flag = false;
            for (int i = 0; i < text.Length; ++i)
            {
                if (!flag && (text[i] == '\n'))
                    sb.Append('\r');
                sb.Append(text[i]);
                flag = text[i] == '\r';
            }
            return sb.ToString();
        }
        private void OpenAndParseFile(string filename)
        {
            try
            {
                if (CurrentFile == filename)
                    return;

                tbEditor.Text = "";
                tvParsedPoints.Nodes.Clear();

                CurrentFile = filename;
                tbEditor.Text = ConvertNewLines(System.IO.File.ReadAllText(CurrentFile));
                bReparseText.Enabled = true;

                ParseText();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void ParseText()
        {
            Stopwatch clock = new Stopwatch();
            clock.Start();
            ParsedPoints = treeManager.GetTree(CurrentFile, tbEditor.Text);
            ParsedPoints.Items.AddRange(treeManager.GetLastParseErrors());
            clock.Stop();

            if (ParsedPoints == null)
            {
                tvAdapter.ClearAspect();
                UpdateStatus("ERROR");
                return;
            }

            UpdateStatus("Время парсинга: " + clock.ElapsedMilliseconds.ToString() + " мс");

            RebuildParsedTree();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                OpenAndParseFile(openFileDialog1.FileName);
        }

        void SetCursor(string filename, QUT.Gppg.LexLocation location)
        {
            //tbEditor.Focus();   //doesn't work after replacing listbox with treeview;
            //Костыль: посылаю сигнал о нажатии кнопки Tab и за счет свойств TabIndex и TabStop переходим на TextBox
            SendKeys.Send("{TAB}");
            if (location.StartLine < 1)
                return;

            int sl = location.StartLine < 0 ? 0 : location.StartLine;
            int sc = location.StartColumn < 0 ? 0 : location.StartColumn;
            int el = location.EndLine < 0 ? 0 : location.EndLine;
            int ec = location.EndColumn < 0 ? 0 : location.EndColumn;

            int start;
            if (el <= tbEditor.Lines.Length)
                start = tbEditor.GetFirstCharIndexFromLine(sl - 1) + sc;
            else
                start = tbEditor.Text.Length;

            int end;
            if (el <= tbEditor.Lines.Length)
                end = tbEditor.GetFirstCharIndexFromLine(el - 1) + ec;
            else
                end = tbEditor.Text.Length;

            if (start > tbEditor.Text.Length)
                tbEditor.SelectionStart = tbEditor.Text.Length;
            else
                tbEditor.SelectionStart = start;
            
            if (end - start < 0)
                tbEditor.SelectionLength = 0;
            else
                tbEditor.SelectionLength = end - start;

            tbEditor.ScrollToCaret();
        }

        private void tvParsedPoints_AfterSelect(object sender, TreeViewEventArgs e)
        {
            PointOfInterest p = tvAdapter.GetPointByNode(e.Node);
            if (p != null)
                SetCursor(AspectManager.GetFullFilePath(p.FileName), p.Location);
        }

        private void bReparseText_Click(object sender, EventArgs e)
        {
            try
            {
                ParseText();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private PointOfInterest FindPointInCurrentPosition(PointOfInterest ParsedPoints)
        {
            int line = tbEditor.GetLineFromCharIndex(tbEditor.SelectionStart) + 1;
            int col = tbEditor.SelectionStart - tbEditor.GetFirstCharIndexFromLine(line - 1);
            try
            {
                return TreeSearchEngine.FindPointByLocation(ParsedPoints, line, col)[0];
            }
            catch (Exception)
            {}
            return null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
