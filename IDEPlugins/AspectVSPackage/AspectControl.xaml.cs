using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AspectCore;

namespace MMX.AspectVSPackage
{
    /// <summary>
    /// Interaction logic for MyControl.xaml
    /// </summary>
    public partial class AspectControl : UserControl
    {
        public AspectControl(AspectWindow win)
        {
            InitializeComponent();
            WinInstance = win;

            // Create the interop host control.
            System.Windows.Forms.Integration.WindowsFormsHost host =
                new System.Windows.Forms.Integration.WindowsFormsHost();

            AspectCore.GlobalData.traceAction = Helpers.OutputWindowHelper.WriteOutput;

            VSInterop ide = new VSInterop(WinInstance);

            // Assign the MaskedTextBox control as the host control's child.
            awp = new AspectWindowPane(ide);
            host.Child = awp;
            if (AspectPackage.dte != null && AspectPackage.dte.Solution != null)
            {
                string FileName = AspectPackage.dte.Solution.FullName.Replace(".sln", AspectCore.Strings.DefaultAspectExtension);
                if (FileName != "")
                    awp.OpenOrCreateAspectFile(FileName);
            }

            // Add the interop host control to the Grid 
            // control's collection of child controls. 
            this.grid1.Children.Add(host);
        }
        private AspectWindowPane awp;
        private AspectWindow WinInstance;
        public AspectWindowPane WindowPane { get { return awp; } }
    }
}