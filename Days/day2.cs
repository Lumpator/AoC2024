using AoC2024.Interfaces;

namespace AoC2024.Days;

public class Day2 : IDay
{
    private readonly string[] _lines;
    private readonly int[][] _intLines;
    public Day2()
    {
        _lines = File.ReadAllLines("Inputs/inputDay2.txt");
        _intLines = _lines
    .Select(line => line.Split(' ').Select(int.Parse).ToArray())
    .ToArray();

    }

    public void SolvePart1()
    {

        int part1Result = 0;
        foreach (var line in _intLines)
        {
            bool isSorted = IsSortedAscOrDesc(line);
            bool isWithinLimits = ValuesDifferenceInRange(line);
            if (isWithinLimits & isSorted)
            {
                part1Result++;
            }
        }

        Console.WriteLine($"Part 1 solution is: {part1Result}");
    }

    public void SolvePart2()
    {
        int part2Result = 0;

        foreach (var line in _intLines)
        {
            bool isSorted = IsSortedAscOrDesc(line);
            bool isWithinLimits = ValuesDifferenceInRange(line);
            if (isSorted)
            {
                if (isWithinLimits)
                {
                    part2Result++;
                }
                else
                // if its already sorted without duplicate elements, only way to make line valid is to remove first or last element 
                {
                    int[] newLineWithoutLastElement = new int[line.Length - 1];
                    Array.Copy(line, 0, newLineWithoutLastElement, 0, line.Length - 1);

                    int[] newLineWithoutFirstElemenet = new int[line.Length - 1];
                    Array.Copy(line, 1, newLineWithoutFirstElemenet, 0, line.Length - 1);

                    if (ValuesDifferenceInRange(newLineWithoutFirstElemenet) || ValuesDifferenceInRange(newLineWithoutLastElement))
                    {
                        part2Result++;
                    }
                }
            }
            else
            // brute force rest because im so bad :(
            // if not sorted, just try to remove each element one by one and try validation from part1
            {
                for (int i = 0; i < line.Length; i++)
                {
                    var newLine = RemoveAt(line, i);
                    bool isSortedNew = IsSortedAscOrDesc(newLine);
                    bool isWithinLimitsNew = ValuesDifferenceInRange(newLine);
                    if (isSortedNew & isWithinLimitsNew)
                    {
                        part2Result++;
                        break;
                    }

                }
            }
        }

        Console.WriteLine($"Part 2 solution is: {part2Result} ");
    }

    bool IsSortedAscOrDesc(int[] list)
    {
        if (list.Length <= 1) return true;

        bool ascending = true;
        bool descending = true;

        for (int i = 1; i < list.Length; i++)
        {
            if (list[i] <= list[i - 1])
                ascending = false;
            if (list[i] >= list[i - 1])
                descending = false;

            // If neither ascending nor descending, break early
            if (!ascending && !descending)
                return false;
        }

        return ascending || descending;
    }

    bool ValuesDifferenceInRange(int[] list)
    {
        for (int i = 1; i < list.Length; i++)
        {
            int diff = Math.Abs(list[i] - list[i - 1]);
            if (diff < 1 || diff > 3)
            {
                return false;
            }
        }
        return true;
    }

    int[] RemoveAt(int[] array, int indexToRemove)
    {
        if (indexToRemove < 0 || indexToRemove >= array.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(indexToRemove), "Index is out of range.");
        }

        // Create a new array with one less element
        int[] newArray = new int[array.Length - 1];

        // Copy elements before the index
        Array.Copy(array, 0, newArray, 0, indexToRemove);

        // Copy elements after the index
        Array.Copy(array, indexToRemove + 1, newArray, indexToRemove, array.Length - indexToRemove - 1);

        return newArray;
    }






}