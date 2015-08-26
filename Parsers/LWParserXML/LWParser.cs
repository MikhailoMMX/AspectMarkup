// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, John Gough, QUT 2005-2014
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.2
// Machine:  BLUEGENE
// DateTime: 25.08.2015 17:34:44
// UserName: MikhailoMMX
// Input file <LWParser.y - 25.08.2015 17:34:43>

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
public enum Tokens {error=2,EOF=3,ID=4,Sign=5,TagStart=6,
    TagEnd=7,_Quest=8,_xml=9,_Excl=10,_DOCTYPE=11,_Slash=12,
    _=13,_BslashDquote=14};

public partial class ValueType
{ 
  public SourceEntity type_SourceEntity;
  public XML_TreeNode type_XML_TreeNode;
  public Token type_Token;
  public XMLDecl type_XMLDecl;
  public DoctypeDcecl type_DoctypeDcecl;
  public Tag type_Tag;
  public Attribute type_Attribute;
  public Text type_Text;
  public String type_String;
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
  private static Rule[] rules = new Rule[56];
  private static State[] states = new State[74];
  private static string[] nonTerms = new string[] {
      "Program", "_ANY2", "_ANY", "XMLDecl", "DoctypeDcecl", "Tag", "CloseTag", 
      "Attribute", "Text", "String", "_ANY3", "_4", "_5", "_Attribute_list", 
      "__ANY_listNE", "__ANY2_listNE", "__ANY3_list", "$accept", };

