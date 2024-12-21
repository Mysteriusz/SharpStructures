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
            RBTInsert(BSTInsert(value));
        }
        public override void Remove(T value)
        {

        }
        #endregion END Main Methods

        #region START Helper Methods
        private RBTNode<T> BSTInsert(T value)
        {
            RBTNode<T>? z = new RBTNode<T>(value);
            RBTNode<T>? y = null;
            RBTNode<T>? x = Root;

            if (x == null)
                return z;

            while (x!.Type != NodeType.Null)
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
            else
                y.Right = z;

            return z;
        }
        private void RBTInsert(RBTNode<T> n)
        {
            RBTNode<T>? p = n.Parent;

            RBTNode<T>? g = null;
            RBTNode<T>? u = null;

            n.Type = NodeType.Red;
            n.Left = RBTNode<T>.NIL;
            n.Right = RBTNode<T>.NIL;

            if (p == null)
            {
                Root = n;
                return;
            }

            int dir = ChildDir(n);

            while (p != null)
            {
                // Case 1
                if (p.Type == NodeType.Black)
                    break;

                // Case 4
                if ((g = p.Parent) == null)
                {
                    p.Type = NodeType.Black;
                    break;
                }

                dir = ChildDir(p);
                u = g[1 - dir];

                // Case 5/6
                if (u!.Type == NodeType.Null || u.Type == NodeType.Black)
                {
                    if (n == p[1 - dir])
                    {
                        RotateDir(p, dir);
                        n = p;
                        p = g[dir];
                    }

                    RotateDirRoot(g, 1 - dir);
                    p!.Type = NodeType.Black;
                    g.Type = NodeType.Red;
                    break;
                }

                // Case 2
                p.Type = NodeType.Black;
                u.Type = NodeType.Black;
                g.Type = NodeType.Red;
                n = g;

                p = n.Parent;
            }

            Root!.Type = NodeType.Black;
        }
        private int ChildDir(RBTNode<T> n)
        {
            if (n.Parent == null)
                return -1;

            return n == n.Parent.Right ? 1 : 0;
        }
        private RBTNode<T> RotateDir(RBTNode<T> n, int dir) => RotateDirRoot(n, dir);
        private RBTNode<T> RotateLeft(RBTNode<T> n) => RotateDirRoot(n, 0);
        private RBTNode<T> RotateRight(RBTNode<T> n) => RotateDirRoot(n, 1);
        private RBTNode<T> RotateDirRoot(RBTNode<T> p, int dir)
        {
            RBTNode<T>? g = p.Parent;
            RBTNode<T>? s = p[1 - dir];
            RBTNode<T>? c = null;

            if (s!.Type == NodeType.Null) throw new InvalidOperationException("Tree structure is invalid or violates Red-Black Tree rules.");

            c = s[dir];
            p[1 - dir] = c; if (c!.Type == NodeType.Null) c.Parent = p;
            s[dir] = p; p.Parent = s;
            s.Parent = g;

            if (g != null)
                g[p == g.Right ? 1 : 0] = s;
            else
                Root = s;

            return s;
        }
        #endregion END Helper Methods
    }
}
