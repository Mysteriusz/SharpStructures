using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SharpStructures.Trees
{
    /// <summary>
    /// Adelson-Velsky and Landis (AVL) representation that allows efficient searching, insertion, deletion and traversal operations.<br />
    /// Tree is built using <see cref="AVLNode{T}"/> class.
    /// </summary>
    public class AVLTree<T> : IDataTree<T, AVLNode<T>>
    {
        public AVLTree(AVLNode<T>? root = null, Comparer<T>? comparer = null, TreeTraversalType traversalType = TreeTraversalType.InOrder)
        {
            if (typeof(IComparable<T>).IsAssignableFrom(typeof(T)) || comparer != null)
                Comparator = comparer ?? Comparer<T>.Default;
            else
                throw new ArgumentNullException(nameof(comparer), "Comparer is required for types that do not implement IComparable.");

            TraversalType = traversalType;
            Root = root;
        }
        
        // Properties
        public AVLNode<T>? Root { get; set; }
        public TreeTraversalType TraversalType { get; set; }
        public Comparer<T> Comparator { get; set; }

        public int Count => GetCountRec(Root);
        public int LeafCount => GetLeafCountRec(Root);
        public int Height => GetHeightRec(Root);
        public int Levels => Height + 1;

        public bool IsValid => IsValidRec(Root);
        public bool IsEmpty => Root == null;

        public T this[int index] => GetIndexValue(index);

        #region START Main Methods
        public void Add(T value)
        {
            AVLNode<T> z = BSTInsert(value);
            AVLInsert(z);
        }
        public void AddRange(T[] values)
        {
            for (int i = 0; i < values.Length; i++)
                Add(values[i]); 
        }
        public void Remove(T value)
        {
            AVLNode<T>? z = SearchRec(value, Root);

            if (z == null)
                return;

            AVLRemove(z);
            BSTRemove(z);
        }
        public void RemoveRange(T[] values)
        {
            for (int i = 0; i < values.Length; i++)
                Remove(values[i]);
        }
        public void Clear()
        {
            if (Root == null)
                return;

            Root.Dispose();
            Root = null;
        }

        public bool Contains(T value) => SearchRec(value, Root) != null;
        public IDataTree<T, AVLNode<T>> Clone() => (IDataTree<T, AVLNode<T>>)this.MemberwiseClone();
        #endregion END Main Methods

        #region START Traversal Methods
        public T GetIndexValue(int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), "Non-negative number required.");

            if (index >= Count)
                throw new IndexOutOfRangeException();

            switch (TraversalType)
            {
                case TreeTraversalType.InOrder:
                    return InOrderTraversalIndexRange(Root, index, [0], 1, []).First();
                case TreeTraversalType.PreOrder:
                    return PreOrderTraversalIndexRange(Root, index, [0], 1, []).First();
                case TreeTraversalType.PostOrder:
                    return PostOrderTraversalIndexRange(Root, index, [0], 1, []).First();
                default:
                    throw new InvalidOperationException("Unknown TreeTraversalType");
            }
        }

        public T? Find(Func<T, bool> predicate) => Find(predicate, Root);
        public T? Find(Func<T, bool> predicate, AVLNode<T>? node)
        {
            if (node == null)
                return default;

            if (predicate(node.Value))
                return node.Value;

            T? leftResult = Find(predicate, node.Left);
            if (leftResult != null)
                return leftResult;

            return Find(predicate, node.Right);
        }

        public IEnumerable<T> Traverse()
        {
            switch (TraversalType)
            {
                case TreeTraversalType.InOrder:
                    return InOrderTraversal();
                case TreeTraversalType.PreOrder:
                    return PreOrderTraversal();
                case TreeTraversalType.PostOrder:
                    return PostOrderTraversal();
                default:
                    throw new InvalidOperationException("Unknown TreeTraversalType");
            }
        }
        public IEnumerable<T> GetRange(int index, int count)
        {
            if (index < 0 || count < 0)
                throw new ArgumentOutOfRangeException(nameof(index), "Non-negative number required.");

            if (index + count >= Count)
                throw new IndexOutOfRangeException();

            switch (TraversalType)
            {
                case TreeTraversalType.InOrder:
                    return InOrderTraversalIndexRange(Root, index, [0], count, []);
                case TreeTraversalType.PreOrder:
                    return PreOrderTraversalIndexRange(Root, index, [0], count, []);
                case TreeTraversalType.PostOrder:
                    return PostOrderTraversalIndexRange(Root, index, [0], count, []);
                default:
                    throw new InvalidOperationException("Unknown TreeTraversalType");
            }
        }

        public IEnumerable<T> InOrderTraversal() => InOrderTraversal(Root, []);
        public IEnumerable<T> PreOrderTraversal() => PreOrderTraversal(Root, []);
        public IEnumerable<T> PostOrderTraversal() => PostOrderTraversal(Root, []);

        private IEnumerable<T> InOrderTraversal(AVLNode<T>? current, List<T> result)
        {
            if (current == null)
                return result;

            InOrderTraversal(current.Left, result);
            result.Add(current.Value);
            InOrderTraversal(current.Right, result);

            return result;
        }
        private IEnumerable<T> PreOrderTraversal(AVLNode<T>? current, List<T> result)
        {
            if (current == null)
                return result;

            result.Add(current.Value);
            PreOrderTraversal(current.Left, result);
            PreOrderTraversal(current.Right, result);

            return result;
        }
        private IEnumerable<T> PostOrderTraversal(AVLNode<T>? current, List<T> result)
        {
            if (current == null)
                return result;

            PostOrderTraversal(current.Left, result);
            PostOrderTraversal(current.Right, result);
            result.Add(current.Value);

            return result;
        }

        private IEnumerable<T> InOrderTraversalIndexRange(AVLNode<T>? current, int index, int[] currIndex, int count, List<T> result)
        {
            if (current == null || currIndex[0] >= index + count)
                return result;

            InOrderTraversalIndexRange(current.Left, index, currIndex, count, result);

            if (currIndex[0] >= index && currIndex[0] < index + count)
                result.Add(current.Value);

            currIndex[0]++;

            return InOrderTraversalIndexRange(current.Right, index, currIndex, count, result);
        }
        private IEnumerable<T> PreOrderTraversalIndexRange(AVLNode<T>? current, int index, int[] currIndex, int count, List<T> result)
        {
            if (current == null || currIndex[0] >= index + count)
                return result;

            if (currIndex[0] >= index && currIndex[0] < index + count)
                result.Add(current.Value);

            currIndex[0]++;

            PreOrderTraversalIndexRange(current.Left, index, currIndex, count, result);
            return PreOrderTraversalIndexRange(current.Right, index, currIndex, count, result);
        }
        private IEnumerable<T> PostOrderTraversalIndexRange(AVLNode<T>? current, int index, int[] currIndex, int count, List<T> result)
        {
            if (current == null || currIndex[0] >= index + count)
                return result;

            PostOrderTraversalIndexRange(current.Left, index, currIndex, count, result);
            PostOrderTraversalIndexRange(current.Right, index, currIndex, count, result);

            if (currIndex[0] >= index && currIndex[0] < index + count)
                result.Add(current.Value);

            currIndex[0]++;

            return result;
        }

        #region START PathFinding
        public IEnumerable<T> DFS(T target) => DFSRec(target, Root, [], []);
        private IEnumerable<T> DFSRec(T target, AVLNode<T>? node, HashSet<AVLNode<T>> seen, List<T> path)
        {
            if (node == null)
                return path;

            if (Comparator.Compare(node.Value, target) == 0)
            {
                path.Add(node.Value);
                return path;
            }

            if (!seen.Contains(node))
            {
                seen.Add(node);
                path.Add(node.Value);

                IEnumerable<T> leftPath = DFSRec(target, node.Left, seen, new List<T>(path));
                if (leftPath.Contains(target))
                    return leftPath;

                IEnumerable<T> rightPath = DFSRec(target, node.Right, seen, new List<T>(path));
                if (rightPath.Contains(target))
                    return rightPath;
            }

            return Enumerable.Empty<T>();
        }
        #endregion END PathFinding

        #endregion END Traversal Methods

        #region START Tree Navigation
        // Generic T Return Types
        public T? Max()
        {
            AVLNode<T>? max = Max(Root);
            return max != null ? max.Value : default;
        }
        public T? Min()
        {
            AVLNode<T>? min = Min(Root);
            return min != null ? min.Value : default;
        }
        public T? Successor()
        {
            AVLNode<T>? succ = Successor(Root);
            return succ != null ? succ.Value : default;
        }
        public T? Predecessor()
        {
            AVLNode<T>? pred = Predecessor(Root);
            return pred != null ? pred.Value : default;
        }

        // Main Methods
        public AVLNode<T>? Max(AVLNode<T>? node)
        {
            if (node == null)
                return null;

            while (node.Right != null)
                node = node.Right;

            return node;
        }
        public AVLNode<T>? Min(AVLNode<T>? node)
        {
            if (node == null)
                return null;

            while (node.Left != null)
                node = node.Left;

            return node;
        }
        public AVLNode<T>? Successor(AVLNode<T>? node)
        {
            if (node == null)
                return null;

            AVLNode<T> x = node;

            if (x.Right != null)
                return Min(x.Right);

            AVLNode<T>? y = x.Parent;
            while (y != null && x == y.Right)
            {
                x = y;
                y = y.Parent;
            }

            return y;
        }
        public AVLNode<T>? Predecessor(AVLNode<T>? node)
        {
            if (node == null)
                return null;

            AVLNode<T> x = node;

            if (x.Left != null)
                return Max(x.Left);

            AVLNode<T>? y = x.Parent;
            while (y != null && x == y.Left)
            {
                x = y;
                y = y.Parent;
            }

            return y;
        }

        #endregion END Tree Navigation

        #region START Conversion Methods
        public T[] ToArray()
        {
            return Traverse().ToArray();
        }
        public IEnumerable<T> AsEnumerable()
        {
            return Traverse();
        }
        public IEnumerator<T> GetEnumerator()
        {
            return Traverse().GetEnumerator();
        }
        public ILookup<T, T> ToLookup(Func<T, T> keySelector)
        {
            return Traverse().ToLookup(keySelector);
        }
        public List<T> ToList()
        {
            return Traverse().ToList();
        }
        public LinkedList<T> ToLinkedList()
        {
            LinkedList<T> list = new LinkedList<T>();
            foreach (T value in Traverse())
            {
                list.AddLast(value);
            }

            return list;
        }
        public ImmutableList<T> ToImmutableList()
        {
            return Traverse().ToImmutableList();
        }
        public HashSet<T> ToHashSet()
        {
            return Traverse().ToHashSet();
        }
        public Stack<T> ToStack()
        {
            Stack<T> stack = new Stack<T>();

            foreach (T value in Traverse())
                stack.Push(value);

            return stack;
        }
        public Queue<T> ToQueue()
        {
            Queue<T> queue = new Queue<T>();

            foreach (T value in Traverse())
                queue.Enqueue(value);

            return queue;
        }
        #endregion END Conversion Methods

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
        private AVLNode<T>? SearchRec(T value, AVLNode<T>? curr)
        {
            if (curr == null)
                return null;

            if (Comparator.Compare(value, curr.Value) == 0)
                return curr;
            else if (Comparator.Compare(value, curr.Value) < 0)
                return SearchRec(value, curr.Left);
            else
                return SearchRec(value, curr.Right);
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

        #region START Utils
        private int GetCountRec(AVLNode<T>? node)
        {
            if (node == null)
                return 0;

            return 1 + GetCountRec(node.Left) + GetCountRec(node.Right);
        }
        private int GetHeightRec(AVLNode<T>? node, int currH = 0)
        {
            if (node == null)
                return 0;

            int l = GetHeightRec(node.Left, currH + 1);
            int r = GetHeightRec(node.Right, currH + 1);

            return 1 + Math.Max(l, r);
        }
        private int GetLeafCountRec(AVLNode<T>? node, int currC = 0)
        {
            if (node == null)
                return 0;

            if (node.IsLeaf)
                return 1;

            int l = GetLeafCountRec(node.Left, currC);
            int r = GetLeafCountRec(node.Right, currC);

            return r + l;
        }
        private bool IsValidRec(AVLNode<T>? node)
        {
            if (node == null)
                return true;

            if (node.BalanceFactor > 1 || node.BalanceFactor < -1)
                return false;

            return IsValidRec(node.Left) && IsValidRec(node.Right);
        }
        #endregion END Utils
    }
}