  static Parser() {
    states[0] = new State(new int[]{6,7},new int[]{-1,1,-12,3,-4,71,-5,72,-6,73}, new int[]{1,0});
    states[1] = new State(new int[]{3,2},new int[]{}, new int[]{1,1});
    states[2] = new State(-1,new int[]{}, new int[]{1,2});
    states[3] = new State(new int[]{6,7,3,-20},new int[]{-4,4,-5,5,-6,6}, new int[]{20,1});
    states[4] = new State(-42,new int[]{}, new int[]{42,2});
    states[5] = new State(-43,new int[]{}, new int[]{43,2});
    states[6] = new State(-44,new int[]{}, new int[]{44,2});
    states[7] = new State(new int[]{8,8,10,42,4,46},new int[]{}, new int[]{21,1,22,1,23,1,24,1});
    states[8] = new State(new int[]{9,9},new int[]{}, new int[]{21,2});
    states[9] = new State(-48,new int[]{-14,10}, new int[]{21,3});
    states[10] = new State(new int[]{8,11,4,32,5,33,6,34,9,36,10,37,11,38,12,39,14,40},new int[]{-8,13,-15,14,-3,41}, new int[]{21,4});
    states[11] = new State(new int[]{7,12,13,-14,4,-14,5,-14,6,-14,8,-14,9,-14,10,-14,11,-14,12,-14,14,-14},new int[]{}, new int[]{21,5,14,1});
    states[12] = new State(-21,new int[]{}, new int[]{21,6});
    states[13] = new State(-49,new int[]{}, new int[]{49,2});
    states[14] = new State(new int[]{13,15,4,32,5,33,6,34,8,35,9,36,10,37,11,38,12,39,14,40},new int[]{-3,31}, new int[]{26,1});
    states[15] = new State(new int[]{14,17},new int[]{-10,16}, new int[]{26,2});
    states[16] = new State(-26,new int[]{}, new int[]{26,3});
    states[17] = new State(-54,new int[]{-17,18}, new int[]{28,1});
    states[18] = new State(new int[]{14,19,4,21,5,22,6,23,7,24,8,25,9,26,10,27,11,28,12,29,13,30},new int[]{-11,20}, new int[]{28,2});
    states[19] = new State(-28,new int[]{}, new int[]{28,3});
    states[20] = new State(-55,new int[]{}, new int[]{55,2});
    states[21] = new State(-29,new int[]{}, new int[]{29,1});
    states[22] = new State(-30,new int[]{}, new int[]{30,1});
    states[23] = new State(-31,new int[]{}, new int[]{31,1});
    states[24] = new State(-32,new int[]{}, new int[]{32,1});
    states[25] = new State(-33,new int[]{}, new int[]{33,1});
    states[26] = new State(-34,new int[]{}, new int[]{34,1});
    states[27] = new State(-35,new int[]{}, new int[]{35,1});
    states[28] = new State(-36,new int[]{}, new int[]{36,1});
    states[29] = new State(-37,new int[]{}, new int[]{37,1});
    states[30] = new State(-38,new int[]{}, new int[]{38,1});
    states[31] = new State(-51,new int[]{}, new int[]{51,2});
    states[32] = new State(-11,new int[]{}, new int[]{11,1});
    states[33] = new State(-12,new int[]{}, new int[]{12,1});
    states[34] = new State(-13,new int[]{}, new int[]{13,1});
    states[35] = new State(-14,new int[]{}, new int[]{14,1});
    states[36] = new State(-15,new int[]{}, new int[]{15,1});
    states[37] = new State(-16,new int[]{}, new int[]{16,1});
    states[38] = new State(-17,new int[]{}, new int[]{17,1});
    states[39] = new State(-18,new int[]{}, new int[]{18,1});
    states[40] = new State(-19,new int[]{}, new int[]{19,1});
    states[41] = new State(-50,new int[]{}, new int[]{50,1});
    states[42] = new State(new int[]{11,43},new int[]{}, new int[]{22,2});
    states[43] = new State(-48,new int[]{-14,44}, new int[]{22,3});
    states[44] = new State(new int[]{7,45,4,32,5,33,6,34,8,35,9,36,10,37,11,38,12,39,14,40},new int[]{-8,13,-15,14,-3,41}, new int[]{22,4});
    states[45] = new State(-22,new int[]{}, new int[]{22,5});
    states[46] = new State(-48,new int[]{-14,47}, new int[]{23,2,24,2});
    states[47] = new State(new int[]{7,48,12,69,4,32,5,33,6,34,8,35,9,36,10,37,11,38,14,40},new int[]{-8,13,-15,14,-3,41}, new int[]{23,3,24,3});
    states[48] = new State(-45,new int[]{-13,49}, new int[]{23,4});
    states[49] = new State(new int[]{6,53,4,59,5,60,8,61,9,62,10,63,11,64,12,65,13,66,14,67},new int[]{-7,50,-6,51,-9,52,-16,57,-2,68}, new int[]{23,5});
    states[50] = new State(-23,new int[]{}, new int[]{23,6});
    states[51] = new State(-46,new int[]{}, new int[]{46,2});
    states[52] = new State(-47,new int[]{}, new int[]{47,2});
    states[53] = new State(new int[]{12,54,4,46},new int[]{}, new int[]{25,1,23,1,24,1});
    states[54] = new State(new int[]{4,55},new int[]{}, new int[]{25,2});
    states[55] = new State(new int[]{7,56},new int[]{}, new int[]{25,3});
    states[56] = new State(-25,new int[]{}, new int[]{25,4});
    states[57] = new State(new int[]{4,59,5,60,8,61,9,62,10,63,11,64,12,65,13,66,14,67,6,-27},new int[]{-2,58}, new int[]{27,1});
    states[58] = new State(-53,new int[]{}, new int[]{53,2});
    states[59] = new State(-2,new int[]{}, new int[]{2,1});
    states[60] = new State(-3,new int[]{}, new int[]{3,1});
    states[61] = new State(-4,new int[]{}, new int[]{4,1});
    states[62] = new State(-5,new int[]{}, new int[]{5,1});
    states[63] = new State(-6,new int[]{}, new int[]{6,1});
    states[64] = new State(-7,new int[]{}, new int[]{7,1});
    states[65] = new State(-8,new int[]{}, new int[]{8,1});
    states[66] = new State(-9,new int[]{}, new int[]{9,1});
    states[67] = new State(-10,new int[]{}, new int[]{10,1});
    states[68] = new State(-52,new int[]{}, new int[]{52,1});
    states[69] = new State(new int[]{7,70,13,-18,4,-18,5,-18,6,-18,8,-18,9,-18,10,-18,11,-18,12,-18,14,-18},new int[]{}, new int[]{24,4,18,1});
    states[70] = new State(-24,new int[]{}, new int[]{24,5});
    states[71] = new State(-39,new int[]{}, new int[]{39,1});
    states[72] = new State(-40,new int[]{}, new int[]{40,1});
    states[73] = new State(-41,new int[]{}, new int[]{41,1});

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-18, new int[]{-1,3});
    rules[2] = new Rule(-2, new int[]{4});
    rules[3] = new Rule(-2, new int[]{5});
    rules[4] = new Rule(-2, new int[]{8});
    rules[5] = new Rule(-2, new int[]{9});
    rules[6] = new Rule(-2, new int[]{10});
    rules[7] = new Rule(-2, new int[]{11});
    rules[8] = new Rule(-2, new int[]{12});
    rules[9] = new Rule(-2, new int[]{13});
    rules[10] = new Rule(-2, new int[]{14});
    rules[11] = new Rule(-3, new int[]{4});
    rules[12] = new Rule(-3, new int[]{5});
    rules[13] = new Rule(-3, new int[]{6});
    rules[14] = new Rule(-3, new int[]{8});
    rules[15] = new Rule(-3, new int[]{9});
    rules[16] = new Rule(-3, new int[]{10});
    rules[17] = new Rule(-3, new int[]{11});
    rules[18] = new Rule(-3, new int[]{12});
    rules[19] = new Rule(-3, new int[]{14});
    rules[20] = new Rule(-1, new int[]{-12});
    rules[21] = new Rule(-4, new int[]{6,8,9,-14,8,7});
    rules[22] = new Rule(-5, new int[]{6,10,11,-14,7});
    rules[23] = new Rule(-6, new int[]{6,4,-14,7,-13,-7});
    rules[24] = new Rule(-6, new int[]{6,4,-14,12,7});
    rules[25] = new Rule(-7, new int[]{6,12,4,7});
    rules[26] = new Rule(-8, new int[]{-15,13,-10});
    rules[27] = new Rule(-9, new int[]{-16});
    rules[28] = new Rule(-10, new int[]{14,-17,14});
    rules[29] = new Rule(-11, new int[]{4});
    rules[30] = new Rule(-11, new int[]{5});
    rules[31] = new Rule(-11, new int[]{6});
    rules[32] = new Rule(-11, new int[]{7});
    rules[33] = new Rule(-11, new int[]{8});
    rules[34] = new Rule(-11, new int[]{9});
    rules[35] = new Rule(-11, new int[]{10});
    rules[36] = new Rule(-11, new int[]{11});
    rules[37] = new Rule(-11, new int[]{12});
    rules[38] = new Rule(-11, new int[]{13});
    rules[39] = new Rule(-12, new int[]{-4});
    rules[40] = new Rule(-12, new int[]{-5});
    rules[41] = new Rule(-12, new int[]{-6});
    rules[42] = new Rule(-12, new int[]{-12,-4});
    rules[43] = new Rule(-12, new int[]{-12,-5});
    rules[44] = new Rule(-12, new int[]{-12,-6});
    rules[45] = new Rule(-13, new int[]{});
    rules[46] = new Rule(-13, new int[]{-13,-6});
    rules[47] = new Rule(-13, new int[]{-13,-9});
    rules[48] = new Rule(-14, new int[]{});
    rules[49] = new Rule(-14, new int[]{-14,-8});
    rules[50] = new Rule(-15, new int[]{-3});
    rules[51] = new Rule(-15, new int[]{-15,-3});
    rules[52] = new Rule(-16, new int[]{-2});
    rules[53] = new Rule(-16, new int[]{-16,-2});
    rules[54] = new Rule(-17, new int[]{});
    rules[55] = new Rule(-17, new int[]{-17,-11});
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
      case 2: // _ANY2 -> ID
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 3: // _ANY2 -> Sign
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 4: // _ANY2 -> _Quest
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 5: // _ANY2 -> _xml
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 6: // _ANY2 -> _Excl
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 7: // _ANY2 -> _DOCTYPE
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 8: // _ANY2 -> _Slash
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 9: // _ANY2 -> _
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 10: // _ANY2 -> _BslashDquote
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 11: // _ANY -> ID
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 12: // _ANY -> Sign
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 13: // _ANY -> TagStart
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 14: // _ANY -> _Quest
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 15: // _ANY -> _xml
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 16: // _ANY -> _Excl
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 17: // _ANY -> _DOCTYPE
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 18: // _ANY -> _Slash
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 19: // _ANY -> _BslashDquote
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 20: // Program -> _4
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddSubItems(ValueStack[ValueStack.Depth-1].type_XML_TreeNode);
		root = CurrentSemanticValue.type_XML_TreeNode;
	}
        break;
      case 21: // XMLDecl -> TagStart, _Quest, _xml, _Attribute_list, _Quest, TagEnd
{
		CurrentSemanticValue.type_XMLDecl = new XMLDecl(new List<string>(), LocationStack[LocationStack.Depth-6].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XMLDecl.Location;
		CurrentSemanticValue.type_XMLDecl.AddValue(ValueStack[ValueStack.Depth-4].type_Token);
		CurrentSemanticValue.type_XMLDecl.AddSubItems(ValueStack[ValueStack.Depth-3].type_XML_TreeNode);

	}
        break;
      case 22: // DoctypeDcecl -> TagStart, _Excl, _DOCTYPE, _Attribute_list, TagEnd
{
		CurrentSemanticValue.type_DoctypeDcecl = new DoctypeDcecl(new List<string>(), LocationStack[LocationStack.Depth-5].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_DoctypeDcecl.Location;
		CurrentSemanticValue.type_DoctypeDcecl.AddValue(ValueStack[ValueStack.Depth-3].type_Token);
		CurrentSemanticValue.type_DoctypeDcecl.AddSubItems(ValueStack[ValueStack.Depth-2].type_XML_TreeNode);

	}
        break;
      case 23: // Tag -> TagStart, ID, _Attribute_list, TagEnd, _5, CloseTag
{
		CurrentSemanticValue.type_Tag = new Tag(new List<string>(), LocationStack[LocationStack.Depth-6].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_Tag.Location;
		CurrentSemanticValue.type_Tag.AddValue(ValueStack[ValueStack.Depth-5].type_Token);
		CurrentSemanticValue.type_Tag.AddSubItems(ValueStack[ValueStack.Depth-4].type_XML_TreeNode);
		CurrentSemanticValue.type_Tag.AddSubItems(ValueStack[ValueStack.Depth-2].type_XML_TreeNode);

	}
        break;
      case 24: // Tag -> TagStart, ID, _Attribute_list, _Slash, TagEnd
{
		CurrentSemanticValue.type_Tag = new Tag(new List<string>(), LocationStack[LocationStack.Depth-5].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_Tag.Location;
		CurrentSemanticValue.type_Tag.AddValue(ValueStack[ValueStack.Depth-4].type_Token);
		CurrentSemanticValue.type_Tag.AddSubItems(ValueStack[ValueStack.Depth-3].type_XML_TreeNode);

	}
        break;
      case 25: // CloseTag -> TagStart, _Slash, ID, TagEnd
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-4].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-2].type_Token);

	}
        break;
      case 26: // Attribute -> __ANY_listNE, _, String
{
		CurrentSemanticValue.type_Attribute = new Attribute(new List<string>(), LocationStack[LocationStack.Depth-3].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_Attribute.Location;
		CurrentSemanticValue.type_Attribute.AddValue(ValueStack[ValueStack.Depth-3].type_XML_TreeNode);
		CurrentSemanticValue.type_Attribute.AddItem(ValueStack[ValueStack.Depth-1].type_String);

	}
        break;
      case 27: // Text -> __ANY2_listNE
{
		CurrentSemanticValue.type_Text = new Text(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_Text.Location;
		CurrentSemanticValue.type_Text.AddValue(ValueStack[ValueStack.Depth-1].type_XML_TreeNode);

	}
        break;
      case 28: // String -> _BslashDquote, __ANY3_list, _BslashDquote
{
		CurrentSemanticValue.type_String = new String(new List<string>(), LocationStack[LocationStack.Depth-3].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_String.Location;
		CurrentSemanticValue.type_String.AddValue(ValueStack[ValueStack.Depth-3].type_Token);
		CurrentSemanticValue.type_String.AddValue(ValueStack[ValueStack.Depth-2].type_XML_TreeNode);
		CurrentSemanticValue.type_String.AddValue(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 29: // _ANY3 -> ID
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 30: // _ANY3 -> Sign
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 31: // _ANY3 -> TagStart
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 32: // _ANY3 -> TagEnd
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 33: // _ANY3 -> _Quest
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 34: // _ANY3 -> _xml
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 35: // _ANY3 -> _Excl
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 36: // _ANY3 -> _DOCTYPE
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 37: // _ANY3 -> _Slash
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 38: // _ANY3 -> _
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Token);
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Token);

	}
        break;
      case 39: // _4 -> XMLDecl
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_XMLDecl);

	}
        break;
      case 40: // _4 -> DoctypeDcecl
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_DoctypeDcecl);

	}
        break;
      case 41: // _4 -> Tag
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Tag);

	}
        break;
      case 42: // _4 -> _4, XMLDecl
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-2].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddSubItems(ValueStack[ValueStack.Depth-2].type_XML_TreeNode);
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_XMLDecl);

	}
        break;
      case 43: // _4 -> _4, DoctypeDcecl
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-2].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddSubItems(ValueStack[ValueStack.Depth-2].type_XML_TreeNode);
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_DoctypeDcecl);

	}
        break;
      case 44: // _4 -> _4, Tag
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-2].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddSubItems(ValueStack[ValueStack.Depth-2].type_XML_TreeNode);
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Tag);

	}
        break;
      case 45: // _5 -> /* empty */
{ CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
        break;
      case 46: // _5 -> _5, Tag
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-2].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddSubItems(ValueStack[ValueStack.Depth-2].type_XML_TreeNode);
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Tag);

	}
        break;
      case 47: // _5 -> _5, Text
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-2].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddSubItems(ValueStack[ValueStack.Depth-2].type_XML_TreeNode);
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Text);

	}
        break;
      case 48: // _Attribute_list -> /* empty */
{ CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
        break;
      case 49: // _Attribute_list -> _Attribute_list, Attribute
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-2].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-2].type_XML_TreeNode);
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_Attribute);
		CurrentSemanticValue.type_XML_TreeNode.AddSubItems(ValueStack[ValueStack.Depth-2].type_XML_TreeNode);
		CurrentSemanticValue.type_XML_TreeNode.AddItem(ValueStack[ValueStack.Depth-1].type_Attribute);

	}
        break;
      case 50: // __ANY_listNE -> _ANY
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_XML_TreeNode);
		CurrentSemanticValue.type_XML_TreeNode.AddSubItems(ValueStack[ValueStack.Depth-1].type_XML_TreeNode);

	}
        break;
      case 51: // __ANY_listNE -> __ANY_listNE, _ANY
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-2].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-2].type_XML_TreeNode);
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_XML_TreeNode);
		CurrentSemanticValue.type_XML_TreeNode.AddSubItems(ValueStack[ValueStack.Depth-2].type_XML_TreeNode);
		CurrentSemanticValue.type_XML_TreeNode.AddSubItems(ValueStack[ValueStack.Depth-1].type_XML_TreeNode);

	}
        break;
      case 52: // __ANY2_listNE -> _ANY2
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-1].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_XML_TreeNode);
		CurrentSemanticValue.type_XML_TreeNode.AddSubItems(ValueStack[ValueStack.Depth-1].type_XML_TreeNode);

	}
        break;
      case 53: // __ANY2_listNE -> __ANY2_listNE, _ANY2
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-2].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-2].type_XML_TreeNode);
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_XML_TreeNode);
		CurrentSemanticValue.type_XML_TreeNode.AddSubItems(ValueStack[ValueStack.Depth-2].type_XML_TreeNode);
		CurrentSemanticValue.type_XML_TreeNode.AddSubItems(ValueStack[ValueStack.Depth-1].type_XML_TreeNode);

	}
        break;
      case 54: // __ANY3_list -> /* empty */
{ CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
        break;
      case 55: // __ANY3_list -> __ANY3_list, _ANY3
{
		CurrentSemanticValue.type_XML_TreeNode = new XML_TreeNode(new List<string>(), LocationStack[LocationStack.Depth-2].Merge(LocationStack[LocationStack.Depth-1]));
        CurrentLocationSpan = CurrentSemanticValue.type_XML_TreeNode.Location;
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-2].type_XML_TreeNode);
		CurrentSemanticValue.type_XML_TreeNode.AddValue(ValueStack[ValueStack.Depth-1].type_XML_TreeNode);
		CurrentSemanticValue.type_XML_TreeNode.AddSubItems(ValueStack[ValueStack.Depth-2].type_XML_TreeNode);
		CurrentSemanticValue.type_XML_TreeNode.AddSubItems(ValueStack[ValueStack.Depth-1].type_XML_TreeNode);

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