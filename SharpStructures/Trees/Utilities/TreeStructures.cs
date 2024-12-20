﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace SharpStructures.Trees.Utilities
{
    /// <summary>
    /// Interface for representing tree`s.<br />
    /// Used in:<br />
    /// <see cref="BinarySearchTree{T}"></see><br />
    /// </summary>
    public interface IDataTree<T, TNode> 
        where TNode : TreeNode<T, TNode>
    {
        /// <summary>
        /// Gets or sets the root node of the <see cref="IDataTree{T, TNode}"/>.
        /// </summary>
        public TNode? Root { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IDataTree{T, TNode}"/> traversal type (e.g., 
        /// <see cref="TreeTraversalType.InOrder{T}"/>, 
        /// <see cref="TreeTraversalType.PreOrder{T}"/>, 
        /// <see cref="TreeTraversalType.PostOrder{T}"/>).
        /// </summary>
        public TreeTraversalType TraversalType { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Comparer{T}"/> for comparing <see cref="IDataTree{T, TNode}"/> elements.
        /// </summary>
        public Comparer<T> Comparator { get; set; }

        /// <summary>
        /// Gets height of the <see cref="IDataTree{T, TNode}"/>.
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Gets the number of leaf nodes in the <see cref="IDataTree{T, TNode}"/>.
        /// </summary>
        public int LeafCount { get; }

        /// <summary>
        /// Gets the total number of nodes in the <see cref="IDataTree{T, TNode}"/>.
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// Indicates whether the <see cref="IDataTree{T, TNode}"/> is correctly structured.
        /// </summary>
        public bool IsValid { get; }

        /// <summary>
        /// Indicates whether the <see cref="IDataTree{T, TNode}"/> is empty.
        /// </summary>
        public bool IsEmpty { get; }

        /// <summary>
        /// Gets or sets the element at the specified index in the <see cref="IDataTree{T, TNode}"/>.
        /// </summary>
        /// <param name="index">The index of the element to retrieve.</param>
        public T? this[int index] { get; }

        /// <summary>
        /// Adds a new element to the <see cref="IDataTree{T, TNode}"/>.
        /// </summary>
        /// <param name="value">The element to add.</param>
        public void Add(T value);

        /// <summary>
        /// Adds a range of elements to the <see cref="IDataTree{T, TNode}"/>.
        /// </summary>
        /// <param name="values">The array of elements to add.</param>
        public void AddRange(T[] values);

        /// <summary>
        /// Removes a specified element from the <see cref="IDataTree{T, TNode}"/>.
        /// </summary>
        /// <param name="value">The element to remove.</param>
        public void Remove(T value);

        /// <summary>
        /// Removes a range of elements to the <see cref="IDataTree{T, TNode}"/>.
        /// </summary>
        /// <param name="values">The array of elements to remove.</param>
        public void RemoveRange(T[] values);

        /// <summary>
        /// Clears all elements from the <see cref="IDataTree{T, TNode}"/>.
        /// </summary>
        public void Clear();

        /// <summary>
        /// Checks is the <see cref="IDataTree{T, TNode}"/> contains specified element.
        /// </summary>
        /// <param name="value">The element to check for.</param>
        /// <returns>True if the element is present; otherwise, false.</returns>
        public bool Contains(T value);

        /// <summary>
        /// Creates a clone of the <see cref="IDataTree{T, TNode}"/>.
        /// </summary>
        /// <returns>A new instance of the <see cref="IDataTree{T, TNode}"/> that is a copy of the original.</returns>
        public IDataTree<T, TNode> Clone();

        /// <summary>
        /// Gets <see cref="T"/> at specified index using specified traversal method <see cref="TreeTraversalType"/>.
        /// </summary>
        /// <param name="index">The index of the element to retrieve.</param>
        /// <returns>The element at the specified index.</returns>
        public T GetIndexValue(int index);

        /// <summary>
        /// Finds maximum <see cref="T"/> in the <see cref="IDataTree{T, TNode}"/>.
        /// </summary>
        /// <returns>The maximum element in the <see cref="IDataTree{T, TNode}"/>.</returns>
        public T? Max();

        /// <summary>
        /// Finds miniumum <see cref="T"/> in the <see cref="IDataTree{T, TNode}"/>.
        /// </summary>
        /// <returns>The miniumum element in the <see cref="IDataTree{T, TNode}"/>.</returns>
        public T? Min();

        /// <summary>
        /// Finds the successor <see cref="T"/> of the root <see cref="TNode{T}"/>.
        /// </summary>
        /// <returns>The successor element of the root <see cref="TNode{T}.Value"/>.</returns>
        public T? Successor();

        /// <summary>
        /// Finds the predecessor <see cref="T"/> of the root <see cref="TNode"/>.
        /// </summary>
        /// <returns>The predecessor element of the root <see cref="TNode.Value"/>.</returns>
        public T? Predecessor();

        /// <summary>
        /// Finds maximum <see cref="TNode"/> in the <see cref="IDataTree{T, TNode}"/>.
        /// </summary>
        /// <returns>The maximum node from the specified <see cref="TNode"/>.</returns>
        public TNode? Max(TNode? node);

        /// <summary>
        /// Finds miniumum <see cref="TNode"/> in the <see cref="IDataTree{T, TNode}"/>.
        /// </summary>
        /// <returns>The miniumum node from the specified <see cref="TNode"/>.</returns>
        public TNode? Min(TNode? node);

        /// <summary>
        /// Finds the successor <see cref="TNode"/> of the root <see cref="TNode{T}"/>.
        /// </summary>
        /// <returns>The successor node of the specified <see cref="TNode"/>.</returns>
        public TNode? Successor(TNode? node);

        /// <summary>
        /// Finds the predecessor <see cref="TNode"/> of the root <see cref="TNode"/>.
        /// </summary>
        /// <returns>The predecessor node of the specified <see cref="TNode"/>.</returns>
        public TNode? Predecessor(TNode? node);

        /// <summary>
        /// Searches for an element that matches specified predicate.
        /// </summary>
        /// <param name="predicate">The function used to evaluate each element.</param>
        /// <returns>The element that matches the predicate, or null if not found.</returns>
        public T? Find(Func<T, bool> predicate);

        /// <summary>
        /// Searches for the last element that matches specified predicate.
        /// </summary>
        /// <param name="predicate">The function used to evaluate each element.</param>
        /// <returns>The last element that matches the predicate, or null if not found.</returns>
        public T? FindLast(Func<T, bool> predicate);

        /// <summary>
        /// Searches for an node that matches specified predicate.
        /// </summary>
        /// <param name="predicate">The function used to evaluate each node.</param>
        /// <returns>The node that matches the predicate, or null if not found.</returns>
        public TNode? Find(Func<T, bool> predicate, TNode? node);

        /// <summary>
        /// Searches for the last node that matches specified predicate.
        /// </summary>
        /// <param name="predicate">The function used to evaluate each node.</param>
        /// <returns>The last node that matches the predicate, or null if not found.</returns>
        public TNode? FindLast(Func<T, bool> predicate, TNode? node);

        /// <summary>
        /// Finds successor <see cref="TNode"/> from the specified node.
        /// </summary>
        /// <param name="node">Node from which the search should start.</param>
        /// <returns>The found successor <see cref="TNode"/>, otherwise returns the node.</returns>
        public TNode? Max(TNode? node);

        /// <summary>
        /// Finds minimum <see cref="TNode"/> from the specified node.
        /// </summary>
        /// <param name="node">Node from which the search should start.</param>
        /// <returns>The found minimum <see cref="TNode"/>, otherwise returns the node.</returns>
        public TNode? Min(TNode? node);

        /// <summary>
        /// Finds successor <see cref="TNode"/> from the specified node.
        /// </summary>
        /// <param name="node">Node from which the search should start.</param>
        /// <returns>The found successor <see cref="TNode"/>, otherwise returns the node.</returns>
        public TNode? Successor(TNode? node);

        /// <summary>
        /// Finds predecessor <see cref="TNode"/> from the specified node.
        /// </summary>
        /// <param name="node">Node from which the search should start.</param>
        /// <returns>The found predecessor <see cref="TNode"/>, otherwise returns the node.</returns>
        public TNode? Predecessor(TNode? node);

        /// <summary>
        /// Gets a <see cref="IEnumerable{T}"/> range of values starting from the specified index.
        /// </summary>
        /// <param name="index">The starting index of the range.</param>
        /// <param name="count">The number of elements to retrive from the specified index.</param>
        /// <returns><see cref="IEnumerable{T}"/> Range of found elements, or an empty <see cref="Enumerable.Empty{T}"/> if none were found.</returns>
        public IEnumerable<T> GetRange(int index, int count);

        /// <summary>
        /// Gets <see cref="IEnumerable{T}"/> of all values in the <see cref="IDataTree{T, TNode}"/> using the specified traversal method (<see cref="TreeTraversalType"/>).
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> of all values in the <see cref="IDataTree{T, TNode}"/>, traversed in the specified order.</returns>
        public IEnumerable<T> Traverse();

        /// <summary>
        /// Gets all values in the <see cref="IDataTree{T, TNode}"/> using the <see cref="TreeTraversalType.InOrder"/>.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> of all values in the <see cref="IDataTree{T, TNode}"/>, traversed <see cref="TreeTraversalType.InOrder"/>.</returns>
        public IEnumerable<T> InOrderTraversal();

        /// <summary>
        /// Gets all values in the <see cref="IDataTree{T, TNode}"/> using the <see cref="TreeTraversalType.PreOrder"/>.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> of all values in the <see cref="IDataTree{T, TNode}"/>, traversed <see cref="TreeTraversalType.PreOrder"/>.</returns>
        public IEnumerable<T> PreOrderTraversal();

        /// <summary>
        /// Gets all values in the <see cref="IDataTree{T, TNode}"/> using the <see cref="TreeTraversalType.PostOrder"/>.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> of all values in the <see cref="IDataTree{T, TNode}"/>, traversed <see cref="TreeTraversalType.PostOrder"/>.</returns>
        public IEnumerable<T> PostOrderTraversal();

        /// <summary>
        /// Finds the path to the target value using Depth-First Search (DFS).
        /// </summary>
        /// <param name="target">Value to which path should be found.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> of values representing the path to the target element, or an <see cref="Enumerable.Empty{T}"/> of no path found.</returns>
        public IEnumerable<T> DFS(T target);

        // Conversions
        /// <summary>
        /// Gets all of values in the <see cref="IDataTree{T, TNode}"/> as an array.
        /// </summary>
        /// <returns>An array of all values in the <see cref="IDataTree{T, TNode}"/> </returns>
        public T[] ToArray();

        /// <summary>
        /// Gets all of values in the <see cref="IDataTree{T, TNode}"/> as an <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of all values in the <see cref="IDataTree{T, TNode}"/></returns>
        public IEnumerable<T> AsEnumerable();

        /// <summary>
        /// Gets all of values in the <see cref="IDataTree{T, TNode}"/> as an <see cref="IEnumerator{T}"/>.
        /// </summary>
        /// <returns>An <see cref="IEnumerator{T}"/> of all values in the <see cref="IDataTree{T, TNode}"/></returns>
        public IEnumerator<T> GetEnumerator();

        /// <summary>
        /// Gets all of values in the <see cref="IDataTree{T, TNode}"/> as an <see cref="ILookup{TKey, TElement}"/>.
        /// </summary>
        /// <returns>An <see cref="ILookup{TKey, TElement}"/> of all values in the <see cref="IDataTree{T, TNode}"/></returns>
        public ILookup<T, T> ToLookup(Func<T, T> keySelector);

        /// <summary>
        /// Gets all of values in the <see cref="IDataTree{T, TNode}"/> as a <see cref="List{T}"/>.
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of all values in the <see cref="IDataTree{T, TNode}"/></returns>
        public List<T> ToList();

        /// <summary>
        /// Gets all of values in the <see cref="IDataTree{T, TNode}"/> as a <see cref="LinkedList{T}"/>.
        /// </summary>
        /// <returns>A <see cref="LinkedList{T}"/> of all values in the <see cref="IDataTree{T, TNode}"/></returns>
        public LinkedList<T> ToLinkedList();

        /// <summary>
        /// Gets all of values in the <see cref="IDataTree{T, TNode}"/> as an <see cref="ImmutableList{T}"/>.
        /// </summary>
        /// <returns>An <see cref="ImmutableList{T}"/> of all values in the <see cref="IDataTree{T, TNode}"/></returns>
        public ImmutableList<T> ToImmutableList();

        /// <summary>
        /// Gets all of values in the <see cref="IDataTree{T, TNode}"/> as an <see cref="HashSet{T}"/>.
        /// </summary>
        /// <returns>An <see cref="HashSet{T}"/> of all values in the <see cref="IDataTree{T, TNode}"/></returns>
        public HashSet<T> ToHashSet();

        /// <summary>
        /// Gets all of values in the <see cref="IDataTree{T, TNode}"/> as an <see cref="Stack{T}"/>.
        /// </summary>
        /// <returns>An <see cref="Stack{T}"/> of all values in the <see cref="IDataTree{T, TNode}"/></returns>
        public Stack<T> ToStack();

        /// <summary>
        /// Gets all of values in the <see cref="IDataTree{T, TNode}"/> as an <see cref="Queue{T}"/>.
        /// </summary>
        /// <returns>An <see cref="Queue{T}"/> of all values in the <see cref="IDataTree{T, TNode}"/></returns>
        public Queue<T> ToQueue();
    }

    /// <summary>
    /// Specifies default tree traversal type
    /// </summary>
    public enum TreeTraversalType
    {
        /// <summary>
        /// InOrder tree traversal type
        /// </summary>
        InOrder,

        /// <summary>
        /// Pre tree traversal type
        /// </summary>
        PreOrder,

        /// <summary>
        /// Post tree traversal type
        /// </summary>
        PostOrder,
    }
    public enum NodeType
    {
        None,
        Null,
        Black,
        Red
    }

    /// <summary>
    /// Class representing a tree node holding data to its left and right child (<see cref="TreeNode{T, TNode}"/>).<br />
    ///
    /// Implements <see cref="IDisposable"/> interface to cleanup the node and all the attached nodes.
    /// </summary>
    public abstract class TreeNode<T, TNode> : IDisposable
        where TNode : TreeNode<T, TNode>
    {
        public TreeNode(T value) { Value = value; }

        public virtual T Value { get; set; }
        public virtual TNode? Left { get; set; }
        public virtual TNode? Right { get; set; }
        public virtual TNode? Parent { get; set; }
        public virtual TNode? this[int index]
        {
            get
            {
                if (index == 0)
                    return Left;
                else if (index == 1)
                    return Right;
                else
                    throw new IndexOutOfRangeException();
            }
            set
            {
                if (index == 0)
                    Left = value;
                else if (index == 1)
                    Right = value;
                else
                    throw new IndexOutOfRangeException();
            }
        }
        public bool IsLeaf => (Left == null || Left.Type == NodeType.Null) && (Right == null || Right.Type == NodeType.Null);

        public NodeType Type { get; set; } = NodeType.None;

        public static implicit operator bool(TreeNode<T, TNode>? node)
        {
            if (node == null || node.Type == NodeType.Null)
                return false;

            return true;
        }

        // Disposing
        private bool _disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Left?.Dispose();
                    Right?.Dispose();

                    Value = default!;
                    Left = null;
                    Right = null;
                    Parent = null;
                }

                _disposed = true;
            }
        }
        ~TreeNode()
        {
            Dispose(false);
        }
    }
    public class BSTNode<T> : TreeNode<T, BSTNode<T>>
    {
        public BSTNode(T value, BSTNode<T>? left = null, BSTNode<T>? right = null, BSTNode<T>? parent = null) : base(value)
        {
            Left = left;
            Right = right;
            Parent = parent;
        }
    }
    public class AVLNode<T> : TreeNode<T, AVLNode<T>>
    {
        public AVLNode(T value, AVLNode<T>? left = null, AVLNode<T>? right = null, AVLNode<T>? parent = null) : base(value)
        {
            Left = left;
            Right = right;
            Parent = parent;
        }
        public int BalanceFactor { get; set; } = 0;
    }
    internal class RBTNode<T> : TreeNode<T, RBTNode<T>>
    {
        public RBTNode(T value, RBTNode<T>? left = null, RBTNode<T>? right = null, RBTNode<T>? parent = null) : base(value)
        {
            Left = left;
            Right = right;
            Parent = parent;
            Type = NodeType.Black;
        }
        public static RBTNode<T> NIL = new RBTNode<T>(default!) { Type = NodeType.Null };
    }
}
