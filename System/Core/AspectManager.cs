using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
//using System.Xml.Serialization;
//using System.Runtime.Serialization.Formatters.Soap;

namespace AspectCore
{
    public class AspectManager
    {
        /// <summary>
        /// Путь к папке проекта.
        /// </summary>
        public string WorkingDir = "";
        internal bool isFileSaved = false;

        /// <summary>
        /// Корень текущего аспектного дерева
        /// </summary>
        public PointOfInterest WorkingAspect = new PointOfInterest();

        
        /// <summary>
        /// Преобразование относительного пути в абсолютный в соответствии с текущей директорией проекта
        /// </summary>
        /// <param name="RelativePath">Относительный путь</param>
        /// <returns>Абсолютный путь к файлу</returns>
        public string GetFullFilePath(string RelativePath)
        {
            if (RelativePath.IndexOf(':') >= 0)
                return RelativePath;
            else
                return WorkingDir + RelativePath;
        }

        /// <summary>
        /// Преобразование полного пути к файлу в относительный, если это возможно
        /// </summary>
        /// <param name="FileName">Полное имя файла</param>
        /// <param name="Folder">Папка, относительно которой вычисляется относительный путь</param>
        /// <returns>Относительный путь, если файл находится в папке (или ее подпапках)
        /// Абсолютный путь (исходное значение параметра FileName) в противном случае</returns>
        public string GetRelativePath(string FileName)
        {
            if (FileName.StartsWith(WorkingDir, StringComparison.CurrentCultureIgnoreCase))
                return FileName.Substring(WorkingDir.Length);
            else
                return FileName;
        }
        
        /// <summary>
        /// Сохранение дерева в файл на диске
        /// </summary>
        /// <param name="FileName">Путь к файлу</param>
        public void SerializeAspect(string FileName, bool Force = false)
        {
            SerializeAspect(WorkingAspect, FileName, Force);
            isFileSaved = true;
        }

        public void SerializeAspect(PointOfInterest subAspect, string FileName, bool Force = false)
        {
            if (subAspect.Items.Count == 0 && !File.Exists(FileName) && !Force)
                return;

            string tempFileName = FileName + "~";
            if (File.Exists(tempFileName))
                File.Delete(tempFileName);

            if (File.Exists(FileName))
                File.Move(FileName, tempFileName);

            XmlDocument Doc = new XmlDocument();
            XmlElement Root = AspectFileBuilder.BuildXMLTree(subAspect, Doc);
            Doc.AppendChild(Root);

            var stringWriter = new StringWriter(new StringBuilder());
            var xmlTextWriter = new XmlTextWriter(stringWriter) { Formatting = Formatting.Indented };
            Doc.Save(xmlTextWriter);
            string XMLFile = stringWriter.ToString();
            File.WriteAllText(FileName, XMLFile);
        }

        /// <summary>
        /// Чтение дерева из файла
        /// </summary>
        public PointOfInterest DeserializeAspect(string FileName)
        {
            try
            {
                XmlDocument Doc = new XmlDocument();
                Doc.LoadXml(File.ReadAllText(FileName));
                if (Doc == null)
                    return null;
                if (Doc.DocumentElement.Attributes != null && Doc.DocumentElement.Attributes.Count != 0)
                    foreach (XmlAttribute attr in Doc.DocumentElement.Attributes)
                        if (attr.Name == "Version")
                        {
                            if (attr.InnerText == "2")
                                return AspectFileV2Reader.BuildTreeFromXML(Doc.DocumentElement.ChildNodes[0]);
                            //Next versions here
                        }
                //default = version 1
                return AspectFileV1Reader.BuildTreeFromXML(Doc.DocumentElement);
            }
            catch (Exception e)
            { }
            return null;
        }
    }
    internal class AspectFileReader
    {
        protected static XmlNode TryFindNode(XmlNode Node, string Name)
        {
            foreach (XmlNode subNode in Node.ChildNodes)
                if (subNode.Name == Name)
                    return subNode;
            return null;
        }
        protected static string TryFindValue(XmlNode node, string Name)
        {
            foreach (XmlNode subNode in node.ChildNodes)
                if (subNode.Name == Name)
                    return subNode.InnerText;
            return "";
        }
        protected static List<string> TryReadListOfStrings(XmlNode Node)
        {
            //Node = <ArrayOfStrings>
            List<string> Result = new List<string>();
            foreach (XmlNode subNode in Node.ChildNodes)
                Result.Add(subNode.InnerText);
            return Result;
        }
    }
    internal class AspectFileV1Reader : AspectFileReader
    {
        public static PointOfInterest BuildTreeFromXML(XmlElement Element)
        {
            XmlNode Items = Element.ChildNodes[0].ChildNodes[0];
            List<PointOfInterest> Points = new List<PointOfInterest>();
            foreach (XmlNode node in Items.ChildNodes)
                Points.Add(ConvertXMLNodeToPointOfInterest(node));
            PointOfInterest result = new PointOfInterest();
            result.Items = Points;
            return result;
        }

