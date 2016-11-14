using System;
using System.Collections.Generic;
using System.Linq;
using QUT.Gppg;

namespace AspectCore
{
    public class TreeSearchEngine
    {
        /// <summary>
        /// Поиск точки в дереве разбора и выдача результата в виде класса TreeSearchResult.
        /// Это либо единственный узел (Если поле Singular == true), либо отсортированный по степени похожести список узлов
        /// </summary>
        /// <param name="TreeRoot"></param>
        /// <param name="Point"></param>
        /// <param name="SourceText"></param>
        /// <returns></returns>
        public static TreeSearchResult FindPointInTree(PointOfInterest TreeRoot, PointOfInterest Point, string SourceText)
        {
            TreeSearchResult result = InitializeResultFromTree(TreeRoot, Point.Context[0].Type);
            ProcessIDs(result, Point);
            if (result.Singular)
            {
                FillLocationForSingularResult(result, SourceText, Point.Text);
                return result;
            }
            ProcessHeaders(result, Point);
            if (result.Singular)
            {
                FillLocationForSingularResult(result, SourceText, Point.Text);
                return result;
            }

            ProcessOuterContext(result, Point);
            if (result.Singular)
            {
                FillLocationForSingularResult(result, SourceText, Point.Text);
                return result;
            }

            ProcessInnerContext(result, Point);
            if (result.Singular)
            {
                FillLocationForSingularResult(result, SourceText, Point.Text);
                return result;
            }

            ProcessText(result, Point, SourceText);
            if (result.Singular)
            {
                FillLocationForSingularResult(result, SourceText, Point.Text);
                return result;
            }

            result.Sort();
            return result;
        }

        public static TreeSearchResult FindPointInTree2(PointOfInterest TreeRoot, PointOfInterest Point, string SourceText)
        {
            TreeSearchResult result = InitializeResultFromTree(TreeRoot, Point.Context[0].Type);
            if (result._result.Count == 0)
                return result;
            ProcessIDs(result, Point, true);
            ProcessHeaders(result, Point, true);
            ProcessOuterContext(result, Point, true);
            ProcessInnerContext(result, Point, true);
            ProcessText(result, Point, SourceText, true);
            result.Sort();
            if (result.GetTotalMatch(0) == TreeSearchOptions.Equility &&
                (result._result.Count == 1 || result._result.Count >= 2 && result.GetTotalMatch(1) != TreeSearchOptions.Equility))
            {
                //result._result.RemoveRange(1, result._result.Count - 1);
                result.Singular = true;
            }
            return result;
        }

        /// <summary>
        /// Поиск точки в дереве аспектов.
        /// Выполняется по имени (нулевому элементу контекста), требуется строгое соответствие имени.
        /// Если в дереве аспектов есть несколько точек, привязанных к искомой сущности, поиск среди них выполняется по текстовой строке.
        /// </summary>
        /// <param name="WorkingAspect"></param>
        /// <param name="Point"></param>
        /// <returns></returns>
        public static PointOfInterest FindPointInAspectTree(PointOfInterest WorkingAspect, PointOfInterest Point)
        {
            //получаем список всех узлов, без фильтрации по типу
            TreeSearchResult res = InitializeResultFromTree(WorkingAspect, "");
            //обрабатываем имена узлов
            ProcessHeaders(res, Point);

            if (res.Singular)
                return res._result[0].TreeNode;
            if (res.Count == 0)
                return null;

            //сортируем список и выбираем из него строго совпадающие элементы, они будут в начале.
            res.Sort();
            List<PointOfInterest> candidates = new List<PointOfInterest>();
            for (int i=0; i< res.Count; ++i)
                if (res._result[i].NameMatch == TreeSearchOptions.Equility)
                    candidates.Add(res._result[i].TreeNode);
                else
                    break;

            //если совпадающих элементов нет
            if (candidates.Count == 0)
                return null;

            //совпадающих элементов 2 или более
            //сравниваем текстовые строки, возвращаем узел с наиболее похожей текстовой строкой
            List<string> pointText = TextSearch.TokenizeString(Point.Text);
            int maxSim = TreeSearchComparer.TokenListsSimilarity(TextSearch.TokenizeString(candidates[0].Text), pointText);
            int maxSimIndex = 0;
            for (int i = 1; i < candidates.Count; ++i)
            {
                int sim = TreeSearchComparer.TokenListsSimilarity(TextSearch.TokenizeString(candidates[i].Text), pointText);
                if (sim> maxSim)
                {
                    maxSim = sim;
                    maxSimIndex = i;
                }
            }
            return candidates[maxSimIndex];
        }

        /// <summary>
        /// Поиск узла дерева, содержащего данную позицию в тексте, вместе со всеми объемлющими узлами
        /// Порядок узлов в списке - от узла к корню. Нулевой элемент - самый вложенный.
        /// </summary>
        /// <param name="p">Корень дерева, в котором искать</param>
        /// <param name="line">строка</param>
        /// <param name="col">столбец</param>
        /// <returns></returns>
        public static List<PointOfInterest> FindPointByLocation(PointOfInterest p, int line, int col)
        {
            if (!p.IsInside(line, col))
                return null;

            List<PointOfInterest> result = new List<PointOfInterest>();

            foreach (PointOfInterest pt in p.Items)
            {
                List<PointOfInterest> l2 = FindPointByLocation(pt, line, col);
                if (l2 != null)
                    result.AddRange(l2);
            }
            result.Add(p);
            return result;
        }

        /// <summary>
        /// Возвращает константы NearG и NearL для заданного узла в дереве
        /// </summary>
        /// <param name="TreeRoot"></param>
        /// <param name="point"></param>
        /// <param name="NearL"></param>
        /// <param name="NearG"></param>
        public static void SetNearLG(PointOfInterest TreeRoot, PointOfInterest point, string Text, out float NearL, out float NearG)
        {
            NearL = 0;
            NearG = 0;
            if (TreeRoot == null || point == null)
                return;
            //В TreeG записываем все потенциально искомые узлы
            TreeSearchResult TreeG = InitializeResultFromTree(TreeRoot, point.Context[0]?.Type);
            TreeSearchResult TreeL = new TreeSearchResult();

            //Переписываем в TreeL соседние узлы, удаляем их из TreeG (Point тоже удаляем)
            HashSet<PointOfInterest> Neighbours = GetNeighbours(TreeRoot, point);
            for (int i = 0; i < TreeG._result.Count;)
                if (TreeG._result[i].TreeNode == point)
                    TreeG._result.RemoveAt(i);
                else if (Neighbours.Contains(TreeG._result[i].TreeNode))
                {
                    TreeL._result.Add(TreeG._result[i]);
                    TreeG._result.RemoveAt(i);
                }
                else
                    ++i;

            //Находим метрику для наиболее похожих имен узлов в двух множествах
            NearL = GetMaxSimilarityByName(TreeL, point, Text);
            NearG = GetMaxSimilarityByName(TreeG, point, Text);
        }

        /// <summary>
        /// Создает первоначальный объект результата поиска, заполняя его всеми узлами заданного типа из заданного дерева
        /// </summary>
        /// <param name="TreeRoot"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        private static TreeSearchResult InitializeResultFromTree(PointOfInterest TreeRoot, string Type)
        {
            TreeSearchResult result = new TreeSearchResult();
            if (string.IsNullOrWhiteSpace(Type) || (TreeRoot.Context != null && TreeRoot.Context.Count != 0 && TreeRoot.Context[0].Type == Type))
                result._result.Add(new TreeSearchResultNode(TreeRoot));

            foreach (PointOfInterest point in TreeRoot.Items)
                result._result.AddRange(InitializeResultFromTree(point, Type)._result);
            return result;
        }

