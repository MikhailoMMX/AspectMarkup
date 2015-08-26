using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio;

namespace MMX.AspectVSPackage.Helpers
{
    /// <summary>
    /// Класс содержит вспомогательные функции для работы с редактором Visual Studio
    /// - Навигация к указанным координатам
    /// - Вставка текста в позиции курсора или вокруг выделенного участка текста
    /// </summary>
    class EditorHelper
    {
        
        ///// <summary>
        ///// Возвращает отступ в начале строки-образца, копируя все пробельные символы
        ///// </summary>
        ///// <param name="str"></param>
        ///// <returns></returns>
        //private static string GetIndent(string str)
        //{
        //    string result = "";
        //    int i = 0;
        //    while (char.IsWhiteSpace(str[i]))
        //    {
        //        result += str[i];
        //        i += 1;
        //    }
        //    return result;
        //}

        ///// <summary>
        ///// Вставляет текст в указанную позицию, добавляя слева и справа переносы строк и выравнивание при необходимости
        ///// </summary>
        ///// <param name="dte"></param>
        ///// <param name="text"></param>
        ///// <param name="line"></param>
        ///// <param name="column"></param>
        //public static void InsertLine(DTE2 dte, string text, int line, int column)
        //{
        //    EnvDTE.TextDocument objTextDoc = GetCurrentTextDocument(dte);
        //    if (objTextDoc == null)
        //        return;

        //    EditPoint objEditPt = objTextDoc.StartPoint.CreateEditPoint();
        //    objEditPt.LineDown(line - 1);
        //    string oldLine = objEditPt.GetText(objEditPt.LineLength);
        //    objEditPt.CharRight(column - 1);

        //    string LineStart = oldLine.Substring(0, column - 1);
        //    string lineEnd = oldLine.Substring(column - 1);
        //    if (LineStart.Trim() != "")
        //        text = Environment.NewLine + GetIndent(oldLine) + text;
        //    if (lineEnd.Trim() != "")
        //        text += Environment.NewLine + GetIndent(oldLine);

        //    objEditPt.ReplaceText(0, text, (int)vsEPReplaceTextOptions.vsEPReplaceTextKeepMarkers);
        //}

        //public static void InsertPragma(DTE2 dte, string pragma)
        //{
        //    EnvDTE.TextDocument objTextDoc = GetCurrentTextDocument(dte);
        //    if (objTextDoc == null)
        //        return;

        //    int startLine = objTextDoc.Selection.TopPoint.Line;
        //    int startCol = objTextDoc.Selection.TopPoint.LineCharOffset;
        //    int endLine = objTextDoc.Selection.BottomPoint.Line;
        //    int endCol = objTextDoc.Selection.BottomPoint.LineCharOffset;

        //    if (startLine != endLine || startCol != endCol)
        //    {
        //        InsertLine(dte, Strings.PragmaEnd, endLine, endCol);
        //        InsertLine(dte, Strings.PragmaBeginPrefix + pragma, startLine, startCol);
        //    }
        //    else
        //        InsertLine(dte, Strings.PragmaAsCommentPrefix + pragma, startLine, startCol);
        //}
    }
}
