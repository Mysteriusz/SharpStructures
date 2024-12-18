using System;
using System.Diagnostics;
using SharpStructures.Trees;

namespace SharpStructuresTesting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BinarySearchTree<int> bst = new();
            AVLTree<int> avl = new();
            avl.Add(1);
            avl.Add(2);
            avl.Add(5);
            avl.Add(-3);
            avl.Add(-6);
            avl.Add(12);

            bst.Add(1);
            bst.Add(2);
            bst.Add(5);
            bst.Add(-3);
            bst.Add(-6);
            bst.Add(12);

            //tree.MaxNode(tree.Root).Left = new TreeNode<int>(5);

            List<int> list = [0, 1, 2, 3, 4, 5];
            //BSTNode<int> node = new(2);
            //node.

            Debug.WriteLine(string.Join(", ", avl.Count));
            Debug.WriteLine(string.Join(", ", avl.InOrderTraversal()));
            //Debug.WriteLine(string.Join(", ", avl.Root.Value));

            //Debug.WriteLine(string.Join(", ", tree[5].Value));
            //Debug.WriteLine(string.Join(", ", tree.PostOrderTraversal()));
        }
    }
}