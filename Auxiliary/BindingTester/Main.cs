using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using AspectCore;

namespace BindingTester
{
    internal class Parser
    {
        static Random R = new Random();
        internal ParserWrapperPool _pool;
        private List<string> supportedExtensions;
        public Parser()
        {
            _pool = new ParserWrapperPool();
            ParserWrapper pw = _pool.GetParserWrapper();
            supportedExtensions = pw.GetParserIDs();
            _pool.ReleaseParserWrapper(pw);
        }

        internal bool IsFileSupported(string FileName)
        {
            FileInfo fi = new FileInfo(FileName);
            return supportedExtensions.Contains(fi.Extension.Substring(1));
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
                pt.Title = string.Join(" ", pt.Context[0]?.Name);
                pt.ApplyInnerContext();
                result.Add(pt);
            }
            foreach (PointOfInterest subnode in TreeRoot.Items)
                FillListFromTree(subnode, ref result, Ratio);
        }

        internal PointOfInterest Parse(string FileName)
        {
            ParserWrapper pw = _pool.GetParserWrapper();
            PointOfInterest TreeRoot = null;
            try
            {
                TreeRoot = pw.ParseFile(FileName);
            }
            catch (Exception e)
            {
                Console.WriteLine(FileName + " " + e.Message);
            }
            _pool.ReleaseParserWrapper(pw);
            return TreeRoot;
        }
        internal List<PointOfInterest> ParseAndBuildList(string FileName, string BaseDirectory, float Ratio = 1)
        {
            List<PointOfInterest> result = new List<PointOfInterest>();
            try
            {
                PointOfInterest TreeRoot = Parse(FileName);
                foreach (PointOfInterest pt in TreeRoot.Items)
                    FillListFromTree(pt, ref result, Ratio);

                string SourceText = File.ReadAllText(FileName, Encoding.Default);
                Parallel.For(0, result.Count, (i) =>
                //for (int i=0; i< result.Count; ++i)
                {
                    TreeSearchEngine.SetNearLG(TreeRoot, result[i], SourceText, out result[i].NearL, out result[i].NearG);
                    result[i].FileName = "\\" + GetRelativePath(result[i].FileName, BaseDirectory);
                    result[i] = result[i].ClonePointWithoutItems();
                }
                );
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }
        internal int GetNodesCount(PointOfInterest treeRoot)
        {
            if (treeRoot == null)
                return 0;
            int result = 1;
            if (treeRoot.Items == null || treeRoot.Items.Count == 0)
                return result;
            foreach (PointOfInterest pt in treeRoot.Items)
                result += GetNodesCount(pt);
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
        const string ParamEnumerateOnly = "-enum";
        const string ParamSubset = "-subset";
        const string ParamFilter = "-filter";

        public static string BaseDirectory = Environment.CurrentDirectory;
        static string InputFile = "";
        static string OutputFile = "";
        static string ListFile = "";
        static string Ext = "";
        static string Filter = "";
        static string SubSet = "";

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
            Semaphore S = new Semaphore(1, 1);
            Parallel.ForEach(Files, (F) =>
            //foreach (string F in Files)
            {
                string file = BaseDir + F;
                try
                {
                    if (!_parser.IsFileSupported(file))
                        //continue;
                        return;

                    List<PointOfInterest> Points = _parser.ParseAndBuildList(file, BaseDirectory, Ratio);
                    S.WaitOne();
                    RootPoint.Items.AddRange(Points);
                    S.Release();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
                S.WaitOne();
                i += 1;
                Console.Write("\rFiles processed: " + i + "/" + Files.Length);
                S.Release();
            }
            );
            Console.WriteLine();
            return RootPoint;
        }

        private static void ProcessInputFile(string BaseDir, string inputFile)
        {
            AspectManager AM = new AspectManager();
            ParserWrapper pw = _parser._pool.GetParserWrapper();
            AM.WorkingAspect = AM.DeserializeAspect(inputFile, pw);
            _parser._pool.ReleaseParserWrapper(pw);
            int TotalNodes = AM.WorkingAspect.Items.Count;
            int NotFound = 0;
            int Errors = 0;
            int Found = 0;
            int NotChanged = 0;
            TreeManager TM = new TreeManager();
            PointOfInterest NewAspect = new PointOfInterest();
            Semaphore S = new Semaphore(1, 1);
            int counter = 0;
            Parallel.For(0, AM.WorkingAspect.Items.Count, (i) =>
            //for (int i = 0; i < AM.WorkingAspect.Items.Count; ++i)
            {
                Interlocked.Increment(ref counter);
                if (counter % 10 == 0)
                {
                    S.WaitOne();
                    Console.Write("\rNodes left: " + (AM.WorkingAspect.Items.Count - counter) + "        ");
                    S.Release();
                }
                try
                {
                    PointOfInterest Pt = AM.WorkingAspect.Items[i];
                    PointOfInterest Root = TM.GetTree(BaseDir + Pt.FileName);
                    string text = TM.GetText(BaseDir + Pt.FileName);
                    TreeSearchResult Search = TreeSearchEngine.FindPointInTree2(Root, Pt, text);
                    if (Search.Count == 0)
                        Interlocked.Increment(ref NotFound);
                    else if (Search.Singular)
                    {
                        if (Search.GetNodeSimilarity(0) == 1)
                        {
                            Interlocked.Increment(ref NotChanged);
                            return;
                            //continue;
                        }
                        else
                        {
                            Interlocked.Increment(ref Found);
                            AM.WorkingAspect.Items[i].Title = "+ " + AM.WorkingAspect.Items[i].Title;
                        }
                    }
                    else
                    {
                        if (Search.Count >= 2)
                        {
                            float d1 = 1 - Search.GetNodeSimilarity(0);
                            float d2 = 1 - Search.GetNodeSimilarity(1);
                            //float near = Math.Max(Pt.NearG, Pt.NearL);
                            //float threshold = ((near + 4) / 5 + 1) /2 ; //hardcoded
                            //if (Search.GetNodeSimilarity(0) >= threshold && Search.GetNodeSimilarity(1) < threshold)
                            if (d2 != 0 && d2>= d1*2)
                            {
                                Interlocked.Increment(ref Found);
                                AM.WorkingAspect.Items[i].Title = "+ " + AM.WorkingAspect.Items[i].Title;
                                //return;
                                //continue;
                            }
                        }
                    }
                    S.WaitOne();
                    NewAspect.Items.Add(AM.WorkingAspect.Items[i]);
                    S.Release();
                }
                catch (Exception e)
                {
                    S.WaitOne();
                    Console.WriteLine(e.Message);
                    Errors += 1;
                    S.Release();
                }
            }
            );
            AM.WorkingAspect = NewAspect;
            int Ambiguous = AM.WorkingAspect.Items.Count - Found;
            AM.SerializeAspect(inputFile, true);
            File.WriteAllText(inputFile + ".report.txt", "Total: " + TotalNodes + ", Not changed: " + NotChanged + ", found: " + Found +", Not found: " + NotFound + ", errors: " + Errors + ", ambiguous: " + Ambiguous);
            Console.Write("\rNodes left: 0        ");
            Console.WriteLine();
        }

        private static int EnumerateFiles(string baseDirectory, string ext, string ListFile)
        {
            string[] Files;
            if (string.IsNullOrWhiteSpace(ListFile))
                Files = Directory.GetFiles(baseDirectory, "*." + ext, SearchOption.AllDirectories);
            else
            {
                string[] t = File.ReadAllText(ListFile).Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                Files = new string[t.Length];
                for (int j = 0; j < t.Length; ++j)
                    Files[j] = baseDirectory + t[j];
            }
            int Count = 0;
            Semaphore S = new Semaphore(1, 1);
            int i = 0;
            //foreach (string F in Files)
            Parallel.ForEach(Files, (F) =>
            {
                int c = 0;
                try
                {
                    c = _parser.GetNodesCount(_parser.Parse(F));
                    //if (!_parser.IsFileSupported(F))
                    //    //continue;
                    //    return;

                    //Interlocked.Add(ref Count, _parser.GetNodesCount(_parser.Parse(F)));
                }
                catch (Exception e)
                {
                    Console.WriteLine(F+" Exception: " + e.Message);
                }
                S.WaitOne();
                Count += c;
                i += 1;
                Console.Write("\rFiles processed: " + i + "/" + Files.Length);
                S.Release();
            }
            );
            Console.WriteLine();
            return Count;
        }

        static void Main(string[] args)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            try
            {
                BaseDirectory = GetDirectoryFromCommandLine(args);
                //if (BaseDirectory == "")
                    
                Ratio = GetRatioFromCommandLine(args);
                InputFile = GetFileFromCommandLine(args, ParamInputFile);
                OutputFile = GetFileFromCommandLine(args, ParamOutputFile);
                ListFile = GetFileFromCommandLine(args, ParamListFile);
                Ext = GetFileFromCommandLine(args, ParamEnumerateOnly);
                SubSet = GetFileFromCommandLine(args, ParamSubset);

                if (!string.IsNullOrWhiteSpace(Ext))
                {
                    int count = EnumerateFiles(BaseDirectory, Ext, ListFile);
                    Console.WriteLine("Total nodes: " + count);
                    return;
                }
                if (!string.IsNullOrWhiteSpace(SubSet))
                {
                    bool filter = args.Contains(ParamFilter);
                    int subSetNum;
                    if (!int.TryParse(SubSet, out subSetNum))
                        throw new Exception("Subset size not specified");
                    else
                    {
                        if (string.IsNullOrWhiteSpace(InputFile) || !File.Exists(InputFile))
                            throw new Exception("Input file doesn not exist");
                        TakeSubsetFromFile(InputFile, subSetNum, filter);
                    }
                    Console.ReadKey();
                    return;
                }
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
            sw.Stop();
            Console.WriteLine("Finished in " + sw.ElapsedMilliseconds/1000 + " seconds");
        }

        private static void TakeSubsetFromFile(string inputFile, int size, bool filter = false)
        {
            AspectManager AM = new AspectManager();
            ParserWrapper pw = _parser._pool.GetParserWrapper();
            AM.WorkingAspect = AM.DeserializeAspect(inputFile, pw);
            System.Console.WriteLine("Reading finished");

            List<PointOfInterest> points = AM.WorkingAspect.Items;

            if (filter)
            {
                int AmbigRemoved = 0;
                int AmbigRemains = 0;
                int GoodRemoved = 0;
                int GoodRemains = 0;
                for (int i = 0; i < points.Count; ++i)
                {
                    if (points[i]?.Title?.StartsWith("+ ") ?? false)
                    {
                        if (points[i].Title.StartsWith("+ using"))
                        {
                            points.RemoveAt(i);
                            i -= 1;
                            GoodRemoved += 1;
                        }
                        else
                            GoodRemains += 1;
                    }
                    else
                    {
                        if (points[i]?.Title?.StartsWith("using") ?? true)
                        {
                            points.RemoveAt(i);
                            i -= 1;
                            AmbigRemoved += 1;
                        }
                        else
                            AmbigRemains += 1;
                    }
                }
                Console.WriteLine("Removed: " + GoodRemoved + " good, " + AmbigRemoved + " ambiguous results");
                Console.WriteLine("Remains: " + GoodRemains + " good, " + AmbigRemains + " ambiguous results");
            }

            if (points.Count > size)
            {
                Shuffle(points);
                points.RemoveRange(size, points.Count - size);
            }

            AM.SerializeAspect(inputFile, true);
            System.Console.WriteLine("Writing finished");
            _parser._pool.ReleaseParserWrapper(pw);
        }

        private static void Shuffle<T>(IList<T> list)
        {
            Random rnd = new Random();
            for (var i = 0; i < list.Count; i++)
            {
                int j = rnd.Next(i, list.Count);
                var temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }
    }
}
