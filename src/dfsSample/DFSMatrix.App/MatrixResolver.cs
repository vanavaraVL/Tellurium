namespace DFSMatrix.App;

public interface IMatrixResolver
{
    int[,] ResolveMatrix(string? matrixString);
}

public sealed class MatrixResolver: IMatrixResolver
{
    public int[,] ResolveMatrix(string? matrixString)
    {
        var rowsData = matrixString?.Split(';');

        switch (rowsData)
        {
            case { Length: 0 }:
                throw new Exception("Empty string");
            case { Length: 1 }:
                throw new Exception("One row only");
            case null:
                throw new Exception("Null string");
            case var r when r.Any(s => s.Length != rowsData[0].Length):
                throw new Exception("Can't build matrix");
        }

        var xLength = rowsData.Length;
        var yLength = rowsData[0].Split(',', StringSplitOptions.RemoveEmptyEntries).Length;

        var resultMatrix = new int[xLength, yLength];
        var rowIndex = 0;

        foreach (var row in rowsData)
        {
            var rowData = row.Split(',');

            if (rowData.Any(r => !r.All(char.IsDigit)))
            {
                throw new Exception($"Found not number in the row {row}");
            }

            var resultRow = rowData.Select(r => Convert.ToInt32(r)).ToList();

            for (var i = 0; i < resultRow.Count; i++)
            {
                resultMatrix[rowIndex, i] = resultRow[i];
            }

            rowIndex++;
        }

        return resultMatrix;
    }
}