using System;
using System.Collections.Generic;
using System.Text;
using VisualPascalABCPlugins;
using WeifenLuo.WinFormsUI.Docking;
using VisualPascalABC;
using System.Reflection;
using Microsoft.Win32;
using System.Windows.Forms;

namespace PascalABCAspects
{
    public class Aspect_VisualPascalABCPlugin : IVisualPascalABCPlugin
    {
        public static string Name = "Окно аспектов";
        public static string Descr = "Подсистема навигации по аспектам программного кода";
        private static string OutputPrefix = "[AspectPlugin] ";
        private AspectForm _form;
        IWorkbench _workbench;
        DockState dockState = DockState.DockLeft;

        public Aspect_VisualPascalABCPlugin(IWorkbench workbench)
        {
            _workbench = workbench;
            AspectCore.GlobalData.traceAction = TraceMessage;
            _form = new AspectForm(workbench);
        }

        string IVisualPascalABCPlugin.Copyright
        {
            get { return "© Малеванный М.С. 2014"; }
        }

        void IVisualPascalABCPlugin.GetGUI(List<IPluginGUIItem> MenuItems, List<IPluginGUIItem> ToolBarItems)
        {
            PluginGUIItem Item = new PluginGUIItem(Name, Descr, _form.PluginImage.Image, _form.PluginImage.BackColor, Execute);
            MenuItems.Add(Item);
            ToolBarItems.Add(Item);
        }

        string IVisualPascalABCPlugin.Name
        {
            get { return "AspectNavigator"; }
        }

        string IVisualPascalABCPlugin.Version
        {
            get { return "1.0"; }
        }

        public void Execute()
        {
            if (_form.Visible)
            {
                dockState = _form.DockState;
                _form.Hide();
                _form.SaveCurrentFile();
                ((DockPanel)(_workbench as Form1).Controls["MainDockPanel"]).ActiveDocumentChanged -= ActiveDocumentChanged;
            }
            else
            {
                DockPanel mdp = (DockPanel)(_workbench as Form1).Controls["MainDockPanel"];
                _form.Show(mdp, dockState);
                ActiveDocumentChanged(null, null);
                ((DockPanel)(_workbench as Form1).Controls["MainDockPanel"]).ActiveDocumentChanged += ActiveDocumentChanged;
            }
        }

        private void ActiveDocumentChanged(object sender, EventArgs e)
        {
            IDockContent AD = ((DockPanel)(_workbench as Form1).Controls["MainDockPanel"]).ActiveDocument;
            if (AD is ICodeFileDocument)
            {
                _form.SaveCurrentFile();
                _form.LoadFile((AD as ICodeFileDocument).FileName);
            }
        }
        private void TraceMessage(string str)
        {
            try
            {
                (_workbench.CompilerConsoleWindow as CompilerConsoleWindowForm).AppendTextToConsoleCompiler(OutputPrefix + str + Environment.NewLine);
            }
            catch (Exception e)
            {}
        }
    }
}
