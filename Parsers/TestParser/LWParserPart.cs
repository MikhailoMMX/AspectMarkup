using System.Collections.Generic;
using AspectCore;
using QUT.Gppg;

namespace LWParser
{
    public class TXT_TreeNode : SourceEntity
    {
        public TXT_TreeNode(List<string> Value, LexLocation Location)
        {
            this.Value = Value;
            this.Location = Location;
        }
        public TXT_TreeNode(string Value, LexLocation Location)
        {
            this.Value = new List<string>();
            this.Value.Add(Value);
            this.Location = Location;
        }
        public TXT_TreeNode()
        {
            this.Value = new List<string>();
            this.Location = new LexLocation();
        }
        public virtual void Accept(TXT_Visitor v)
        {
            v.Visit(this);
        }
    }
    public class Text : TXT_TreeNode
    {
        public Text(List<string> Value, LexLocation Location)
        {
            this.Value = Value;
            this.Location = Location;
        }
        public Text(string Value, LexLocation Location)
        {
            this.Value = new List<string>();
            this.Value.Add(Value);
            this.Location = Location;
        }
        public Text()
        {
            this.Value = new List<string>();
            this.Location = new LexLocation();
        }
        public override void Accept(TXT_Visitor v)
        {
            v.Visit(this);
        }
    }
    public class Header : TXT_TreeNode
    {
        public Header(List<string> Value, LexLocation Location)
        {
            this.Value = Value;
            this.Location = Location;
        }
        public Header(string Value, LexLocation Location)
        {
            this.Value = new List<string>();
            this.Value.Add(Value);
            this.Location = Location;
        }
        public Header()
        {
            this.Value = new List<string>();
            this.Location = new LexLocation();
        }
        public override void Accept(TXT_Visitor v)
        {
            v.Visit(this);
        }
    }
    public class Token : TXT_TreeNode
    {
        public Token(List<string> Value, LexLocation Location)
        {
            this.Value = Value;
            this.Location = Location;
        }
        public Token(string Value, LexLocation Location)
        {
            this.Value = new List<string>();
            this.Value.Add(Value);
            this.Location = Location;
        }
        public Token()
        {
            this.Value = new List<string>();
            this.Location = new LexLocation();
        }
        public override void Accept(TXT_Visitor v)
        {
            v.Visit(this);
        }
    }
    public interface TXT_Visitor
    {
       void Visit(TXT_TreeNode _TXT_TreeNode);
       void Visit(Text _Text);
       void Visit(Header _Header);
       void Visit(Token _Token);

    }
  public sealed partial class Scanner : ScanBase
	{
        private bool IsRegionStart(string str)
        { return false; }
        private bool IsRegionEnd(string str)
        { return false; }
        private bool IsDirectiveDefine(string str)
        { return false; }
        private bool IsDirectiveUndef(string str)
        { return false; }
        private bool IsDirectiveIfdef(string str)
        { return false; }
        private bool IsDirectiveIfndef(string str)
        { return false; }
        private bool IsDirectiveElse(string str)
        { return false; }
        private bool IsDirectiveElif(string str)
        { return false; }
        private bool IsDirectiveEnd(string str)
        { return false; }
    }
    public partial class LightweightScanner : ILightWeightScanner
    {
        public string[] LanguageID { get { return new string[] { "txt" }; } }
    }
    public partial class LightweightParser : LightweightParserBase
    {
        public override string[] LanguageID { get { return new string[] { "txt" }; } }
    }


    public sealed partial class Scanner : ScanBase
    {
        internal string Text;
        private int OpenBraceCount = 0;
        private Stack<int> OpenBraceStack = new Stack<int>();

        internal int LineAdd = 0;
        private Stack<string> nextTokens = new Stack<string>();
        public void AddNextToken(string str)
        {
            nextTokens.Push(str);
        }
        public void RemoveNextToken()
        {
            if (nextTokens.Count > 0)
                nextTokens.Pop();
        }

        public string errorMsg;
        public LexLocation errorLoc;

        public override void yyerror(string format, params object[] args)
        {
            errorMsg = string.Format(format, args);
            errorLoc = yylloc;
        }

        public void GoToSkipState(int state)
        {
            OpenBraceStack.Push(OpenBraceCount);
            OpenBraceCount = 1;
            yy_push_state(state);
        }
        public void GoToSkipDirectiveState(int state)
        {
            OpenBraceStack.Push(OpenBraceCount);
            OpenBraceCount = 1;
            int tmp = yy_top_state();
            yy_pop_state();
            yy_push_state(state);
            yy_push_state(tmp);
        }
        public void ReturnToLastState()
        {
            OpenBraceCount = OpenBraceStack.Pop();
            yy_pop_state();
        }

        public void ReturnFromSkipDirective()
        {
            ReturnToLastState();
        }