        /// <summary>
        /// Возвращает множество соседних узлов (исключая сам узел point) в дереве TreeRoot
        /// </summary>
        /// <param name="TreeRoot"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        private static HashSet<PointOfInterest> GetNeighbours(PointOfInterest TreeRoot, PointOfInterest point)
        {
            if (TreeRoot == null || point == null)
                return null;
            HashSet<PointOfInterest> Result = new HashSet<PointOfInterest>();
            if (TreeRoot.Items == null || TreeRoot.Items.Count == 0)
                return Result;
            if (TreeRoot.Items.Contains(point))
            {
                Result.UnionWith(TreeRoot.Items);
                Result.Remove(point);
            }
            else
                foreach (PointOfInterest pt in TreeRoot.Items)
                    Result.UnionWith(GetNeighbours(pt, point));
            return Result;
        }

        /// <summary>
        /// Возвращает величину похожести имени для самого похожего узла в множестве
        /// </summary>
        /// <param name="Nodes"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        private static float GetMaxSimilarityByName(TreeSearchResult Nodes, PointOfInterest point, string Text)
        {
            if (Nodes.Count == 0)
                return -1;

            int maxSim = 0;
            foreach (TreeSearchResultNode node in Nodes._result)
            {
                int sim = TreeSearchComparer.TokenListsSimilarity(point.Context[0].Name, node.TreeNode.Context[0].Name);
                if (sim > maxSim)
                    maxSim = sim;
            }

            //ProcessSearchResult(Nodes, point, Text);
            return (float)maxSim / TreeSearchOptions.Equility;
        }

        /// <summary>
        /// Обрабатывает идентификаторы всех узлов из Result, заполняет компонент похожести идентификатора.
        /// Если по имени найден ровно один совпадающий узел - устанавливает флаг Singular, удаляет несовпадающие узлы из результата.
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="Point"></param>
        private static void ProcessIDs(TreeSearchResult Result, PointOfInterest Point, bool Full = false)
        {
            //пустой идентификатор похож на все непустые. Для совместимости со старыми версиями, где идентификатора не было.
            if (string.IsNullOrWhiteSpace(Point.ID))
            {
                foreach (TreeSearchResultNode node in Result._result)
                    node.NameMatch = TreeSearchOptions.Equility;
                return;
            }

            List<TreeSearchResultNode> exactMatch = new List<TreeSearchResultNode>();
            foreach (TreeSearchResultNode node in Result._result)
                if (!string.IsNullOrWhiteSpace(node.TreeNode.ID))
                {
                    node.NameMatch = TreeSearchComparer.StringsSimilarity(node.TreeNode.ID, Point.ID);
                    if (node.NameMatch == TreeSearchOptions.Equility)
                        exactMatch.Add(node);
                }
            if (exactMatch.Count == 1 && !Full)
                if ((exactMatch[0].TreeNode.Context[0]?.Type ?? "") == (Point.Context[0]?.Type ?? ""))
                {
                    Result.Singular = true;
                    Result._result = exactMatch;
                }
        }

        /// <summary>
        /// Обрабатывает заголовки всех узлов из Result, заполняет компонент похожести заголовка.
        /// Если по имени найден ровно один совпадающий узел - устанавливает флаг Singular, удаляет несовпадающие узлы из результата.
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="Point"></param>
        private static void ProcessHeaders(TreeSearchResult Result, PointOfInterest Point, bool Full = false)
        {
            List<TreeSearchResultNode> exactMatch = new List<TreeSearchResultNode>();
            foreach (TreeSearchResultNode node in Result._result)
                if (node.TreeNode.Context.Count != 0)
                {
                    int sim = 0;
                    if (string.IsNullOrWhiteSpace(Point.Context[0].Type) || node.TreeNode.Context[0].Type == Point.Context[0].Type)
                        sim = TreeSearchComparer.TokenListsSimilarity(node.TreeNode.Context[0].Name, Point.Context[0].Name);
                    node.HeaderMatch = sim;
                    if (sim == TreeSearchOptions.Equility)
                        exactMatch.Add(node);
                }
            if (exactMatch.Count == 1 && !Full)
            {
                Result.Singular = true;
                Result._result = exactMatch;
            }
        }
        /// <summary>
        /// Обрабатывает внешний контект всех узлов из Result, заполняет компонент похожести внешнего контекста.
        /// Если по паре (Имя, Внешний контекст) найден ровно один совпадающий узел - устанавливает флаг Singular, удаляет несовпадающие узлы из результата.
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="Point"></param>
        private static void ProcessOuterContext(TreeSearchResult Result, PointOfInterest Point, bool Full = false)
        {
            List<TreeSearchResultNode> ExactMatch = new List<TreeSearchResultNode>();
            foreach (TreeSearchResultNode node in Result._result)
            {
                int Sim = TreeSearchComparer.OuterContextsSimilarity(node.TreeNode.Context.GetRange(1, node.TreeNode.Context.Count-1), Point.Context.GetRange(1, Point.Context.Count-1));
                node.OuterContextMatch = Sim;
                if (Sim == TreeSearchOptions.Equility && node.NameMatch == TreeSearchOptions.Equility)
                    ExactMatch.Add(node);
            }
            if (ExactMatch.Count == 1 && !Full)
            {
                Result.Singular = true;
                Result._result = ExactMatch;
            }
        }
        /// <summary>
        /// Обрабатывает внутренний контект всех узлов из Result, заполняет компонент похожести внутреннего контекста.
        /// Если по паре (Имя, Внутренний контекст) или по тройке (Имя, Внешний контекст, Внутренний контекст)
        /// найден ровно один совпадающий узел - устанавливает флаг Singular, удаляет несовпадающие узлы из результата.
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="Point"></param>
        private static void ProcessInnerContext(TreeSearchResult Result, PointOfInterest Point, bool Full = false)
        {
            //return; //For testing, TODO remove

            List<TreeSearchResultNode> ExactMatchPair = new List<TreeSearchResultNode>();
            List<TreeSearchResultNode> ExactMatchThree = new List<TreeSearchResultNode>();
            List<List<string>> PointContext = ConvertInnerContextToList(Point);

            foreach (TreeSearchResultNode node in Result._result)
            {
                node.TreeNode.ApplyInnerContext();
                List<List<string>> ResContext = ConvertInnerContextToList(node.TreeNode);
                int Sim = TreeSearchComparer.TokenListListsIntersection(ResContext, PointContext);
                node.InnerContextMatch = Sim;
                if (Sim == TreeSearchOptions.Equility && node.NameMatch == TreeSearchOptions.Equility)
                    ExactMatchPair.Add(node);
                if (Sim == TreeSearchOptions.Equility && node.NameMatch == TreeSearchOptions.Equility && node.OuterContextMatch == TreeSearchOptions.Equility)
                    ExactMatchThree.Add(node);
            }
            if (ExactMatchPair.Count == 1 && !Full)
            {
                Result.Singular = true;
                Result._result = ExactMatchPair;
            }
            if (ExactMatchThree.Count == 1 && !Full)
            {
                Result.Singular = true;
                Result._result = ExactMatchThree;
            }
        }

