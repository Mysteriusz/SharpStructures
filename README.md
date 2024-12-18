# Trees
<details>
  <summary>BinarySearchTree&lt;T&gt;</summary>
    
  ### Definition
  ```csharp
  public class BinarySearchTree<T> : IDataTree<T, BSTNode<T>>
  ```
  ### Constructor
  ```csharp 
  public BinarySearchTree(BSTNode<T>? root = null, Comparer<T>? comparer = null, TreeTraversalType traversalType = TreeTraversalType.InOrder)
  ```

  #### Interface Parameter Implementations
  | **Interface**            |**Parameter**|**Description**|**MethodType**|
  |--------------------------|-------------|---------------|--------------|
  | **IDataTree<T, TNode>**          |`Root`       | Returns the root of the tree.|[`BSTNode<T>?`](#bstnode)|
  | **IDataTree<T, TNode>**          |`TraversalType`| Returns tree's TreeTraversalType.|[`TreeTraversalType`](#treetraversaltype)|
  | **IDataTree<T, TNode>**          |`Comparator` | Returns tree's Comparator.|[`Comparer<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.icomparer?view=net-9.0)|
  | **IDataTree<T, TNode>**          |`Height`     | Returns Height of the tree.|`int32`|
  | **IDataTree<T, TNode>**          |`LeafCount`  | Returns tree's leaf nodes count.|`int32`|
  | **IDataTree<T, TNode>**          |`Levels`     | Returns tree's level count.|`int32`|
  | **IDataTree<T, TNode>**          |`Count`      | Returns tree's overall value count.|`int32`|
  | **IDataTree<T, TNode>**          |`IsValid`      | Returns if tree is valid.|`bool`|
  | **IDataTree<T, TNode>**          |`IsEmpty`    | Returns if tree is empty.|`bool`|
  | **IDataTree<T, TNode>**          |`[int index]`| Returns value T from the tree at the specified index.|`T`|
  
  #### Interface Methods Implementations 
  | **Interface**            |**Method**                 |**Description**                                   |**MethodType**|
  |--------------------------|---------------------------|--------------------------------------------------|--------------|
  | **IDataTree<T, TNode>**          |`Add(T value)`             | Inserts a value into the tree.                   |`void`|
  | **IDataTree<T, TNode>**          |`AddRange(T[] values)`     | Inserts an array of values into the tree.        |`void`|
  | **IDataTree<T, TNode>**          |`Remove(T value)`          | Removes a value from the tree.                   |`void`|
  | **IDataTree<T, TNode>**          |`RemoveRange(T[] values)`  | Removes an array of values from the tree.        |`void`|
  | **IDataTree<T, TNode>**          |`Clear()`                  | Clears all elements in the tree.                 |`void`|
  | **IDataTree<T, TNode>**          |`Contains(T value)`        | Checks if a value is present in the tree.        |`bool`|
  | **IDataTree<T, TNode>**          |`Find(Func<T, bool> predicate)`| Checks for a value with a specified condition.|`T`|
  | **IDataTree<T, TNode>**          |`Find(Func<T, bool> predicate, BSTNode<T>? node)`| Checks for a value with a specified condition from specified node.|`T`|
  | **IDataTree<T, TNode>**          |`Clone()`                  | Creates a copy of the current instance of the tree.|[`IDataTree<T>`](#idatatree)|
  | **IDataTree<T, TNode>**          |`GetIndexValue(int index)` | Returns a value at a specified index using the current traversal type.|`T`|
  | **IDataTree<T, TNode>**          |`Max()`                    | Returns the maximum value in the tree.           |`T`|
  | **IDataTree<T, TNode>**          |`Max(BSTNode<T> node)`    | Returns the maximum node starting from the specified node.|[`BSTNode<T>?`](#bstnode)|
  | **IDataTree<T, TNode>**          |`Min()`                    | Returns the minimum value in the tree.           |`T`|
  | **IDataTree<T, TNode>**          |`Min(BSTNode<T> node)`    | Returns the minimum node starting from the specified node.|[`BSTNode<T>?`](#bstnode)|
  | **IDataTree<T, TNode>**          |`Successor()`              | Returns the successor value of the root node.    |`T`|
  | **IDataTree<T, TNode>**          |`Successor(BSTNode<T> node)`| Returns the successor node of the specified node.|[`BSTNode<T>?`](#bstnode)|
  | **IDataTree<T, TNode>**          |`Predecessor()`            | Returns the predecessor value of the root node.  |`T`|
  | **IDataTree<T, TNode>**          |`Predecessor(BSTNode<T> node)`| Returns the predecessor node of the specified node.|[`BSTNode<T>?`](#bstnode)|
  | **IDataTree<T, TNode>**          |`GetRange(int index, int count)`| Returns a range of values as `IEnumerable<T>` from the specified index.|[`IEnumerable<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-9.0)|
  | **IDataTree<T, TNode>**          |`Traverse()`               | Returns all values using the specified traversal method.|[`IEnumerable<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-9.0)|
  | **IDataTree<T, TNode>**          |`InOrderTraversal()`       | Returns all values using InOrder traversal.      |[`IEnumerable<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-9.0)|
  | **IDataTree<T, TNode>**          |`PreOrderTraversal()`      | Returns all values using PreOrder traversal.     |[`IEnumerable<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-9.0)|
  | **IDataTree<T, TNode>**          |`PostOrderTraversal()`     | Returns all values using PostOrder traversal.    |[`IEnumerable<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-9.0)|
  | **IDataTree<T, TNode>**          |`DFS(T target)`            | Returns a path from the Root to the target value.|[`IEnumerable<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-9.0)|
  | **IDataTree<T, TNode>**          |`ToArray()`                | Returns all values using the current traversal type.|[`T[]`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/arrays)|
  | **IDataTree<T, TNode>**          |`AsEnumerable()`           | Returns all values using the current traversal type.|[`IEnumerable<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-9.0)|
  | **IDataTree<T, TNode>**          |`GetEnumerator()`          | Returns all values using the current traversal type.|[`IEnumerable<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-9.0)|
  | **IDataTree<T, TNode>**          |`ToLookup()`               | Returns `ILookup<T, T>` of all values using the current traversal type.|[`ILookup<T, T>`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.ilookup-2?view=net-9.0)|
  | **IDataTree<T, TNode>**          |`ToLinkedList()`           | Returns `LinkedList<T>` of all values using the current traversal type.|[`LinkedList<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.linkedlist-1?view=net-9.0)|
  | **IDataTree<T, TNode>**          |`ToImmutableList()`        | Returns `ImmutableList<T>` of all values using the current traversal type.|[`ImmutableList<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.immutable.immutablelist-1?view=net-9.0)|
  | **IDataTree<T, TNode>**          |`ToHashSet()`              | Returns `HashSet<T>` of all values using the current traversal type.|[`HashSet<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1?view=net-9.0)|
  | **IDataTree<T, TNode>**          |`ToStack()`                | Returns `Stack<T>` of all values using the current traversal type.|[`Stack<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1?view=net-9.0)|
  | **IDataTree<T, TNode>**          |`ToQueue()`                | Returns `Queue<T>` of all values using the current traversal type.|[`Queue<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1?view=net-9.0)|
</details>

---

<details>
  <summary>AVLTree&lt;T&gt;</summary>
    
  ### Definition
  ```csharp
  public class AVLTree<T> : IDataTree<T, AVLNode<T>>
  ```
  ### Constructor
  ```csharp 
  public AVLTree(AVLNode<T>? root = null, Comparer<T>? comparer = null, TreeTraversalType traversalType = TreeTraversalType.InOrder)
  ```

  #### Interface Parameter Implementations
  | **Interface**            |**Parameter**|**Description**|**MethodType**|
  |--------------------------|-------------|---------------|--------------|
  | **IDataTree<T, TNode>**          |`Root`       | Returns the root of the tree.|[`AVLNode<T>?`](#avlnode)|
  | **IDataTree<T, TNode>**          |`TraversalType`| Returns tree's TreeTraversalType.|[`TreeTraversalType`](#treetraversaltype)|
  | **IDataTree<T, TNode>**          |`Comparator` | Returns tree's Comparator.|[`Comparer<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.icomparer?view=net-9.0)|
  | **IDataTree<T, TNode>**          |`Height`     | Returns Height of the tree.|`int32`|
  | **IDataTree<T, TNode>**          |`LeafCount`  | Returns tree's leaf nodes count.|`int32`|
  | **IDataTree<T, TNode>**          |`Levels`     | Returns tree's level count.|`int32`|
  | **IDataTree<T, TNode>**          |`Count`      | Returns tree's overall value count.|`int32`|
  | **IDataTree<T, TNode>**          |`IsValid`      | Returns if tree is valid.|`bool`|
  | **IDataTree<T, TNode>**          |`IsEmpty`    | Returns if tree is empty.|`bool`|
  | **IDataTree<T, TNode>**          |`[int index]`| Returns value T from the tree at the specified index.|`T`|
  
  #### Interface Methods Implementations 
  | **Interface**            |**Method**                 |**Description**                                   |**MethodType**|
  |--------------------------|---------------------------|--------------------------------------------------|--------------|
  | **IDataTree<T, TNode>**          |`Add(T value)`             | Inserts a value into the tree.                   |`void`|
  | **IDataTree<T, TNode>**          |`AddRange(T[] values)`     | Inserts an array of values into the tree.        |`void`|
  | **IDataTree<T, TNode>**          |`Remove(T value)`          | Removes a value from the tree.                   |`void`|
  | **IDataTree<T, TNode>**          |`RemoveRange(T[] values)`  | Removes an array of values from the tree.        |`void`|
  | **IDataTree<T, TNode>**          |`Clear()`                  | Clears all elements in the tree.                 |`void`|
  | **IDataTree<T, TNode>**          |`Contains(T value)`        | Checks if a value is present in the tree.        |`bool`|
  | **IDataTree<T, TNode>**          |`Find(Func<T, bool> predicate)`| Checks for a value with a specified condition.|`T`|
  | **IDataTree<T, TNode>**          |`Find(Func<T, bool> predicate, AVLNode<T>? node)`| Checks for a value with a specified condition from specified node.|`T`|
  | **IDataTree<T, TNode>**          |`Clone()`                  | Creates a copy of the current instance of the tree.|[`IDataTree<T>`](#idatatree)|
  | **IDataTree<T, TNode>**          |`GetIndexValue(int index)` | Returns a value at a specified index using the current traversal type.|`T`|
  | **IDataTree<T, TNode>**          |`Max()`                    | Returns the maximum value in the tree.           |`T`|
  | **IDataTree<T, TNode>**          |`Max(AVLNode<T> node)`    | Returns the maximum node starting from the specified node.|[`AVLNode<T>?`](#avlnode)|
  | **IDataTree<T, TNode>**          |`Min()`                    | Returns the minimum value in the tree.           |`T`|
  | **IDataTree<T, TNode>**          |`Min(AVLNode<T> node)`    | Returns the minimum node starting from the specified node.|[`AVLNode<T>?`](#avlnode)|
  | **IDataTree<T, TNode>**          |`Successor()`              | Returns the successor value of the root node.    |`T`|
  | **IDataTree<T, TNode>**          |`Successor(AVLNode<T> node)`| Returns the successor node of the specified node.|[`AVLNode<T>?`](#avlnode)|
  | **IDataTree<T, TNode>**          |`Predecessor()`            | Returns the predecessor value of the root node.  |`T`|
  | **IDataTree<T, TNode>**          |`Predecessor(AVLNode<T> node)`| Returns the predecessor node of the specified node.|[`AVLNode<T>?`](#avlnode)|
  | **IDataTree<T, TNode>**          |`GetRange(int index, int count)`| Returns a range of values as `IEnumerable<T>` from the specified index.|[`IEnumerable<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-9.0)|
  | **IDataTree<T, TNode>**          |`Traverse()`               | Returns all values using the specified traversal method.|[`IEnumerable<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-9.0)|
  | **IDataTree<T, TNode>**          |`InOrderTraversal()`       | Returns all values using InOrder traversal.      |[`IEnumerable<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-9.0)|
  | **IDataTree<T, TNode>**          |`PreOrderTraversal()`      | Returns all values using PreOrder traversal.     |[`IEnumerable<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-9.0)|
  | **IDataTree<T, TNode>**          |`PostOrderTraversal()`     | Returns all values using PostOrder traversal.    |[`IEnumerable<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-9.0)|
  | **IDataTree<T, TNode>**          |`DFS(T target)`            | Returns a path from the Root to the target value.|[`IEnumerable<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-9.0)|
  | **IDataTree<T, TNode>**          |`ToArray()`                | Returns all values using the current traversal type.|[`T[]`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/arrays)|
  | **IDataTree<T, TNode>**          |`AsEnumerable()`           | Returns all values using the current traversal type.|[`IEnumerable<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-9.0)|
  | **IDataTree<T, TNode>**          |`GetEnumerator()`          | Returns all values using the current traversal type.|[`IEnumerable<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-9.0)|
  | **IDataTree<T, TNode>**          |`ToLookup()`               | Returns `ILookup<T, T>` of all values using the current traversal type.|[`ILookup<T, T>`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.ilookup-2?view=net-9.0)|
  | **IDataTree<T, TNode>**          |`ToLinkedList()`           | Returns `LinkedList<T>` of all values using the current traversal type.|[`LinkedList<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.linkedlist-1?view=net-9.0)|
  | **IDataTree<T, TNode>**          |`ToImmutableList()`        | Returns `ImmutableList<T>` of all values using the current traversal type.|[`ImmutableList<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.immutable.immutablelist-1?view=net-9.0)|
  | **IDataTree<T, TNode>**          |`ToHashSet()`              | Returns `HashSet<T>` of all values using the current traversal type.|[`HashSet<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1?view=net-9.0)|
  | **IDataTree<T, TNode>**          |`ToStack()`                | Returns `Stack<T>` of all values using the current traversal type.|[`Stack<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1?view=net-9.0)|
  | **IDataTree<T, TNode>**          |`ToQueue()`                | Returns `Queue<T>` of all values using the current traversal type.|[`Queue<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1?view=net-9.0)|
</details>

---

# Structures
<details>
  <a id="treenode"></a>
  <summary>TreeNode&lt;T&gt;</summary>
  
  #### Definition
  ```csharp
  public class TreeNode<T> : IDisposable
  ```
  #### Constructor
  ```csharp
  public TreeNode(T value, TreeNode<T>? left = null, TreeNode<T>? right = null, TreeNode<T>? parent = null)
  ```

  #### Parameters
  | **Parameter**   |**Description**|**ValueType**|
  |-----------------|---------------|-------------|
  | **Value**       | Returns the value of the node.|`T`|
  | **IsLeaf**      | Returns if node is marked as leaf.|`bool`|
  | **Left**        | Returns left child of the node.|[`TreeNode<T>?`](#treenode)|
  | **Right**       | Returns right child of the node.|[`TreeNode<T>?`](#treenode)|
  | **Parent**      | Returns parent of the node.|[`TreeNode<T>?`](#treenode)|
  #### Interface Methods Implementations 
  | **Interface**            |**Method**|**Description**|**MethodType**|
  |--------------------------|----------|---------------|--------------|
  | **IDisposable**          |`Dispose()`| Disposes the node and all the child nodes.|`void`|
</details>

---

<details>
  <a id="bstnode"></a>
  <summary>BSTNode&lt;T&gt;</summary>
  
  #### Definition
  ```csharp
  public class BSTNode<T> : TreeNode<T>
  ```
  #### Constructor
  ```csharp
  public BSTNode(T value, BSTNode<T>? left = null, BSTNode<T>? right = null, BSTNode<T>? parent = null)
  ```

  #### Parameters
  | **Parameter**   |**Description**|**ValueType**|
  |-----------------|---------------|-------------|
  | **Left**        | Returns left child of the node.|[`BSTNode<T>?`](#bstnode)|
  | **Right**       | Returns right child of the node.|[`BSTNode<T>?`](#bstnode)|
  | **Parent**      | Returns parent of the node.|[`BSTNode<T>?`](#bstnode)|
  #### Inheritence Parameters
  | **Parameter**   |**Description**|**ValueType**|
  |-----------------|---------------|-------------|
  | **Value**       | Returns the value of the node.|`T`|
  | **IsLeaf**      | Returns if node is marked as leaf.|`bool`|
  | **Left**        | Returns left child of the node.|[`TreeNode<T>?`](#treenode)|
  | **Right**       | Returns right child of the node.|[`TreeNode<T>?`](#treenode)|
  | **Parent**      | Returns parent of the node.|[`TreeNode<T>?`](#treenode)|
  #### Inheritence Methods
  | **Inheritence**            |**Method**|**Description**|**MethodType**|
  |--------------------------|----------|---------------|--------------|
  | **TreeNode<T>**          |`Dispose()`| Disposes the node and all the child nodes.|`void`|
</details>

---

<details>
  <a id="avlnode"></a>
  <summary>AVLNode&lt;T&gt;</summary>
  
  #### Definition
  ```csharp
  public class AVLNode<T> : TreeNode<T>
  ```
  #### Constructor
  ```csharp
  public AVLNode(T value, AVLNode<T>? left = null, AVLNode<T>? right = null, AVLNode<T>? parent = null)
  ```

  #### Parameters
  | **Parameter**   |**Description**|**ValueType**|
  |-----------------|---------------|-------------|
  | **BalanceFactor**| Returns node`s current balance factor.|`int32`|
  | **Left**        | Returns left child of the node.|[`AVLNode<T>?`](#avlnode)|
  | **Right**       | Returns right child of the node.|[`AVLNode<T>?`](#avlnode)|
  | **Parent**      | Returns parent of the node.|[`AVLNode<T>?`](#avlnode)|
  #### Inheritence Parameters
  | **Parameter**   |**Description**|**ValueType**|
  |-----------------|---------------|-------------|
  | **Value**       | Returns the value of the node.|`T`|
  | **IsLeaf**      | Returns if node is marked as leaf.|`bool`|
  | **Left**        | Returns left child of the node.|[`TreeNode<T>?`](#treenode)|
  | **Right**       | Returns right child of the node.|[`TreeNode<T>?`](#treenode)|
  | **Parent**      | Returns parent of the node.|[`TreeNode<T>?`](#treenode)|
  #### Inheritence Methods
  | **Inheritence**            |**Method**|**Description**|**MethodType**|
  |--------------------------|----------|---------------|--------------|
  | **TreeNode<T>**          |`Dispose()`| Disposes the node and all the child nodes.|`void`|
</details>

---

<details>
  <a id="treetraversaltype"></a>
  <summary>TreeTraversalType</summary>
  
  #### Definition
  ```csharp
  public enum TreeTraversalType
  ```
  #### Fields
  | **Name**   |**Description**|**Value**|
  |-----------------|---------------|-------------|
  | **InOrder**       | InOrder traversal type.|`0`|
  | **PreOrder**        | PreOrder traversal type.|`1`|
  | **PostOrder**      | PostOrder traversal type.|`2`|
</details>

---

# Interfaces
> [!IMPORTANT] 
> TNode value must inherit [`TreeNode<T>`](#treenode) or be the [`TreeNode<T>`](#treenode) itself
<a id="idatatree"></a>
<details>
  <summary>IDataTree&lt;T, TNode&gt;</summary>

  #### Definition
  ``` csharp
  public interface IDataTree<T, TNode> where TNode : TreeNode<T>
  ```

  #### Parameters
  | **Parameter**    |**Description**|**ValueType**|
  |------------------|---------------|-------------|
  | **Root**         | Returns the root of the tree.|[`TNode?`](#treenode)|
  | **TraversalType**| Returns tree`s TreeTraversalType.|[`TreeTraversalType`](#treetraversaltype)|
  | **Comparator**   | Returns tree`s Comparator.|[`Comparer<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.comparer-1?view=net-9.0)|
  | **Height**       | Returns Height of the tree.|`int32`|
  | **LeafCount**    | Returns tree`s leaf nodes count.|`int32`|
  | **Levels**       | Returns tree`s level count.|`int32`|
  | **Count**        | Returns tree`s overall value count.|`int32`|
  | **IsValis**        | Returns if tree is valid.|`bool`|
  | **IsEmpty**      | Returns if tree is empty.|`bool`|
  | **[int index]**  | Returns value T from the tree at the specified index.|`T`|
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
  |                         |`Find(Func<T, bool> predicate, TNode? node)`| Checks for a value with specified condition from specified node.|`T`|
  | **Cloning**             |`Clone()`| Creates a copy of the current instance of the tree.|[`IDataTree<T>`](#idatatree)|
  | **Index Access**        |`GetIndexValue(int index)`| Returns a value at a specified index using the current traversal type.|`T`|
  | **Min/Max**             |`Max()`| Returns the maximum value in the tree.|`T`|
  |                         |`Max(TNode node)`| Returns the maximum node starting from the specified node.|[`TNode?`](#treenode)|
  |                         |`Min()`| Returns the minimum value in the tree.|`T`|
  |                         |`Min(TNode node)`| Returns the minimum node starting from the specified node.|[`TNode?`](#treenode)|
  | **Successor/Predecessor**|`Successor()`| Returns the successor value of the root node.|`T`|
  |                         | `Successor(TNode node)`| Returns the successor node of the specified node.|[`TNode?`](#treenode)|
  |                         |`Predecessor()`| Returns the predecessor value of the root node.|`T`|
  |                         |`Predecessor(TNode node)`| Returns the predecessor node of the specified node.|[`TNode?`](#treenode)|
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
</details>

---
