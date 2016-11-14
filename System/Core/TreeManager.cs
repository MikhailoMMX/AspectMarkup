using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AspectCore
{
    public class TreeManager
    {
        ParserWrapperPool parserWrapper;

        ConcurrentDictionary<string, PointOfInterest> FilenameToTree = new ConcurrentDictionary<string, PointOfInterest>();
        ConcurrentDictionary<string, string> FilenameToText = new ConcurrentDictionary<string, string>();
        ConcurrentDictionary<string, DateTime> FilenameToDateTime = new ConcurrentDictionary<string, DateTime>();
        List<PointOfInterest> _LastTimeErrors;
        List<string> parserIDs;

        public TreeManager(string ParsersPath = "")
        {
            //if (ParsersPath == "")
                parserWrapper = new ParserWrapperPool();
            //else
            //parserWrapper = new ParserWrapper(Environment.CurrentDirectory);

            ParserWrapper pw = parserWrapper.GetParserWrapper();
            parserIDs = pw.GetParserIDs();
            parserWrapper.ReleaseParserWrapper(pw);
        }

        public ParserWrapper GetParserWrapper()
        {
            return parserWrapper.GetParserWrapper();
        }
        public void ReleaseParserWrapper(ParserWrapper pw)
        {
            parserWrapper.ReleaseParserWrapper(pw);
        }

        public List<string> GetParserIDs()
        {
            return parserIDs;
        }

        public int GetParsersCount()
        {
            return parserIDs.Count();
        }
        public List<PointOfInterest> GetLastParseErrors()
        {
            return _LastTimeErrors;
        }

        private void Parse(string Filename, string Text, DateTime ModifiedTime)
        {
            ParserWrapper pw = parserWrapper.GetParserWrapper();
            PointOfInterest Point = pw.ParseText(Text, Filename);
            _LastTimeErrors = pw.GetLastParseErrors();
            parserWrapper.ReleaseParserWrapper(pw);
            if (FilenameToTree.ContainsKey(Filename))
            {
                FilenameToText[Filename] = Text;
                FilenameToTree[Filename] = Point;
                FilenameToDateTime[Filename] = ModifiedTime;
            }
            else
            {
                FilenameToText.AddOrUpdate(Filename, Text, (k,v) => v);
                FilenameToTree.AddOrUpdate(Filename, Point, (k, v) => v);
                FilenameToDateTime.AddOrUpdate(Filename, ModifiedTime, (k, v) => v);
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
