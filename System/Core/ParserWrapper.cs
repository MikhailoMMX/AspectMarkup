using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using Microsoft.Win32;

namespace AspectCore
{
    public class ParserWrapper
    {
        private static string ScannerClassName = "LightweightScanner";
        private static string ParserClassName = "LightweightParser";

        private Dictionary<string, LightweightParserBase> parsers = new Dictionary<string, LightweightParserBase>();
        private Dictionary<string, ILightWeightScanner> scanners = new Dictionary<string, ILightWeightScanner>();
        private LightweightParserBase DefaultParser;
        private ILightWeightScanner DefaultScanner;
        private List<PointOfInterest> _LastTimeErrors = new List<PointOfInterest>();

        /// <summary>
        /// Конструктор без параметров загружает парсеры из текущей директории
        /// </summary>
        public ParserWrapper()
        {
            ReloadParsers();
        }
		
        /// <summary>
        /// Конструктор загружает парсеры из указанной директории
        /// </summary>
        /// <param name="Path">Путь к папке, содержащей сборки с парсерами</param>
        public ParserWrapper(string Path)
        {
            LoadParsers(Path);
        }

        /// <summary>
        /// Перезагрузка парсеров
        /// </summary>
        public void ReloadParsers()
        {
            RegistryKey rk = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("Software\\AspectCore");
            string path = "";
            if (rk != null)
                path = (string)rk.GetValue("Install_Dir");

            scanners.Clear();
            parsers.Clear();
            if (path == "")
                LoadParsersFromGAC();
            else
                LoadParsers(path+"\\Parsers");
        }

        public void LoadParsersFromGAC()
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            string gacPath = Environment.GetFolderPath(Environment.SpecialFolder.Windows) + "\\Microsoft.NET\\assembly";

            DirectoryInfo di = new DirectoryInfo(gacPath);
            FileInfo [] files = di.GetFiles(GlobalData.ParserAssemblyMask, SearchOption.AllDirectories);
            sw.Stop();
            GlobalData.traceAction("Поиск сборок с парсерами: " + sw.ElapsedMilliseconds + " мс");
            sw.Reset();
            sw.Start();
            foreach (FileInfo fi in files)
                LoadParserFromFile(fi.FullName);
            GlobalData.traceAction("Загрузка парсеров: " + sw.ElapsedMilliseconds + " мс");
        }

        public bool LoadParserFromFile(string path, bool ReplaceExisting = false)
        {
            try
            {
                GlobalData.traceAction("Чтение файла " + path);
                byte[] AsmBytes = File.ReadAllBytes(path);
                //Assembly asm = Assembly.LoadFrom(path);
                Assembly asm = Assembly.Load(File.ReadAllBytes(path));
                ILightWeightScanner scanner = null;
                LightweightParserBase parser = null;
                // найти и создать сканер
                foreach (Type T in asm.GetTypes())
                    try
                    {
                        if (T.IsClass && T.IsPublic && T.FullName.IndexOf(ScannerClassName) >= 0)
                        {
                            scanner = (ILightWeightScanner)Activator.CreateInstance(T);
                            break;
                        }
                    }
                    catch (Exception)
                    { }
                if (scanner == null)
                {
                    GlobalData.traceAction("Сборка не содержит парсера");
                    return false;
                }
                // найти и создать парсер
                foreach (Type T in asm.GetTypes())
                    try
                    {
                        if (T.IsClass && T.IsPublic && T.FullName.IndexOf(ParserClassName) >= 0)
                        {
                            object[] parametersArray = new object[] { scanner.Scanner };
                            parser = (LightweightParserBase)Activator.CreateInstance(T, parametersArray);
                            break;
                        }
                    }
                    catch (Exception)
                    { }

                // добавить парсер и сканер в словари
                if (parser != null)
                    foreach (string id in parser.LanguageID)
                        if (id == "*")
                            if (DefaultParser == null || ReplaceExisting)
                            {
                                DefaultScanner = scanner;
                                DefaultParser = parser;
                                GlobalData.traceAction("Загружен парсер по-умолчанию");
                                return true;
                            }
                            else
                                GlobalData.traceAction("Парсер по-умолчанию был загружен ранее, пропускаем");
                        else 
                            if (!parsers.ContainsKey(id))
                            {
                                scanners.Add(id, scanner);
                                parsers.Add(id, parser);
                                GlobalData.traceAction("Загружен парсер для файлов " + id);
                            }
                            else
                                if (ReplaceExisting)
                                {
                                    scanners[id] = scanner;
                                    parsers[id] = parser;
                                    GlobalData.traceAction("Перезагружен парсер для файлов " + id);
                                }
                                else
                                    GlobalData.traceAction("Парсер для файлов " + id + " был загружен ранее, пропускаем");
                else
                    GlobalData.traceAction("Сборка не содержит парсера");
            }
            catch (Exception e)
            {
                GlobalData.traceAction("ИСКЛЮЧЕНИЕ: " + e);
            }
            return false;
        }

