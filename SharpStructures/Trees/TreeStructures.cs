﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpStructures.Trees
{
    public interface IDataTree<T>
    {
        /// <summary>
        /// Gets or sets the root node of the <see cref="IDataTree{T}"/>.
        /// </summary>
        public TreeNode<T>? Root { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IDataTree{T}"/> traversal type (e.g., 
        /// <see cref="TreeTraversalType.InOrder{T}"/>, 
        /// <see cref="TreeTraversalType.PreOrder{T}"/>, 
        /// <see cref="TreeTraversalType.PostOrder{T}"/>).
        /// </summary>
        public TreeTraversalType IndexingType { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Comparer{T}"/> for comparing <see cref="IDataTree{T}"/> elements.
        /// </summary>
        public Comparer<T> Comparator { get; set; }

        /// <summary>
        /// Gets height of the <see cref="IDataTree{T}"/>.
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Gets the number of leaf nodes in the <see cref="IDataTree{T}"/>.
        /// </summary>
        public int LeafCount { get; }

        /// <summary>
        /// Gets the number of levels in the <see cref="IDataTree{T}"/>.
        /// </summary>
        public int Levels { get; }

        /// <summary>
        /// Gets the total number of nodes in the <see cref="IDataTree{T}"/>.
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// Indicates whether the <see cref="IDataTree{T}"/> is correctly structured.
        /// </summary>
        public bool IsBST { get; }

        /// <summary>
        /// Indicates whether the <see cref="IDataTree{T}"/> is empty.
        /// </summary>
        public bool IsEmpty { get; }

        /// <summary>
        /// Gets or sets the element at the specified index in the <see cref="IDataTree{T}"/>.
        /// </summary>
        /// <param name="index">The index of the element to retrieve.</param>
        public T? this[int index] { get; }

        /// <summary>
        /// Adds a new element to the <see cref="IDataTree{T}"/>.
        /// </summary>
        /// <param name="value">The element to add.</param>
        public void Add(T value);

        /// <summary>
        /// Adds a range of elements to the <see cref="IDataTree{T}"/>.
        /// </summary>
        /// <param name="values">The array of elements to add.</param>
        public void AddRange(T[] values);

        /// <summary>
        /// Removes a specified element from the <see cref="IDataTree{T}"/>.
        /// </summary>
        /// <param name="value">The element to remove.</param>
        public void Remove(T value);

        /// <summary>
        /// Removes a range of elements to the <see cref="IDataTree{T}"/>.
        /// </summary>
        /// <param name="values">The array of elements to remove.</param>
        public void RemoveRange(T[] values);

        /// <summary>
        /// Clears all elements from the <see cref="IDataTree{T}"/>.
        /// </summary>
        public void Clear();

        /// <summary>
        /// Checks is the <see cref="IDataTree{T}"/> contains specified element.
        /// </summary>
        /// <param name="value">The element to check for.</param>
        /// <returns>True if the element is present; otherwise, false.</returns>
        public bool Contains(T value);

        /// <summary>
        /// Creates a clone of the <see cref="IDataTree{T}"/>.
        /// </summary>
        /// <returns>A new instance of the <see cref="IDataTree{T}"/> that is a copy of the original.</returns>
        public IDataTree<T> Clone();

        /// <summary>
        /// Gets <see cref="T"/> at specified index using specified traversal method <see cref="TreeTraversalType"/>.
        /// </summary>
        /// <param name="index">The index of the element to retrieve.</param>
        /// <returns>The element at the specified index.</returns>
        public T GetIndexValue(int index);

        /// <summary>
        /// Finds successor <see cref="T"/> in the <see cref="IDataTree{T}"/>.
        /// </summary>
        /// <returns>The successor element in the <see cref="IDataTree{T}"/>.</returns>
        public T? Max();

        /// <summary>
        /// Finds miniumum <see cref="T"/> in the <see cref="IDataTree{T}"/>.
        /// </summary>
        /// <returns>The miniumum element in the <see cref="IDataTree{T}"/>.</returns>
        public T? Min();

        /// <summary>
        /// Finds the successor <see cref="T"/> of the root <see cref="TreeNode{T}"/>.
        /// </summary>
        /// <returns>The successor element of the root <see cref="TreeNode{T}.Value"/>.</returns>
        public T? Successor();

        /// <summary>
        /// Finds the predecessor <see cref="T"/> of the root <see cref="TreeNode{T}"/>.
        /// </summary>
        /// <returns>The predecessor element of the root <see cref="TreeNode{T}.Value"/>.</returns>
        public T? Predecessor();

        /// <summary>
        /// Searches for an element that matches specified predicate.
        /// </summary>
        /// <param name="predicate">The function used to evaluate each element.</param>
        /// <returns>The element that matches the predicate, or null if not found.</returns>
        public T? Find(Func<T, bool> predicate);

        /// <summary>
        /// Finds successor <see cref="TreeNode{T}"/> from the specified node.
        /// </summary>
        /// <param name="node">Node from which the search should start.</param>
        /// <returns>The found successor <see cref="TreeNode{T}"/>, otherwise returns the node.</returns>
        public TreeNode<T>? MaxNode(TreeNode<T>? node);

        /// <summary>
        /// Finds minimum <see cref="TreeNode{T}"/> from the specified node.
        /// </summary>
        /// <param name="node">Node from which the search should start.</param>
        /// <returns>The found minimum <see cref="TreeNode{T}"/>, otherwise returns the node.</returns>
        public TreeNode<T>? MinNode(TreeNode<T>? node);

        /// <summary>
        /// Finds successor <see cref="TreeNode{T}"/> from the specified node.
        /// </summary>
        /// <param name="node">Node from which the search should start.</param>
        /// <returns>The found successor <see cref="TreeNode{T}"/>, otherwise returns the node.</returns>
        public TreeNode<T>? SuccessorNode(TreeNode<T>? node);

        /// <summary>
        /// Finds predecessor <see cref="TreeNode{T}"/> from the specified node.
        /// </summary>
        /// <param name="node">Node from which the search should start.</param>
        /// <returns>The found predecessor <see cref="TreeNode{T}"/>, otherwise returns the node.</returns>
        public TreeNode<T>? PredecessorNode(TreeNode<T>? node);

        /// <summary>
        /// Gets a <see cref="IEnumerable{T}"/> range of values starting from the specified index.
        /// </summary>
        /// <param name="index">The starting index of the range.</param>
        /// <param name="count">The number of elements to retrive from the specified index.</param>
        /// <returns><see cref="IEnumerable{T}"/> Range of found elements, or an empty <see cref="Enumerable.Empty{T}"/> if none were found.</returns>
        public IEnumerable<T> GetRange(int index, int count);

        /// <summary>
        /// Gets all values in the <see cref="IDataTree{T}"/> using the specified traversal method (TreeTraversalType).
        /// </summary>
        /// <returns>A collection of all values in the <see cref="IDataTree{T}"/>, traversed in the specified order.</returns>
        public IEnumerable<T> Traverse();

        /// <summary>
        /// Gets all values in the <see cref="IDataTree{T}"/> using the <see cref="TreeTraversalType.InOrder"/>.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> of all values in the <see cref="IDataTree{T}"/>, traversed <see cref="TreeTraversalType.InOrder"/>.</returns>
        public IEnumerable<T> InOrderTraversal();

        /// <summary>
        /// Gets all values in the <see cref="IDataTree{T}"/> using the <see cref="TreeTraversalType.PreOrder"/>.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> of all values in the <see cref="IDataTree{T}"/>, traversed <see cref="TreeTraversalType.PreOrder"/>.</returns>
        public IEnumerable<T> PreOrderTraversal();

        /// <summary>
        /// Gets all values in the <see cref="IDataTree{T}"/> using the <see cref="TreeTraversalType.PostOrder"/>.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> of all values in the <see cref="IDataTree{T}"/>, traversed <see cref="TreeTraversalType.PostOrder"/>.</returns>
        public IEnumerable<T> PostOrderTraversal();

        /// <summary>
        /// Finds the path to the target value using Depth-First Search (DFS).
        /// </summary>
        /// <param name="target">Value to which path should be found.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> of values representing the path to the target element, or an <see cref="Enumerable.Empty{T}"/> of no path found.</returns>
        public IEnumerable<T> DFS(T target);

        // Conversions
        /// <summary>
        /// Gets all of values in the <see cref="IDataTree{T}"/> as an array.
        /// </summary>
        /// <returns>An array of all values in the <see cref="IDataTree{T}"/> </returns>
        public T[] ToArray();

        /// <summary>
        /// Gets all of values in the <see cref="IDataTree{T}"/> as an <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of all values in the <see cref="IDataTree{T}"/></returns>
        public IEnumerable<T> AsEnumerable();

        /// <summary>
        /// Gets all of values in the <see cref="IDataTree{T}"/> as an <see cref="IEnumerator{T}"/>.
        /// </summary>
        /// <returns>An <see cref="IEnumerator{T}"/> of all values in the <see cref="IDataTree{T}"/></returns>
        public IEnumerator<T> GetEnumerator();

        /// <summary>
        /// Gets all of values in the <see cref="IDataTree{T}"/> as an <see cref="ILookup{TKey, TElement}"/>.
        /// </summary>
        /// <returns>An <see cref="ILookup{TKey, TElement}"/> of all values in the <see cref="IDataTree{T}"/></returns>
        public ILookup<T, T> ToLookup(Func<T, T> keySelector);

        /// <summary>
        /// Gets all of values in the <see cref="IDataTree{T}"/> as a <see cref="List{T}"/>.
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of all values in the <see cref="IDataTree{T}"/></returns>
        public List<T> ToList();

        /// <summary>
        /// Gets all of values in the <see cref="IDataTree{T}"/> as a <see cref="LinkedList{T}"/>.
        /// </summary>
        /// <returns>A <see cref="LinkedList{T}"/> of all values in the <see cref="IDataTree{T}"/></returns>
        public LinkedList<T> ToLinkedList();

        /// <summary>
        /// Gets all of values in the <see cref="IDataTree{T}"/> as an <see cref="ImmutableList{T}"/>.
        /// </summary>
        /// <returns>An <see cref="ImmutableList{T}"/> of all values in the <see cref="IDataTree{T}"/></returns>
        public ImmutableList<T> ToImmutableList();

        /// <summary>
        /// Gets all of values in the <see cref="IDataTree{T}"/> as an <see cref="HashSet{T}"/>.
        /// </summary>
        /// <returns>An <see cref="HashSet{T}"/> of all values in the <see cref="IDataTree{T}"/></returns>
        public HashSet<T> ToHashSet();

        /// <summary>
        /// Gets all of values in the <see cref="IDataTree{T}"/> as an <see cref="Stack{T}"/>.
        /// </summary>
        /// <returns>An <see cref="Stack{T}"/> of all values in the <see cref="IDataTree{T}"/></returns>
        public Stack<T> ToStack();

        /// <summary>
        /// Gets all of values in the <see cref="IDataTree{T}"/> as an <see cref="Queue{T}"/>.
        /// </summary>
        /// <returns>An <see cref="Queue{T}"/> of all values in the <see cref="IDataTree{T}"/></returns>
        public Queue<T> ToQueue();
    }
    public enum TreeTraversalType
    {
        InOrder,
        PreOrder,
        PostOrder,
    }

    /// <summary>
    /// Class representing a tree node holding data to its left and right child (<see cref="TreeNode{T}"/>).<br />
    ///
    /// Implements <see cref="IDisposable"/> interface to cleanup the node and all the attached nodes.
    /// </summary>
    public class TreeNode<T> : IDisposable
    {
        public TreeNode(T value, TreeNode<T>? left = null, TreeNode<T>? right = null, TreeNode<T>? parent = null)
        {
            Value = value;
            Left = left;
            Right = right;
            Parent = parent;
        }

        public T Value;
        public int Size { get; internal set; } = 1;
        public bool IsLeaf => Left == null && Right == null;  
        public TreeNode<T>? Left { get; set; } = null;
        public TreeNode<T>? Right { get; set; } = null;
        public TreeNode<T>? Parent { get; set; } = null;

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
    }
}