        private  static PointOfInterest ConvertXMLNodeToPointOfInterest(XmlNode Node)
        {
            PointOfInterest Result = new PointOfInterest();
            Result.FileName = TryFindValue(Node, "FileName");
            Result.Name = TryFindValue(Node, "Name");
            Result.Note = TryFindValue(Node, "Note");
            Result.Text = TryFindValue(Node, "Text");
            
            XmlNode CTX = TryFindNode(Node, "Context");
            if (CTX != null)
            {
                Result.Context = TryReadOuterContext(CTX);
                string Type = TryFindValue(Node, "ParserClassName");
                if (Result.Context != null && Result.Context.Count != 0 && !string.IsNullOrWhiteSpace(Type))
                    Result.Context[0].Type = Type;
            }
            
            XmlNode InnerCTX = TryFindNode(Node, "InnerContext");
            if (InnerCTX != null)
                Result.InnerContext = TryReadInnerContext(InnerCTX);

            XmlNode Items = TryFindNode(Node, "Items");
            if (Items != null)
            {
                Result.Items = new List<PointOfInterest>();
                foreach (XmlNode subNode in Items)
                    Result.Items.Add(ConvertXMLNodeToPointOfInterest(subNode));
            }
            return Result;
        }
        private static List<OuterContextNode> TryReadOuterContext(XmlNode Node)
        {
            List<OuterContextNode> Result = new List<OuterContextNode>();
            foreach (XmlNode subNode in Node)
                Result.Add(new OuterContextNode(TryReadListOfStrings(subNode), ""));

            return Result;
        }
        private static List<InnerContextNode> TryReadInnerContext(XmlNode Node)
        {
            List<InnerContextNode> Result = new List<InnerContextNode>();
            foreach (XmlNode subNode in Node.ChildNodes)
            {
                List<string> name = new List<string>();
                XmlNode NameNode = TryFindNode(subNode, "Name");
                InnerContextNode IC = new InnerContextNode();
                IC.Name = TryReadListOfStrings(NameNode);
                IC.Type = TryFindValue(Node, "Type");
                Result.Add(IC);
            }
            return Result;
        }
    }
    internal class AspectFileV2Reader : AspectFileReader
    {
        public static PointOfInterest BuildTreeFromXML(XmlNode Node)
        {
            PointOfInterest Result = new PointOfInterest();
            Result.FileName = TryFindValue(Node, "FileName");
            Result.Name = TryFindValue(Node, "Name");
            Result.Note = TryFindValue(Node, "Note");
            Result.Text = TryFindValue(Node, "Text");

            XmlNode CTX = TryFindNode(Node, "OuterContext");
            if (CTX != null)
                Result.Context = TryReadOuterContext(CTX);

            XmlNode InnerCTX = TryFindNode(Node, "InnerContext");
            if (InnerCTX != null)
                Result.InnerContext = TryReadInnerContext(InnerCTX);

            XmlNode Items = TryFindNode(Node, "Items");
            if (Items != null)
            {
                Result.Items = new List<PointOfInterest>();
                foreach (XmlNode subNode in Items)
                    Result.Items.Add(BuildTreeFromXML(subNode));
            }
            return Result;
        }
        private static List<OuterContextNode> TryReadOuterContext(XmlNode Node)
        {
            List<OuterContextNode> Result = new List<OuterContextNode>();
            foreach (XmlNode subNode in Node)
                Result.Add(TryReadOuterContextNode(subNode));
            return Result;
        }
        private static OuterContextNode TryReadOuterContextNode(XmlNode Node)
        {
            OuterContextNode Result = new OuterContextNode();
            foreach (XmlAttribute attr in Node.Attributes)
                if (attr.Name == "Type")
                    Result.Type = attr.InnerText;
            foreach (XmlNode subNode in Node.ChildNodes)
                Result.Name.Add(subNode.InnerText);
            return Result;
        }
        private static List<InnerContextNode> TryReadInnerContext(XmlNode Node)
        {
            List<InnerContextNode> Result = new List<InnerContextNode>();
            foreach (XmlNode subNode in Node.ChildNodes)
                Result.Add(TryReadInnerContextNode(subNode));
            return Result;
        }
        private static InnerContextNode TryReadInnerContextNode(XmlNode Node)
        {
            InnerContextNode Result = new InnerContextNode();
            Result.Name = new List<string>();
            foreach (XmlAttribute attr in Node.Attributes)
                if (attr.Name == "Type")
                    Result.Type = attr.InnerText;
            foreach (XmlNode subNode in Node.ChildNodes)
            {
                if (subNode.Name == "string")
                    Result.Name.Add(subNode.InnerText);
                else
                    Result.SubNodes.Add(TryReadInnerContextNode(subNode));
            }
            return Result;
        }
    }
    internal class AspectFileBuilder
    {
        public static XmlElement BuildXMLTree(PointOfInterest AspectTree, XmlDocument Doc)
        {
            XmlElement Root = Doc.CreateElement("AspectFile");
            XmlAttribute Ver = Doc.CreateAttribute("Version");
            Ver.InnerText = "2";
            Root.Attributes.Append(Ver);
            Root.AppendChild(BuildPoint(AspectTree, Doc));
            return Root;
        }
        private static XmlNode BuildInnerContext(List<InnerContextNode> IC, XmlDocument Doc)
        {
            XmlElement ICElement = Doc.CreateElement("InnerContext");
            foreach (InnerContextNode i in IC)
                ICElement.AppendChild(BuildInnerContextNode(i, Doc));
            return ICElement;
        }
        private static XmlNode BuildInnerContextNode(InnerContextNode IN, XmlDocument Doc)
        {
            XmlElement icNode = Doc.CreateElement("InnerContextItem");
            XmlAttribute icNodeType = Doc.CreateAttribute("Type");
            icNodeType.InnerText = IN.Type;
            icNode.Attributes.Append(icNodeType);
            foreach (string str in IN.Name)
            {
                XmlElement s = Doc.CreateElement("string");
                s.InnerText = str;
                icNode.AppendChild(s);
            }
            if (IN.SubNodes != null)
                foreach (InnerContextNode IN2 in IN.SubNodes)
                    icNode.AppendChild(BuildInnerContextNode(IN2, Doc));
            return icNode;
        }