        /// <summary>
        /// Заполняет компонент похожести всех узлов из Result на текстовую строку из заданной точки.
        /// Если по четверке (Имя, внешний контекст, внутренний контекст, текстовая строка)
        /// найден ровно один совпадающий узел - устанавливает флаг Singular, удаляет несовпадающие узлы из результата.
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="Point"></param>
        /// <param name="Source"></param>
        private static void ProcessText(TreeSearchResult Result, PointOfInterest Point, string Source, bool Full = false)
        {
            List<TreeSearchResultNode> ExactMatch = new List<TreeSearchResultNode>();
            TextSearch ts = new TextSearch(Source);
            foreach (TreeSearchResultNode node in Result._result)
            {
                Pair<int, int> Sim = ts.Similarity(node.TreeNode.Location, Point.Text);
                node.TextStringMatch = Sim.Second;
                if (Sim.First == TreeSearchOptions.Equility && node.NameMatch == TreeSearchOptions.Equility
                        && node.OuterContextMatch == TreeSearchOptions.Equility && node.InnerContextMatch == TreeSearchOptions.Equility)
                    ExactMatch.Add(node);
            }
            if (ExactMatch.Count == 1 && !Full)
            {
                Result.Singular = true;
                Result._result = ExactMatch;
            }
        }
        ///// <summary>
        ///// Заполняет i-й элемент массива результатов поиска элементами заданного массива
        ///// </summary>
        ///// <param name="Result">Объект, хранящий результат поиска</param>
        ///// <param name="array">Массив компонентов, который нужно сохранить в объекте Result</param>
        ///// <param name="i">Номер элемента массива в объекте Result, в который нужно сохранить новый массив</param>
        //private void SaveArrayInResult(TreeSearchResult Result, int index, int[] array)
        //{
        //    System.Diagnostics.Debug.Assert(array.Length == Result._result.Count);
        //    for (int i = 0; i < array.Length; ++i)
        //        Result._result[i]._match[index] = array[i];
        //}
        /// <summary>
        /// Преобразует внутренний контекст в список списков лексем
        /// </summary>
        /// <param name="Point"></param>
        /// <returns></returns>
        private static List<List<string>> ConvertInnerContextToList(PointOfInterest Point)
        {
            List<List<string>> Result = new List<List<string>>();
            if (Point.InnerContext != null)
                foreach (InnerContextNode node in Point.InnerContext)
                    Result.AddRange(ConvertInnerContextToList(node));
            return Result;
        }
        /// <summary>
        /// Преобразует внутренний контекст в список списков лексем
        /// </summary>
        /// <param name="Node"></param>
        /// <returns></returns>
        private static List<List<string>> ConvertInnerContextToList(InnerContextNode Node)
        {
            List<List<string>> Result = new List<List<string>>();
            Result.Add(Node.Name);
            if (Node.SubNodes != null)
                foreach (InnerContextNode subnode in Node.SubNodes)
                    Result.AddRange(ConvertInnerContextToList(subnode));
            return Result;
        }

        /// <summary>
        /// Возвращает местоположение заданной строки из заданной точки в исходном тексте
        /// </summary>
        /// <param name="point"></param>
        /// <param name="Source"></param>
        /// <returns></returns>
        public static LexLocation FindTextPosition(PointOfInterest point, string Source)
        {
            TextSearch ts = new TextSearch(Source);
            Pair<int, int> p = ts.Similarity(point.Location, point.Text);
            int Line = p.First;
            int Col;
            if (Line == point.Location.StartLine)
                Col = point.Location.StartColumn;
            else
                Col = ts.GetFirstCharPosAtLine(Line);
            return new LexLocation(Line, Col, Line, Col);
        }

        /// <summary>
        /// Заполняет поле Location в единственном узле результата
        /// </summary>
        /// <param name="Result"></param>
        /// <param name="Source"></param>
        /// <param name="Text"></param>
        private static void FillLocationForSingularResult(TreeSearchResult Result, string Source, string Text)
        {
            PointOfInterest Point = Result._result[0].TreeNode.ClonePoint();
            Point.ApplyInnerContext();
            Point.Items.Clear();
            Point.Text = Text;
            LexLocation Loc = FindTextPosition(Point, Source);
            Point.Location = Loc;
            Result._result[0].TreeNode = Point;
        }
    }

    //public class TreeSearchEngineOld
    //{
    //    #region Old
    //    /// <summary>
    //    /// Поиск точки в аспектном дереве (в окне аспектов)
    //    /// </summary>
    //    /// <param name="point">Искомая точка</param>
    //    /// <param name="WorkingAspect">Корень дерева аспектов</param>
    //    /// <returns></returns>
    //    public static PointOfInterest FindPointInAspectTree(PointOfInterest point, PointOfInterest WorkingAspect)
    //    {
    //        List<PointOfInterest> candidates = FindPointsInAspectTree(WorkingAspect, point);
    //        if (candidates.Count == 0)
    //            return null;

    //        string[] Strs = new string[candidates.Count];
    //        for (int i = 0; i < candidates.Count; ++i)
    //            Strs[i] = candidates[i].Text;
    //        List<string> Str = TokenizeString(point.Text);
    //        List<List<string>> Strl = TokenizeStrings(Strs, 0, candidates.Count - 1);
    //        int R = FindMostSimilarSequence(Strl, Str);
    //        return candidates[R];
    //    }

    //    /// <summary>
    //    /// Отфильтровывает точки-кандидаты проверяя контекст уровень за уровнем пока не останется не более одной точки.
    //    /// </summary>
    //    /// <param name="Candidates">Список точек - кандидатов</param>
    //    /// <param name="point">Точка с контекстом</param>
    //    /// <returns>Точка, удовлетворяющая контексту</returns>
    //    private static List<PointOfInterest> FindPointsInAspectTree(PointOfInterest tree, PointOfInterest point)
    //    {
    //        if (tree == null)
    //            return null;

    //        List<PointOfInterest> result = new List<PointOfInterest>();
    //        if (IsContextEqual(tree, point))
    //            result.Add(tree);
    //        foreach (PointOfInterest pt in tree.Items)
    //        {
    //            List<PointOfInterest> l = FindPointsInAspectTree(pt, point);
    //            if (l != null && l.Count != 0)
    //                result.AddRange(l);
    //        }
    //        return result;
    //    }

    //    /// <summary>
    //    /// Поиск узла дерева, содержащего данную позицию в тексте
    //    /// </summary>
    //    /// <param name="p">Корень дерева, в котором искать</param>
    //    /// <param name="line">строка</param>
    //    /// <param name="col">столбец</param>
    //    /// <returns></returns>
    //    public static PointOfInterest FindPointByLocation(PointOfInterest p, int line, int col)
    //    {
    //        if (!p.IsInside(line, col))
    //            return null;

    //        if (p.Items == null || p.Items.Count == 0)
    //            return p;

    //        foreach (PointOfInterest pt in p.Items)
    //        {
    //            PointOfInterest pt2 = FindPointByLocation(pt, line, col);
    //            if (pt2 != null)
    //                return pt2;
    //        }
    //        return p;
    //    }

    //    /// <summary>
    //    /// Возвращает список точек, похожих на данную точку
    //    /// Если точка одна - текст и позиция с учетом текста заполнены. Иначе позицию с учетом текста нужно вычислять отдельно
    //    /// </summary>
    //    /// <param name="point">Искомая точка</param>
    //    /// <param name="Root">Дерево, в котором надо найти точку</param>
    //    /// <param name="SourceText">Полный текст файла</param>
    //    /// <returns></returns>
    //    public static List<PointOfInterest> FindPointsFromSavedPoint(PointOfInterest point, PointOfInterest Root, string SourceText)
    //    {
    //        if (point == null || point.Context.Count == 0 || Root == null)
    //            return null;
    //        List<PointOfInterest> parsedPoints = FindPointInTree(Root, point);
    //        if (parsedPoints != null && parsedPoints.Count == 1)
    //        {
    //            parsedPoints[0].Text = point.Text;
    //            if (point.Text != "")
    //            {
    //                QUT.Gppg.LexLocation loc = parsedPoints[0].Location;
    //                int line = parsedPoints[0].Location.StartLine + FindTextPosition(parsedPoints[0], SourceText);
    //                parsedPoints[0].Location = new QUT.Gppg.LexLocation(line, 0, line, 0);
    //            }
    //        }
    //        return parsedPoints;
    //    }

