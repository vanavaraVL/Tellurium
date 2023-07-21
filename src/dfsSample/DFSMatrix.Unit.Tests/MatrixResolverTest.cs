using DFSMatrix.App;
using NUnit.Framework;

namespace DFSMatrix.Unit.Tests;

internal sealed class MatrixResolverTest
{
    private readonly IMatrixResolver _sut;

    public MatrixResolverTest()
    {
        _sut = new MatrixResolver();
    }

    [Theory]
    [TestCase("")]
    [TestCase("1,0,1;0,1")]
    [TestCase("1,0,1")]
    [TestCase(null)]
    [TestCase("1,0,1;0,1")]
    [TestCase("1,0,1;0,1,a")]
    public void Check_matrix_string_for_validity_should_pass(string matrix)
    {
        Assert.Throws<Exception>(() => _sut.ResolveMatrix(matrix));
    }

    [Theory]
    [TestCase("1,0,1;0,1,0", 6)]
    public void Matrix_should_be_resolved_pass(string matrix, int length)
    {
        var result = _sut.ResolveMatrix(matrix);

        Assert.That(length, Is.EqualTo(result!.Length));
    }
}