        private static XmlNode BuildOuterContext(List<OuterContextNode> OC, XmlDocument Doc)
        {
            XmlElement OCElement = Doc.CreateElement("OuterContext");
            foreach (OuterContextNode o in OC)
            {
                XmlElement ocNode = Doc.CreateElement("OuterContextItem");
                XmlAttribute OcNodeType = Doc.CreateAttribute("Type");
                OcNodeType.InnerText = o.Type;
                ocNode.Attributes.Append(OcNodeType);
                foreach (string str in o.Name)
                {
                    XmlElement s = Doc.CreateElement("string");
                    s.InnerText = str;
                    ocNode.AppendChild(s);
                }
                OCElement.AppendChild(ocNode);
            }
            return OCElement;
        }

        private static XmlNode BuildPoint(PointOfInterest Point, XmlDocument Doc)
        {
            XmlElement Result = Doc.CreateElement("PointOfInterest");
            if (!string.IsNullOrWhiteSpace(Point.Name))
            {
                XmlElement Name = Doc.CreateElement("Name");
                Result.AppendChild(Name);
                Name.InnerText = Point.Name;
            }
            if (!string.IsNullOrWhiteSpace(Point.FileName))
            {
                XmlElement FileName = Doc.CreateElement("FileName");
                FileName.InnerText = Point.FileName;
                Result.AppendChild(FileName);
            }
            if (!string.IsNullOrWhiteSpace(Point.Note))
            {
                XmlElement Note = Doc.CreateElement("Note");
                Note.InnerText = Point.Note;
                Result.AppendChild(Note);
            }
            if (!string.IsNullOrWhiteSpace(Point.Text))
            {
                XmlElement Text = Doc.CreateElement("Text");
                Text.InnerText = Point.Text;
                Result.AppendChild(Text);
            }
            
            if (Point.Context != null && Point.Context.Count != 0)
                Result.AppendChild(BuildOuterContext(Point.Context, Doc));
            if (Point.InnerContext != null && Point.InnerContext.Count != 0)
                Result.AppendChild(BuildInnerContext(Point.InnerContext, Doc));
            if (Point.Items != null)
            {
                XmlElement Items = Doc.CreateElement("Items");
                foreach (PointOfInterest p in Point.Items)
                    Items.AppendChild(BuildPoint(p, Doc));
                Result.AppendChild(Items);
            }
            return Result;
        }
    }
}
