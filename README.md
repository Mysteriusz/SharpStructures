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
  - **Max()** Returns maximum value of the tree.
  - **Min()** Returns miniumum value of the tree.
  - **Successor()**
  - - **Successor()** Returns successor of the Root node.
  - - **Successor(TreeNode<T> node)** Returns successor of the specified node.
  - **Predecessor()**
  - - **Predecessor()** Returns predecessor of the Root node.
  - - **Predecessor(TreeNode<T> node)** Returns predecessor of the specified node.
