using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AspectCore
{
    public class TreeManager
    {
        ParserWrapper parserWrapper;

        Dictionary<string, PointOfInterest> FilenameToTree = new Dictionary<string, PointOfInterest>();
        Dictionary<string, string> FilenameToText = new Dictionary<string, string>();
        Dictionary<string, DateTime> FilenameToDateTime = new Dictionary<string, DateTime>();
        List<PointOfInterest> _LastTimeErrors;

        public TreeManager(string ParsersPath = "")
        {
            if (ParsersPath == "")
                parserWrapper = new ParserWrapper();
            else
                parserWrapper = new ParserWrapper(Environment.CurrentDirectory);
        }

        public ParserWrapper Parsers { get { return parserWrapper; } }

        public List<string> GetParserIDs()
        {
            return parserWrapper.GetParserIDs();
        }

        public int GetParsersCount()
        {
            return parserWrapper.GetParsersCount();
        }
        public List<PointOfInterest> GetLastParseErrors()
        {
            return _LastTimeErrors;
        }

        private void Parse(string Filename, string Text, DateTime ModifiedTime)
        {
            PointOfInterest Point = parserWrapper.ParseText(Text, Filename);
            _LastTimeErrors = parserWrapper.GetLastParseErrors();
            if (FilenameToTree.ContainsKey(Filename))
            {
                FilenameToText[Filename] = Text;
                FilenameToTree[Filename] = Point;
                FilenameToDateTime[Filename] = ModifiedTime;
            }
            else
            {
                FilenameToText.Add(Filename, Text);
                FilenameToTree.Add(Filename, Point);
                FilenameToDateTime.Add(Filename, ModifiedTime);
            }
        }

        public PointOfInterest GetTree(string Filename, string Text)
        {
            if (FilenameToTree.ContainsKey(Filename))
                if (FilenameToText[Filename].Length == Text.Length && FilenameToText[Filename] == Text)
                    return FilenameToTree[Filename];
            Parse(Filename, Text, DateTime.Now);
            return FilenameToTree[Filename];
        }
        public PointOfInterest GetTree(string Filename, bool AlwaysUseCached = false)
        {
            if (FilenameToTree.ContainsKey(Filename))
                if (AlwaysUseCached || File.GetLastWriteTime(Filename) == FilenameToDateTime[Filename])
                    return FilenameToTree[Filename];
            Parse(Filename, File.ReadAllText(Filename, Encoding.Default), File.GetLastWriteTime(Filename));
            return FilenameToTree[Filename];
        }

        /// <summary>
        /// Возвращает текст файла на момент последнего парсинга
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public string GetText(string FileName)
        {
            if (FilenameToText.ContainsKey(FileName))
                //&& (File.GetLastWriteTime(FileName) == FilenameToDateTime[FileName]))
                return FilenameToText[FileName];
            else
                return File.ReadAllText(FileName, Encoding.Default);
        }

    }
}
