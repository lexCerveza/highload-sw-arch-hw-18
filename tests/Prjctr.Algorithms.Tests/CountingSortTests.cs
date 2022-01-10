using FluentAssertions;
using Xunit;

namespace Prjctr.Algorithms.Tests;

public class CountingSortTests
{
    [Fact]
    public void Sort_ReturnsExpectedResult()
    {
        new[] { 2, 5, -4, 11, 0, 8, 22, 67, 51, 6 }
            .Sort()
            .Should()
            .BeInAscendingOrder();
    }

    [Fact]
    public void Sort_WorstCase()
    {
        // worst case scenario occurs when one element is slightly larger than other elements
        new[] { int.MaxValue / 2, 5, -4, 11, 0, 8, 22, 67, 51, 6 }
            .Sort()
            .Should()
            .BeInAscendingOrder();
    }
}