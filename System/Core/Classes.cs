using System;
using System.Collections.Generic;
using System.Text;
using QUT.Gppg;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace AspectCore
{
    public class SourceEntity
    {
        /// <summary>
        /// Координаты сущности в исходном коде
        /// </summary>
        public LexLocation Location;
        /// <summary>
        /// Список лексем, являющийся именем данной сущности
        /// </summary>
        public List<string> Value;
        /// <summary>
        /// Дочерние сущности
        /// </summary>
        public List<SourceEntity> Items = new List<SourceEntity>();

        /// <summary>
        /// Информация об однородном наборе подузлов
        /// </summary>
        public UniformSetInfo UniformSet;

        public SourceEntity(List<string> Value, LexLocation Location)
        {
            this.Value = Value;
            this.Location = Location;
        }

        public SourceEntity(string Value, LexLocation Location)
        {
            this.Value = new List<string>();
            this.Value.Add(Value);
            this.Location = Location;
        }
        public SourceEntity()
        {
            this.Value = new List<string>();
            this.Location = new LexLocation();
        }
        public void AddItem(SourceEntity item)
        {
            if (item != null)
                Items.Add(item);
        }
        public void AddSubItems(SourceEntity item)
        {
            if (item != null)
            {
                if (item is SourceEntityUniformSet)
                {
                    this.UniformSet = item.UniformSet;
                    this.UniformSet.Location = item.Location;
                    UniformSet.RangeStart = Items.Count;
                    UniformSet.RangeEnd = UniformSet.RangeStart + item.Items.Count;
                }
                Items.AddRange(item.Items);
            }
        }

        public void AddValue(SourceEntity node)
        {
            if (node != null)
                Value.AddRange(node.Value);
        }
    }

    public class UniformSetInfo
    {
        /// <summary>
        /// Номер первого элемента, принадлежащего однородному набору
        /// </summary>
        public int RangeStart;
        /// <summary>
        /// Номер элемента следующего за последним, принадлежащим набору
        /// </summary>
        public int RangeEnd;
        public string Separator;
        public bool CanBeEmpty;
        public LexLocation Location;

        public UniformSetInfo(int RangeStart, int RangeEnd, string Separator, bool CanBeEmpty)
        {
            this.RangeStart = RangeStart;
            this.RangeEnd = RangeEnd;
            this.Separator = Separator;
            this.CanBeEmpty = CanBeEmpty;
        }
    }

    public class SourceEntityUniformSet:SourceEntity
    {
        public SourceEntityUniformSet(string Separator, bool CanBeEmpty, LexLocation Location)
        {
            this.UniformSet = new UniformSetInfo(0, 0, Separator, CanBeEmpty);
            Value = new List<string>();
            this.Location = Location;
        }
    }


    [Serializable]
    public class AspectFile
    {
        //[XmlAttribute("Version")]
        //public int Version = 2;
        public PointOfInterest TreeRoot;
    }

    public class PointOfInterest : ISerializable
    {
        /// <summary>
        /// Прописывается менеджером парсеров. Путь к файлу, где находится нужная нам точка
        /// </summary>
        public string FileName;
        /// <summary>
        /// Возвращается парсером. Координаты точки в тексте файла
        /// </summary>
        public LexLocation Location;
        /// <summary>
        /// Строка текста, к которой необходимо привязаться
        /// </summary>
        public string Text;
        /// <summary>
        /// Задается пользователем. Имя, отображаемое в дереве аспектов в среде разработки, задается пользователем.
        /// По-умолчанию предлагается конкатенация лексем из Value
        /// </summary>
        public string Name;
        /// <summary>
        /// Возвращается парсером. Вложенные узлы.
        /// В дереве аспектов здесь хранятся дочерние узлы
        /// </summary>
        public List<PointOfInterest> Items = new List<PointOfInterest>();
        /// <summary>
        /// Задается пользователем. Примечание, может содержать развернутое пояснение к узлу.
        /// </summary>
        public string Note;
        /// <summary>
        /// Контекст, описывает путь от узла к корню дерева
        /// 0-й элемент - имя данного узла
        /// </summary>       
        public List<OuterContextNode> Context = new List<OuterContextNode>();
        /// <summary>
        /// Внутренний контекст.
        /// </summary>
        public List<InnerContextNode> InnerContext;
        ///// <summary>
        ///// Имя класса, экземпляром которого был возвращенный парсером узел
        ///// </summary>
        //public string ParserClassName;

        /// <summary>
        /// Проверка принадлежности точки с указанными координатами данной сущности
        /// </summary>
        /// <param name="line">Строка</param>
        /// <param name="col">Столбец</param>
        /// <returns>Принадлежат ли указанные координаты данной сущности</returns>
        public bool IsInside(int line, int col)
        {
            if (Location == null)
                return false;
            if ((line < Location.StartLine) || (line == Location.StartLine && col < Location.StartColumn))
                return false;
            else if ((line > Location.EndLine) || (line == Location.EndLine && col > Location.EndColumn))
                return false;
            return true;
        }
        public PointOfInterest(LexLocation Location)
        {
            this.Location = Location;
        }
        public PointOfInterest()
        {
        }

        public void ApplyInnerContext()
        {
            InnerContext = GetInnerContext(Items);
            if (InnerContext == null || InnerContext.Count <= TreeSearchOptions.MaxInnerContectCount)
                return;
            InnerContext.RemoveRange(TreeSearchOptions.MaxInnerContectCount, InnerContext.Count - TreeSearchOptions.MaxInnerContectCount);
        }
        private List<InnerContextNode> GetInnerContext(List<PointOfInterest> Points)
        {
            if (Points == null || Points.Count == 0)
                return null;
            List<InnerContextNode> Result = new List<InnerContextNode>();
            foreach (PointOfInterest Point in Points)
            {
                InnerContextNode node = new InnerContextNode();
                if (Point.Context != null && Point.Context.Count != 0)
                {
                    node.Name = Point.Context[0].Name;
                    node.Type = Point.Context[0].Type;
                }
                if (Point.Items != null && Point.Items.Count != 0)
                    //Вариант
                    //node.SubNodes = GetInnerContext(Point.Items);  //сохранение в виде дерева
                    Result.AddRange(GetInnerContext(Point.Items));  //сохранение в виде списка
                Result.Add(node);
            }
            return Result;
        }
        /// <summary>
        /// Заменить все поля полями из другой точки, оставив экземпляр класса
        /// </summary>
        /// <param name="Point"></param>
        public void ApplyChanges(PointOfInterest Point)
        {
            this.FileName = Point.FileName;
            this.Items = Point.Items;
            this.Location = Point.Location;
            this.Name = Point.Name;
            this.Note = Point.Note;
            this.Text = Point.Text;
            this.Context = Point.Context;
            this.InnerContext = Point.InnerContext;
        }
        /// <summary>
        /// Создает новый экземпляр, содержащий копию всех полей, кроме Items.
        /// Поле Items ссылается на список из точки-оригинала
        /// </summary>
        /// <returns></returns>
        public PointOfInterest ClonePointAssignItems()
        {
            PointOfInterest result = new PointOfInterest();
            result.FileName = FileName;
            result.Name = Name;
            result.Note = Note;
            result.Items = Items;
            result.Context = new List<OuterContextNode>();
            result.Text = Text;
            result.Location = Location;
            result.InnerContext = InnerContext;
            foreach (OuterContextNode c in Context)
                result.Context.Add(new OuterContextNode(c.Name, c.Type));
            return result;
        }
        /// <summary>
        /// Создает новый экземпляр, содержащий копию всех полей, кроме Items
        /// Поле Items - пустой список
        /// </summary>
        /// <returns></returns>
        public PointOfInterest ClonePointWithoutItems()
        {
            PointOfInterest pt = ClonePointAssignItems();
            pt.Items = new List<PointOfInterest>();
            return pt;
        }
        /// <summary>
        /// Полная копия точки, вместе со всеми подэлементами
        /// </summary>
        /// <returns></returns>
        public PointOfInterest ClonePoint()
        {
            PointOfInterest result = ClonePointWithoutItems();
            foreach (PointOfInterest pt in Items)
                result.Items.Add(pt.ClonePoint());
            return result;
        }


        /// <summary>
        /// Конструктор, реализация требуется для интерфейса ISerializable. Вызывается при десериализации.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public PointOfInterest(SerializationInfo info, StreamingContext context)
        {
            Name = TryGetString(info, "Name");
            FileName = TryGetString(info, "FileName");
            Text = TryGetString(info, "Text");
            Note = TryGetString(info, "Note");
            //Items
            List<PointOfInterest> Items = TryGetValue(info, "Items", typeof(List<PointOfInterest>)) as List<PointOfInterest>;
            //InnerContext
            InnerContext = TryGetValue(info, "InnerContext", typeof(List<InnerContextNode>)) as List<InnerContextNode>;
            //Context
            Context = TryGetValue(info, "Context", typeof(List<OuterContextNode>)) as List<OuterContextNode>;
            if (Context == null)
            {
                //Пробуем прочитать старый формат и преобразовать в новый
                List<List<string>> contextOld = TryGetValue(info, "Context", typeof(List<List<string>>)) as List<List<string>>;
                string C0Type = TryGetString(info, "ParserClassName");
                if (contextOld != null)
                {
                    Context = new List<OuterContextNode>();
                    bool first = true;
                    foreach (List<string> l in contextOld)
                    {
                        OuterContextNode cNew = new OuterContextNode(l, first? C0Type : "");
                        first = false;
                        Context.Add(cNew);
                    }
                }
            }
        }
        /// <summary>
        /// Реализация ISerializable
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("FileName", FileName);
            info.AddValue("Text", Text);
            info.AddValue("Name", Name);
            info.AddValue("Note", Note);
            info.AddValue("Items", Items);
            info.AddValue("Context", Context);
            info.AddValue("InnerContext", InnerContext);
        }
        /// <summary>
        /// Вспомогательный метод для конструктора при десериализации
        /// </summary>
        /// <param name="info"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private object TryGetValue(SerializationInfo info, string name, Type type)
        {
            object result = null;
            try
            {
                result = info.GetValue(name, type);
            }
            catch (Exception)
            { }
            return result;
        }
        /// <summary>
        /// Вспомогательный метод для конструктора при десериализации
        /// </summary>
        /// <param name="info"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private string TryGetString(SerializationInfo info, string name)
        {
            string result = "";
            try
            {
                result = info.GetString(name);
            }
            catch (Exception)
            { }
            return result;
        }
    }

    [Serializable]
    public class InnerContextNode
    {
        public String Type;
        public List<string> Name;
        public List<InnerContextNode> SubNodes;

        /// <summary>
        /// Вспомогательное, для сериализации
        /// </summary>
        [XmlIgnore]
        public bool SubNodesSpecified { get { return SubNodes != null && SubNodes.Count != 0; } }
    }

    [Serializable]
    public class OuterContextNode
    {
        public string Type;
        public List<string> Name;

        public OuterContextNode(List<string> Name, string Type)
        {
            this.Name = Name;
            this.Type = Type;
        }
        public OuterContextNode()
        {
            Name = new List<string>();
            Type = "";
        }
    }


    public class Pair<T, U>
    {
        public Pair()
        {
        }

        public Pair(T first, U second)
        {
            this.First = first;
            this.Second = second;
        }

        public T First { get; set; }
        public U Second { get; set; }
    }

    public abstract class LightweightParserBase
    {
        public abstract SourceEntity Root { get; }
        public abstract List<SourceEntity> Errors { get; }
        public abstract bool Parse();
        public abstract string[] LanguageID { get; }
        public virtual void ProcessTree() { }
    }

    public interface ILightWeightScanner
    {
        object Scanner {get;}
        void SetSource(string source);
        string[] LanguageID { get; }
    }

    public delegate void TraceAction(string Message);
}
