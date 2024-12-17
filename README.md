# SharpStructures
  ## Interfaces
  ### IDataTree<T>  
#### Parameters
| **Parameter**   |**Description**|
| **Root**        | Returns the root of the tree.|
| **IndexingType**| Returns trees TreeTraversalType.|
| **Comparator**  | Returns trees Comparator.|
| **Height**      | Returns Height of the tree.|
| **LeafCount**   | Returns trees leaf nodes count.|
| **Levels**      | Returns trees level count.|
| **Count**       | Returns trees overall value count.|
| **IsBST**       | Returns if tree is valid.|
| **IsEmpty**     | Returns if tree is empty.|
| **[int index]** | Returns value T from the tree at the specified index.|
#### Methods
| **Category**            |**Method**|**Description**|
|-------------------------|-----------|----------------|
| **Insertion**           |`Add(T value)`| Inserts a value into the tree.|
|                         |`AddRange(T[] values)`| Inserts an array of values into the tree.|
| **Deletion**            |`Remove(T value)`| Removes a value from the tree.|
|                         |`RemoveRange(T[] values)`| Removes an array of values from the tree.|
| **Clearing**            |`Clear()`| Clears all elements in the tree.|
| **Search**              |`Contains(T value)`| Checks if a value is present in the tree.|
| **Cloning**             |`Clone()`| Creates a copy of the current instance of the tree.|
| **Index Access**        |`GetIndexValue(int index)`| Returns a value at a specified index using the current traversal type.|
| **Min/Max**             |`Max()`| Returns the maximum value in the tree.|
|                         |`Max(TreeNode<T> node)`| Returns the maximum node starting from the specified node.|
|                         |`Min()`| Returns the minimum value in the tree.|
|                         |`Min(TreeNode<T> node)`| Returns the minimum node starting from the specified node.|
| **Successor/Predecessor**|`Successor()`| Returns the successor value of the root node.|
|                         | `Successor(TreeNode<T> node)`| Returns the successor node of the specified node.|
|                         |`Predecessor()`| Returns the predecessor value of the root node.|
|                         |`Predecessor(TreeNode<T> node)`| Returns the predecessor node of the specified node.|
| **Range Access**        |`GetRange(int index, int count)`| Returns a range of values as `IEnumerable<T>` from the specified index.|
| **Traversal**           |`Traverse()`| Returns `IEnumerable<T>` of all values using the specified traversal method.|
|                         |`InOrderTraversal()`| Returns `IEnumerable<T>` using InOrder traversal.|
|                         |`PreOrderTraversal()`| Returns `IEnumerable<T>` using PreOrder traversal.|
|                         |`PostOrderTraversal()`| Returns `IEnumerable<T>` using PostOrder traversal.|
| **Path Search**         |`DFS(T target)`| Returns `IEnumerable<T>` of a path from the root to the target value.|
| **Conversion Methods**  |`ToArray()`| Returns `T[]` of all values using the current traversal type.|
|                         |`AsEnumerable()`| Returns `IEnumerable<T>` of all values using the current traversal type.|
|                         |`GetEnumerator()`| Returns `IEnumerator<T>` using the current traversal type.|
|                         |`ToLookup()`| Returns `ILookup<T, T>` of all values using the current traversal type.|
|                         |`ToLinkedList()`| Returns `LinkedList<T>` of all values using the current traversal type.|
|                         |`ToImmutableList()`| Returns `ImmutableList<T>` of all values using the current traversal type.|
|                         |`ToHashSet()`| Returns `HashSet<T>` of all values using the current traversal type.|
|                         |`ToStack()`| Returns `Stack<T>` of all values using the current traversal type.|
|                         |`ToQueue()`| Returns `Queue<T>` of all values using the current traversal type.|

