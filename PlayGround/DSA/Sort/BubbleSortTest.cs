using Shouldly;

namespace DSA.Sort;

public class BubbleSortTest
{
    private static void Sort(int[] array)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            for (int j = i + 1; j < array.Length; j++)
            {
                if (array[i] > array[j])
                {
                    (array[i], array[j]) = (array[j], array[i]);
                }
            }
        }
    }

    [Theory]
    [InlineData(new[] { 4, 6, 9, 2, 3, 5 }, new[] { 2, 3, 4, 5, 6, 9 })]
    [InlineData(new int[] { }, new int[] { })]
    [InlineData(new[] { 1 }, new[] { 1 })]
    [InlineData(new[] { 3, 1, 3 }, new[] { 1, 3, 3 })]
    [InlineData(new[] { 3, 2, 1 }, new[] { 1, 2, 3 })]
    [InlineData(new[] { -3, 2, 1 }, new[] { -3, 1, 2 })]
    public void BubbleSort(int[] inputArray, int[] expected)
    {
        Sort(inputArray);
        inputArray.ShouldBe(expected);
    }
}
