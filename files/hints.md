This problem can be solved using graph traversal algorithms such as depth-first search (DFS) or breadth-first search (BFS). 

To start, you need to represent the cave system as a graph. Each cave will be a node, and the connections between caves will be the edges of the graph. You can use a dictionary or a list of lists to represent the graph.

Once you have the graph representation, you can perform a DFS or BFS starting from the "start" node and stopping when you reach the "end" node. During the traversal, you need to keep track of visited small caves to ensure that they are not visited more than once.

To find all distinct paths, you can use recursion in the DFS or BFS algorithm. Each time you reach the "end" node, you can store the path and continue exploring other paths.

Finally, you need to count the number of distinct paths that satisfy the given conditions (visiting small caves at most once).

Here are the general steps to solve the problem:

1. Parse the input and create a graph representation.
2. Implement a DFS or BFS algorithm to traverse the graph, keeping track of visited small caves.
3. Use recursion to find all distinct paths from the "start" node to the "end" node.
4. Count the number of distinct paths that satisfy the conditions.
5. Return the count as the result.

Let me know if you need any further help with the specific implementation of the solution.