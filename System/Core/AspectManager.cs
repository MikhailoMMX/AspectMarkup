using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;


namespace AspectCore
{
    public class AspectManager
    {
        /// <summary>
        /// Путь к папке проекта.
        /// </summary>
        public string WorkingDir = "";
        internal bool isFileSaved;

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
        /// <param name="Force"></param>
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
        public PointOfInterest DeserializeAspect(string FileName, ParserWrapper _parserWrapper)
        {
            try
            {
                XmlDocument Doc = new XmlDocument();
                Doc.LoadXml(File.ReadAllText(FileName));
                if (Doc.DocumentElement.Attributes != null && Doc.DocumentElement.Attributes.Count != 0)
                    foreach (XmlAttribute attr in Doc.DocumentElement.Attributes)
                        if (attr.Name == "Version")
                        {
                            if (attr.InnerText == "2")
                                return AspectFileV2Reader.BuildTreeFromXML(Doc.DocumentElement.ChildNodes[0]);
                            if (attr.InnerText == "3")
                                return AspectFileV3Reader.BuildTreeFromXML(Doc.DocumentElement.ChildNodes[0], _parserWrapper);
                        }
                //default = version 1
                return AspectFileV1Reader.BuildTreeFromXML(Doc.DocumentElement);
            }
            catch (Exception)
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
        //protected static 
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

        private static PointOfInterest ConvertXMLNodeToPointOfInterest(XmlNode Node)
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
    internal class AspectFileV3Reader : AspectFileReader
    {
        public static PointOfInterest BuildTreeFromXML(XmlNode Node, ParserWrapper _parserWrapper)
        {
            PointOfInterest Result = new PointOfInterest();
            Result.FileName = TryFindValue(Node, "FileName");
            Result.Name = Node.Attributes["Name"]?.InnerText;
            Result.Note = TryFindValue(Node, "Note");
            Result.Text = TryFindValue(Node, "Text");

            dynamic Lexer = _parserWrapper.GetLexer(Result.FileName ?? "")?.Scanner ?? new CommonLexer.Scanner();

            XmlNode CTX = TryFindNode(Node, "OCtx");
            if (CTX != null)
                Result.Context = TryReadOuterContext(CTX, Lexer);

            XmlNode InnerCTX = TryFindNode(Node, "ICtx");
            if (InnerCTX != null)
                Result.InnerContext = TryReadInnerContext(InnerCTX, Lexer);

            XmlNode Items = TryFindNode(Node, "Items");
            if (Items != null)
            {
                Result.Items = new List<PointOfInterest>();
                foreach (XmlNode subNode in Items)
                    Result.Items.Add(BuildTreeFromXML(subNode, _parserWrapper));
            }
            return Result;
        }
        private static List<string> TokenizeString(string Str, dynamic Lexer)
        {
            List<string> Result = new List<string>();
            if (string.IsNullOrWhiteSpace(Str))
                return Result;

            //Warning - hardcoded constants. May be wrong.
            int EOF = Lexer is CommonLexer.Scanner ? 0 : 3;
            Lexer.SetSource(Str, 0);
            while (Lexer.yylex() != EOF) //0 == EOF
                Result.Add(Lexer.yytext);
            return Result;
        }
        private static List<OuterContextNode> TryReadOuterContext(XmlNode Node, dynamic Lexer)
        {
            List<OuterContextNode> Result = new List<OuterContextNode>();
            foreach (XmlNode subNode in Node)
                Result.Add(TryReadOuterContextNode(subNode, Lexer));
            return Result;
        }
        private static OuterContextNode TryReadOuterContextNode(XmlNode Node, dynamic Lexer)
        {
            OuterContextNode Result = new OuterContextNode();
            Result.Type = Node.Attributes["Type"]?.InnerText;
            Result.Name = TokenizeString(Node.InnerText, Lexer);
            return Result;
        }
        private static List<InnerContextNode> TryReadInnerContext(XmlNode Node, dynamic Lexer)
        {
            List<InnerContextNode> Result = new List<InnerContextNode>();
            foreach (XmlNode subNode in Node.ChildNodes)
                Result.Add(TryReadInnerContextNode(subNode, Lexer));
            return Result;
        }
        private static InnerContextNode TryReadInnerContextNode(XmlNode Node, dynamic Lexer)
        {
            InnerContextNode Result = new InnerContextNode();
            Result.Name = new List<string>();
            Result.Type = Node.Attributes["Type"]?.InnerText;
            Result.Name = TokenizeString(Node.InnerText, Lexer);
            return Result;
        }
    }
    internal class AspectFileBuilder
    {
        public static XmlElement BuildXMLTree(PointOfInterest AspectTree, XmlDocument Doc)
        {
            XmlElement Root = Doc.CreateElement("AspectFile");
            XmlAttribute Ver = Doc.CreateAttribute("Version");
            Ver.InnerText = "3";
            Root.Attributes.Append(Ver);
            Root.AppendChild(BuildPoint(AspectTree, Doc));
            return Root;
        }
        private static XmlNode BuildInnerContext(List<InnerContextNode> IC, XmlDocument Doc)
        {
            XmlElement ICElement = Doc.CreateElement("ICtx");
            foreach (InnerContextNode i in IC)
                ICElement.AppendChild(BuildInnerContextNode(i, Doc));
            return ICElement;
        }
        private static XmlNode BuildInnerContextNode(InnerContextNode IN, XmlDocument Doc)
        {
            XmlElement icNode = Doc.CreateElement("i");
            XmlAttribute icNodeType = Doc.CreateAttribute("Type");
            icNodeType.InnerText = IN.Type;
            icNode.Attributes.Append(icNodeType);
            icNode.InnerText = string.Join(" ", IN.Name);
            return icNode;
        }

        private static XmlNode BuildOuterContext(List<OuterContextNode> OC, XmlDocument Doc)
        {
            XmlElement OCElement = Doc.CreateElement("OCtx");
            foreach (OuterContextNode o in OC)
            {
                XmlElement ocNode = Doc.CreateElement("i");
                XmlAttribute OcNodeType = Doc.CreateAttribute("Type");
                OcNodeType.InnerText = o.Type;
                ocNode.Attributes.Append(OcNodeType);
                ocNode.InnerText = string.Join(" ", o.Name);
                OCElement.AppendChild(ocNode);
            }
            return OCElement;
        }

        private static XmlNode BuildPoint(PointOfInterest Point, XmlDocument Doc)
        {
            XmlElement Result = Doc.CreateElement("Node");
            if (!string.IsNullOrWhiteSpace(Point.Name))
            {
                XmlAttribute Name = Doc.CreateAttribute("Name");
                Result.Attributes.Append(Name);
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
            if (Point.Items != null && Point.Items.Count != 0)
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
