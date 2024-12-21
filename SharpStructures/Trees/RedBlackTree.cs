using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpStructures.Trees.Utilities;

namespace SharpStructures.Trees
{
    /// <summary>
    /// Red-Black Tree (RBT) representation that allows efficient searching, insertion, deletion and traversal operations.<br />
    /// Tree is built using <see cref="RBTNode{T}"/> class.
    /// </summary>
    public class RedBlackTree<T> : DefaultTree<T, RBTNode<T>>
    {
        public RedBlackTree(RBTNode<T>? root = null, Comparer<T>? comparator = null, TreeTraversalType traversalType = TreeTraversalType.InOrder) : base(root, comparator, traversalType) { }

        // Properties
        public override bool IsValid => Root == null || Root.Type == NodeType.Black && TreeHelper<T, RBTNode<T>>.IsValidRec(Root);

        #region START Main Methods
        public override void Add(T value)
        {
        }
        public override void Remove(T value)
        {
            RBTNode<T>? z = Find(v => Comparator.Compare(v, value) == 0, Root);

            if (!z) return;

            RBTNode<T>? x = null;
            NodeType original = NodeType.None;

            if (!z!.Left)
            {
                x = z.Right;
                Transplant(z, z.Right!);
            }
            else if (!z!.Right)
            {
                x = z.Left;
                Transplant(z, z.Left!);
            }
            else
            {
                RBTNode<T>? y = Min(z.Right);
                original = y!.Type;
                x = y.Right;

                if (y.Parent == z)
                    x!.Parent = y;
                else
                {
                    Transplant(y, y.Right!);
                    y.Right = z.Right;
                    y.Right!.Parent = y;
                }

                Transplant(z, y);
                y.Left = z.Left;
                y.Left!.Parent = y;
                y.Type = original;
            }

            //if (original == NodeType.Black)
            //    Fixup(x!);
        }
        #endregion END Main Methods

        #region START Helper Methods
        //private void Fixup(RBTNode<T> x)
        //{
        //    while (x != Root && x.Type == NodeType.Black)
        //    {
        //        if (x == x.Parent!.Left)
        //        {
        //            RBTNode<T>? w = x.Parent!.Right;

        //            if (w!.Type == NodeType.Red)
        //            {
        //                w.Type = NodeType.Black;
        //                x.Parent.Type = NodeType.Red;
        //                RotateLeft(x.Parent);
        //                w = x.Parent.Right;
        //            }

        //            if (w!.Left!.Type == NodeType.Black && w.Right!.Type == NodeType.Black)
        //            {
        //                w.Type = NodeType.Red;
        //                x = x.Parent;
        //            }
        //            else
        //            {
        //                if (w.Right!.Type == NodeType.Black)
        //                {
        //                    w.Left.Type = NodeType.Black;
        //                    w.Type = NodeType.Red;
        //                    RotateRight(w);
        //                    w = x.Parent.Right;
        //                }

        //                w!.Type = x.Parent.Type;
        //                x.Parent.Type = NodeType.Black;
        //                w.Right!.Type = NodeType.Black;
        //                RotateLeft(x.Parent);
        //                x = Root!;
        //            }
        //        }
        //        else
        //        {
        //            RBTNode<T>? w = x.Parent!.Left;

        //            if (w!.Type == NodeType.Red)
        //            {
        //                w.Type = NodeType.Black;
        //                x.Parent.Type = NodeType.Red;
        //                RotateRight(x.Parent);
        //                w = x.Parent.Left;
        //            }

        //            if (w!.Right!.Type == NodeType.Black && w.Left!.Type == NodeType.Black)
        //            {
        //                w.Type = NodeType.Red;
        //                x = x.Parent;
        //            }
        //            else
        //            {
        //                if (w.Left!.Type == NodeType.Black)
        //                {
        //                    w.Right.Type = NodeType.Black;
        //                    w.Type = NodeType.Red;
        //                    RotateLeft(w);
        //                    w = x.Parent.Left;
        //                }

        //                w!.Type = x.Parent.Type;
        //                x.Parent.Type = NodeType.Black;
        //                w.Left!.Type = NodeType.Black;
        //                RotateRight(x.Parent);
        //                x = Root!;
        //            }
        //        }

        //    }
        //    x.Type = NodeType.Black;
        //}

        private void Transplant(RBTNode<T> u, RBTNode<T> v)
        {
            if (!u.Parent)
                Root = v;
            else if (u == u.Parent!.Left)
                u.Parent.Left = v;
            else
                u.Parent.Right = v;

            v.Parent = u.Parent;
        }
        #endregion END Helper Methods
    }
}
