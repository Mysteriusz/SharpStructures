using System;
using System.Diagnostics;
using SharpStructures.Trees;

namespace SharpStructuresTesting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BinarySearchTree<int> tree = new();
            tree.Add(1);
            tree.Add(2);
            tree.Add(5);
            tree.Add(-3);
            tree.Add(-6);
            tree.Add(12);

            //tree.MaxNode(tree.Root).Left = new TreeNode<int>(5);

            List<int> list = [0, 1, 2, 3, 4, 5];
            tree.IndexingType = TreeTraversalType.InOrder;
            TreeNode<int> node = new(0);

            //Debug.WriteLine(string.Join(", ", list.GetRange(0, -1)));
            Debug.WriteLine(string.Join(", ", tree.Root));
            Debug.WriteLine(string.Join(", ", tree.InOrderTraversal()));

            //Debug.WriteLine(string.Join(", ", tree[5].Value));
            //Debug.WriteLine(string.Join(", ", tree.PostOrderTraversal()));
        }
    }
}