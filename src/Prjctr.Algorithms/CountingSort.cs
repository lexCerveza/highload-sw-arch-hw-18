using System.Linq;

namespace Prjctr.Algorithms;

public static class CountingSort
{
    public static int[] Sort(this int[] array)
    {
        var sortedArray = new int[array.Length];

        var min = array.Min();
        var max = array.Max();
        
        var counts = new int[max - min + 1];
 
        for (var i = 0; i < array.Length; i++)
        {
            counts[array[i] - min]++;
        }
 
        counts[0]--;
        
        for (var i = 1; i < counts.Length; i++)
        {
            counts[i] += counts[i - 1];
        }
 
        for (var i = array.Length - 1; i >= 0; i--)
        {
            sortedArray[counts[array[i] - min]--] = array[i];
        }

        return sortedArray;
    }
}