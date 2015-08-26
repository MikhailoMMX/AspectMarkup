using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AspectCore;
using QUT.Gppg;

namespace LWParser
{
    public sealed partial class Scanner : ScanBase
    {
        /// <summary>
        /// стек состояний на момент начала директивы #if
        /// находимся ли мы во вложенной директиве или это директива "верхнего уровня"
        /// </summary>
        private Stack<bool> SkipStateStack = new Stack<bool>();
        /// <summary>
        /// стек флагов, был ли уже внедрен фрагмент в текущей цепочке if [elif]*
        /// </summary>
        private Stack<bool> EmbedFlagStack = new Stack<bool>();
        private HashSet<String> DefinedNames = new HashSet<String>();

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

            if (directive.ToUpper() == "#REGION")
                ProcessRegion(param);
            else if (directive.ToUpper() == "#ENDREGION")
                ProcessRegionEnd();
            else if (directive.ToUpper() == "#IF")
            {
                SkipStateStack.Push(false);
                if (DefinedNames.Contains(param))
                    EmbedFlagStack.Push(true);
                else
                {
                    EmbedFlagStack.Push(false);
                    yy_push_state(SKIPDIRECTIVE);
                }
            }
            else if (directive.ToUpper() == "#ELSE" || directive.ToUpper() == "#ELIF")
            {
                if (EmbedFlagStack.Count == 0)
                    ;// TODO ошибка
                else
                    yy_push_state(SKIPDIRECTIVE);
            }
            else if (directive.ToUpper() == "#ENDIF")
            {
                if (EmbedFlagStack.Count == 0)
                    ;// TODO ошибка
                else
                {
                    EmbedFlagStack.Pop();
                    SkipStateStack.Pop();
                }
            }
            else if (directive.ToUpper() == "#DEFINE")
                DefinedNames.Add(param);
            else if (directive.ToUpper() == "#UNDEF")
                DefinedNames.Remove(param);
        }

        private void ProcessDirectiveInSkipState(string DirectiveText)
        {
            string directive;
            string param;
            DivideDirective(DirectiveText, out directive, out param);

            if (directive.ToUpper() == "#REGION")
                ProcessRegion(param);
            else if (directive.ToUpper() == "#ENDREGION")
                ProcessRegionEnd();
            else if (directive.ToUpper() == "#IF")
            {
                SkipStateStack.Push(true);
            }
            else if (directive.ToUpper() == "#ELSE")
            {
                if (!SkipStateStack.Peek())
                    if (EmbedFlagStack.Peek())
                        ;// TODO ошибка - уже вставляли секцию кода, а теперь надо еще раз
                    else
                        yy_pop_state();
            }
            else if (directive.ToUpper() == "#ELIF")
            {
                if (!SkipStateStack.Peek())
                    if (DefinedNames.Contains(param))
                        if (EmbedFlagStack.Peek())
                            ;// TODO ошибка - уже вставляли секцию кода, а теперь надо еще раз
                        else
                            yy_pop_state();
            }
            else if (directive.ToUpper() == "#ENDIF")
            {
                if (!SkipStateStack.Peek())
                {
                    yy_pop_state();
                    EmbedFlagStack.Pop();
                }
                SkipStateStack.Pop();
            }
        }

        private int OpenBraceCount = 0;
        private bool EnteringContainer = false;
        public void ResetEnteringContainer()
        {
            EnteringContainer = false;
        }

        /// <summary>
        /// Преобразует строку в TokenList с именем прагмы, если строка таковой является
        /// </summary>
        /// <param name="pragma">Строка, содержащая имя директивы или комментарий</param>
        /// <returns>TokenList с описанием прагмы или null, если входная строка не содержит описание аспекта</returns>
        private List<string> GetPragmaName(string pragma)
        {
            if (pragma == "" || !pragma.StartsWith(GlobalData.PragmaPrefix + " "))
                return null;
            string[] str = pragma.Split(" ".ToCharArray(), 2);
            List<string> tl = new List<string>();
            tl.Add(str[0]);
            tl.Add(str[1]);
            return tl;
        }

        /// <summary>
        /// Обрабатывает начало директивы #region. Если данная директива содержит описание аспекта - сохраняет эту информацию в стеке
        /// </summary>
        /// <param name="region">текстовая часть директивы #region</param>
        private void ProcessRegion(string region)
        {
            List<string> RegionName = GetPragmaName(region);
            if (RegionName == null)
            {
                Regions.Push(null);
                return;
            }
            SourceEntity pt = new SourceEntity(RegionName, new LexLocation(tokLin, tokCol, tokELin, tokECol));
            Regions.Push(pt);
        }
        /// <summary>
        /// Обрабатывает конец директивы #region. Обновляет Location описания точки на вершине стека и переносит его в список для возврата
        /// </summary>
        private void ProcessRegionEnd()
        {
            SourceEntity pt = Regions.Pop();
            if (pt == null)
                return;
            pt.Location.Merge(new LexLocation(tokLin, tokCol, tokELin, tokECol));
            Pragmas.Add(pt);
        }
        /// <summary>
        /// Обрабатывает комментарий
        /// </summary>
        /// <param name="comment"></param>
        private void ProcessComment(string comment)
        {
            List<string> CommentName = GetPragmaName(comment);
            if (CommentName == null)
                return;
            Pragmas.Add(new SourceEntity(CommentName, new LexLocation(tokLin, tokCol, tokELin, tokECol)));
        }
        internal List<SourceEntity> Pragmas = new List<SourceEntity>();
        private Stack<SourceEntity> Regions = new Stack<SourceEntity>();

        // Статический класс, определяющий ключевые слова языка
        public static class Keywords
        {
            private static Dictionary<string, int> keywords = new Dictionary<string, int>();

            static Keywords()
            {
                keywords.Add("class", (int)Tokens.tkNamespaceOrClass);
                keywords.Add("namespace", (int)Tokens.tkNamespaceOrClass);
                keywords.Add("interface", (int)Tokens.tkNamespaceOrClass);
                keywords.Add("enum", (int)Tokens.tkNamespaceOrClass);
            }

            public static int KeywordOrIDToken(string s)
            {
                if (keywords.ContainsKey(s))
                    return keywords[s];
                else
                    return (int)Tokens.LetterDigit;
            }
        }
    }
    public class LightweightScanner : ILightWeightScanner
    {
        Scanner _scanner;
        public object Scanner { get { return _scanner; } }
        public LightweightScanner()
        {
            _scanner = new Scanner();
        }
        public void SetSource(string source)
        {
            _scanner.SetSource(source, 0);
        }
        public string LanguageID { get { return "cs"; } }
    }
}
