using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspectCore;

namespace BindingTester
{
    internal class Parser
    {
        static Random R = new Random();
        internal ParserWrapper _parsers = new ParserWrapper();
        internal bool IsFileSupported(string FileName)
        {
            FileInfo fi = new FileInfo(FileName);
            return _parsers.GetParserIDs().Contains(fi.Extension.Substring(1));
        }

        private static string GetRelativePath(string FileName, string BaseDirectory)
        {
            if (FileName.StartsWith(BaseDirectory, StringComparison.CurrentCultureIgnoreCase))
                return FileName.Substring(BaseDirectory.Length);
            else
                return FileName;
        }

        private void FillListFromTree(PointOfInterest TreeRoot, ref List<PointOfInterest> result, float Ratio)
        {
            if (TreeRoot == null)
                return;
            if (R.NextDouble() <= Ratio)
            {
                PointOfInterest pt = TreeRoot;//.ClonePointAssignItems();
                pt.ApplyInnerContext();
                pt.Name = string.Join(" ", pt.Context[0]?.Name);
                pt.ApplyInnerContext();
                result.Add(pt);
            }
            foreach (PointOfInterest subnode in TreeRoot.Items)
                FillListFromTree(subnode, ref result, Ratio);
        }

        internal List<PointOfInterest> Parse(string FileName, string BaseDirectory, float Ratio = 1)
        {
            PointOfInterest TreeRoot = _parsers.ParseFile(FileName);
            List<PointOfInterest> result = new List<PointOfInterest>();

            FillListFromTree(TreeRoot, ref result, Ratio);

            string SourceText = File.ReadAllText(FileName, Encoding.Default);
            for (int i=0; i< result.Count; ++i)
            {
                TreeSearchEngine.SetNearLG(TreeRoot, result[i], SourceText, out result[i].NearL, out result[i].NearG);
                result[i].FileName = "\\"+GetRelativePath(result[i].FileName, BaseDirectory);
                result[i] = result[i].ClonePointWithoutItems();
            }
            return result;
        }
    }

    class Program
    {
        //enum ProgramMode { BuildAspectTree, CheckAspectTree }
        //static ProgramMode mode = ProgramMode.BuildAspectTree;

        const string ParamDir = "-dir";
        const string ParamInputFile = "-i";
        const string ParamOutputFile = "-o";
        const string ParamListFile = "-lf";
        const string ParamRatio = "-r";

        public static string BaseDirectory = Environment.CurrentDirectory;
        static string InputFile = "";
        static string OutputFile = "";
        static string ListFile = "";

        static float Ratio = 1;

        static Parser _parser = new Parser();

        /// <summary>
        /// считывает из командной строки рабочую директорию
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static string GetDirectoryFromCommandLine(string[] args)
        {
            for (int i = 0; i < args.Length-1; ++i)
                if (args[i] == ParamDir)
                {
                    string Dir = args[i + 1];
                    if (! Directory.Exists(Dir))
                        throw new Exception("Working dir does not exist");
                    return Dir;
                }
            throw new Exception("Working dir is not specified");
        }

        static string GetFileFromCommandLine(string[] args, string Param)
        {
            for (int i = 0; i < args.Length - 1; ++i)
                if (args[i] == Param)
                {
                    string F = args[i + 1];
                    if (Param == ParamInputFile && !File.Exists(F))
                        throw new Exception("Input file does not exist");
                    if (Param == ParamOutputFile && File.Exists(F))
                        Console.WriteLine("Output file already exists");
                    return F;
                }
            return "";
        }

        /// <summary>
        /// считывает из командной строки процент включаемых узлов
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static float GetRatioFromCommandLine(string[] args)
        {
            for (int i = 0; i < args.Length - 1; ++i)
                if (args[i] == ParamRatio)
                {
                    float r;
                    float.TryParse(args[i + 1], out r);
                    return r;
                }
            return 1;
        }

