// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, John Gough, QUT 2005-2014
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.2
// Machine:  BLUEGENE
// DateTime: 24.05.2016 12:09:55
// UserName: MikhailoMMX
// Input file <LWParser.y - 24.05.2016 12:09:55>

// options: no-lines gplex

using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Text;
using QUT.Gppg;
using AspectCore;

namespace LWParser
{
public enum Tokens {error=2,EOF=3,Tk=4,NewLine=5,_StarRopen=6,
    _StarRclose=7};

public partial class ValueType
{ 
  public SourceEntity type_SourceEntity;
  public ANY_TreeNode type_ANY_TreeNode;
  public Token type_Token;
  public CommentStart type_CommentStart;
  public CommentEnd type_CommentEnd;
  public SourceEntityUniformSet type_SourceEntityUniformSet;

}
// Abstract base class for GPLEX scanners
[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
public abstract class ScanBase : AbstractScanner<ValueType,LexLocation> {
  private LexLocation __yylloc = new LexLocation();
  public override LexLocation yylloc { get { return __yylloc; } set { __yylloc = value; } }
  protected virtual bool yywrap() { return true; }
}

// Utility class for encapsulating token information
[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
public class ScanObj {
  public int token;
  public ValueType yylval;
  public LexLocation yylloc;
  public ScanObj( int t, ValueType val, LexLocation loc ) {
    this.token = t; this.yylval = val; this.yylloc = loc;
  }
}

[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
public partial class Parser: ShiftReduceParser<ValueType, LexLocation>
{
#pragma warning disable 649
  private static Dictionary<int, string> aliases;
#pragma warning restore 649
  private static Rule[] rules = new Rule[15];
  private static State[] states = new State[17];
  private static string[] nonTerms = new string[] {
      "Program", "TextLine", "CommentStart", "CommentEnd", "ProgramNode", "_Tk_listNE", 
      "_ProgramNode_list", "$accept", };

  static Parser() {
    states[0] = new State(-13,new int[]{-1,1,-7,3}, new int[]{1,0});
    states[1] = new State(new int[]{3,2},new int[]{}, new int[]{1,1});
    states[2] = new State(-1,new int[]{}, new int[]{1,2});
    states[3] = new State(new int[]{6,6,7,12,4,10,5,15,2,16,3,-5},new int[]{-5,4,-3,5,-4,11,-2,14,-6,8}, new int[]{5,1});
    states[4] = new State(-14,new int[]{}, new int[]{14,2});
    states[5] = new State(-6,new int[]{}, new int[]{6,1});
    states[6] = new State(new int[]{4,10},new int[]{-2,7,-6,8}, new int[]{3,1});
    states[7] = new State(-3,new int[]{}, new int[]{3,2});
    states[8] = new State(new int[]{4,9,6,-2,7,-2,5,-2,2,-2,3,-2},new int[]{}, new int[]{2,1});
    states[9] = new State(-12,new int[]{}, new int[]{12,2});
    states[10] = new State(-11,new int[]{}, new int[]{11,1});
    states[11] = new State(-7,new int[]{}, new int[]{7,1});
    states[12] = new State(new int[]{4,10},new int[]{-2,13,-6,8}, new int[]{4,1});
    states[13] = new State(-4,new int[]{}, new int[]{4,2});
    states[14] = new State(-8,new int[]{}, new int[]{8,1});
    states[15] = new State(-9,new int[]{}, new int[]{9,1});
    states[16] = new State(-10,new int[]{}, new int[]{10,1});

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-8, new int[]{-1,3});
    rules[2] = new Rule(-2, new int[]{-6});
    rules[3] = new Rule(-3, new int[]{6,-2});
    rules[4] = new Rule(-4, new int[]{7,-2});
    rules[5] = new Rule(-1, new int[]{-7});
    rules[6] = new Rule(-5, new int[]{-3});
    rules[7] = new Rule(-5, new int[]{-4});
    rules[8] = new Rule(-5, new int[]{-2});
    rules[9] = new Rule(-5, new int[]{5});
    rules[10] = new Rule(-5, new int[]{2});
    rules[11] = new Rule(-6, new int[]{4});
    rules[12] = new Rule(-6, new int[]{-6,4});
    rules[13] = new Rule(-7, new int[]{});
    rules[14] = new Rule(-7, new int[]{-7,-5});
  }

  protected override void Initialize() {
    this.InitSpecialTokens((int)Tokens.error, (int)Tokens.EOF);
    this.InitStates(states);
    this.InitRules(rules);
    this.InitNonTerminals(nonTerms);
  }

  protected override void DoAction(int action)
  {
    CurrentSemanticValue = new ValueType();
#pragma warning disable 162, 1522
    switch (action)
    {
      case 2: // TextLine -> _Tk_listNE
{
		CurrentSemanticValue.type_ANY_TreeNode = new ANY_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_ANY_TreeNode.Location;
		CurrentSemanticValue.type_ANY_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_ANY_TreeNode);

	}
        break;
      case 3: // CommentStart -> _StarRopen, TextLine
{
		CurrentSemanticValue.type_CommentStart = new CommentStart(new List<string>(), LocationStack[LocationStack.Depth-2].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_CommentStart.Location;
		CurrentSemanticValue.type_CommentStart.AddValue(ValueStack[ValueStack.Depth-1].type_ANY_TreeNode);

	}
        break;
      case 4: // CommentEnd -> _StarRclose, TextLine
{
		CurrentSemanticValue.type_CommentEnd = new CommentEnd(new List<string>(), LocationStack[LocationStack.Depth-2].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_CommentEnd.Location;
		CurrentSemanticValue.type_CommentEnd.AddValue(ValueStack[ValueStack.Depth-1].type_ANY_TreeNode);

	}
        break;
      case 5: // Program -> _ProgramNode_list
{
		CurrentSemanticValue.type_ANY_TreeNode = new ANY_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_ANY_TreeNode.Location;
		CurrentSemanticValue.type_ANY_TreeNode.AddSubItems(ValueStack[ValueStack.Depth-1].type_ANY_TreeNode);
		root = CurrentSemanticValue.type_ANY_TreeNode;
	}
        break;
      case 6: // ProgramNode -> CommentStart
{
		CurrentSemanticValue.type_ANY_TreeNode = new ANY_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_ANY_TreeNode.Location;
		CurrentSemanticValue.type_ANY_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_CommentStart);
errBegin = CurrentLocationSpan;
	}
        break;
      case 7: // ProgramNode -> CommentEnd
{
		CurrentSemanticValue.type_ANY_TreeNode = new ANY_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_ANY_TreeNode.Location;
		CurrentSemanticValue.type_ANY_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_CommentEnd);
errBegin = CurrentLocationSpan;
	}
        break;
      case 8: // ProgramNode -> TextLine
{ CurrentSemanticValue.type_ANY_TreeNode = new ANY_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    errBegin = CurrentLocationSpan;
}
        break;
      case 9: // ProgramNode -> NewLine
{ CurrentSemanticValue.type_ANY_TreeNode = new ANY_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    errBegin = CurrentLocationSpan;
}
        break;
      case 10: // ProgramNode -> error
{
        CurrentLocationSpan = new LexLocation(errBegin.EndLine, errBegin.EndColumn, LocationStack[LocationStack.Depth-1].StartLine, LocationStack[LocationStack.Depth-1].StartColumn);
        ANY_TreeNode err = new ANY_TreeNode((Scanner as Scanner).errorMsg, CurrentLocationSpan);
        Errors.Add(err);
        errBegin = CurrentLocationSpan;
    }
        break;
      case 11: // _Tk_listNE -> Tk
{
		CurrentSemanticValue.type_ANY_TreeNode = new ANY_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_ANY_TreeNode.Location;
		CurrentSemanticValue.type_ANY_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_ANY_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 12: // _Tk_listNE -> _Tk_listNE, Tk
{
		CurrentSemanticValue.type_ANY_TreeNode = new ANY_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-2].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_ANY_TreeNode.Location;
		CurrentSemanticValue.type_ANY_TreeNode.AddValue(ValueStack[ValueStack.Depth-2].type_ANY_TreeNode);
		CurrentSemanticValue.type_ANY_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_ANY_TreeNode.AddSubItems(ValueStack[ValueStack.Depth-2].type_ANY_TreeNode);
		CurrentSemanticValue.type_ANY_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 13: // _ProgramNode_list -> /* empty */
{ CurrentSemanticValue.type_ANY_TreeNode = new ANY_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
        break;
      case 14: // _ProgramNode_list -> _ProgramNode_list, ProgramNode
{
		CurrentSemanticValue.type_ANY_TreeNode = new ANY_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-2].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_ANY_TreeNode.Location;
		CurrentSemanticValue.type_ANY_TreeNode.AddValue(ValueStack[ValueStack.Depth-2].type_ANY_TreeNode);
		CurrentSemanticValue.type_ANY_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_ANY_TreeNode);
		CurrentSemanticValue.type_ANY_TreeNode.AddSubItems(ValueStack[ValueStack.Depth-2].type_ANY_TreeNode);
		CurrentSemanticValue.type_ANY_TreeNode.AddSubItems(ValueStack[ValueStack.Depth-1].type_ANY_TreeNode);

	}
        break;
    }
#pragma warning restore 162, 1522
  }

  protected override string TerminalToString(int terminal)
  {
    if (aliases != null && aliases.ContainsKey(terminal))
        return aliases[terminal];
    else if (((Tokens)terminal).ToString() != terminal.ToString(CultureInfo.InvariantCulture))
        return ((Tokens)terminal).ToString();
    else
        return CharToString((char)terminal);
  }

public LexLocation errBegin = new LexLocation(1,0,1,0);
public List<SourceEntity> Errors = new List<SourceEntity>();
}
}
