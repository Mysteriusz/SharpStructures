using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpStructures.Trees
{
    public class AVLTree<T>
    {
        public AVLTree(TreeNode<T>? root = null, Comparer<T>? comparer = null, TreeTraversalType traversalType = TreeTraversalType.InOrder)
        {
            if (typeof(IComparable<T>).IsAssignableFrom(typeof(T)) || comparer != null)
                Comparator = comparer ?? Comparer<T>.Default;
            else
                throw new ArgumentNullException(nameof(comparer), "Comparer is required for types that do not implement IComparable.");

            TraversalType = traversalType;
            Root = root;
        }

        // Properties
        public TreeNode<T>? Root { get; set; }
        public TreeTraversalType TraversalType { get; set; }
        public Comparer<T> Comparator { get; set; }
    }
}