    //    /// <summary>
    //    /// Поиск узла с заданным именем в заданном дереве
    //    /// </summary>
    //    /// <param name="tree">Дерево, в котором нужно найти узел</param>
    //    /// <param name="point">Имя узла, которое нужно найти</param>
    //    /// <returns>Список наиболее похожих узлов, либо null</returns>
    //    private static List<PointOfInterest> FindPointInTree(PointOfInterest tree, PointOfInterest point)
    //    {
    //        List<PointOfInterest> result = new List<PointOfInterest>();

    //        //Находим кандидатов и редакционное расстояние для каждого из них
    //        List<Pair<PointOfInterest, int>> candidates = FindAllPointsInTree(tree, point);

    //        //0 или 1 кандидат - сразу возвращаем результат
    //        if (candidates.Count == 0)
    //            return null;
    //        if (candidates.Count == 1)
    //        {
    //            result.Add(candidates[0].First.ClonePointAssignItems());
    //            return result;
    //        }

    //        //кандидатов 2+, сортируем по возрастанию редакционного расстояния
    //        candidates = candidates.OrderBy(o => o.Second).ToList();

    //        //если ровно 1 кандидат с нулевым расстоянием, остальные - с ненулевым - возвращаем его
    //        if (candidates[0].Second == 0 && candidates[1].Second != 0)
    //        {
    //            result.Add(candidates[0].First.ClonePointAssignItems());
    //            return result;
    //        }

    //        //прибавляем к расстояниям разницу в контексте и сортируем
    //        List<string> Context = new List<string>();
    //        for (int i = 1; i < point.Context.Count; ++i)
    //            Context.Add(string.Join("", point.Context[i]));
    //        foreach (Pair<PointOfInterest, int> P in candidates)
    //        {
    //            List<string> Ctx = new List<string>();
    //            for (int i = 1; i < P.First.Context.Count; ++i)
    //                Ctx.Add(string.Join("", P.First.Context[i]));
    //            P.Second += CalculateEditDistance(Context, Ctx) * TreeSearchOptions.ContextDistanceMultiplier;
    //        }
    //        candidates = candidates.OrderBy(o => o.Second).ToList();

    //        //если ровно 1 кандидат с нулевым расстоянием, остальные - с ненулевым - возвращаем его
    //        if (candidates[0].Second == 0 && candidates[1].Second != 0)
    //        {
    //            result.Add(candidates[0].First.ClonePointAssignItems());
    //            return result;
    //        }

    //        int max = Math.Min(TreeSearchOptions.MaxDistanceToReturn, candidates[0].Second * TreeSearchOptions.MaxDistanceDifference);
    //        //отсекаем слишком непохожие кандидаты
    //        for (int i = 0; i < candidates.Count; ++i)
    //            if (candidates[i].Second > max)
    //                break;
    //            else
    //                result.Add(candidates[i].First.ClonePointAssignItems());

    //        //возвращаем полученный список
    //        return result;
    //    }
        
    //    /// <summary>
    //    /// Проверка контекста на совпадение
    //    /// </summary>
    //    /// <param name="tree"></param>
    //    /// <param name="point"></param>
    //    /// <returns></returns>
    //    private static bool IsContextEqual(PointOfInterest tree, PointOfInterest point)
    //    {
    //        if (tree == null || point == null)
    //            return false;
    //        if (tree.Context.Count != point.Context.Count)
    //            return false;
    //        for (int i = 0; i < point.Context.Count; ++i)
    //        {
    //            if (point.Context[i].Count != tree.Context[i].Count)
    //                return false;
    //            for (int j = 0; j < point.Context[i].Count; ++j)
    //                if (point.Context[i][j] != tree.Context[i][j])
    //                    return false;
    //        }
    //        return true;
    //    }
    //    /// <summary>
    //    /// Вычисление расстояния для заданного уровня контекста
    //    /// </summary>
    //    /// <param name="foundPoint">Проверяемая точка</param>
    //    /// <param name="contextPoint">Точка с полным контекстом</param>
    //    /// <returns></returns>
    //    private static int GetContextDistance(PointOfInterest foundPoint, PointOfInterest contextPoint, int level)
    //    {
    //        if (foundPoint == null)
    //            return TreeSearchOptions.CostContextSizeMismatch;
    //        if (contextPoint.Context == null || contextPoint.Context.Count <= level)
    //            return TreeSearchOptions.CostParserTypeMismatch;
    //        if (foundPoint.Context.Count <= level)
    //            return TreeSearchOptions.CostParserTypeMismatch;

    //        return CalculateEditDistance(foundPoint.Context[level], contextPoint.Context[level]);
    //    }

    //    /// <summary>
    //    /// Вычисление редакционного расстояния до всех узлов дерева
    //    /// </summary>
    //    /// <param name="tree"></param>
    //    /// <param name="point"></param>
    //    /// <param name="fillContext">Заполнять контекст для возвращаемых точек</param>
    //    /// <returns></returns>
    //    private static List<Pair<PointOfInterest, int>> FindAllPointsInTree(PointOfInterest tree, PointOfInterest point)
    //    {
    //        if (tree == null || point == null)
    //            return null;

    //        List<Pair<PointOfInterest, int>> result = new List<Pair<PointOfInterest, int>>();

    //        if (tree.Context.Count != 0 && point.Context.Count != 0)
    //        {
    //            int Dist = CalculateEditDistance(tree.Context[0], point.Context[0]);
    //            if (tree.ParserClassName != point.ParserClassName)
    //                Dist += TreeSearchOptions.CostParserTypeMismatch;
    //            if (point.Context[0].Count < TreeSearchOptions.MinNameLength || (float)Dist / point.Context[0].Count <= TreeSearchOptions.NameMismatchCutOff)
    //                result.Add(new Pair<PointOfInterest, int>(tree, Dist));
    //        }

    //        foreach (PointOfInterest pt in tree.Items)
    //        {
    //            List<Pair<PointOfInterest, int>> l = FindAllPointsInTree(pt, point);
    //            if (l != null)
    //                foreach (Pair<PointOfInterest, int> pt2 in l)
    //                    result.Add(pt2);
    //        }
    //        return result;
    //    }

    //    /// <summary>
    //    /// Возвращает номер строки, содержащей заданный фрагмент текста
    //    /// </summary>
    //    /// <param name="Point">Точка с описанием сущности, её местоположения и фрагмента искомого текста</param>
    //    /// <param name="Source">Исходный код</param>
    //    /// <returns></returns>
    //    public static int FindTextPosition(PointOfInterest Point, string Source)
    //    {
    //        string[] AllLines = Source.Split('\n');
    //        List<List<string>> FragmentTokens = TokenizeStrings(AllLines, Point.Location.StartLine - 1, Point.Location.EndLine - 1);
    //        List<string> PatternTokens = TokenizeString(Point.Text);

    //        int result = FindExactTokenSequence(FragmentTokens, PatternTokens);
    //        if (result != -1)
    //            return result;

    //        return FindMostSimilarSequence(FragmentTokens, PatternTokens);
    //    }

    //    /// <summary>
    //    /// Разбивает указанный диапазон строк из массива на списки лексем
    //    /// </summary>
    //    /// <param name="Array"></param>
    //    /// <param name="start"></param>
    //    /// <param name="end"></param>
    //    /// <param name="Lexer"></param>
    //    /// <returns></returns>
    //    private static List<List<string>> TokenizeStrings(string[] Array, int start, int end)
    //    {
    //        List<List<string>> result = new List<List<string>>();

