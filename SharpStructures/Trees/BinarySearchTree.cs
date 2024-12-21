using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Xml.Linq;
using SharpStructures.Trees.Utilities;

namespace SharpStructures.Trees
{
    /// <summary>
    /// Indexed Binary Search Tree (IBST) representation that allows efficient searching, insertion, deletion and traversal operations.<br />
    /// Tree is built using <see cref="BSTNode{T}"/> class.
    /// </summary>
    public class BinarySearchTree<T> : DefaultTree<T, BSTNode<T>>
    {
        public BinarySearchTree(BSTNode<T>? root = null, Comparer<T>? comparator = null, TreeTraversalType traversalType = TreeTraversalType.InOrder) : base(root, comparator, traversalType) { }

        public override bool IsValid => TreeHelper<T, BSTNode<T>>.IsValidRec(this, Root);

        #region START Main Methods
        public override void Add(T value)
        {
            BSTNode<T> z = new BSTNode<T>(value);
            BSTNode<T>? y = null;
            BSTNode<T>? x = Root;

            while (x != null)
            {
                y = x;
                if (Comparator.Compare(z.Value, x.Value) < 0)
                    x = x.Left;
                else
                    x = x.Right;
            }

            z.Parent = y;
            if (y == null)
                Root = z;
            else if (Comparator.Compare(z.Value, y.Value) < 0)
                y.Left = z;
            else y.Right = z;
        }
        public override void Remove(T value)
        {
            BSTNode<T>? z = TreeHelper<T, BSTNode<T>>.SearchRec(value, this, Root);

            if (z == null)
                return;

            if (z.Left == null)
                ShiftNodes(z, z.Right!);
            else if (z.Right == null)
                ShiftNodes(z, z.Left);
            else
            {
                BSTNode<T>? y = Successor(z);
                if (y!.Parent != z)
                {
                    ShiftNodes(y, y.Right!);
                    y.Right = z.Right;
                    y.Right.Parent = y;
                }

                ShiftNodes(z, y);
                y.Left = z.Left;
                y.Left.Parent = y;  
            }
        }
        #endregion END Main Methods

        #region START BST Operations
        private void ShiftNodes(BSTNode<T> u, BSTNode<T> v)
        {
            if (u.Parent == null)
                Root = v;
            else if (u == u.Parent.Left)
                u.Parent.Left = v;
            else
                u.Parent.Right = v;

            if (v != null)
                v.Parent = u.Parent;
        }
        #endregion END BST Operations
    }
}
