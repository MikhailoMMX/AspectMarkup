// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, John Gough, QUT 2005-2014
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.2
// Machine:  BLUEGENE
// DateTime: 21.05.2016 0:53:44
// UserName: MikhailoMMX
// Input file <LWParser.y - 21.05.2016 0:53:44>

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
public enum Tokens {error=2,EOF=3,LetterDigits=4,Sign=5,tkClassNamespace=6,
    _Copen=7,_Cclose=8,_Scolon=9};

public partial class ValueType
{ 
  public SourceEntity type_SourceEntity;
  public C_TreeNode type_C_TreeNode;
  public Token type_Token;
  public ClassOrNamespace type_ClassOrNamespace;
  public Field type_Field;
  public Method type_Method;
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
  private static Rule[] rules = new Rule[25];
  private static State[] states = new State[30];
  private static string[] nonTerms = new string[] {
      "Program", "_ANY", "Block", "Tk", "ProgramNode", "ClassOrNamespace", "Field", 
      "Method", "_ANY7", "_", "_ProgramNode_list", "_Tk_list", "$accept", };

  static Parser() {
    states[0] = new State(-21,new int[]{-1,1,-11,3}, new int[]{1,0});
    states[1] = new State(new int[]{3,2},new int[]{}, new int[]{1,1});
    states[2] = new State(-1,new int[]{}, new int[]{1,2});
    states[3] = new State(new int[]{2,14,3,-8,6,-23,9,-23,4,-23,5,-23,7,-23},new int[]{-5,4,-6,5,-12,6,-7,12,-8,13}, new int[]{8,1});
    states[4] = new State(-22,new int[]{}, new int[]{22,2});
    states[5] = new State(-9,new int[]{}, new int[]{9,1});
    states[6] = new State(new int[]{6,7,9,19,4,17,5,18,7,21},new int[]{-4,15,-3,20,-9,16}, new int[]{13,1,14,1,15,1});
    states[7] = new State(-23,new int[]{-12,8}, new int[]{13,2});
    states[8] = new State(new int[]{7,9,4,17,5,18},new int[]{-4,15,-9,16}, new int[]{13,3});
    states[9] = new State(-21,new int[]{-11,10}, new int[]{13,4});
    states[10] = new State(new int[]{8,11,2,14,6,-23,9,-23,4,-23,5,-23,7,-23},new int[]{-5,4,-6,5,-12,6,-7,12,-8,13}, new int[]{13,5});
    states[11] = new State(-13,new int[]{}, new int[]{13,6});
    states[12] = new State(-10,new int[]{}, new int[]{10,1});
    states[13] = new State(-11,new int[]{}, new int[]{11,1});
    states[14] = new State(-12,new int[]{}, new int[]{12,1});
    states[15] = new State(-24,new int[]{}, new int[]{24,2});
    states[16] = new State(-7,new int[]{}, new int[]{7,1});
    states[17] = new State(-16,new int[]{}, new int[]{16,1});
    states[18] = new State(-17,new int[]{}, new int[]{17,1});
    states[19] = new State(-14,new int[]{}, new int[]{14,2});
    states[20] = new State(-15,new int[]{}, new int[]{15,2});
    states[21] = new State(-18,new int[]{-10,22}, new int[]{6,1});
    states[22] = new State(new int[]{8,23,4,26,5,27,6,28,9,29,7,21},new int[]{-2,24,-3,25}, new int[]{6,2});
    states[23] = new State(-6,new int[]{}, new int[]{6,3});
    states[24] = new State(-19,new int[]{}, new int[]{19,2});
    states[25] = new State(-20,new int[]{}, new int[]{20,2});
    states[26] = new State(-2,new int[]{}, new int[]{2,1});
    states[27] = new State(-3,new int[]{}, new int[]{3,1});
    states[28] = new State(-4,new int[]{}, new int[]{4,1});
    states[29] = new State(-5,new int[]{}, new int[]{5,1});

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-13, new int[]{-1,3});
    rules[2] = new Rule(-2, new int[]{4});
    rules[3] = new Rule(-2, new int[]{5});
    rules[4] = new Rule(-2, new int[]{6});
    rules[5] = new Rule(-2, new int[]{9});
    rules[6] = new Rule(-3, new int[]{7,-10,8});
    rules[7] = new Rule(-4, new int[]{-9});
    rules[8] = new Rule(-1, new int[]{-11});
    rules[9] = new Rule(-5, new int[]{-6});
    rules[10] = new Rule(-5, new int[]{-7});
    rules[11] = new Rule(-5, new int[]{-8});
    rules[12] = new Rule(-5, new int[]{2});
    rules[13] = new Rule(-6, new int[]{-12,6,-12,7,-11,8});
    rules[14] = new Rule(-7, new int[]{-12,9});
    rules[15] = new Rule(-8, new int[]{-12,-3});
    rules[16] = new Rule(-9, new int[]{4});
    rules[17] = new Rule(-9, new int[]{5});
    rules[18] = new Rule(-10, new int[]{});
    rules[19] = new Rule(-10, new int[]{-10,-2});
    rules[20] = new Rule(-10, new int[]{-10,-3});
    rules[21] = new Rule(-11, new int[]{});
    rules[22] = new Rule(-11, new int[]{-11,-5});
    rules[23] = new Rule(-12, new int[]{});
    rules[24] = new Rule(-12, new int[]{-12,-4});
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
      case 2: // _ANY -> LetterDigits
{
		CurrentSemanticValue.type_C_TreeNode = new C_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_C_TreeNode.Location;
		CurrentSemanticValue.type_C_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_C_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 3: // _ANY -> Sign
{
		CurrentSemanticValue.type_C_TreeNode = new C_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_C_TreeNode.Location;
		CurrentSemanticValue.type_C_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_C_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 4: // _ANY -> tkClassNamespace
{
		CurrentSemanticValue.type_C_TreeNode = new C_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_C_TreeNode.Location;
		CurrentSemanticValue.type_C_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_C_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 5: // _ANY -> _Scolon
{
		CurrentSemanticValue.type_C_TreeNode = new C_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_C_TreeNode.Location;
		CurrentSemanticValue.type_C_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_C_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 6: // Block -> _Copen, _, _Cclose
{
		CurrentSemanticValue.type_C_TreeNode = new C_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-3].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_C_TreeNode.Location;
		CurrentSemanticValue.type_C_TreeNode.Value.Add("Block");

	}
        break;
      case 7: // Tk -> _ANY7
{
		CurrentSemanticValue.type_C_TreeNode = new C_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_C_TreeNode.Location;
		CurrentSemanticValue.type_C_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_C_TreeNode);

	}
        break;
      case 8: // Program -> _ProgramNode_list
{
		CurrentSemanticValue.type_C_TreeNode = new C_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_C_TreeNode.Location;
		CurrentSemanticValue.type_C_TreeNode.AddSubItems(ValueStack[ValueStack.Depth-1].type_C_TreeNode);
		root = CurrentSemanticValue.type_C_TreeNode;
	}
        break;
      case 9: // ProgramNode -> ClassOrNamespace
{
		CurrentSemanticValue.type_C_TreeNode = new C_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_C_TreeNode.Location;
		CurrentSemanticValue.type_C_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_ClassOrNamespace);
errBegin = CurrentLocationSpan;
	}
        break;
      case 10: // ProgramNode -> Field
{
		CurrentSemanticValue.type_C_TreeNode = new C_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_C_TreeNode.Location;
		CurrentSemanticValue.type_C_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Field);
errBegin = CurrentLocationSpan;
	}
        break;
      case 11: // ProgramNode -> Method
{
		CurrentSemanticValue.type_C_TreeNode = new C_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_C_TreeNode.Location;
		CurrentSemanticValue.type_C_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Method);
errBegin = CurrentLocationSpan;
	}
        break;
      case 12: // ProgramNode -> error
{
        CurrentLocationSpan = new LexLocation(errBegin.EndLine, errBegin.EndColumn, LocationStack[LocationStack.Depth-1].StartLine, LocationStack[LocationStack.Depth-1].StartColumn);
        C_TreeNode err = new C_TreeNode((Scanner as Scanner).errorMsg, CurrentLocationSpan);
        Errors.Add(err);
        errBegin = CurrentLocationSpan;
    }
        break;
      case 13: // ClassOrNamespace -> _Tk_list, tkClassNamespace, _Tk_list, _Copen, 
               //                     _ProgramNode_list, _Cclose
{
		CurrentSemanticValue.type_ClassOrNamespace = new ClassOrNamespace(new List<string>(), LocationStack[LocationStack.Depth-6].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_ClassOrNamespace.Location;
		CurrentSemanticValue.type_ClassOrNamespace.AddValue(ValueStack[ValueStack.Depth-6].type_C_TreeNode);
		CurrentSemanticValue.type_ClassOrNamespace.AddValue(ValueStack[ValueStack.Depth-5].type_Token);
		CurrentSemanticValue.type_ClassOrNamespace.AddValue(ValueStack[ValueStack.Depth-4].type_C_TreeNode);
		CurrentSemanticValue.type_ClassOrNamespace.AddSubItems(ValueStack[ValueStack.Depth-2].type_C_TreeNode);

	}
        break;
      case 14: // Field -> _Tk_list, _Scolon
{
		CurrentSemanticValue.type_Field = new Field(new List<string>(), LocationStack[LocationStack.Depth-2].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_Field.Location;
		CurrentSemanticValue.type_Field.AddValue(ValueStack[ValueStack.Depth-2].type_C_TreeNode);

	}
        break;
      case 15: // Method -> _Tk_list, Block
{
		CurrentSemanticValue.type_Method = new Method(new List<string>(), LocationStack[LocationStack.Depth-2].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_Method.Location;
		CurrentSemanticValue.type_Method.AddValue(ValueStack[ValueStack.Depth-2].type_C_TreeNode);

	}
        break;
      case 16: // _ANY7 -> LetterDigits
{
		CurrentSemanticValue.type_C_TreeNode = new C_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_C_TreeNode.Location;
		CurrentSemanticValue.type_C_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_C_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 17: // _ANY7 -> Sign
{
		CurrentSemanticValue.type_C_TreeNode = new C_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_C_TreeNode.Location;
		CurrentSemanticValue.type_C_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_C_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 18: // _ -> /* empty */
{ CurrentSemanticValue.type_C_TreeNode = new C_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
        break;
      case 19: // _ -> _, _ANY
{ CurrentSemanticValue.type_C_TreeNode = new C_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
        break;
      case 20: // _ -> _, Block
{ CurrentSemanticValue.type_C_TreeNode = new C_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
        break;
      case 21: // _ProgramNode_list -> /* empty */
{ CurrentSemanticValue.type_C_TreeNode = new C_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
        break;
      case 22: // _ProgramNode_list -> _ProgramNode_list, ProgramNode
{
		CurrentSemanticValue.type_C_TreeNode = new C_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-2].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_C_TreeNode.Location;
		CurrentSemanticValue.type_C_TreeNode.AddValue(ValueStack[ValueStack.Depth-2].type_C_TreeNode);
		CurrentSemanticValue.type_C_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_C_TreeNode);
		CurrentSemanticValue.type_C_TreeNode.AddSubItems(ValueStack[ValueStack.Depth-2].type_C_TreeNode);
		CurrentSemanticValue.type_C_TreeNode.AddSubItems(ValueStack[ValueStack.Depth-1].type_C_TreeNode);

	}
        break;
      case 23: // _Tk_list -> /* empty */
{ CurrentSemanticValue.type_C_TreeNode = new C_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
        break;
      case 24: // _Tk_list -> _Tk_list, Tk
{
		CurrentSemanticValue.type_C_TreeNode = new C_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-2].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_C_TreeNode.Location;
		CurrentSemanticValue.type_C_TreeNode.AddValue(ValueStack[ValueStack.Depth-2].type_C_TreeNode);
		CurrentSemanticValue.type_C_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_C_TreeNode);
		CurrentSemanticValue.type_C_TreeNode.AddSubItems(ValueStack[ValueStack.Depth-2].type_C_TreeNode);
		CurrentSemanticValue.type_C_TreeNode.AddSubItems(ValueStack[ValueStack.Depth-1].type_C_TreeNode);

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