    //        for (int i = start; i <= end; ++i)
    //            result.Add(TokenizeString(Array[i]));
    //        return result;
    //    }
    //    /// <summary>
    //    /// Разбивает заданную строку на список лексем
    //    /// </summary>
    //    /// <param name="str"></param>
    //    /// <param name="Lexer"></param>
    //    /// <returns></returns>
    //    private static List<string> TokenizeString(string str)
    //    {
    //        CommonLexer.Scanner Lexer = new CommonLexer.Scanner();
    //        List<string> list = new List<string>();
    //        if (str == null || str == "")
    //            return list;
    //        Lexer.SetSource(str, 0);
    //        while (Lexer.yylex() != (int)CommonLexer.Tokens.EOF)
    //            list.Add(Lexer.yytext); ;
    //        return list;
    //    }

    //    /// <summary>
    //    /// В списке TokenLists находит номер списка, точно совпадающего с Pattern или -1
    //    /// </summary>
    //    /// <param name="TokenLists"></param>
    //    /// <param name="Pattern"></param>
    //    /// <returns></returns>
    //    private static int FindExactTokenSequence(List<List<string>> TokenLists, List<string> Pattern)
    //    {
    //        int pos = -1;
    //        foreach (List<string> lst in TokenLists)
    //        {
    //            pos += 1;
    //            if (lst.Count != Pattern.Count)
    //                continue;
    //            bool match = true;
    //            for (int i = 0; i < lst.Count; ++i)
    //                if (lst[i] != Pattern[i])
    //                {
    //                    match = false;
    //                    break;
    //                }
    //            if (match)
    //                return pos;
    //        }
    //        return -1;
    //    }

    //    /// <summary>
    //    /// Среди списка TokenLists находит номер списка, наиболее похожего на Pattern
    //    /// </summary>
    //    /// <param name="TokenLists"></param>
    //    /// <param name="Pattern"></param>
    //    /// <returns></returns>
    //    private static int FindMostSimilarSequence(List<List<string>> TokenLists, List<string> Pattern)
    //    {
    //        List<int> Distances = new List<int>();
    //        for (int i = 0; i < TokenLists.Count; ++i)
    //            Distances.Add(CalculateEditDistance(TokenLists[i], Pattern));

    //        int minDist = int.MaxValue;
    //        int minIndex = 0;
    //        for (int i = 0; i < Distances.Count; ++i)
    //            if (Distances[i] < minDist)
    //            {
    //                minDist = Distances[i];
    //                minIndex = i;
    //            }

    //        return minIndex;
    //    }

    //    /// <summary>
    //    /// Вычисляет редакционное расстояние между двумя списками лексем
    //    /// </summary>
    //    /// <param name="Tokens"></param>
    //    /// <param name="Pattern"></param>
    //    /// <returns></returns>
    //    private static int CalculateEditDistance(List<string> Tokens, List<string> Pattern)
    //    {
    //        if (Tokens.Count == 0)
    //            return Pattern.Count * TreeSearchOptions.CostInsertRemoveChar;
    //        if (Pattern.Count == 0)
    //            return Tokens.Count * TreeSearchOptions.CostInsertRemoveChar;

    //        int[,] D = new int[Tokens.Count + 1, Pattern.Count + 1];
    //        D[0, 0] = 0;
    //        for (int j = 1; j <= Pattern.Count; ++j)
    //            D[0, j] = D[0, j - 1] + TreeSearchOptions.CostInsertRemoveChar;
    //        for (int i = 1; i <= Tokens.Count; ++i)
    //        {
    //            D[i, 0] = D[i - 1, 0] + TreeSearchOptions.CostInsertRemoveChar;
    //            for (int j = 1; j <= Pattern.Count; ++j)
    //                D[i, j] = Math.Min(
    //                    D[i - 1, j] + TreeSearchOptions.CostInsertRemoveChar,
    //                    Math.Min(
    //                        D[i, j - 1] + TreeSearchOptions.CostInsertRemoveChar,
    //                        D[i - 1, j - 1] + (Tokens[i - 1] == Pattern[j - 1] ? 0 : TreeSearchOptions.CostReplaceChar)
    //                    )
    //                );
    //        }
    //        return D[Tokens.Count, Pattern.Count];
    //    }
    //    #endregion
    //}

    /// <summary>
    /// Содержит результат поиска узла в дереве
    /// </summary>
    public class TreeSearchResult
    {
        /// <summary>
        /// Результат был найден однозначно, единственный элемент массива результатов
        /// </summary>
        public bool Singular = false;
        /// <summary>
        /// Список узлов с весами
        /// </summary>
        internal List<TreeSearchResultNode> _result = new List<TreeSearchResultNode>();
        /// <summary>
        /// Элементы массива результатов
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public PointOfInterest this[int Index] { get { return _result[Index].TreeNode; } }
        public double SimilarityOf(int Index) 
        {
            TreeSearchResultNode Node = _result[Index];
            int Total = Node.NameMatch * _wHeader + Node.OuterContextMatch * _wOuterCTX + Node.InnerContextMatch * _wInnerCTX + Node.TextStringMatch * _wText;
            int wSum = _wHeader + _wOuterCTX + _wInnerCTX + _wText;
            return (double)Total / wSum / TreeSearchOptions.Equility;
        }
        /// <summary>
        /// Количество элементов
        /// </summary>
        public int Count { get { return _result.Count; } }
        private int _wName = 4;
        private int _wHeader = 1;
        private int _wOuterCTX = 2;
        private int _wInnerCTX = 1;
        private int _wText = 1;
        /// <summary>
        /// Сортирует массив результатов с равными весами компонентов
        /// </summary>
        public void Sort()
        {
            _result = _result.OrderByDescending(x => x.NameMatch            * _wName
                                                   + x.HeaderMatch          * _wHeader
                                                   + x.OuterContextMatch    * _wOuterCTX
                                                   + x.InnerContextMatch    * _wInnerCTX
                                                   + x.TextStringMatch      * _wText
                                               ).ToList();
        }
        public void SetWeights(int NameWeight, int HeaderWeight, int OuterCtxWeight, int InnerCtxWeight, int TextWeight)
        {
            _wName = NameWeight;
            _wHeader = HeaderWeight;
            _wOuterCTX = OuterCtxWeight;
            _wInnerCTX = InnerCtxWeight;
            _wText = TextWeight;
        }
        public float GetNodeSimilarity(int index)
        {
            if (index < 0 || index >= _result.Count)
                    return 0;
            return (float)GetTotalMatch(index) / TreeSearchOptions.Equility;
        }
        /// <summary>
        /// Сортирует массив результатов с заданными весами компонентов
        /// </summary>
        /// <param name="NameWeight">Вес компонента имени</param>
        /// <param name="OuterCtxWeight">Вес компонента внешнего контекста</param>
        /// <param name="InnerCtxWeight">Вес компонента внутреннего контекста</param>
        /// <param name="TextWeight">Вес компонента текстовой строки</param>
        public void Sort(int NameWeight, int HeaderWeight, int OuterCtxWeight, int InnerCtxWeight, int TextWeight)
        {
            _wName = NameWeight;
            _wHeader = HeaderWeight;
            _wOuterCTX = OuterCtxWeight;
            _wInnerCTX = InnerCtxWeight;
            _wText = TextWeight;
        }
        public int GetTotalMatch(int index)
        {
            System.Diagnostics.Debug.Assert(index >= 0 && index < _result.Count);
            TreeSearchResultNode node = _result[index];
            int num = node.NameMatch         * _wName
                    + node.HeaderMatch       * _wHeader
                    + node.OuterContextMatch * _wOuterCTX
                    + node.InnerContextMatch * _wInnerCTX
                    + node.TextStringMatch   * _wText;
            int denom = _wName + _wHeader + _wOuterCTX + _wInnerCTX + _wText;
            return num / denom;
        }
    }
    /// <summary>
    /// Используется для хранения узлов, похожих на искомый узел в TreeSearchResult
    /// </summary>
    internal class TreeSearchResultNode
    {
        public PointOfInterest TreeNode;
        internal int[] _match = new int[ArraySize];
        internal const int ArraySize = 5;
        internal const int NameIndex = 0;
        internal const int HeaderIndex = 1;
        internal const int OuterContextIndex = 2;
        internal const int InnerContextIndex = 3;
        internal const int TextStringIndex = 4;
        public TreeSearchResultNode(PointOfInterest Point)
        {
            TreeNode = Point;
            for (int i = 0; i < ArraySize; ++i)
                _match[i] = TreeSearchOptions.Equility;
        }
        public int NameMatch { get { return _match[NameIndex]; } set { _match[NameIndex] = value; } }
        public int HeaderMatch { get { return _match[HeaderIndex]; } set { _match[HeaderIndex] = value; } }
        public int OuterContextMatch { get { return _match[OuterContextIndex]; } set { _match[OuterContextIndex] = value; } }
        public int InnerContextMatch { get { return _match[InnerContextIndex]; } set { _match[InnerContextIndex] = value; } }
        public int TextStringMatch { get { return _match[TextStringIndex]; } set { _match[TextStringIndex] = value; } }
        //public int TotalMatch { get { return (_match[0] + _match[1] + _match[2] + _match[3] + _match[4]) / 5; } }
    }