        /// <summary>
        /// Загрузка парсеров из указанной директории
        /// </summary>
        /// <param name="path">Путь к директории, содержащей сборки с парсерами</param>
        public void LoadParsers(string path)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(path);
                if (!di.Exists)
                    GlobalData.traceAction("Сбой при загрузке парсеров. Папка не существует: " + di.FullName);
                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                sw.Start();
                FileInfo[] dllFiles = di.GetFiles("*.dll");
                foreach (FileInfo fi in dllFiles)
                    LoadParserFromFile(fi.FullName);
                sw.Stop();
                GlobalData.traceAction("Загрузка парсеров: " + sw.ElapsedMilliseconds + " мс");
            }
            catch (Exception e)
            {
                GlobalData.traceAction("ИСКЛЮЧЕНИЕ: " + e);
            }
        }

        /// <summary>
        /// Преобразование имени файла в идентификатор языка, использующийся в словарях в качестве ключа
        /// </summary>
        /// <param name="filename">Имя файла с расширением</param>
        /// <returns>Идентификатор языка</returns>
        private string FilenameToLangID(string filename)
        {
            if (filename == null)
                return "";
            int ch = filename.LastIndexOf('.') + 1;
            return filename.Substring(ch, filename.Length - ch);
        }

        public List<PointOfInterest> GetLastParseErrors()
        {
            return _LastTimeErrors;
        }

        /// <summary>
        /// Парсинг файла
        /// </summary>
        /// <param name="FileName">Путь к файлу</param>
        /// <returns>Корень дерева</returns>
        public PointOfInterest ParseFile(string FileName)
        {
            string text = File.ReadAllText(FileName, Encoding.Default);
            PointOfInterest Result = ParseText(text, FileName);
            return Result;
        }
        /// <summary>
        /// Парсинг текста.
        /// Этой функцией можно вызывать парсинг открытого существующего файла, если он изменен и еще не сохранен, получив его обновленное содержимое из среды разработки.
        /// </summary>
        /// <param name="Text">Текст</param>
        /// <param name="FileName">Имя файла. Должно содержать как минимум расширение для корректного выбора парсера.
        /// Имя файла будет сохранено в узлах аспектного дерева</param>
        /// <returns>Корень дерева</returns>
        public PointOfInterest ParseText(string Text, string FileName)
        {
            string LangID = FilenameToLangID(FileName);
            ILightWeightScanner Scanner;
            LightweightParserBase Parser;
            if (scanners.ContainsKey(LangID) && parsers.ContainsKey(LangID))
            {
                Scanner = scanners[LangID];
                Parser = parsers[LangID];
            }
            else if (DefaultParser != null && DefaultScanner != null)
            {
                Scanner = DefaultScanner;
                Parser = DefaultParser;
            }
            else
            {
                GlobalData.traceAction("Нет парсера для файлов " + LangID);
                return null;
            }
            Scanner.SetSource(Text);
            Parser.Parse();
            SourceEntity result = Parser.Root;
            if (result == null)
                return null;
            PointOfInterest rootPt = ConvertSourceEntityToPointOfInterest(result, FileName);
            SetContext(rootPt);
            _LastTimeErrors.Clear();
            foreach (SourceEntity se in Parser.Errors)
                _LastTimeErrors.Add(ConvertSourceEntityToPointOfInterest(se, FileName));
            return rootPt;
        }

        /// <summary>
        /// Преобразование дерева из парсера в рабочее дерево 
        /// </summary>
        /// <param name="s">Корень аспектного дерева</param>
        /// <param name="FileName">относительный путь к файлу, содержащему данный подаспект</param>
        private PointOfInterest ConvertSourceEntityToPointOfInterest(SourceEntity s, string FileName)
        {
            if (s == null)
                return null;
            PointOfInterest res = new PointOfInterest(s.Location);
            res.Context.Add(new OuterContextNode(s.Value, s.GetType().Name));
            if (s.Value != null && s.Value.Count != 0)
                res.Title = string.Join(" ", s.Value);
            res.FileName = FileName;
            res.ID = s.Name;
            foreach (SourceEntity se in s.Items)
                res.Items.Add(ConvertSourceEntityToPointOfInterest(se, FileName));
            return res;
        }

        /// <summary>
        /// Поиск точки в дереве по координатам
        /// </summary>
        /// <param name="p">Корень дерева, в котором нужно найти точку</param>
        /// <param name="line">Номер строки в исходном файле</param>
        /// <param name="col">Столбец в исходном файле</param>
        /// <returns>Самая вложенная точка, содержащая указанные координаты или null</returns>

        private void SetContext(PointOfInterest root)
        {
            foreach (PointOfInterest pt in root.Items)
            {
                pt.Context.AddRange(root.Context);
                SetContext(pt);
            }
        }

        public int GetParsersCount()
        {
            return parsers.Count + (DefaultParser != null? 1 : 0);
        }

        public List<string> GetParserIDs()
        {
            List<string> result = new List<string>();
            foreach (string str in parsers.Keys)
                result.Add(str);
            if (DefaultParser != null)
                result.Add("*");
            return result;
        }

        internal ILightWeightScanner GetLexer(string FileName)
        {
            string ID = FilenameToLangID(FileName);
            if (scanners.ContainsKey(ID))
                return scanners[ID];
            else
                return null;
        }

    }
}
