# SharpStructures
  ## Interfaces
  ### IDataTree<T>  
#### Parameters
| **Parameter**   |**Description**|**ValueType**|
|-----------------|---------------|-------------|
| **Root**        | Returns the root of the tree.|`TreeNode<T>`|
| **IndexingType**| Returns trees TreeTraversalType.|`TreeTraversalType`|
| **Comparator**  | Returns trees Comparator.|`Comparator<T>`|
| **Height**      | Returns Height of the tree.|`int32`|
| **LeafCount**   | Returns trees leaf nodes count.|`int32`|
| **Levels**      | Returns trees level count.|`int32`|
| **Count**       | Returns trees overall value count.|`int32`|
| **IsBST**       | Returns if tree is valid.|`bool`|
| **IsEmpty**     | Returns if tree is empty.|`bool`|
| **[int index]** | Returns value T from the tree at the specified index.|`T`|
#### Methods
| **Category**            |**Method**|**Description**|**MethodType**|
|-------------------------|----------|---------------|--------------|
| **Insertion**           |`Add(T value)`| Inserts a value into the tree.|`void`|
|                         |`AddRange(T[] values)`| Inserts an array of values into the tree.|`void`|
| **Deletion**            |`Remove(T value)`| Removes a value from the tree.|`void`|
|                         |`RemoveRange(T[] values)`| Removes an array of values from the tree.|`void`|
| **Clearing**            |`Clear()`| Clears all elements in the tree.|`void`|
| **Search**              |`Contains(T value)`| Checks if a value is present in the tree.|`bool`|
|                         |`Find(Func<T, bool> predicate)`| Checks for a value with specified condition.|`T`|
| **Cloning**             |`Clone()`| Creates a copy of the current instance of the tree.|`IDataTree<T>`|
| **Index Access**        |`GetIndexValue(int index)`| Returns a value at a specified index using the current traversal type.|`T`|
| **Min/Max**             |`Max()`| Returns the maximum value in the tree.|`T`|
|                         |`Max(TreeNode<T> node)`| Returns the maximum node starting from the specified node.|`TreeNode<T>`|
|                         |`Min()`| Returns the minimum value in the tree.|`T`|
|                         |`Min(TreeNode<T> node)`| Returns the minimum node starting from the specified node.|`TreeNode<T>`|
| **Successor/Predecessor**|`Successor()`| Returns the successor value of the root node.|`T`|
|                         | `Successor(TreeNode<T> node)`| Returns the successor node of the specified node.|`TreeNode<T>`|
|                         |`Predecessor()`| Returns the predecessor value of the root node.|`T`|
|                         |`Predecessor(TreeNode<T> node)`| Returns the predecessor node of the specified node.|`TreeNode<T>`|
| **Range Access**        |`GetRange(int index, int count)`| Returns a range of values as `IEnumerable<T>` from the specified index.|`IEnumerable<T>`|
| **Traversal**           |`Traverse()`| Returns `IEnumerable<T>` of all values using the specified traversal method.|`IEnumerable<T>`|
|                         |`InOrderTraversal()`| Returns `IEnumerable<T>` using InOrder traversal.|`IEnumerable<T>`|
|                         |`PreOrderTraversal()`| Returns `IEnumerable<T>` using PreOrder traversal.|`IEnumerable<T>`|
|                         |`PostOrderTraversal()`| Returns `IEnumerable<T>` using PostOrder traversal.|`IEnumerable<T>`|
| **Path Search**         |`DFS(T target)`| Returns `IEnumerable<T>` of a path from the root to the target value.|`IEnumerable<T>`|
| **Conversion Methods**  |`ToArray()`| Returns `T[]` of all values using the current traversal type.|`T[]`|
|                         |`AsEnumerable()`| Returns `IEnumerable<T>` of all values using the current traversal type.|`IEnumerable<T>`|
|                         |`GetEnumerator()`| Returns `IEnumerator<T>` using the current traversal type.|`IEnumerator<T>`|
|                         |`ToLookup()`| Returns `ILookup<T, T>` of all values using the current traversal type.|`ILookup<T, T>`|
|                         |`ToLinkedList()`| Returns `LinkedList<T>` of all values using the current traversal type.|`LinkedList<T>`|
|                         |`ToImmutableList()`| Returns `ImmutableList<T>` of all values using the current traversal type.|`ImmutableList<T>`|
|                         |`ToHashSet()`| Returns `HashSet<T>` of all values using the current traversal type.|`HashSet<T>`|
|                         |`ToStack()`| Returns `Stack<T>` of all values using the current traversal type.|`Stack<T>`|
|                         |`ToQueue()`| Returns `Queue<T>` of all values using the current traversal type.|`Queue<T>`|
