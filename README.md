# Trees
  ### BinarySearchTree&lt;T&gt;
  #### Definition
  `public class BinarySearchTree<T> : IDataTree<T>`
  #### Constructor
  `public BinarySearchTree(TreeNode<T>? root = null, Comparer<T>? comparer = null, TreeTraversalType indexingType = TreeTraversalType.InOrder)`
  #### Interface Implementations Parameters
  | **Interface Parameter**            |**Description**|**MethodType**|
  |--------------------------|---------------|--------------|
  | **[IDataTree<T>.Root](#idataTreeTroot)** | Returns the root of the tree.|[`TreeNode<T>?`](#treenodet)|
  | **IDataTree<T>.IndexingType**| Returns tree`s TreeTraversalType.|`TreeTraversalType`|
  | **IDataTree<T>.Comparator**  | Returns tree`s Comparator.|[`Comparer<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.icomparer?view=net-9.0)|
  | **IDataTree<T>.Height**      | Returns Height of the tree.|`int32`|
  | **IDataTree<T>.LeafCount**   | Returns tree`s leaf nodes count.|`int32`|
  | **IDataTree<T>.Levels**      | Returns tree`s level count.|`int32`|
  | **IDataTree<T>.Count**       | Returns tree`s overall value count.|`int32`|
  | **IDataTree<T>.IsBST**       | Returns if tree is valid.|`bool`|
  | **IDataTree<T>.IsEmpty**     | Returns if tree is empty.|`bool`|
  | **IDataTree<T>.[int index]** | Returns value T from the tree at the specified index.|`T`|

  
# Structures
  ### TreeNode&lt;T&gt;
  #### Definition
  `public class TreeNode<T> : IDisposable`
  #### Constructor
  `public TreeNode(T value, TreeNode<T>? left = null, TreeNode<T>? right = null, TreeNode<T>? parent = null)`
  #### Parameters
  | **Parameter**   |**Description**|**ValueType**|
  |-----------------|---------------|-------------|
  | **Value**       | Returns the value of the node.|`T`|
  | **Size**        | Returns node`s subtree size.|`int32`|
  | **IsLeaf**      | Returns if node is marked as leaf.|`bool`|
  | **Left**        | Returns left child of the node.|[`TreeNode<T>?`](#treenodet)|
  | **Right**       | Returns right child of the node.|[`TreeNode<T>?`](#treenodet)|
  | **Parent**      | Returns parent of the node.|[`TreeNode<T>?`](#treenodet)|
  #### Interface Implementations
  | **Interface**            |**Method**|**Description**|**MethodType**|
  |--------------------------|----------|---------------|--------------|
  | **IDisposable**          |`Dispose()`| Disposes the node and all the child nodes.|`void`|

# Interfaces
  ### IDataTree<T>  
  #### Parameters
  | **Parameter**   |**Description**|**ValueType**|
  |-----------------|---------------|-------------|
  | **Root**        | Returns the root of the tree.|[`TreeNode<T>?`](#treenodet)|
  | **IndexingType**| Returns tree`s TreeTraversalType.|`TreeTraversalType`|
  | **Comparator**  | Returns tree`s Comparator.|[`Comparer<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.icomparer?view=net-9.0)|
  | **Height**      | Returns Height of the tree.|`int32`|
  | **LeafCount**   | Returns tree`s leaf nodes count.|`int32`|
  | **Levels**      | Returns tree`s level count.|`int32`|
  | **Count**       | Returns tree`s overall value count.|`int32`|
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
  | **Cloning**             |`Clone()`| Creates a copy of the current instance of the tree.|[`IDataTree<T>`](#idataTree-t)|
  | **Index Access**        |`GetIndexValue(int index)`| Returns a value at a specified index using the current traversal type.|`T`|
  | **Min/Max**             |`Max()`| Returns the maximum value in the tree.|`T`|
  |                         |`Max(TreeNode<T> node)`| Returns the maximum node starting from the specified node.|[`TreeNode<T>?`](#treenodet)|
  |                         |`Min()`| Returns the minimum value in the tree.|`T`|
  |                         |`Min(TreeNode<T> node)`| Returns the minimum node starting from the specified node.|[`TreeNode<T>?`](#treenodet)|
  | **Successor/Predecessor**|`Successor()`| Returns the successor value of the root node.|`T`|
  |                         | `Successor(TreeNode<T> node)`| Returns the successor node of the specified node.|[`TreeNode<T>?`](#treenodet)|
  |                         |`Predecessor()`| Returns the predecessor value of the root node.|`T`|
  |                         |`Predecessor(TreeNode<T> node)`| Returns the predecessor node of the specified node.|[`TreeNode<T>?`](#treenodet)|
  | **Range Access**        |`GetRange(int index, int count)`| Returns a range of values as `IEnumerable<T>` from the specified index.|[`IEnumerable<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-9.0)|
  | **Traversal**           |`Traverse()`| Returns all values using the specified traversal method.|[`IEnumerable<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-9.0)|
  |                         |`InOrderTraversal()`| Returns all values using InOrder traversal.|[`IEnumerable<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-9.0)|
  |                         |`PreOrderTraversal()`| Returns all values using PreOrder traversal.|[`IEnumerable<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-9.0)|
  |                         |`PostOrderTraversal()`| Returns all values using PostOrder traversal.|[`IEnumerable<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-9.0)|
  | **Path Search**         |`DFS(T target)`| Returns a path from the Root to the target value.|[`IEnumerable<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-9.0)|
  | **Conversion Methods**  |`ToArray()`| Returns all values using the current traversal type.|[`T[]`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/arrays)|
  |                         |`AsEnumerable()`| Returns all values using the current traversal type.|[`IEnumerable<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-9.0)|
  |                         |`GetEnumerator()`| Returns all values using the current traversal type.|[`IEnumerable<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-9.0)|
  |                         |`ToLookup()`| Returns `ILookup<T, T>` of all values using the current traversal type.|[`ILookup<T, T>`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.ilookup-2?view=net-9.0)|
  |                         |`ToLinkedList()`| Returns `LinkedList<T>` of all values using the current traversal type.|[`LinkedList<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.linkedlist-1?view=net-9.0)|
  |                         |`ToImmutableList()`| Returns `ImmutableList<T>` of all values using the current traversal type.|[`ImmutableList<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.immutable.immutablelist-1?view=net-9.0)|
  |                         |`ToHashSet()`| Returns `HashSet<T>` of all values using the current traversal type.|[`HashSet<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1?view=net-9.0)|
  |                         |`ToStack()`| Returns `Stack<T>` of all values using the current traversal type.|[`Stack<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1?view=net-9.0)|
  |                         |`ToQueue()`| Returns `Queue<T>` of all values using the current traversal type.|[`Queue<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1?view=net-9.0)|
