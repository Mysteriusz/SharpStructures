﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace SharpStructures.Trees
{
    /// <summary>
    /// Indexed Binary Search Tree (IBST) representation that allows efficient searching, insertion, deletion and traversal operations.<br />
    /// Tree is built using <see cref="BSTNode{T}"/> class.
    /// </summary>
    public class BinarySearchTree<T> : IDataTree<T, BSTNode<T>>
    {
        public BinarySearchTree(BSTNode<T>? root = null, Comparer<T>? comparer = null, TreeTraversalType traversalType = TreeTraversalType.InOrder) 
        {
            if (typeof(IComparable<T>).IsAssignableFrom(typeof(T)) || comparer != null)
                Comparator = comparer ?? Comparer<T>.Default;
            else
                throw new ArgumentNullException(nameof(comparer), "Comparer is required for types that do not implement IComparable.");

            TraversalType = traversalType;
            Root = root;
        }
        
        // Properties
        public BSTNode<T>? Root { get; set; }
        public TreeTraversalType TraversalType { get; set; }
        public Comparer<T> Comparator { get; set; }

        public int Height => GetHeightRec(Root);
        public int LeafCount => GetLeafCountRec(Root);
        public int Count => GetCountRec(Root);
        public int Levels => Height + 1;

        public bool IsValid => IsValidRec(Root);
        public bool IsEmpty => Root == null;

        public T this[int index] => GetIndexValue(index);

        #region START Main Methods
        public void Add(T value)
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
            else
                y.Right = z;
        }
        public void AddRange(T[] values)
        {
            for (int i = 0; i < values.Length; i++)
                Add(values[i]);
        }
        public void Remove(T value)
        {
            BSTNode<T>? z = SearchRec(value, Root);

            if (z == null)
                throw new ArgumentException(nameof(BinarySearchTree<T>) + "does not contain value: " + value);

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
        public IDataTree<T, BSTNode<T>> Clone() => (IDataTree<T, BSTNode<T>>)this.MemberwiseClone();
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
        public T? Find(Func<T, bool> predicate, BSTNode<T>? node)
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

        private IEnumerable<T> InOrderTraversal(BSTNode<T>? current, List<T> result)
        {
            if (current == null)
                return result;

            InOrderTraversal(current.Left, result);
            result.Add(current.Value);
            InOrderTraversal(current.Right, result);
            
            return result;
        }
        private IEnumerable<T> PreOrderTraversal(BSTNode<T>? current, List<T> result)
        {
            if (current == null)
                return result;

            result.Add(current.Value);
            PreOrderTraversal(current.Left, result);
            PreOrderTraversal(current.Right, result);

            return result;
        }
        private IEnumerable<T> PostOrderTraversal(BSTNode<T>? current, List<T> result)
        {
            if (current == null)
                return result;

            PostOrderTraversal(current.Left, result);
            PostOrderTraversal(current.Right, result);
            result.Add(current.Value);

            return result;
        }

        private IEnumerable<T> InOrderTraversalIndexRange(BSTNode<T>? current, int index, int[] currIndex, int count, List<T> result)
        {
            if (current == null || currIndex[0] >= index + count)
                return result;

            InOrderTraversalIndexRange(current.Left, index, currIndex, count, result);

            if (currIndex[0] >= index && currIndex[0] < index + count)
                result.Add(current.Value);

            currIndex[0]++;

            return InOrderTraversalIndexRange(current.Right, index, currIndex, count, result);
        }
        private IEnumerable<T> PreOrderTraversalIndexRange(BSTNode<T>? current, int index, int[] currIndex, int count, List<T> result)
        {
            if (current == null || currIndex[0] >= index + count)
                return result;

            if (currIndex[0] >= index && currIndex[0] < index + count)
                result.Add(current.Value);

            currIndex[0]++;

            PreOrderTraversalIndexRange(current.Left, index, currIndex, count, result);
            return PreOrderTraversalIndexRange(current.Right, index, currIndex, count, result);
        }
        private IEnumerable<T> PostOrderTraversalIndexRange(BSTNode<T>? current, int index, int[] currIndex, int count, List<T> result)
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
        private IEnumerable<T> DFSRec(T target, BSTNode<T>? node, HashSet<BSTNode<T>> seen, List<T> path)
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
            BSTNode<T>? max = Max(Root);
            return max != null ? max.Value : default;
        }
        public T? Min()
        {
            BSTNode<T>? min = Min(Root);
            return min != null ? min.Value : default;
        }
        public T? Successor()
        {
            BSTNode<T>? succ = Successor(Root);
            return succ != null ? succ.Value : default;
        }
        public T? Predecessor()
        {
            BSTNode<T>? pred = Predecessor(Root);
            return pred != null ? pred.Value : default;
        }

        // Main Methods
        public BSTNode<T>? Max(BSTNode<T>? node)
        {
            if (node == null)
                return null;

            while (node.Right != null)
                node = node.Right;

            return node;
        }
        public BSTNode<T>? Min(BSTNode<T>? node)
        {
            if (node == null) return null;

            while (node.Left != null)
                node = node.Left;

            return node;
        }
        public BSTNode<T>? Successor(BSTNode<T>? node)
        {
            if (node == null)
                return null;

            BSTNode<T> x = node;

            if (x.Right != null)
                return Min(x.Right);

            BSTNode<T>? y = x.Parent;
            while (y != null && x == y.Right)
            {
                x = y;
                y = y.Parent;
            }

            return y;
        }
        public BSTNode<T>? Predecessor(BSTNode<T>? node)
        {
            if (node == null)
                return null;

            BSTNode<T> x = node;

            if (x.Left != null)
                return Max(x.Left);

            BSTNode<T>? y = x.Parent;
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
        private BSTNode<T>? SearchRec(T value, BSTNode<T>? curr)
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
        #endregion END Helper Methods    

        #region START Utils
        private int GetHeightRec(BSTNode<T>? node, int currH = 0)
        {
            if (node == null)
                return 0;

            int l = GetHeightRec(node.Left, currH + 1);
            int r = GetHeightRec(node.Right, currH + 1);

            return 1 + Math.Max(l, r);
        }
        private int GetLeafCountRec(BSTNode<T>? node, int currC = 0)
        {
            if (node == null)
                return 0;

            if (node.IsLeaf)
                return 1;

            int l = GetLeafCountRec(node.Left, currC);
            int r = GetLeafCountRec(node.Right, currC);

            return r + l;
        }
        private int GetCountRec(BSTNode<T>? node)
        {
            if (node == null)
                return 0;
            
            return 1 + GetCountRec(node.Left) + GetCountRec(node.Right);
        }
        private bool IsValidRec(BSTNode<T>? node)
        {
            if (node == null)
                return true;

            if (node.Left != null && Comparator.Compare(Max(node.Left)!.Value, node.Value) >= 0)
                return false;
            if (node.Right != null && Comparator.Compare(Max(node.Right)!.Value, node.Value) < 0)
                return false;

            return IsValidRec(node.Left) && IsValidRec(node.Right);
        }
        #endregion END Utils
    }
}