    /// <summary>
    /// Содержит набор методов для четкого и нечеткого сравнения строк и списков
    /// </summary>
    public static class TreeSearchComparer
    {
        /// <summary>
        /// Сравнивает списки строк на равенство по содержимому
        /// </summary>
        /// <param name="L1"></param>
        /// <param name="L2"></param>
        /// <returns></returns>
        public static bool IsEqual(List<string> L1, List<string> L2)
        {
            if (L1 == null || L2 == null || L1.Count != L2.Count)
                return false;

            for (int i = 0; i < L1.Count; ++i)
                if (L1[i] != L2[i])
                    return false;

            return true;
        }

        public static bool IsEqual(OuterContextNode C1, OuterContextNode C2)
        {
            if (C1 == null || C2 == null || C1.Type != C2.Type)
                return false;

            return IsEqual(C1.Name, C2.Name);
        }

        /// <summary>
        /// Сравнивает списки списков на равенство по содержимому
        /// </summary>
        /// <param name="L1"></param>
        /// <param name="L2"></param>
        /// <returns></returns>
        public static bool IsEqual(dynamic L1, dynamic L2)
        {
            if (L1 == null || L2 == null || L1.Count != L2.Count)
                return false;

            for (int i = 0; i < L1.Count; ++i)
                if (!IsEqual(L1[i], L2[i]))
                    return false;

            return true;
        }
        public static bool IsEqual(string S1, string S2)
        {
            return S1 == S2;
        }

        /// <summary>
        /// Вычисляет степень похожести двух строк
        /// </summary>
        /// <param name="S1"></param>
        /// <param name="S2"></param>
        /// <returns>Число от 0 (строки не похожи) до TreeSearchOptions.Equality (строки совпадают)</returns>
        public static int StringsSimilarity(string S1, string S2)
        {
            //Доступно: Lwvwnshtein.Similarity, JaroWinkler.Similarity
            return LevenshteinSimilarity.Similarity(S1, S2);
        }

        /// <summary>
        /// Вычисляет степень похожести двух списков строк
        /// </summary>
        /// <param name="L1"></param>
        /// <param name="L2"></param>
        /// <returns>Число от 0 (строки не похожи) до TreeSearchOptions.Equality (строки совпадают)</returns>
        public static int TokenListsSimilarity(List<string> L1, List<string> L2)
        {
            return LevenshteinSimilarity.Similarity(L1, L2);
        }

        /// <summary>
        /// Вычисляет степень похожести двух списков списков строк
        /// </summary>
        /// <param name="L1"></param>
        /// <param name="L2"></param>
        /// <returns>Число от 0 (строки не похожи) до TreeSearchOptions.Equality (строки совпадают)</returns>
        public static int OuterContextsSimilarity(List<OuterContextNode> L1, List<OuterContextNode> L2)
        {
            return LevenshteinSimilarity.Similarity(L1, L2);
        }

        /// <summary>
        /// Вычисляет похожесть списков списков строк, как отношение количества элементов в пересечении к количеству элементов в объединении
        /// </summary>
        /// <param name="L1"></param>
        /// <param name="L2"></param>
        /// <returns>Число от 0 (общих элементов нет) до TreeSearchOptions.Equality (одинаковое множество элементов)</returns>
        public static int TokenListListsIntersection(List<List<string>> L1, List<List<string>> L2)
        {
            if (L1 == null || L2 == null)
                return 0;
            if (L1.Count == 0 && L2.Count == 0)
                return TreeSearchOptions.Equility;
            if (L1.Count == 0 || L2.Count == 0)
                return 0;
            int Union = Math.Max(L1.Count, L2.Count);
            int Intersection = 0;
            foreach (List<string> Lst1 in L1)
                foreach (List<string> Lst2 in L2)
                    if (IsEqual(Lst1, Lst2))
                    {
                        Intersection += 1;
                        break;
                    }
            return Intersection * TreeSearchOptions.Equility / Union;
        }

        /// <summary>
        /// Пара строк, использующихся в качестве ключа для словаря
        /// </summary>
        private struct StringPairKey
        {
            public readonly string S1;
            public readonly string S2;
            public StringPairKey(string str1, string str2)
            {
                if (str1.CompareTo(str2) <= 0)
                {
                    S1 = str1;
                    S2 = str2;
                }
                else
                {
                    S1 = str2;
                    S2 = str1;
                }
            }
            public override bool Equals(object K2)
            {
                if (K2 is StringPairKey)
                    return (S1 == ((StringPairKey)K2).S1 && S2 == ((StringPairKey)K2).S2);
                else
                    return false;
            }
            public override int GetHashCode()
            {
                return S1.GetHashCode() ^ S2.GetHashCode();
            }

        }
        /// <summary>
        /// Реализация вычисления степени похожести строк и списков строк с использованием метрики Левенштейна
        /// </summary>
        public static class LevenshteinSimilarity
        {
            /// <summary>
            /// Словарь степеней похожести уже вычисленных пар строк
            /// </summary>
            static System.Collections.Concurrent.ConcurrentDictionary<StringPairKey, int> _Similarity = new System.Collections.Concurrent.ConcurrentDictionary<StringPairKey, int>();
            /// <summary>
            /// Возвращает степень похожести двух строк
            /// </summary>
            /// <param name="S1"></param>
            /// <param name="S2"></param>
            /// <returns>От 0 до TreeSearchOptions.Equality</returns>
            public static int Similarity(string S1, string S2)
            {
                if (S1 == S2)
                    return TreeSearchOptions.Equility;
                int MinLen = Math.Min(S1.Length, S2.Length);
                int MaxLen = Math.Max(S1.Length, S2.Length);
                if (MinLen == 0 && MaxLen != 0)
                    return 0;

                //если в словаре уже есть такая пара - возвращаем сохраненный результат
                StringPairKey K = new StringPairKey(S1, S2);
                if (_Similarity.ContainsKey(K))
                    return _Similarity[K];

                //найдем длины общих префиксов и суффиксов
                int prefix = 0;
                int suffix = 0;

                while (prefix < MinLen && S1[prefix] == S2[prefix])
                    ++prefix;
                while (suffix < MinLen && S1[S1.Length - 1 - suffix] == S2[S2.Length - 1 - suffix])
                    ++suffix;

                int Dist;
                if (prefix == MinLen || suffix == MinLen || (prefix + suffix >= MinLen))
                    //одна строка является префиксом или суффиксом другой
                    Dist = (MaxLen - MinLen) * TreeSearchOptions.CostInsertRemoveChar;
                else
                {
                    int S1Len = S1.Length - prefix - suffix;
                    int S2Len = S2.Length - prefix - suffix;

                    //вычислим матрицу расстояний
                    int[,] D = new int[S1Len + 1, S2Len + 1];
                    D[0, 0] = 0;
                    for (int j = 1; j <= S2Len; ++j)
                        D[0, j] = D[0, j - 1] + TreeSearchOptions.CostInsertRemoveChar;

                    for (int i = 1; i <= S1Len; ++i)
                    {
                        D[i, 0] = D[i - 1, 0] + TreeSearchOptions.CostInsertRemoveChar;
                        for (int j = 1; j <= S2Len; ++j)
                            D[i, j] = Math.Min(
                                D[i - 1, j] + TreeSearchOptions.CostInsertRemoveChar,
                                Math.Min(
                                    D[i, j - 1] + TreeSearchOptions.CostInsertRemoveChar,
                                    D[i - 1, j - 1] + (S1[i - 1 + prefix] == S2[j - 1 + prefix] ? 0 : TreeSearchOptions.CostReplaceChar)
                                )
                            );
                    }
                    Dist = D[S1Len, S2Len];
                }
                //максимальное расстояние для пары строк такой длины
                int MaxDist = Math.Min(S1.Length, S2.Length) * Math.Min(TreeSearchOptions.CostReplaceChar, TreeSearchOptions.CostInsertRemoveChar * 2);
                MaxDist += Math.Abs(S1.Length - S2.Length) * TreeSearchOptions.CostInsertRemoveChar;

                //Нормированная степень похожести
                int Sim = (MaxDist - Dist) * TreeSearchOptions.Equility / MaxDist;

                _Similarity.AddOrUpdate(K, Sim, (k, v) => v);
                return Sim;
            }

