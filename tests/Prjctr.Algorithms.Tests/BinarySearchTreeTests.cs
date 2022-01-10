using System;
using System.Diagnostics;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly, MaxParallelThreads = 32)]

namespace Prjctr.Algorithms.Tests;

#pragma warning disable xUnit1026
public class BinarySearchTreeTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    private const int Delta = 100;
    private const int MaxCount = 10_000;
    
    private static readonly Random Random = new();

    public BinarySearchTreeTests(ITestOutputHelper testOutputHelper) => _testOutputHelper = testOutputHelper;

    [Fact]
    public void Insert_Benchmark()
    {
        for (var count = Delta; count < MaxCount; count += Delta)
        {
            var tree = new BalancedBinaryTree();

            var sw = Stopwatch.StartNew();

            foreach (var number in Enumerable.Range(0, count).Select(_ => Random.Next(0, int.MaxValue)))
            {
                tree.Insert(number, rebalance: true);
            }

            sw.Stop();
            _testOutputHelper.WriteLine($"Inserting {count} elements. Elapsed time {sw.ElapsedMilliseconds}.");
        }
    }
    
    [Fact]
    public void Delete_Benchmark()
    {
        for (var count = Delta; count < MaxCount; count += Delta)
        {
            var tree = new BalancedBinaryTree();

            var numbers = Enumerable.Range(0, count)
                .Select(_ => Random.Next(0, int.MaxValue))
                .OrderByDescending(x => x)
                .ToList();

            foreach (var number in numbers)
            {
                tree.Insert(number, rebalance: true);
            }

            var sw = Stopwatch.StartNew();
        
            foreach (var number in numbers)
            {
                tree.Delete(number);
            }
        
            sw.Stop();
            _testOutputHelper.WriteLine($"Deleting {count} elements. Elapsed time {sw.ElapsedMilliseconds}.");
        }
    }

    [Fact]
    public void Search_Benchmark()
    {
        for (var count = Delta; count < MaxCount; count += Delta)
        {
            var tree = new BalancedBinaryTree();

            var numbers = Enumerable.Range(0, count)
                .Select(_ => Random.Next(0, int.MaxValue))
                .OrderByDescending(x => x)
                .ToList();

            foreach (var number in numbers)
            {
                tree.Insert(number, rebalance: true);
            }
            
            var sw = Stopwatch.StartNew();

            foreach (var number in numbers)
            {
                tree.Search(number);
            }
            
            sw.Stop();
            _testOutputHelper.WriteLine($"Searching {count} elements. Elapsed time {sw.ElapsedMilliseconds}.");
        }
    }
}