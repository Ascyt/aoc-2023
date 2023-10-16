This problem is a classic example of the traveling salesman problem (TSP), which is known to be NP-hard. The input will consist of distances between pairs of locations, and the goal is to find the shortest route that visits each location exactly once.

One possible approach to solve this problem is to use a brute force technique, which involves generating all possible permutations of the locations and calculating the distance of each route. The shortest distance can then be determined by finding the minimum distance among all routes.

Here is a possible high-level algorithm to solve this problem:

1. Read the input distances between each pair of locations.
2. Generate all possible permutations of the locations.
3. For each permutation:
   - Calculate the total distance of the route.
4. Find the minimum distance among all routes.
5. Output the minimum distance.

Keep in mind that brute force can be computationally expensive, especially for larger inputs. If the number of locations is relatively small, this approach should work well. However, for larger inputs, more efficient algorithms such as dynamic programming or heuristics like the nearest neighbor algorithm or the 2-opt algorithm can be considered.