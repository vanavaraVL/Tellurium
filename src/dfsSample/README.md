# Documentation

Create a C# console application that accepts a matrix of values 0 and 1. The application should
output only one value into the console – number of areas formed of number 1.
The matrix is presented as a string value where ‘,’ is used as a separator for columns, ‘;’ is used as
a separator for rows. For instance, “1,0,1;0,1,0” string value should be converted to the matrix
[[1,0,1], [0,1,0]].

The maximum size of the matrix is 100x100.

Examples of the input and output:
1. Input: “1,0,1;0,1,0”
Output: 3
2. Input: “1,0,1;1,1,0”
Output: 2
3. Input: “1,1,1,0;0,1,0,0”
Output: 2

## Approach

![DFS overview](./assets/dfs.svg)

Here we are looking for neighbors from current position of element:

1. If a neighbor is 1 or has been visited - we skip it
2. If a neighbor is 0 then we are looking for its neighbors from its current position starting again from point 1

We are looking by the circle: top-right-bottom-left.

Finally we just return 1 (current visited element) + the sum of all neighbors.
The sum in that case - just the sum of '0' elements in the region of all neighbors.

- [Application](./DFSMatrix.App)\
Uses DFS approach to find all regions in the matrix.

- [Matrix resolver](./DFSMatrix.App/MatrixResolver.cs)\
The dependency which allows to build and validate the matrix from the string.

- [Region finder](./DFSMatrix.App/RegionFinder.cs)\
Resolves all regions in the matrix and return the total count of regions.

- [Unit tests](./DFSMatrix.Unit.Tests)\
Unit tests.
