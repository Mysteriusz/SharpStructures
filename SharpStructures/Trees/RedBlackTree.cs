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
    internal class RedBlackTree<T> : DefaultTree<T, RBTNode<T>>
    {
        public RedBlackTree(RBTNode<T>? root = null, Comparer<T>? comparator = null, TreeTraversalType traversalType = TreeTraversalType.InOrder) : base(root, comparator, traversalType) { }

        // Properties
        public override bool IsValid => Root == null || Root.Type == NodeType.Black && TreeHelper<T, RBTNode<T>>.IsValidRec(Root);

        #region START Main Methods
        public override void Add(T value)
        {
            RBTNode<T> z = new RBTNode<T>(value);
            RBTNode<T>? y = null;
            RBTNode<T>? x = Root;

            while (x)
            {
                y = x;
                if (Comparator.Compare(value, x!.Value) < 0)
                    x = x.Left;
                else
                    x = x.Right;
            }

            z.Parent = y;

            if (!y)
                Root = z;
            else if (Comparator.Compare(value, y!.Value) < 0)
                y.Left = z;
            else
                y.Right = z;

            z.Left = RBTNode<T>.NIL;
            z.Right = RBTNode<T>.NIL;
            z.Type = NodeType.Red;

            AddFixup(z);
        }
        public override void Remove(T value)
        {
            RBTNode<T>? z = Find(v => Comparator.Compare(v, value) == 0, Root);

            if (!z) return;

            RBTNode<T>? y = null;
            RBTNode<T>? x = null;

            if (!z!.Left || !z.Right)
                y = z;
            else
                y = Successor(z);

            if (y!.Left)
                x = y.Left;
            else
                x = y.Right;

            x!.Parent = y.Parent;

            if (!y.Parent)
                Root = x;
            else if (y == y.Parent!.Left)
                y.Parent.Left = x;
            else
                y.Parent.Right = x;

            if (y != z) 
                z.Value = y.Value;

            if (y.Type == NodeType.Black)
                RemoveFixup(x);
        }
        #endregion END Main Methods

        #region START Helper Methods
        private void AddFixup(RBTNode<T> x)
        {
            while (x != Root && x.Parent?.Type == NodeType.Red)
            {
                if (x.Parent == x.Parent.Parent?.Left)
                {
                    RBTNode<T>? y = x.Parent.Parent.Right;

                    if (y!.Type == NodeType.Red)
                    {
                        x.Parent.Type = NodeType.Black;
                        y.Type = NodeType.Black;
                        x.Parent.Parent.Type = NodeType.Red;

                        x = x.Parent.Parent;
                    }
                    else
                    {
                        if (x == x.Parent.Right)
                        {
                            x = x.Parent;
                            RotateLeft(x);
                        }

                        x.Parent!.Type = NodeType.Black;
                        x.Parent.Parent!.Type = NodeType.Red;
                        RotateRight(x.Parent.Parent);
                    }
                }
                else
                {
                    RBTNode<T>? y = x.Parent.Parent!.Left;

                    if (y!.Type == NodeType.Red)
                    {
                        x.Parent.Type = NodeType.Black;
                        y.Type = NodeType.Black;
                        x.Parent.Parent.Type = NodeType.Red;

                        x = x.Parent.Parent;
                    }
                    else
                    {
                        if (x == x.Parent.Left)
                        {
                            x = x.Parent;
                            RotateRight(x);
                        }

                        x.Parent!.Type = NodeType.Black;
                        x.Parent.Parent!.Type = NodeType.Red;
                        RotateLeft(x.Parent.Parent);
                    }
                }
            }

            Root!.Type = NodeType.Black;
        }
        private void RemoveFixup(RBTNode<T> x)
        {
            while (x != Root && x.Type == NodeType.Black)
            {
                if (x == x.Parent!.Left)
                {
                    RBTNode<T>? w = x.Parent!.Right;

                    if (w!.Type == NodeType.Red)
                    {
                        w.Type = NodeType.Black;
                        x.Parent.Type = NodeType.Red;
                        RotateLeft(x.Parent);
                        w = x.Parent.Right;
                    }

                    if (w!.Left!.Type == NodeType.Black && w.Right!.Type == NodeType.Black)
                    {
                        w.Type = NodeType.Red;
                        x = x.Parent;
                    }
                    else if (w.Right!.Type == NodeType.Black)
                    {
                        if (w.Right!.Type == NodeType.Black)
                        {
                            w.Left.Type = NodeType.Black;
                            w.Type = NodeType.Red;
                            RotateRight(w);
                            w = x.Parent.Right;
                        }

                        w!.Type = x.Parent.Type;
                        x.Parent.Type = NodeType.Black;
                        w.Right!.Type = NodeType.Black;
                        RotateLeft(x.Parent);
                        x = Root!;
                    }
                }
                else
                {
                    RBTNode<T>? w = x.Parent!.Left;

                    if (w!.Type == NodeType.Red)
                    {
                        w.Type = NodeType.Black;
                        x.Parent.Type = NodeType.Red;
                        RotateRight(x.Parent);
                        w = x.Parent.Left;
                    }

                    if (w!.Right!.Type == NodeType.Black && w.Left!.Type == NodeType.Black)
                    {
                        w.Type = NodeType.Red;
                        x = x.Parent;
                    }
                    else if (w.Left!.Type == NodeType.Black)
                    {
                        if (w.Left!.Type == NodeType.Black)
                        {
                            w.Right.Type = NodeType.Black;
                            w.Type = NodeType.Red;
                            RotateLeft(w);
                            w = x.Parent.Left;
                        }

                        w!.Type = x.Parent.Type;
                        x.Parent.Type = NodeType.Black;
                        w.Left!.Type = NodeType.Black;
                        RotateRight(x.Parent);
                        x = Root!;
                    }
                }

            }
            x.Type = NodeType.Black;
        }

        private void RotateLeft(RBTNode<T> x)
        {
            RBTNode<T> y = x.Right!;
            x.Right = y.Left;
            
            if (y.Left)
                y.Left!.Parent = x;
            
            y.Parent = x.Parent;

            if (!x.Parent)
                Root = y;
            else if (x == x.Parent!.Left)
                x.Parent.Left = y;
            else
                x.Parent.Right = y;

            y.Left = x;
            x.Parent = y;
        }
        private void RotateRight(RBTNode<T> x)
        {
            RBTNode<T> y = x.Left!;
            x.Left = y.Right;

            if (y.Right)
                y.Right!.Parent = x;

            y.Parent = x.Parent;

            if (!x.Parent)
                Root = y;
            else if (x == x.Parent!.Right)
                x.Parent.Right = y;
            else
                x.Parent.Left = y;

            y.Right = x;
            x.Parent = y;
        }
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
