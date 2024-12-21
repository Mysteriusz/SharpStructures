using System;
using System.Collections.Generic;

namespace SharpStructures.Trees.Utilities
{
    public static class TreeHelper<T, TNode>
        where TNode : TreeNode<T, TNode>
    {
        // Recursive Methods
        public static TNode? SearchRec(T value, IDataTree<T, TNode> tree, TNode? curr)
        {
            if (curr == null)
                return null;

            if (tree.Comparator.Compare(value, curr.Value) == 0)
                return curr;
            else if (tree.Comparator.Compare(value, curr.Value) < 0)
                return SearchRec(value, tree, curr.Left);
            else
                return SearchRec(value, tree, curr.Right);
        }
        public static int GetHeightRec(TNode? node, int currH = 0)
        {
            if (node == null)
                return 0;

            int l = GetHeightRec(node.Left, currH + 1);
            int r = GetHeightRec(node.Right, currH + 1);

            return 1 + Math.Max(l, r);
        }
        public static int GetLeafCountRec(TNode? node, int currC = 0)
        {
            if (node == null)
                return 0;

            if (node.IsLeaf)
                return 1;

            int l = GetLeafCountRec(node.Left, currC);
            int r = GetLeafCountRec(node.Right, currC);

            return r + l;
        }
        public static int GetCountRec(TNode? node)
        {
            if (node == null)
                return 0;

            return 1 + GetCountRec(node.Left) + GetCountRec(node.Right);
        }

        // Validations
        public static bool IsValidRec(BinarySearchTree<T> tree, BSTNode<T>? node)
        {
            if (node == null)
                return true;

            if (node.Left != null && tree.Comparator.Compare(tree.Max(node.Left)!.Value, node.Value) >= 0)
                return false;
            if (node.Right != null && tree.Comparator.Compare(tree.Max(node.Right)!.Value, node.Value) < 0)
                return false;

            return IsValidRec(tree, node.Left) && IsValidRec(tree, node.Right);
        }
        public static bool IsValidRec(AVLNode<T>? node)
        {
            if (node == null)
                return true;

            if (node.BalanceFactor > 1 || node.BalanceFactor < -1)
                return false;

            return IsValidRec(node.Left) && IsValidRec(node.Right);
        }
        public static bool IsValidRec(RBTNode<T>? node)
        {
            if (node == null)
                return true;

            if (node.Type == NodeType.Red)
            {
                if (node.Left != null && node.Left.Type == NodeType.Red) return false;
                if (node.Right != null && node.Right.Type == NodeType.Red) return false;
            }

            return IsValidRec(node.Left) && IsValidRec(node.Right);
        }
    }
}