            /// <summary>
            /// Возвращает степень похожести двух списков.
            /// Элементами списков могут быть строки или списки строк, списки списков строк...
            /// </summary>
            /// <param name="L1"></param>
            /// <param name="L2"></param>
            /// <returns>От 0 до TreeSearchOptions.Equality</returns>
            public static int Similarity(dynamic L1, dynamic L2)
            {
                if (L1 == null || L2 == null)
                    return 0;
                int MinLen = Math.Min(L1.Count, L2.Count);
                int MaxLen = Math.Max(L1.Count, L2.Count);
                if (MinLen == 0 && MaxLen != 0)
                    return 0;
                if (TreeSearchComparer.IsEqual(L1, L2))
                    return TreeSearchOptions.Equility;

                //найдем длины общих префиксов и суффиксов
                int prefix = 0;
                int suffix = 0;
                while (prefix < MinLen && IsEqual(L1[prefix], L2[prefix]))
                    ++prefix;
                while (suffix < MinLen && IsEqual(L1[L1.Count - 1 - suffix], L2[L2.Count - 1 - suffix]))
                    ++suffix;

                double Dist = 0;
                if (prefix == MinLen || suffix == MinLen || (prefix + suffix >= MinLen))
                    //одна строка является префиксом или суффиксом другой
                    Dist = (MaxLen - MinLen) * TreeSearchOptions.CostInsertRemoveToken;
                else
                {
                    int L1Len = L1.Count - prefix - suffix;
                    int L2Len = L2.Count - prefix - suffix;

                    //вычислим матрицу расстояний
                    double[,] D = new double[L1Len + 1, L2Len + 1];
                    D[0, 0] = 0;
                    for (int j = 1; j <= L2Len; ++j)
                        D[0, j] = D[0, j - 1] + TreeSearchOptions.CostInsertRemoveToken;

                    for (int i = 1; i <= L1Len; ++i)
                    {
                        D[i, 0] = D[i - 1, 0] + TreeSearchOptions.CostInsertRemoveToken;
                        for (int j = 1; j <= L2Len; ++j)
                        {
                            int SimToken;
                            if (L1 is List<string>)
                                SimToken = TreeSearchComparer.StringsSimilarity(L1[i - 1 + prefix], L2[j - 1 + prefix]);
                            else // L1 is List<OuterContextNode>
                                SimToken = TreeSearchComparer.TokenListsSimilarity(L1[i - 1 + prefix].Name, L2[j - 1 + prefix].Name);
                            double DReplace = 1 - ((double)SimToken) / TreeSearchOptions.Equility; //расстояние, от 0 до 1
                            if (SimToken != TreeSearchOptions.Equility)
                                DReplace = DReplace * TreeSearchOptions.CostReplaceToken;
                            //DToken содержит вес замены лексемы. 0 - лексемы совпадают, значение на отрезке от MinCost до MaxCost - не совпадают
                            D[i, j] = Math.Min(
                                D[i - 1, j] + TreeSearchOptions.CostInsertRemoveToken,
                                Math.Min(
                                    D[i, j - 1] + TreeSearchOptions.CostInsertRemoveToken,
                                    D[i-1, j - 1] + DReplace
                                )
                            );
                        }
                    }
                    Dist = D[L1Len, L2Len];
                }
                //максимальное расстояние для пары списков такой длины
                int MaxDist = MinLen * Math.Min(TreeSearchOptions.CostReplaceToken, TreeSearchOptions.CostInsertRemoveToken * 2) + (MaxLen-MinLen) * TreeSearchOptions.CostInsertRemoveToken;

                //Нормированная степень похожести
                return (int)((MaxDist - Dist) * TreeSearchOptions.Equility / MaxDist);
            }

            public static void ClearDictionary()
            {
                _Similarity.Clear();
            }
        }

        /// <summary>
        /// Реализация вычисления степени похожести строк с использованием метрики Джаро-Винклера
        /// </summary>
        private static class JaroWinklerSimilarity
        {
            static Dictionary<StringPairKey, int> _Similarity = new Dictionary<StringPairKey, int>();

            /* The Winkler modification will not be applied unless the 
             * percent match was at or above the mWeightThreshold percent 
             * without the modification. 
             * Winkler's paper used a default value of 0.7
             */
            private static readonly double mWeightThreshold = 0.7;

            /* Size of the prefix to be concidered by the Winkler modification. 
             * Winkler's paper used a default value of 4
             */
            private static readonly int mNumChars = 4;