        /// <summary>
        /// Генерирует выходной файл по заданному списку
        /// </summary>
        /// <param name="FileList"></param>
        /// <param name="BaseDir"></param>
        /// <returns></returns>
        private static PointOfInterest BuildOutputFile(string FileList, string BaseDir)
        {
            string[] Files = File.ReadAllText(FileList).Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            PointOfInterest RootPoint = new PointOfInterest();
            int i = 0;
            foreach (string F in Files)
            {
                string file = BaseDir + F;
                try
                {
                    if (!_parser.IsFileSupported(file))
                        continue;

                    List<PointOfInterest> Points = _parser.Parse(file, BaseDirectory, Ratio);
                    RootPoint.Items.AddRange(Points);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
                i += 1;
                Console.Write("\rFiles processed: " + i + "/"+Files.Length+"    ");
            }
            return RootPoint;
        }

        private static void ProcessInputFile(string BaseDir, string inputFile)
        {
            AspectManager AM = new AspectManager();
            AM.WorkingAspect = AM.DeserializeAspect(inputFile, _parser._parsers);
            int TotalNodes = AM.WorkingAspect.Items.Count;
            int NotFound = 0;
            int Errors = 0;
            int Found = 0;
            int NotChanged = 0;
            TreeManager TM = new TreeManager();
            for (int i = 0; i < AM.WorkingAspect.Items.Count; ++i)
                try
                {
                    PointOfInterest Pt = AM.WorkingAspect.Items[i];
                    PointOfInterest Root = TM.GetTree(BaseDir + Pt.FileName);
                    string text = TM.GetText(BaseDir + Pt.FileName);
                    TreeSearchResult Search = TreeSearchEngine.FindPointInTree2(Root, Pt, text);
                    if (Search.Count == 0)
                        NotFound += 1;
                    else if (Search.Singular)
                    {
                        if (Search.SimilarityOf(0) != 1)
                            Found += 1;
                        else
                            NotChanged += 1;
                        AM.WorkingAspect.Items.RemoveAt(i);
                        i -= 1;
                    }
                    else
                    {
                        if (Search.Count >= 2)
                        {
                            float near = Math.Max(Pt.NearG, Pt.NearL);
                            float threshold = (near + 4) / 5; //hardcoded
                            if (Search.GetNodeSimilarity(0) > threshold && Search.GetNodeSimilarity(1) < threshold)
                            {
                                AM.WorkingAspect.Items.RemoveAt(i);
                                i -= 1;
                                Found += 1;
                            }
                        }
                    }
                    Console.Write("\rNodes left: " + (AM.WorkingAspect.Items.Count - i)+"        ");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Errors += 1;
                    AM.WorkingAspect.Items.RemoveAt(i);
                    i -= 1;
                }
            int Ambiguous = AM.WorkingAspect.Items.Count;
            AM.SerializeAspect(inputFile, true);
            File.WriteAllText(inputFile + ".report.txt", "Total: " + TotalNodes + ", Not changed: " + NotChanged + ", found: " + Found +", Not found: " + NotFound + ", errors: " + Errors + ", ambiguous: " + Ambiguous);
        }

        static void Main(string[] args)
        {
            try
            {
                BaseDirectory = GetDirectoryFromCommandLine(args);
                if (BaseDirectory == "")
                    
                Ratio = GetRatioFromCommandLine(args);
                InputFile = GetFileFromCommandLine(args, ParamInputFile);
                OutputFile = GetFileFromCommandLine(args, ParamOutputFile);
                ListFile = GetFileFromCommandLine(args, ParamListFile);

                if (!string.IsNullOrWhiteSpace(OutputFile))
                {
                    if (!File.Exists(ListFile))
                        throw new Exception("List file doesn not exist");
                    PointOfInterest Tree = BuildOutputFile(ListFile, BaseDirectory);
                    AspectManager AM = new AspectManager();
                    AM.WorkingAspect = Tree;
                    AM.SerializeAspect(OutputFile);
                }
                if (!string.IsNullOrWhiteSpace(InputFile))
                {
                    ProcessInputFile(BaseDirectory, InputFile);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //Console.ReadKey();
            }  
        }
    }
}
