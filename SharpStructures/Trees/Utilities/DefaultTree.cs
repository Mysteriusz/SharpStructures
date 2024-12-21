using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace SharpStructures.Trees.Utilities
{
    public abstract class DefaultTree<T, TNode> : IDataTree<T, TNode>
        where TNode : TreeNode<T, TNode>
    {
        public DefaultTree(TNode? root, Comparer<T>? comparator = null, TreeTraversalType traversalType = TreeTraversalType.InOrder)
        {
            if (typeof(IComparable<T>).IsAssignableFrom(typeof(T)) || comparator != null)
                Comparator = comparator ?? Comparer<T>.Default;
            else
                throw new ArgumentNullException(nameof(comparator), "Comparer is required for types that do not implement IComparable.");

            TraversalType = traversalType;
            Root = root;
        }

        public TNode? Root { get; set; }
        public TreeTraversalType TraversalType { get; set; }
        public Comparer<T> Comparator { get; set; }

        public virtual int Height => TreeHelper<T, TNode>.GetHeightRec(Root);
        public virtual int LeafCount => TreeHelper<T, TNode>.GetLeafCountRec(Root);
        public virtual int Count => TreeHelper<T, TNode>.GetCountRec(Root);

        public virtual bool IsEmpty => Root == null;
        public abstract bool IsValid { get; }

        public virtual T this[int index] => GetIndexValue(index);

        #region START Main Methods
        public abstract void Add(T value);
        public abstract void Remove(T value);
        public virtual void AddRange(T[] values) => Array.ForEach(values, Add);
        public virtual void RemoveRange(T[] values) => Array.ForEach(values, Remove);

        public void Clear()
        {
            Root?.Dispose();
            Root = null;
        }
        public bool Contains(T value) => TreeHelper<T, TNode>.SearchRec(value, this, Root) != null;
        public IDataTree<T, TNode> Clone() => (IDataTree<T, TNode>)MemberwiseClone();
        #endregion END Main Methods

        #region START Traversal Methods
        public virtual T GetIndexValue(int index)
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

        public virtual T? Find(Func<T, bool> predicate)
        {
            TNode? found = Find(predicate, Root);
            return found ? found!.Value : default;
        }
        public virtual T? FindLast(Func<T, bool> predicate)
        {
            TNode? found = FindLast(predicate, Root);
            return found ? found!.Value : default;
        }

        public virtual TNode? Find(Func<T, bool> predicate, TNode? node)
        {
            if (!node)
                return null;

            if (predicate(node!.Value))
                return node;

            return Find(predicate, node.Left) ?? Find(predicate, node.Right);
        }
        public virtual TNode? FindLast(Func<T, bool> predicate, TNode? node)
        {
            if (!node)
                return null;

            TNode? right = FindLast(predicate, node!.Right);
            if (right != null)
                return right;

            TNode? left = FindLast(predicate, node!.Left);
            if (left != null)
                return left;

            if (predicate(node!.Value))
                return node;

            return null;
        }

        public virtual IEnumerable<T> Traverse()
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
        public virtual IEnumerable<T> GetRange(int index, int count)
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

        public virtual IEnumerable<T> InOrderTraversal() => InOrderTraversal(Root, []);
        public virtual IEnumerable<T> PreOrderTraversal() => PreOrderTraversal(Root, []);
        public virtual IEnumerable<T> PostOrderTraversal() => PostOrderTraversal(Root, []);

        private IEnumerable<T> InOrderTraversal(TNode? current, List<T> result)
        {
            if (!current)
                return result;

            InOrderTraversal(current!.Left, result);
            result.Add(current.Value);
            InOrderTraversal(current.Right, result);

            return result;
        }
        private IEnumerable<T> PreOrderTraversal(TNode? current, List<T> result)
        {
            if (!current)
                return result;

            result.Add(current!.Value);
            PreOrderTraversal(current.Left, result);
            PreOrderTraversal(current.Right, result);

            return result;
        }
        private IEnumerable<T> PostOrderTraversal(TNode? current, List<T> result)
        {
            if (!current)
                return result;

            PostOrderTraversal(current!.Left, result);
            PostOrderTraversal(current.Right, result);
            result.Add(current.Value);

            return result;
        }

        private IEnumerable<T> InOrderTraversalIndexRange(TNode? current, int index, int[] currIndex, int count, List<T> result)
        {
            if (!current || currIndex[0] >= index + count)
                return result;

            InOrderTraversalIndexRange(current!.Left, index, currIndex, count, result);

            if (currIndex[0] >= index && currIndex[0] < index + count)
                result.Add(current.Value);

            currIndex[0]++;

            return InOrderTraversalIndexRange(current.Right, index, currIndex, count, result);
        }
        private IEnumerable<T> PreOrderTraversalIndexRange(TNode? current, int index, int[] currIndex, int count, List<T> result)
        {
            if (!current || currIndex[0] >= index + count)
                return result;

            if (currIndex[0] >= index && currIndex[0] < index + count)
                result.Add(current!.Value);

            currIndex[0]++;

            PreOrderTraversalIndexRange(current!.Left, index, currIndex, count, result);
            return PreOrderTraversalIndexRange(current.Right, index, currIndex, count, result);
        }
        private IEnumerable<T> PostOrderTraversalIndexRange(TNode? current, int index, int[] currIndex, int count, List<T> result)
        {
            if (!current || currIndex[0] >= index + count)
                return result;

            PostOrderTraversalIndexRange(current!.Left, index, currIndex, count, result);
            PostOrderTraversalIndexRange(current.Right, index, currIndex, count, result);

            if (currIndex[0] >= index && currIndex[0] < index + count)
                result.Add(current.Value);

            currIndex[0]++;

            return result;
        }

        #region START PathFinding
        public virtual IEnumerable<T> DFS(T target) => DFSRec(target, Root, [], []);
        private IEnumerable<T> DFSRec(T target, TNode? node, HashSet<TNode> seen, List<T> path)
        {
            if (!node)
                return path;

            if (Comparator.Compare(node!.Value, target) == 0)
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
        public virtual T? Max()
        {
            TNode? max = Max(Root);
            return max ? max!.Value : default;
        }
        public virtual T? Min()
        {
            TNode? min = Min(Root);
            return min ? min!.Value : default;
        }
        public virtual T? Successor()
        {
            TNode? succ = Successor(Root);
            return succ ? succ!.Value : default;
        }
        public virtual T? Predecessor()
        {
            TNode? pred = Predecessor(Root);
            return pred ? pred!.Value : default;
        }

        // Main Methods
        public virtual TNode? Max(TNode? node)
        {
            if (!node) return null;

            while (node!.Right)
                node = node.Right;

            return node;
        }
        public virtual TNode? Min(TNode? node)
        {
            if (!node) return null;

            while (node!.Left)
                node = node.Left;

            return node;
        }
        public virtual TNode? Successor(TNode? node)
        {
            if (!node)
                return null;

            TNode x = node!;

            if (x.Right)
                return Min(x.Right);

            TNode? y = x.Parent;
            while (y && x == y!.Right)
            {
                x = y;
                y = y.Parent;
            }

            return y;
        }
        public virtual TNode? Predecessor(TNode? node)
        {
            if (!node)
                return null;

            TNode x = node!;

            if (x.Left)
                return Max(x.Left);

            TNode? y = x.Parent;
            while (y && x == y!.Left)
            {
                x = y;
                y = y.Parent;
            }

            return y;
        }
        #endregion END Tree Navigation

        #region START Conversion Methods
        public virtual T[] ToArray()
        {
            return Traverse().ToArray();
        }
        public virtual IEnumerable<T> AsEnumerable()
        {
            return Traverse();
        }
        public virtual IEnumerator<T> GetEnumerator()
        {
            return Traverse().GetEnumerator();
        }
        public virtual ILookup<T, T> ToLookup(Func<T, T> keySelector)
        {
            return Traverse().ToLookup(keySelector);
        }
        public virtual List<T> ToList()
        {
            return Traverse().ToList();
        }
        public virtual LinkedList<T> ToLinkedList()
        {
            LinkedList<T> list = new LinkedList<T>();
            foreach (T value in Traverse())
            {
                list.AddLast(value);
            }

            return list;
        }
        public virtual ImmutableList<T> ToImmutableList()
        {
            return Traverse().ToImmutableList();
        }
        public virtual HashSet<T> ToHashSet()
        {
            return Traverse().ToHashSet();
        }
        public virtual Stack<T> ToStack()
        {
            Stack<T> stack = new Stack<T>();

            foreach (T value in Traverse())
                stack.Push(value);

            return stack;
        }
        public virtual Queue<T> ToQueue()
        {
            Queue<T> queue = new Queue<T>();

            foreach (T value in Traverse())
                queue.Enqueue(value);

            return queue;
        }
        #endregion END Conversion Methods
    }
}
