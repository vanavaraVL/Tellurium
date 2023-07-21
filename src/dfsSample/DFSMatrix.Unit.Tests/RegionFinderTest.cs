using DFSMatrix.App;
using NUnit.Framework;

namespace DFSMatrix.Unit.Tests;

internal sealed class RegionFinderTest
{
    private readonly RegionFinder _sut;

    public RegionFinderTest()
    {
        _sut = new RegionFinder(new MatrixResolver());
    }

    [Theory]
    [TestCase("1,0,1;0,1,0", 3)]
    [TestCase("1,0,1;1,1,0", 2)]
    [TestCase("1,1,1,0;0,1,0,0", 2)]
    public void Find_all_regions_should_pass(string matrix, int length)
    {
        var result = _sut.FindAllRegions(matrix);

        Assert.That(result, Is.EqualTo(length));
    }
}