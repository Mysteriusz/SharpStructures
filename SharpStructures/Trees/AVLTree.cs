using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using SharpStructures.Trees.Utilities;

namespace SharpStructures.Trees
{
    /// <summary>
    /// Adelson-Velsky and Landis (AVL) representation that allows efficient searching, insertion, deletion and traversal operations.<br />
    /// Tree is built using <see cref="AVLNode{T}"/> class.
    /// </summary>
    public class AVLTree<T> : DefaultTree<T, AVLNode<T>>
    {
        public AVLTree(AVLNode<T>? root = null, Comparer<T>? comparer = null, TreeTraversalType traversalType = TreeTraversalType.InOrder) : base(root, comparer, traversalType) { }

        public override bool IsValid => TreeHelper<T, AVLNode<T>>.IsValidRec(Root);

        #region START Main Methods
        public override void Add(T value)
        {
            AVLInsert(BSTInsert(value));
        }
        public override void Remove(T value)
        {
            AVLNode<T>? z = TreeHelper<T, AVLNode<T>>.SearchRec(value, this, Root);

            if (z == null)
                return;

            AVLRemove(z);
            BSTRemove(z);
        }
        #endregion END Main Methods

        #region START Helper Methods
        private void AVLRemove(AVLNode<T> n)
        {
            AVLNode<T>? g = null;
            AVLNode<T>? z = null;
            int b = 0;

            for (AVLNode<T>? x = n.Parent; x != null; x = g)
            {
                g = x.Parent;

                if (n == x.Left)
                {
                    if (x.BalanceFactor > 0)
                    {
                        z = x.Right;
                        b = z!.BalanceFactor;

                        if (b < 0)
                            n = RotateRightLeft(x, z);
                        else
                            n = RotateLeft(x, z);
                    }
                    else
                    {
                        if (x.BalanceFactor == 0)
                        {
                            x.BalanceFactor = 1;
                            break;
                        }

                        n = x;
                        n.BalanceFactor = 0;
                        continue;
                    }
                }
                else
                {
                    if (x.BalanceFactor < 0)
                    {
                        z = x.Left;
                        b = z!.BalanceFactor;

                        if (b > 0)
                            n = RotateLeftRight(x, z);
                        else
                            n = RotateRight(x, z);
                    }
                    else
                    {
                        if (x.BalanceFactor == 0)
                        {
                            x.BalanceFactor = -1;
                            break;
                        }

                        n = x;
                        n.BalanceFactor = 0;
                        continue;
                    }
                }

                n.Parent = g;
                if (g != null)
                {
                    if (x == g.Left)
                        g.Left = n;
                    else
                        g.Right = n;
                }
                else
                    Root = n;

                if (b == 0)
                    break;
            }
        }
        private void AVLInsert(AVLNode<T> z)
        {
            AVLNode<T>? g = null;
            AVLNode<T>? n = null;

            for (AVLNode<T>? x = z.Parent; x != null; x = z.Parent)
            {
                if (z == x.Right)
                {
                    if (x.BalanceFactor > 0)
                    {
                        g = x.Parent;
                        if (z.BalanceFactor < 0)
                            n = RotateRightLeft(x, z);
                        else
                            n = RotateLeft(x, z);
                    }
                    else
                    {
                        if (x.BalanceFactor < 0)
                        {
                            x.BalanceFactor = 0;
                            break;
                        }

                        x.BalanceFactor = 1;
                        z = x;
                        continue;
                    }
                }
                else
                {
                    if (x.BalanceFactor < 0)
                    {
                        g = x.Parent;
                        if (z.BalanceFactor > 0)
                            n = RotateLeftRight(x, z);
                        else
                            n = RotateRight(x, z);
                    }
                    else
                    {
                        if (x.BalanceFactor > 0)
                        {
                            x.BalanceFactor = 0;
                            break;
                        }

                        x.BalanceFactor = -1;
                        z = x;
                        continue;
                    }
                }

                n.Parent = g;
                if (g != null)
                {
                    if (x == g.Left)
                        g.Left = n;
                    else
                        g.Right = n;
                }
                else
                    Root = n;
                break;
            }
        }
        private AVLNode<T> BSTInsert(T value)
        {
            AVLNode<T> z = new AVLNode<T>(value);

            AVLNode<T>? y = null;
            AVLNode<T>? x = Root;

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
            else
                y.Right = z;

            return z;
        }
        private void BSTRemove(AVLNode<T> z)
        {
            if (z!.Left == null)
                ShiftNodes(z, z.Right!);
            else if (z.Right == null)
                ShiftNodes(z, z.Left);
            else
            {
                AVLNode<T>? y = Successor(z);

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

        private void ShiftNodes(AVLNode<T> u, AVLNode<T> v)
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
        #region START AVL Operations
        private AVLNode<T> RotateRightLeft(AVLNode<T> x, AVLNode<T> z)
        {
            AVLNode<T> y = z.Left!;

            // T3
            AVLNode<T>? t3 = y.Right;
            z.Left = t3;

            if (t3 != null)
                t3.Parent = z;
            y.Right = z;
            z.Parent = y;

            // T2
            AVLNode<T>? t2 = y.Left;
            x.Right = t2;

            if (t2 != null)
                t2.Parent = x;
            y.Left = x;
            x.Parent = y;

            if (y.BalanceFactor == 0)
            {
                x.BalanceFactor = 0;
                z.BalanceFactor = 0;
            }
            else if (y.BalanceFactor > 0)
            {
                x.BalanceFactor = -1;
                z.BalanceFactor = 0;
            }
            else
            {
                x.BalanceFactor = 0;
                z.BalanceFactor = 1;
            }
            y.BalanceFactor = 0;
            return y;
        }
        private AVLNode<T> RotateLeftRight(AVLNode<T> x, AVLNode<T> z)
        {
            AVLNode<T> y = z.Right!;

            // T3
            AVLNode<T>? t3 = y.Left;
            z.Right = t3;

            if (t3 != null)
                t3.Parent = z;
            y.Left = z;
            z.Parent = y;

            // T2
            AVLNode<T>? t2 = y.Right;
            x.Left = t2;

            if (t2 != null)
                t2.Parent = x;
            y.Right = x;
            x.Parent = y;

            if (y.BalanceFactor == 0)
            {
                x.BalanceFactor = 0;
                z.BalanceFactor = 0;
            }
            else if (y.BalanceFactor < 0)
            {
                x.BalanceFactor = 1;
                z.BalanceFactor = 0;
            }
            else
            {
                x.BalanceFactor = 0;
                z.BalanceFactor = -1;
            }
            y.BalanceFactor = 0;
            return y;
        }
        private AVLNode<T> RotateLeft(AVLNode<T> x, AVLNode<T> z)
        {
            AVLNode<T>? t23 = z.Left;
            x.Right = t23;

            if (t23 != null)
                t23.Parent = x;

            z.Left = x;
            x.Parent = z;

            if (z.BalanceFactor == 0)
            {
                x.BalanceFactor = 1;
                z.BalanceFactor = -1;
            }
            else
            {
                x.BalanceFactor = 0;
                z.BalanceFactor = 0;
            }

            return z;
        }
        private AVLNode<T> RotateRight(AVLNode<T> x, AVLNode<T> z)
        {
            AVLNode<T>? t23 = z.Right;
            x.Left = t23;

            if (t23 != null)
                t23.Parent = x;

            z.Right = x;
            x.Parent = z;

            if (z.BalanceFactor == 0)
            {
                x.BalanceFactor = -1;
                z.BalanceFactor = 1;
            }
            else
            {
                x.BalanceFactor = 0;
                z.BalanceFactor = 0;
            }

            return z;
        }
        #endregion END AVL Operations

        #endregion END Helper Methods
    }
}
