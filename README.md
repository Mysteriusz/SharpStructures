# SharpStructures
  ## Interfaces
  ### IDataTree<T>
  Interface for tree data structures.

  #### Methods
  - **Add(T value)**: Inserts a value into the tree.
  - **AddRange(T[] values)** Inserts an array of values from the tree.
  - **Remove(T value)** Removes a value from the tree.
  - **RemoveRange(T[] values)** Removes an array of values from the tree.
  - **Clear()** Clears all elements in the tree.
  - **Contains(T value)** Checks if values is present in the tree.
  - **Clone()** Creates a copy of the current instance of IDataTree<T> as a new one.
  - **GetIndexValue(int index)** Returns a value at specified index using currently set TreeTraversalType.
  - **Max()**
    - **Max()** Returns maximum T of the tree.
    - **Max(TreeNode<T> node)** Returns maximum TreeNode<T> from the specified node.
  - **Min()** Returns miniumum value of the tree.
    - **Min()** Returns minimum T of the tree.
    - **Min(TreeNode<T> node)** Returns minimum TreeNode<T> from the specified node.
  - **Successor()**
    - **Successor()** Returns successor T of the Root node.
    - **Successor(TreeNode<T> node)** Returns successor TreeNode<T> of the specified node.
  - **Predecessor()**
    - **Predecessor()** Returns predecessor T of the Root node.
    - **Predecessor(TreeNode<T> node)** Returns predecessor TreeNode<T> of the specified node.
  - **GetRange(int index, int count)** Returns range of values as IEnumerable<T> from index.
  - **Traverse()** Returns IEnumerable<T> of all values in the tree using specified in tree Parameters traversal method.
  - **InOrderTraversal()** Returns IEnumerable<T> of all values in the tree using InOrder traversal method.
  - **PreOrderTraversal()** Returns IEnumerable<T> of all values in the tree using PreOrder traversal method.
  - **PostOrderTraversal()** Returns IEnumerable<T> of all values in the tree using PostOrder traversal method.
  - **DFS(T target)** Returns IEnumerable<T> of a path from the Root to specified target value.
   
  - #### Conversion Methods
  - **ToArray()** Returns <span style="color: #FF5733;">T[]</span> of all the values in the tree using TreeTraversalType.
  - **AsEnumerable()** Returns IEnumerable<T> of all the values in the tree using TreeTraversalType.
  - **GetEnumerator()** Returns IEnumerator<T> of all the values in the tree using TreeTraversalType.
  - **ToLookup()** Returns ILookup<T, T> of all values in the tree using TreeTraversalType.
  - **ToLinkedList()** Returns LinkedList<T> of all values in the tree using TreeTraversalType.
  - **ToImmutableList()** Returns ImmutableList<T> of all values in the tree using TreeTraversalType.
  - **ToHashSet()** Returns HashSet<T> of all values in the tree using TreeTraversalType.
  - **ToStack()** Returns Stack<T> of all values in the tree using TreeTraversalType.
  - **ToQueue()** Returns Queue<T> of all values in the tree using TreeTraversalType.
