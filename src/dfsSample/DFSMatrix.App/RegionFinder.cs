using DFSMatrix.App.Extensions;

namespace DFSMatrix.App;

public sealed class RegionFinder
{
    private readonly IMatrixResolver _matrixResolver;

    private int[,]? _matrix;
    private bool[,]? _visited;


    public RegionFinder(IMatrixResolver matrixResolver)
    {
        _matrixResolver = matrixResolver;
    }

    public int FindAllRegions(string? matrixString)
    {
        _matrix = _matrixResolver.ResolveMatrix(matrixString);
        _visited = new bool[_matrix.GetLength(0), _matrix.GetLength(1)];

        DisplayMatrix();

        var resultRegions = new List<int>();

        for (var i = 0; i < _matrix.GetLength(0); i++)
        for (var j = 0; j < _matrix.GetLength(1); j++)
            if (!_visited[i, j] && _matrix[i, j] != 1)
            {
                var neighborRegions = _matrix.FindRegionDfs(i, j, _visited);

                if (neighborRegions > 0)
                {
                    resultRegions.Add(neighborRegions);
                }
            }


        Console.WriteLine($"Found regions: {(resultRegions.Count > 0 ? string.Join(',', resultRegions) : "No regions")}");
        Console.WriteLine($"Total regions: {resultRegions.Count}");

        return resultRegions.Count;
    }

    private void DisplayMatrix()
    {
        for (var i = 0; i < _matrix!.GetLength(0); i++)
        {
            for (var j = 0; j < _matrix.GetLength(1); j++)
                Console.Write(_matrix[i, j] + " ");

            Console.WriteLine();
        }
    }
}