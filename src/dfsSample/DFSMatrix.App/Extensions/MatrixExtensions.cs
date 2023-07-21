namespace DFSMatrix.App.Extensions;

public static class MatrixExtensions
{
    /// <summary>
    /// Here we are looking for neighbors from current position
    /// 1. If a neighbor is 1 or has been visited - we skip it
    /// 2. If a neighbor is 0 then we are looking for its neighbors from its current position starting again from point 1
    /// We are looking by circle: top-right-bottom-left
    /// Finally we just return 1 + the sum of all neighbors
    /// The sum in that case - just the sum of '0' elements in the region
    /// </summary>
    public static int FindRegionDfs(this int[,] matrix, int row, int column, bool[,] visited)
    {
        if (matrix[row, column] == 1 || visited[row, column])
            return 0;

        visited[row, column] = true;

        var leftNeighbours = 0;
        var rightNeighbours = 0;
        var topNeighbours = 0;
        var bottomNeighbours = 0;

        if (row > 0)
            topNeighbours = FindRegionDfs(matrix, row - 1, column, visited);

        if (column + 1 < matrix.GetLength(1))
            rightNeighbours = FindRegionDfs(matrix, row, column + 1, visited);

        if (row + 1 < matrix.GetLength(0))
            bottomNeighbours = FindRegionDfs(matrix, row + 1, column, visited);
            
        if (column > 0)
            leftNeighbours = FindRegionDfs(matrix, row, column - 1, visited);
            
        return 1 + leftNeighbours + rightNeighbours + topNeighbours + bottomNeighbours;
    }
}