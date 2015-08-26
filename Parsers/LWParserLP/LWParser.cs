// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, John Gough, QUT 2005-2014
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.2
// Machine:  BLUEGENE
// DateTime: 25.08.2015 17:34:39
// UserName: MikhailoMMX
// Input file <LWParser.y - 25.08.2015 17:34:38>

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
public enum Tokens {error=2,EOF=3,LetterDigit=4,String=5,Sign=6,
    _Skip=7,_Rule=8,_Token=9,_Perc=10,_Colon=11};

public partial class ValueType
{ 
  public SourceEntity type_SourceEntity;
  public LP_TreeNode type_LP_TreeNode;
  public Token type_Token;
  public Directive type_Directive;
  public dSkip type_dSkip;
  public dToken type_dToken;
  public dRule type_dRule;
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
  private static Rule[] rules = new Rule[23];
  private static State[] states = new State[29];
  private static string[] nonTerms = new string[] {
      "Program", "ProgramNode", "Tk", "Directive", "dSkip", "dToken", "dRule", 
      "_ANY", "_ProgramNode_list", "_Tk_list", "_Sign_opt", "$accept", };

  static Parser() {
    states[0] = new State(-17,new int[]{-1,1,-9,3}, new int[]{1,0});
    states[1] = new State(new int[]{3,2},new int[]{}, new int[]{1,1});
    states[2] = new State(-1,new int[]{}, new int[]{1,2});
    states[3] = new State(new int[]{10,6,7,15,9,18,8,22,2,28,3,-2},new int[]{-2,4,-4,5,-5,14,-6,17,-7,21}, new int[]{2,1});
    states[4] = new State(-18,new int[]{}, new int[]{18,2});
    states[5] = new State(-3,new int[]{}, new int[]{3,1});
    states[6] = new State(-19,new int[]{-10,7}, new int[]{9,1});
    states[7] = new State(new int[]{4,10,5,11,6,12,11,13,10,-9,7,-9,9,-9,8,-9,2,-9,3,-9},new int[]{-3,8,-8,9}, new int[]{9,2});
    states[8] = new State(-20,new int[]{}, new int[]{20,2});
    states[9] = new State(-8,new int[]{}, new int[]{8,1});
    states[10] = new State(-13,new int[]{}, new int[]{13,1});
    states[11] = new State(-14,new int[]{}, new int[]{14,1});
    states[12] = new State(-15,new int[]{}, new int[]{15,1});
    states[13] = new State(-16,new int[]{}, new int[]{16,1});
    states[14] = new State(-4,new int[]{}, new int[]{4,1});
    states[15] = new State(-19,new int[]{-10,16}, new int[]{10,1});
    states[16] = new State(new int[]{4,10,5,11,6,12,11,13,10,-10,7,-10,9,-10,8,-10,2,-10,3,-10},new int[]{-3,8,-8,9}, new int[]{10,2});
    states[17] = new State(-5,new int[]{}, new int[]{5,1});
    states[18] = new State(new int[]{4,19},new int[]{}, new int[]{11,1});
    states[19] = new State(-19,new int[]{-10,20}, new int[]{11,2});
    states[20] = new State(new int[]{4,10,5,11,6,12,11,13,10,-11,7,-11,9,-11,8,-11,2,-11,3,-11},new int[]{-3,8,-8,9}, new int[]{11,3});
    states[21] = new State(-6,new int[]{}, new int[]{6,1});
    states[22] = new State(new int[]{6,27,4,-21},new int[]{-11,23}, new int[]{12,1});
    states[23] = new State(new int[]{4,24},new int[]{}, new int[]{12,2});
    states[24] = new State(new int[]{11,25},new int[]{}, new int[]{12,3});
    states[25] = new State(-19,new int[]{-10,26}, new int[]{12,4});
    states[26] = new State(new int[]{4,10,5,11,6,12,11,13,10,-12,7,-12,9,-12,8,-12,2,-12,3,-12},new int[]{-3,8,-8,9}, new int[]{12,5});
    states[27] = new State(-22,new int[]{}, new int[]{22,1});
    states[28] = new State(-7,new int[]{}, new int[]{7,1});

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-12, new int[]{-1,3});
    rules[2] = new Rule(-1, new int[]{-9});
    rules[3] = new Rule(-2, new int[]{-4});
    rules[4] = new Rule(-2, new int[]{-5});
    rules[5] = new Rule(-2, new int[]{-6});
    rules[6] = new Rule(-2, new int[]{-7});
    rules[7] = new Rule(-2, new int[]{2});
    rules[8] = new Rule(-3, new int[]{-8});
    rules[9] = new Rule(-4, new int[]{10,-10});
    rules[10] = new Rule(-5, new int[]{7,-10});
    rules[11] = new Rule(-6, new int[]{9,4,-10});
    rules[12] = new Rule(-7, new int[]{8,-11,4,11,-10});
    rules[13] = new Rule(-8, new int[]{4});
    rules[14] = new Rule(-8, new int[]{5});
    rules[15] = new Rule(-8, new int[]{6});
    rules[16] = new Rule(-8, new int[]{11});
    rules[17] = new Rule(-9, new int[]{});
    rules[18] = new Rule(-9, new int[]{-9,-2});
    rules[19] = new Rule(-10, new int[]{});
    rules[20] = new Rule(-10, new int[]{-10,-3});
    rules[21] = new Rule(-11, new int[]{});
    rules[22] = new Rule(-11, new int[]{6});
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
      case 2: // Program -> _ProgramNode_list
{
		CurrentSemanticValue.type_LP_TreeNode = new LP_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_LP_TreeNode.Location;
		CurrentSemanticValue.type_LP_TreeNode.AddSubItems(ValueStack[ValueStack.Depth-1].type_LP_TreeNode);
		root = CurrentSemanticValue.type_LP_TreeNode;
	}
        break;
      case 3: // ProgramNode -> Directive
{
		CurrentSemanticValue.type_LP_TreeNode = new LP_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_LP_TreeNode.Location;
		CurrentSemanticValue.type_LP_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Directive);
errBegin = CurrentLocationSpan;
	}
        break;
      case 4: // ProgramNode -> dSkip
{
		CurrentSemanticValue.type_LP_TreeNode = new LP_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_LP_TreeNode.Location;
		CurrentSemanticValue.type_LP_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_dSkip);
