using System;
using System.Collections.Generic;
using System.Text;
using QUT.Gppg;

namespace AspectCore
{
    public class UndoEngine
    {
        private Stack<UndoUnit> UndoStack = new Stack<UndoUnit>();
        private Stack<UndoUnit> RedoStack = new Stack<UndoUnit>();

        /// <summary>
        /// Создавет экземпляр UndoAction по нынешнему состоянию точки
        /// При kind == move, параметр newParent обязателен
        /// </summary>
        /// <param name="point">Исходная точка</param>
        /// <param name="parent">Исходный родитель точки</param>
        /// <param name="kind">Тип действия</param>
        private UndoUnit CreateUndoUnit(PointOfInterest point, PointOfInterest parent, ActionKind kind, PointOfInterest newParent = null)
        {
            if (kind == ActionKind.Move && newParent == null)
                throw new ArgumentException();
            UndoUnit undo = new UndoUnit();
            undo.Kind = kind;
            undo.OriginalParent = parent;
            undo.OriginalPosition = parent == null ? 0 : parent.Items.IndexOf(point);
            undo.OriginalPointRef = point;
            if (kind == ActionKind.Edit)
                undo.OriginalPointContent = point.ClonePointAssignItems();
            if (kind == ActionKind.Move)
                undo.OriginalPointContent = newParent;

            return undo;
        }

        /// <summary>
        /// Сохраняет состояние точки перед действием
        /// Вызывается при любом изменении аспектного дерева
        /// </summary>
        /// <param name="point">Исходная точка</param>
        /// <param name="parent">Исходный родитель точки</param>
        /// <param name="kind">Тип действия</param>
        public void SavePointState(PointOfInterest point, PointOfInterest parent, ActionKind kind, PointOfInterest newParent = null)
        {
            UndoStack.Push(CreateUndoUnit(point, parent, kind, newParent));
            RedoStack.Clear();
        }

        /// <summary>
        /// Создает описание обратного действия
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        private UndoUnit CreateReverseUndoAction(UndoUnit unit)
        {
            UndoUnit result = new UndoUnit();
            if (unit.Kind == ActionKind.Add)
            {
                result.Kind = ActionKind.Remove;
                result.OriginalPointRef = unit.OriginalPointRef;
                result.OriginalParent = unit.OriginalParent;
                result.OriginalPosition = unit.OriginalParent.Items.IndexOf(unit.OriginalPointRef);
            }
            else if (unit.Kind == ActionKind.Remove)
            {
                result.Kind = ActionKind.Add;
                result.OriginalPointRef = unit.OriginalPointRef;
                result.OriginalParent = unit.OriginalParent;
                result.OriginalPosition = unit.OriginalPosition;
            }
            else if (unit.Kind == ActionKind.Move)
            {
                result.Kind = ActionKind.Move;
                result.OriginalPointRef = unit.OriginalPointRef;
                result.OriginalParent = unit.OriginalPointContent;
                result.OriginalPointContent = unit.OriginalParent;
                result.OriginalPosition = unit.OriginalPointContent.Items.IndexOf(unit.OriginalPointRef);
            }
            else //if (result.Kind == ActionKind.Edit)
            {
                result.Kind = ActionKind.Edit;
                result.OriginalPointRef = unit.OriginalPointRef;
                result.OriginalPointContent = unit.OriginalPointRef.ClonePointAssignItems();
            }
            return result;
        }

        /// <summary>
        /// Возвращает описание отменяемого действия, попутно сохраняя состояние точки для возможного повторения действия
        /// </summary>
        /// <returns></returns>
        public UndoUnit Undo()
        {
            if (UndoStack.Count == 0)
                return null;
            UndoUnit result = UndoStack.Pop();
            RedoStack.Push(CreateReverseUndoAction(result));
            return result;
        }

        /// <summary>
        /// Возвращает описание повторяемого действия, попутно сохраняя состояние точки для возможной отмены действия
        /// </summary>
        /// <returns></returns>
        public UndoUnit Redo()
        {
            if (RedoStack.Count == 0)
                return null;
            UndoUnit result = RedoStack.Pop();
            UndoStack.Push(CreateReverseUndoAction(result));
            return result;
        }

        /// <summary>
        /// Очищает стек отмены и стек повторения.
        /// </summary>
        public void Clear()
        {
            UndoStack.Clear();
            RedoStack.Clear();
        }
        
        #region !Aspect UndoButtonsEnabling
        public bool HasUndoActions()
        {
            return UndoStack.Count > 0;
        }

        public bool HasRedoActions()
        {
            return RedoStack.Count > 0;
        }
        #endregion

        //public void Undo(PointOfInterest newPoint = null)
    }

    //!Aspect UndoActionKind
    /// <summary>
    /// Описывает совершенное пользователем действие
    /// </summary>
    public enum ActionKind {Add, Remove, Move, Edit}

    /// <summary>
    /// Описание одного действия
    /// </summary>
    public class UndoUnit
    {
        /// <summary>
        /// Тип действия
        /// </summary>
        public ActionKind Kind;
        /// <summary>
        /// Ссылка на точку, затронутую изменениями
        /// </summary>
        public PointOfInterest OriginalPointRef;
        /// <summary>
        /// Исходное состояние точки
        /// Используется при редактировании и удалении точки для хранения исходного содержимого
        /// И при перемещении для хранения нового родителя
        /// </summary>
        public PointOfInterest OriginalPointContent;
        /// <summary>
        /// Родительская точка до выполнения действия
        /// </summary>
        public PointOfInterest OriginalParent;
        /// <summary>
        /// Положение данной точки в коллекции до выполнения действия
        /// </summary>
        public int OriginalPosition;
    }
}
