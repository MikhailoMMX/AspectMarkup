using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell.Interop;
using System.ComponentModel;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using System.Windows.Forms;
using AspectCore;

namespace MMX.AspectVSPackage.Helpers
{
    public class AspectPropertiesHelper
    {
        private static SelectionContainer selContainer;

        public static void UpdateSubAspectProperties(PointOfInterest point, TreeNode node, ITrackSelection trackSel)
        {
            VSSubAspectProperties prop = new VSSubAspectProperties(point, node);

            ArrayList listObjects = new ArrayList();
            listObjects.Add(prop);

            selContainer = new SelectionContainer(true, false);
            selContainer.SelectableObjects = listObjects;
            selContainer.SelectedObjects = listObjects;

            trackSel.OnSelectChange((ISelectionContainer)selContainer);
        }

    }

    public class VSSubAspectProperties
    {
        private PointOfInterest _point;
        private TreeNode _node;

        public VSSubAspectProperties(PointOfInterest point, TreeNode node)
        {
            _point = point;
            _node = node;
            
        }

        [Description(Strings.SubAspectPropNameDescr)]
        [Category(Strings.SubAspectPropNameCat)]
        [DisplayName(Strings.SubAspectPropNameName)]
        public string Name
        {
            get { return _point.Title; }
            set { _point.Title = value; _node.Text = value; }
        }

        [Description(Strings.SubAspectPropFileNameDescr)]
        [Category(Strings.SubAspectPropFileNameCat)]
        [DisplayName(Strings.SubAspectPropFileNameName)]
        public string FileName
        {
            get { return _point.FileName; }
            set { _point.FileName = value; }
        }

        [Description(Strings.SubAspectPropValueDescr)]
        [Category(Strings.SubAspectPropValueCat)]
        [DisplayName(Strings.SubAspectPropValueName)]
        public string Value
        {
            get
            {
                if (_point.Context!= null && _point.Context.Count>0)
                    return string.Join(" ", _point.Context[0].Name.ToArray());
                return "";
            }
        }

        [Description(Strings.SubAspectTextDescr)]
        [Category(Strings.SubAspectTextCat)]
        [DisplayName(Strings.SubAspectTextName)]
        public string Text
        {
            get
            {
                if (_point.Context != null && _point.Context.Count > 0)
                    return string.Join(" ", _point.Context[0].Name.ToArray());
                return "";
            }
        }
        [Description(Strings.SubAspectPropContextDepthDescr)]
        [Category(Strings.SubAspectPropContextDepthCat)]
        [DisplayName(Strings.SubAspectPropContextDepthName)]
        public int ContextDepth
        {
            get
            {
                if (_point.Context == null)
                    return 0;
                return _point.Context.Count;
            }
        }
    }

    //[ToolboxItem(false)]
    //[DesignerCategory("")]
    //public class AspectOptionsPage : DialogPage
    //{
    //    static string PathToParsers = "";
    //    public string ParsersDirectory
    //    {
    //        get { return PathToParsers; }
    //        set { PathToParsers = value; }
    //    }
    //}

    //public class AspectOptionsReader
    //{
    //    public static string ReadParsersDirectory()
    //    {
    //        string result = Environment.CurrentDirectory;
    //        if (AspectPackage.instance != null)
    //        {
    //            object obj = AspectPackage.GetAutomationObjectD("Aspect Navigator.General");

    //            AspectOptionsPage options = obj as AspectOptionsPage;
    //            if (options != null && options.ParsersDirectory != "")
    //                result = options.ParsersDirectory;
    //        }
    //        return result;
    //    }
    //}
}