        /// <summary>
        /// стек состояний на момент начала директивы #if
        /// находимся ли мы во вложенной директиве или это директива "верхнего уровня"
        /// </summary>
        private Stack<bool> SkipStateStack = new Stack<bool>();
        /// <summary>
        /// стек флагов, был ли уже внедрен фрагмент в текущей цепочке if [elif]*
        /// </summary>
        private Stack<bool> EmbedFlagStack = new Stack<bool>();
        private HashSet<string> DefinedNames = new HashSet<string>();

        private void DivideDirective(string input, out string directive, out string param)
        {
            int space = input.IndexOf(' ');
            if (space == -1)
            {
                directive = input;
                param = "";
            }
            else
            {
                directive = input.Substring(0, space);
                param = input.Substring(space + 1);
            }
        }

        private void ProcessDirectiveInNonSkipState(string DirectiveText)
        {
            string directive;
            string param;
            DivideDirective(DirectiveText, out directive, out param);
            directive = directive.ToLower();
            if (IsDirectiveIfdef(directive))
            {
                SkipStateStack.Push(false);
                if (DefinedNames.Contains(param))
                    EmbedFlagStack.Push(true);
                else
                {
                    EmbedFlagStack.Push(false);
                    GoToSkipDirectiveState(SKIPDIRECTIVE);
                }
            }
            if (IsDirectiveIfndef(directive))
            {
                SkipStateStack.Push(false);
                if (!DefinedNames.Contains(param))
                    EmbedFlagStack.Push(true);
                else
                {
                    EmbedFlagStack.Push(false);
                    GoToSkipDirectiveState(SKIPDIRECTIVE);
                }
            }
            else if (IsDirectiveElse(directive) || IsDirectiveElif(directive))
            {
                if (EmbedFlagStack.Count == 0)
                    ;// TODO ошибка
                else
                    GoToSkipDirectiveState(SKIPDIRECTIVE);
            }
            else if (IsDirectiveEnd(directive))
            {
                if (EmbedFlagStack.Count == 0)
                    ;// TODO ошибка
                else
                {
                    EmbedFlagStack.Pop();
                    SkipStateStack.Pop();
                }
            }
            else if (IsDirectiveDefine(directive))
                DefinedNames.Add(param);
            else if (IsDirectiveUndef(directive))
                DefinedNames.Remove(param);
        }

        private void ProcessDirectiveInSkipState(string DirectiveText)
        {
            string directive;
            string param;
            DivideDirective(DirectiveText, out directive, out param);

            if (IsDirectiveIfdef(directive) || IsDirectiveIfndef(directive))
            {
                SkipStateStack.Push(true);
            }
            else if (IsDirectiveElse(directive))
            {
                if (!SkipStateStack.Peek())
                    if (EmbedFlagStack.Peek())
                        ;// TODO ошибка - уже вставляли секцию кода, а теперь надо еще раз
                    else
                        ReturnFromSkipDirective();
            }
            else if (IsDirectiveElif(directive))
            {
                if (!SkipStateStack.Peek())
                    if (DefinedNames.Contains(param))
                        if (EmbedFlagStack.Peek())
                            ;// TODO ошибка - уже вставляли секцию кода, а теперь надо еще раз
                        else
                            ReturnFromSkipDirective();
            }
            else if (IsDirectiveEnd(directive))
            {
                if (!SkipStateStack.Peek())
                {
                    ReturnFromSkipDirective();
                    EmbedFlagStack.Pop();
                }
                SkipStateStack.Pop();
            }
        }

    }
    public partial class LightweightScanner : ILightWeightScanner
    {
        Scanner _scanner;
        public object Scanner { get { return _scanner; } }
        public LightweightScanner()
        {
            _scanner = new Scanner();
        }
        public void SetSource(string source)
        {
            _scanner.Text = source;
            _scanner.LineAdd = 0;
            _scanner.ResetYYLLoc();
            _scanner.SetSource(source, 0);
        }
    }
	
	public partial class Parser : ShiftReduceParser<ValueType, LexLocation>
    {
        public SourceEntity root;
        public Parser(Scanner scanner) : base(scanner) { }
    }

    public partial class LightweightParser : LightweightParserBase
    {
        Parser _parser;
        Scanner _scanner;
        public LightweightParser(Scanner scanner)
        {
            _scanner = scanner;
            _parser = new Parser(scanner);
        }
        public override bool Parse()
        {
            _parser.root = null;
            _parser.Errors.Clear();
            bool success = _parser.Parse();
            ProcessTree();
            if (!success)
            {
                _parser.root = new SourceEntity();
                _parser.root.Items.Add(new SourceEntity(_scanner.errorMsg, _scanner.errorLoc));
            }
            return success;

        }
        public override SourceEntity Root
        { 
            get { return _parser.root; }
        }
        public override List<SourceEntity> Errors
        {
            get { return _parser.Errors; }
        }
    }

}