errBegin = CurrentLocationSpan;
	}
        break;
      case 5: // ProgramNode -> dToken
{
		CurrentSemanticValue.type_LP_TreeNode = new LP_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_LP_TreeNode.Location;
		CurrentSemanticValue.type_LP_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_dToken);
errBegin = CurrentLocationSpan;
	}
        break;
      case 6: // ProgramNode -> dRule
{
		CurrentSemanticValue.type_LP_TreeNode = new LP_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_LP_TreeNode.Location;
		CurrentSemanticValue.type_LP_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_dRule);
errBegin = CurrentLocationSpan;
	}
        break;
      case 7: // ProgramNode -> error
{
        CurrentLocationSpan = new LexLocation(errBegin.EndLine, errBegin.EndColumn, LocationStack[LocationStack.Depth-1].StartLine, LocationStack[LocationStack.Depth-1].StartColumn);
        LP_TreeNode err = new LP_TreeNode((Scanner as Scanner).errorMsg, CurrentLocationSpan);
        Errors.Add(err);
        errBegin = CurrentLocationSpan;
    }
        break;
      case 8: // Tk -> _ANY
{
		CurrentSemanticValue.type_LP_TreeNode = new LP_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_LP_TreeNode.Location;
		CurrentSemanticValue.type_LP_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_LP_TreeNode);

	}
        break;
      case 9: // Directive -> _Perc, _Tk_list
{
		CurrentSemanticValue.type_Directive = new Directive(new List<string>(), LocationStack[LocationStack.Depth-2].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_Directive.Location;
		CurrentSemanticValue.type_Directive.Value.Add("Directive");
		CurrentSemanticValue.type_Directive.AddValue(ValueStack[ValueStack.Depth-1].type_LP_TreeNode);

	}
        break;
      case 10: // dSkip -> _Skip, _Tk_list
{
		CurrentSemanticValue.type_dSkip = new dSkip(new List<string>(), LocationStack[LocationStack.Depth-2].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_dSkip.Location;
		CurrentSemanticValue.type_dSkip.AddValue(ValueStack[ValueStack.Depth-2].type_Token);
		CurrentSemanticValue.type_dSkip.AddValue(ValueStack[ValueStack.Depth-1].type_LP_TreeNode);

	}
        break;
      case 11: // dToken -> _Token, LetterDigit, _Tk_list
{
		CurrentSemanticValue.type_dToken = new dToken(new List<string>(), LocationStack[LocationStack.Depth-3].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_dToken.Location;
		CurrentSemanticValue.type_dToken.AddValue(ValueStack[ValueStack.Depth-2].type_Token);

	}
        break;
      case 12: // dRule -> _Rule, _Sign_opt, LetterDigit, _Colon, _Tk_list
{
		CurrentSemanticValue.type_dRule = new dRule(new List<string>(), LocationStack[LocationStack.Depth-5].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_dRule.Location;
		CurrentSemanticValue.type_dRule.AddValue(ValueStack[ValueStack.Depth-3].type_Token);

	}
        break;
      case 13: // _ANY -> LetterDigit
{
		CurrentSemanticValue.type_LP_TreeNode = new LP_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_LP_TreeNode.Location;
		CurrentSemanticValue.type_LP_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_LP_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 14: // _ANY -> String
{
		CurrentSemanticValue.type_LP_TreeNode = new LP_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_LP_TreeNode.Location;
		CurrentSemanticValue.type_LP_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_LP_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 15: // _ANY -> Sign
{
		CurrentSemanticValue.type_LP_TreeNode = new LP_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_LP_TreeNode.Location;
		CurrentSemanticValue.type_LP_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_LP_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 16: // _ANY -> _Colon
{
		CurrentSemanticValue.type_LP_TreeNode = new LP_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_LP_TreeNode.Location;
		CurrentSemanticValue.type_LP_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_LP_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 17: // _ProgramNode_list -> /* empty */
{ CurrentSemanticValue.type_LP_TreeNode = new LP_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
        break;
      case 18: // _ProgramNode_list -> _ProgramNode_list, ProgramNode
{
		CurrentSemanticValue.type_LP_TreeNode = new LP_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-2].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_LP_TreeNode.Location;
		CurrentSemanticValue.type_LP_TreeNode.AddValue(ValueStack[ValueStack.Depth-2].type_LP_TreeNode);
		CurrentSemanticValue.type_LP_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_LP_TreeNode);
		CurrentSemanticValue.type_LP_TreeNode.AddSubItems(ValueStack[ValueStack.Depth-2].type_LP_TreeNode);
		CurrentSemanticValue.type_LP_TreeNode.AddSubItems(ValueStack[ValueStack.Depth-1].type_LP_TreeNode);

	}
        break;
      case 19: // _Tk_list -> /* empty */
{ CurrentSemanticValue.type_LP_TreeNode = new LP_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
        break;
      case 20: // _Tk_list -> _Tk_list, Tk
{
		CurrentSemanticValue.type_LP_TreeNode = new LP_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-2].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_LP_TreeNode.Location;
		CurrentSemanticValue.type_LP_TreeNode.AddValue(ValueStack[ValueStack.Depth-2].type_LP_TreeNode);
		CurrentSemanticValue.type_LP_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_LP_TreeNode);
		CurrentSemanticValue.type_LP_TreeNode.AddSubItems(ValueStack[ValueStack.Depth-2].type_LP_TreeNode);
		CurrentSemanticValue.type_LP_TreeNode.AddSubItems(ValueStack[ValueStack.Depth-1].type_LP_TreeNode);

	}
        break;
      case 21: // _Sign_opt -> /* empty */
{ CurrentSemanticValue.type_LP_TreeNode = new LP_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
        break;
      case 22: // _Sign_opt -> Sign
{
		CurrentSemanticValue.type_LP_TreeNode = new LP_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_LP_TreeNode.Location;
		CurrentSemanticValue.type_LP_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_LP_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

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
