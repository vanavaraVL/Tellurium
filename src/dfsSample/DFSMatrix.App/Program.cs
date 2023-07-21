using DFSMatrix.App;

var matrixFinder = new RegionFinder(new MatrixResolver());

var matrixString = Console.ReadLine();

var result = matrixFinder.FindAllRegions(matrixString);

Console.WriteLine($"Result: {result}");