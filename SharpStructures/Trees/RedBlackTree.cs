using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpStructures.Trees
{
    /// <summary>
    /// Red-Black Tree (RBT) representation that allows efficient searching, insertion, deletion and traversal operations.<br />
    /// Tree is built using <see cref="RBTNode{T}"/> class.
    /// </summary>
    public class RedBlackTree<T> : IDataTree<T, RBTNode<T>>
    {
        public RedBlackTree(RBTNode<T>? root = null, Comparer<T>? comparer = null, TreeTraversalType traversalType = TreeTraversalType.InOrder)
        {
            if (typeof(IComparable<T>).IsAssignableFrom(typeof(T)) || comparer != null)
                Comparator = comparer ?? Comparer<T>.Default;
            else
                throw new ArgumentNullException(nameof(comparer), "Comparer is required for types that do not implement IComparable.");

            TraversalType = traversalType;
            Root = root;
        }

        // Properties
        public RBTNode<T>? Root { get; set; }
        public TreeTraversalType TraversalType { get; set; }
        public Comparer<T> Comparator { get; set; }

        public int Count => GetCountRec(Root);
        public int LeafCount => GetLeafCountRec(Root);
        public int Height => GetHeightRec(Root);
        public int Levels => Height + 1;

        public bool IsValid => Root == null || Root.Color == RBTColor.Black && IsValidRec(Root);
        public bool IsEmpty => Root == null;

        public T this[int index] => GetIndexValue(index);
        #region START Main Methods
        public void Add(T value)
        {
            RBTInsert(BSTInsert(value));
        }
        public void AddRange(T[] values)
        {
            for (int i = 0; i < values.Length; i++)
                Add(values[i]);
        }
        // TODO
        public void Remove(T value)
        {

        }
        public void RemoveRange(T[] values)
        {

        }
        public void Clear()
        {
            if (Root == null)
                return;

            Root.Dispose();
            Root = null;
        }

        public bool Contains(T value) => SearchRec(value, Root) != null;
        public IDataTree<T, RBTNode<T>> Clone() => (IDataTree<T, RBTNode<T>>)this.MemberwiseClone();
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
        public T? Find(Func<T, bool> predicate, RBTNode<T>? node)
        {
            if (node == null || node.IsNIL)
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

        private IEnumerable<T> InOrderTraversal(RBTNode<T>? current, List<T> result)
        {
            if (current == null || current.IsNIL)
                return result;

            InOrderTraversal(current.Left, result);
            result.Add(current.Value);
            InOrderTraversal(current.Right, result);

            return result;
        }
        private IEnumerable<T> PreOrderTraversal(RBTNode<T>? current, List<T> result)
        {
            if (current == null || current.IsNIL)
                return result;

            result.Add(current.Value);
            PreOrderTraversal(current.Left, result);
            PreOrderTraversal(current.Right, result);

            return result;
        }
        private IEnumerable<T> PostOrderTraversal(RBTNode<T>? current, List<T> result)
        {
            if (current == null || current.IsNIL)
                return result;

            PostOrderTraversal(current.Left, result);
            PostOrderTraversal(current.Right, result);
            result.Add(current.Value);

            return result;
        }

        private IEnumerable<T> InOrderTraversalIndexRange(RBTNode<T>? current, int index, int[] currIndex, int count, List<T> result)
        {
            if (current == null || current.IsNIL || currIndex[0] >= index + count)
                return result;

            InOrderTraversalIndexRange(current.Left, index, currIndex, count, result);

            if (currIndex[0] >= index && currIndex[0] < index + count)
                result.Add(current.Value);

            currIndex[0]++;

            return InOrderTraversalIndexRange(current.Right, index, currIndex, count, result);
        }
        private IEnumerable<T> PreOrderTraversalIndexRange(RBTNode<T>? current, int index, int[] currIndex, int count, List<T> result)
        {
            if (current == null || current.IsNIL || currIndex[0] >= index + count)
                return result;

            if (currIndex[0] >= index && currIndex[0] < index + count)
                result.Add(current.Value);

            currIndex[0]++;

            PreOrderTraversalIndexRange(current.Left, index, currIndex, count, result);
            return PreOrderTraversalIndexRange(current.Right, index, currIndex, count, result);
        }
        private IEnumerable<T> PostOrderTraversalIndexRange(RBTNode<T>? current, int index, int[] currIndex, int count, List<T> result)
        {
            if (current == null || current.IsNIL || currIndex[0] >= index + count)
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
        private IEnumerable<T> DFSRec(T target, RBTNode<T>? node, HashSet<RBTNode<T>> seen, List<T> path)
        {
            if (node == null || node.IsNIL)
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
            RBTNode<T>? max = Max(Root);
            return max != null ? max.Value : default;
        }
        public T? Min()
        {
            RBTNode<T>? min = Min(Root);
            return min != null ? min.Value : default;
        }
        public T? Successor()
        {
            RBTNode<T>? succ = Successor(Root);
            return succ != null ? succ.Value : default;
        }
        public T? Predecessor()
        {
            RBTNode<T>? pred = Predecessor(Root);
            return pred != null ? pred.Value : default;
        }

        // Main Methods
        public RBTNode<T>? Max(RBTNode<T>? node)
        {
            if (node == null)
                return null;

            while (node.Right != null && !node.Right.IsNIL)
                node = node.Right;

            return node;
        }
        public RBTNode<T>? Min(RBTNode<T>? node)
        {
            if (node == null) return null;

            while (node.Left != null && !node.Left.IsNIL)
                node = node.Left;

            return node;
        }
        public RBTNode<T>? Successor(RBTNode<T>? node)
        {
            if (node == null)
                return null;

            RBTNode<T> x = node;

            if (x.Right != null && !x.Right.IsNIL)
                return Min(x.Right);

            RBTNode<T>? y = x.Parent;
            while (y != null && x == y.Right)
            {
                x = y;
                y = y.Parent;
            }

            return y;
        }
        public RBTNode<T>? Predecessor(RBTNode<T>? node)
        {
            if (node == null)
                return null;

            RBTNode<T> x = node;

            if (x.Left != null && !x.Left.IsNIL)
                return Max(x.Left);

            RBTNode<T>? y = x.Parent;
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
        private RBTNode<T> BSTInsert(T value)
        {
            RBTNode<T>? z = new RBTNode<T>(value); 
            RBTNode<T>? y = null;
            RBTNode<T>? x = Root;

            if (x == null)
                return z;

            while (!x!.IsNIL)
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

            n.Color = RBTColor.Red;
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
                if (p.Color == RBTColor.Black)
                    break;

                // Case 4
                if ((g = p.Parent) == null)
                {
                    p.Color = RBTColor.Black;
                    break;
                }

                dir = ChildDir(p);
                u = g[1 - dir];

                // Case 5/6
                if (u!.IsNIL || u.Color == RBTColor.Black)
                {
                    if (n == p[1 - dir])
                    {
                        RotateDir(p, dir);
                        n = p;
                        p = g[dir];
                    }

                    RotateDirRoot(g, 1 - dir);
                    p!.Color = RBTColor.Black;
                    g.Color = RBTColor.Red;
                    break;
                }

                // Case 2
                p.Color = RBTColor.Black;
                u.Color = RBTColor.Black;
                g.Color = RBTColor.Red;
                n = g;

                p = n.Parent;
            }

            Root!.Color = RBTColor.Black;
        }

        private RBTNode<T>? SearchRec(T value, RBTNode<T>? curr)
        {
            if (curr == null || curr.IsNIL)
                return null;

            if (Comparator.Compare(value, curr.Value) == 0)
                return curr;
            else if (Comparator.Compare(value, curr.Value) < 0)
                return SearchRec(value, curr.Left);
            else
                return SearchRec(value, curr.Right);
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

            if (s!.IsNIL) throw new InvalidOperationException("Tree structure is invalid or violates Red-Black Tree rules.");

            c = s[dir];
            p[1 - dir] = c; if (c!.IsNIL) c.Parent = p;
            s[dir] = p; p.Parent = s;
            s.Parent = g;

            if (g != null)
                g[p == g.Right ? 1 : 0] = s;
            else
                Root = s;

            return s;
        }
        #endregion END Helper Methods

        #region START Utils
        private int GetHeightRec(RBTNode<T>? node, int currH = 0)
        {
            if (node == null || node.IsNIL)
                return 0;

            int l = GetHeightRec(node.Left, currH + 1);
            int r = GetHeightRec(node.Right, currH + 1);

            return 1 + Math.Max(l, r);
        }
        private int GetLeafCountRec(RBTNode<T>? node, int currC = 0)
        {
            if (node == null || node.IsNIL)
                return 0;

            if (node.IsLeaf)
                return 1;

            int l = GetLeafCountRec(node.Left, currC);
            int r = GetLeafCountRec(node.Right, currC);

            return r + l;
        }
        private int GetCountRec(RBTNode<T>? node)
        {
            if (node == null || node.IsNIL)
                return 0;

            return 1 + GetCountRec(node.Left) + GetCountRec(node.Right);
        }
        private bool IsValidRec(RBTNode<T>? node)
        {
            if (node == null)
                return true;

            if (node.Color == RBTColor.Red)
            {
                if (node.Left != null && node.Left.Color == RBTColor.Red) return false;
                if (node.Right != null && node.Right.Color == RBTColor.Red) return false;
            }

            return IsValidRec(node.Left) && IsValidRec(node.Right);
        }
        #endregion END Utils
    }
}
