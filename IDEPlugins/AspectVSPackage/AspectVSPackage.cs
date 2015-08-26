using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using EnvDTE;
using EnvDTE80;
using MMX.AspectVSPackage.Helpers;
using System.Reflection;

namespace MMX.AspectVSPackage
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    ///
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// </summary>
    // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
    // a package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    // This attribute is used to register the information needed to show this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    // This attribute is needed to let the shell know that this package exposes some menus.
    [ProvideMenuResource("Menus.ctmenu", 1)]
    // This attribute registers a tool window exposed by this package.
    [ProvideToolWindow(typeof(AspectWindow))]
    [Guid(GuidList.guidAspectPackagePkgString)]
    public sealed class AspectPackage : Package
    {
        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public AspectPackage()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
        }

        private static AspectPackage _instance;
        public static AspectPackage instance { get { return _instance; } }

        private static DTE2 _dte;
        public static DTE2 dte { get { return _dte; } }
        private DteInitializer dteInitializer;
        SolutionEvents _solutionEvents;

        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>

        private void InitializeDTE()
        {
            IVsShell shellService;

            _dte = this.GetService(typeof(Microsoft.VisualStudio.Shell.Interop.SDTE)) as EnvDTE80.DTE2;

            if (_dte == null) // The IDE is not yet fully initialized
            {
                shellService = this.GetService(typeof(SVsShell)) as IVsShell;
                this.dteInitializer = new DteInitializer(shellService, this.InitializeDTE);
            }
            else
            {
                this.dteInitializer = null;
                _solutionEvents = _dte.Events.SolutionEvents;
                _solutionEvents.Opened += new _dispSolutionEvents_OpenedEventHandler(SolutionEvents_Opened);
                _solutionEvents.Renamed += new _dispSolutionEvents_RenamedEventHandler(SolutionEvents_Renamed);
                _solutionEvents.AfterClosing += new _dispSolutionEvents_AfterClosingEventHandler(SolutionEvents_AfterClosing);
            }
        }

        internal class DteInitializer : IVsShellPropertyEvents
        {
            private IVsShell shellService;
            private uint cookie;
            private Action callback;

            internal DteInitializer(IVsShell shellService, Action callback)
            {
                int hr;

                this.shellService = shellService;
                this.callback = callback;

                // Set an event handler to detect when the IDE is fully initialized
                hr = this.shellService.AdviseShellPropertyChanges(this, out this.cookie);

                Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(hr);
            }

            int IVsShellPropertyEvents.OnShellPropertyChange(int propid, object var)
            {
                int hr;
                bool isZombie;

                if (propid == (int)__VSSPROPID.VSSPROPID_Zombie)
                {
                    isZombie = (bool)var;
                    if (!isZombie)
                    {
                        // Release the event handler to detect when the IDE is fully initialized
                        hr = this.shellService.UnadviseShellPropertyChanges(this.cookie);
                        Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(hr);
                        this.cookie = 0;
                        this.callback();
                    }
                }
                return VSConstants.S_OK;
            }
        }

        #endregion

        private void SolutionEvents_Opened()
        {
            //ShowAspectWindow(null, null);
            AspectWindow MyWindow = (AspectWindow)this.FindToolWindow(typeof(AspectWindow), 0, true);
            string FileName = _dte.Solution.FullName.Replace(".sln", AspectCore.Strings.DefaultAspectExtension);
            if (FileName != "")
                MyWindow.control.OpenOrCreateAspectFile(FileName);
        }
        private void SolutionEvents_Renamed(string oldName)
        {
            AspectWindow MyWindow = (AspectWindow)this.FindToolWindow(typeof(AspectWindow), 0, true);
            string FileName = _dte.Solution.FullName.Replace(".sln", AspectCore.Strings.DefaultAspectExtension);
            MyWindow.control.RenameAspectFile(FileName);
        }

        private void SolutionEvents_AfterClosing()
        {
            AspectWindow MyWindow = (AspectWindow)this.FindToolWindow(typeof(AspectWindow), 0, true);
            MyWindow.control.SaveAspectFile();
        }

        /// <summary>
        /// This function is called when the user clicks the menu item that shows the 
        /// tool window. See the Initialize method to see how the menu item is associated to 
        /// this function using the OleMenuCommandService service and the MenuCommand class.
        /// </summary>
        private void ShowToolWindow(object sender, EventArgs e)
        {
            // Get the instance number 0 of this tool window. This window is single instance so this instance
            // is actually the only one.
            // The last flag is set to true so that if the tool window does not exists it will be created.
            ToolWindowPane window = this.FindToolWindow(typeof(AspectWindow), 0, true);
            if ((null == window) || (null == window.Frame))
            {
                throw new NotSupportedException(Resources.CanNotCreateWindow);
            }
            IVsWindowFrame windowFrame = (IVsWindowFrame)window.Frame;
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());
        }

        private Assembly MyResolveEventHandler(object sender, ResolveEventArgs args)
        {
            if (args.Name.StartsWith("AspectCore,"))
            {
                RegistryKey rk = null;
                string Path = "";
                //may fail on 32-bit windows?
                rk = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("Software\\AspectCore");
                if (rk != null)
                    Path = (string)rk.GetValue("Install_Dir");
                else
                    return null;
			
                return Assembly.LoadFrom(Path + "\\AspectCore.dll");
            } 
            return null;
        }

        /////////////////////////////////////////////////////////////////////////////
        // Overridden Package Implementation
        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(MyResolveEventHandler);

            _instance = this;
            InitializeDTE();
            Debug.WriteLine (string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
            base.Initialize();

            // Add our command handlers for menu (commands must exist in the .vsct file)
            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if ( null != mcs )
            {
                // Create the command for the tool window
                CommandID toolwndCommandID = new CommandID(GuidList.guidAspectVSPackage2CmdSet, (int)PkgCmdIDList.ShowAspectWindow);
                MenuCommand menuToolWin = new MenuCommand(ShowToolWindow, toolwndCommandID);
                mcs.AddCommand( menuToolWin );
            }
        }
        #endregion

    }
}
