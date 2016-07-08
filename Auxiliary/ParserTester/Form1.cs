using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AspectCore;

namespace ParserTester
{
    public partial class Form1 : Form
    {
        //AspectCore.AspectManager AspectParser = new AspectCore.AspectManager();
        ParserWrapper AspectParser = new ParserWrapper();

        public Form1()
        {
            InitializeComponent();
        }

        int PointsCount(PointOfInterest pt)
        {
            if (pt == null)
                return 0;
            int result = 1;
            foreach (PointOfInterest p in pt.Items)
                result += PointsCount(p);
            return result;
        }

        private void bChooseFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                listBox1.Items.Clear();
                DirectoryInfo di = new DirectoryInfo(folderBrowserDialog1.SelectedPath);
                FileInfo[] Files = di.GetFiles("*.pas", SearchOption.AllDirectories);
                foreach (FileInfo fi in Files)
                {
                    string msgFile = "Файл: \"" + fi.FullName + "\" ";
                    try
                    {
                        PointOfInterest ParsedPoints = AspectParser.ParseFile(fi.FullName);
                        int ptsCount = PointsCount(ParsedPoints);
                        string msgResult;
                        if (ParsedPoints == null)
                        {
                            msgResult = "не разобран";
                            msgFile = "  x   " + msgFile;
                        }
                        else
                        {
                            msgResult = "разобран, сущностей найдено: " + ptsCount.ToString();
                            msgFile = "      " + msgFile;
                        }
                        listBox1.Items.Add(msgFile + msgResult);
                    }
                    catch (Exception exc)
                    {
                        msgFile = "===== " + msgFile;
                        listBox1.Items.Add(msgFile + "ПАРСЕР УПАЛ " + exc.Message);
                        AspectParser.ReloadParsers();
                    }
                }
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = Environment.CurrentDirectory;
        }
    }

}