            /// <summary>
            /// Returns the Jaro-Winkler distance between the specified  
            /// strings. The distance is symmetric and will fall in the 
            /// range 0 (no match) to 1 (perfect match). 
            /// </summary>
            /// <param name="S1">First String</param>
            /// <param name="S2">Second String</param>
            /// <returns></returns>
            public static int Similarity(string S1, string S2)
            {
                int lLen1 = S1.Length;
                int lLen2 = S2.Length;
                if (lLen1 == 0)
                    return lLen2 == 0 ? TreeSearchOptions.Equility : 0;

                StringPairKey K = new StringPairKey(S1, S2);
                if (_Similarity.ContainsKey(K))
                    return _Similarity[K];

                int Sim = 0;

                int lSearchRange = Math.Max(0, Math.Max(lLen1, lLen2) / 2 - 1);

                // default initialized to false
                bool[] lMatched1 = new bool[lLen1];
                bool[] lMatched2 = new bool[lLen2];

                int lNumCommon = 0;
                for (int i = 0; i < lLen1; ++i)
                {
                    int lStart = Math.Max(0, i - lSearchRange);
                    int lEnd = Math.Min(i + lSearchRange + 1, lLen2);
                    for (int j = lStart; j < lEnd; ++j)
                    {
                        if (lMatched2[j]) continue;
                        if (S1[i] != S2[j])
                            continue;
                        lMatched1[i] = true;
                        lMatched2[j] = true;
                        ++lNumCommon;
                        break;
                    }
                }
                if (lNumCommon == 0)
                {
                    _Similarity.Add(K, 0);
                    return 0;
                };

                int lNumHalfTransposed = 0;
                int k = 0;
                for (int i = 0; i < lLen1; ++i)
                {
                    if (!lMatched1[i]) continue;
                    while (!lMatched2[k]) ++k;
                    if (S1[i] != S2[k])
                        ++lNumHalfTransposed;
                    ++k;
                }
                // System.Diagnostics.Debug.WriteLine("numHalfTransposed=" + numHalfTransposed);
                int lNumTransposed = lNumHalfTransposed / 2;

                // System.Diagnostics.Debug.WriteLine("numCommon=" + numCommon + " numTransposed=" + numTransposed);
                double lNumCommonD = lNumCommon;
                double lWeight = (lNumCommonD / lLen1
                                 + lNumCommonD / lLen2
                                 + (lNumCommon - lNumTransposed) / lNumCommonD) / 3.0;

                if (lWeight <= mWeightThreshold)
                {
                    Sim = (int)(lWeight * TreeSearchOptions.Equility);
                    _Similarity.Add(K, Sim);
                    return Sim;
                }
                int lMax = Math.Min(mNumChars, Math.Min(S1.Length, S2.Length));
                int lPos = 0;
                while (lPos < lMax && S1[lPos] == S2[lPos])
                    ++lPos;
                if (lPos == 0) return (int)lWeight * TreeSearchOptions.Equility;
                Sim = (int)((lWeight + 0.1 * lPos * (1.0 - lWeight)) * TreeSearchOptions.Equility);
                _Similarity.Add(K, Sim);
                return Sim;
            }
        }
    }

    internal class TextSearch
    {
        string[] _lines;
        public TextSearch(string Text)
        {
            _lines = Text.Split('\n');
        }

        /// <summary>
        /// Находит строку из заданного диапазона, наиболее похожую на заданную строку
        /// </summary>
        /// <returns>Пара (Номер строки, степень похожести)</returns>
        public Pair<int, int> Similarity(QUT.Gppg.LexLocation Loc, string pattern)
        {
            //образец пуст - считаем совпадающим
            if (string.IsNullOrWhiteSpace(pattern))
                return new Pair<int,int>(Loc.StartLine, TreeSearchOptions.Equility);
            //разбиваем образец на лексемы
            List<string> PatternTokens = TokenizeString(pattern);
            //если узел занимает одну строку - вычисляем похожесть для нее, возвращаем результат
            if (Loc.StartLine == Loc.EndLine)
            {
                string Text = _lines[Loc.StartLine - 1];//.Substring(Loc.StartColumn, Loc.EndColumn - Loc.StartColumn);
                List<string> TextTokens = TokenizeString(Text);
                int Sim = TreeSearchComparer.TokenListsSimilarity(TextTokens, PatternTokens);
                return new Pair<int, int>(Loc.StartLine, Sim);
            }
            //формируем списки лексем для каждой строки, занимаемой узлом
            List<List<string>> Lines = new List<List<string>>();
            string FirstLine = _lines[Loc.StartLine-1].Substring(Loc.StartColumn);
            string LastLine = _lines[Loc.EndLine-1].Substring(0, Loc.EndColumn);
            Lines.Add(TokenizeString(FirstLine));
            Lines.AddRange(TokenizeStrings(_lines, Loc.StartLine, Loc.EndLine - 2));
            Lines.Add(TokenizeString(LastLine));
            //Находим самую похожую строку
            int MaxSim = TreeSearchComparer.TokenListsSimilarity(Lines[0], PatternTokens);
            int MaxSimIndex = 0;
            for (int i = 1; i < Lines.Count; ++i)
            {
                int Sim = TreeSearchComparer.TokenListsSimilarity(Lines[i], PatternTokens);
                if (Sim > MaxSim)
                {
                    MaxSim = Sim;
                    MaxSimIndex = i;
                }
            }
            return new Pair<int, int>(MaxSimIndex + Loc.StartLine, MaxSim);
        }
        /// <summary>
        /// Разбивает строку на список лексем
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        internal static List<string> TokenizeString(string str)
        {
            List<string> list = new List<string>();
            if (string.IsNullOrEmpty(str))
                return list;
            CommonLexer.Scanner Lexer = new CommonLexer.Scanner();
            Lexer.SetSource(str, 0);
            while (Lexer.yylex() != (int)CommonLexer.Tokens.EOF)
                list.Add(Lexer.yytext);
            return list;
        }
        /// <summary>
        /// Разбивает список строк на списки лексем
        /// </summary>
        /// <param name="Array"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private List<List<string>> TokenizeStrings(string[] Array, int start, int end)
        {
            List<List<string>> result = new List<List<string>>();
            if (end < start)
                return result;
            for (int i = start; i <= end; ++i)
                result.Add(TokenizeString(Array[i]));
            return result;
        }
        /// <summary>
        /// Возращает позицию первого непробельного символа в строке.
        /// </summary>
        /// <param name="Line"></param>
        /// <returns></returns>
        public int GetFirstCharPosAtLine(int Line)
        {
            if (Line <= 0 || Line > _lines.Length)
                return 0;
            string str = _lines[Line - 1];
            return str.Length - str.TrimStart().Length;
        }
    }
    internal static class TreeSearchOptions
    {
        /// <summary>
        /// Результат сравнения объектов принадлежит отрезку от нуля (не похожи) до этого значения (совпадают)
        /// </summary>
        public const int Equility = 1024;

        //Ограничения для следующих двух констант: 
        //      CostInsertRemove > 0
        //      CostReplaceChar > 0;
        //      CostReplaceChar <= 2 * CostInsertRemoveChar
        //По-умолчанию:
        //      CostInsertRemoveChar = 1;
        //      CostReplaceChar = 1;

        /// <summary>
        /// Cтоимость вставки или удаления символа (Метрика Левенштейна)
        /// </summary>
        public const int CostInsertRemoveChar = 1;
        /// <summary>
        /// стоимость замены символа (Метрика Левенштейна)
        /// </summary>
        public const int CostReplaceChar = 1;

        //Ограничения для следующих трех констант:
        //      CostInsertRemoveToken > 0
        //      MinCostReplaceToken >= 0
        //      MaxCostReplaceToken >= MinCostReplaceToken
        //      MaxCostReplaceToken <= 2 * CostInsertRemoveToken
        //По-умолчанию:
        //      CostInsertRemoveToken = 3;
        //      MinCostReplaceToken = 1;
        //      MaxCostReplaceToken = 6;

        /// <summary>
        /// стоимость вставки или удаления лексемы (Метрика Левенштейна)
        /// </summary>
        public const int CostInsertRemoveToken = 3;
        /// <summary>
        /// Стоимость замены лексемы (Метрика Левенштейна)
        /// </summary>
        public const int CostReplaceToken = CostInsertRemoveToken * 2;

        public const int MaxInnerContectCount = 10;

        //
        //public const int CostParserTypeMismatch = 1;     //стоимость несоответствия типов узлов
        public const int CostContextSizeMismatch = 2;    //стоимость несоответствия размера контекста
        public const float NameMismatchCutOff = 0.5f;    //Степень похожести, чтобы считать узел кандидатом к дальнейшему поиску
        public const int MinNameLength = 3;              //Минимальная длина имени, чтобы применять правило NameMismatchCutOff
        public const int ContextDistanceMultiplier = 1;  //множитель для весов действий для контекста
        public const int MaxDistanceToReturn = 10;       //максимальное расстояние, в пределах которого кандидаты отобразятся в окошке
        public const int MaxDistanceDifference = 5;      //Отсечь кандидаты, которые отличаются более чем во столько раз от самого похожего
        //
        
    }    

}
