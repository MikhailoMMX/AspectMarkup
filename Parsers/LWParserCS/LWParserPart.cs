using System.Collections.Generic;
using System.Globalization;
using System.Text;
using QUT.Gppg;
using AspectCore;

namespace LWParser
{
    public partial class Parser : ShiftReduceParser<ValueType, LexLocation>
    {
        public List<PointOfInterest> CommentNodes = new List<PointOfInterest>();
        public SourceEntity root;
        public Parser(Scanner scanner) : base(scanner) { }
    }

    public class LightweightParser : ILightweightParser
    {
        Parser _parser;
        public LightweightParser(Scanner scanner)
        {
            _parser = new Parser(scanner);
        }
        public bool Parse()
        {
            return _parser.Parse();
        }
        public SourceEntity Root
        { 
            get { return _parser.root; }
        }
        public List<PointOfInterest> CommentNodes
        {
            get { return _parser.CommentNodes; } 
        }
        public string LanguageID { get { return "cs"; } }
    }
}