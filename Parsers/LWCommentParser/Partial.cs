using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspectCore;

namespace LWParser
{
    public partial class LightweightParser : LightweightParserBase
    {
        public override void ProcessTree()
        {
            if (Root == null || Root.Items.Count == 0)
                return;
            List<ANY_TreeNode> Nodes = new List<ANY_TreeNode>();
            List<SourceEntity> Result = new List<SourceEntity>();
            foreach (ANY_TreeNode node in Root.Items)
            {
                if (node.Value.Count == 0)
                    continue;
                if (node is CommentStart)
                    Nodes.Add(node);
                if (node is CommentEnd)
                {
                    int startIndex;
                    for (startIndex = Nodes.Count - 1; startIndex >= 0; --startIndex)
                        if (node.Value[0] == Nodes[startIndex].Value[0])
                            break;
                    if (startIndex < 0)
                        continue;
                    ANY_TreeNode newNode = new ANY_TreeNode(Nodes[startIndex].Value, Nodes[startIndex].Location.Merge(node.Location));
                    for (int i = startIndex + 1; i < Nodes.Count; ++i)
                        if (Nodes[i].GetType() == typeof(ANY_TreeNode))
                            newNode.Items.Add(Nodes[i]);
                    Nodes.RemoveRange(startIndex, Nodes.Count - startIndex);
                    Nodes.Add(newNode);
                }
            }
            foreach (ANY_TreeNode node in Nodes)
                if (node.GetType() == typeof(ANY_TreeNode))
                    Result.Add(node);
            Root.Items = Result;
        }
    }
}
