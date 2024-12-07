using System.IO.Compression;
using AoC2024.Interfaces;

namespace AoC2024.Days;

public class Day7 : IDay
{
    private readonly string[] _lines;
    public Day7()
    {
        _lines = File.ReadAllLines("Inputs/inputDay7.txt");

    }

    public void SolvePart1()
    {
        long part1Result = 0;
        long testValue = 0;
        long[] values;
        foreach (var line in _lines)
        {
            string[] parsedLine = line.Split(":");
            testValue = long.Parse(parsedLine[0]);
            values = parsedLine[1].Trim().Split(" ").Select(long.Parse).ToArray();

            if (CheckIfLineIsValid(testValue, values))
            {
                part1Result += testValue;
            };

        }

        bool CheckIfLineIsValid(long testValue, long[] values)
        {
            return EvaluateCombinations(values, 1, values[0], testValue);
        }

        // Recursive function to check all possible options for combining and summing numbers. If some combination match test value, return true
        bool EvaluateCombinations(long[] values, int index, long currentResult, long testValue)
        {
            if (index == values.Length)
            {
                return currentResult == testValue;
            }

            long nextValue = values[index];

            return EvaluateCombinations(values, index + 1, currentResult + nextValue, testValue) ||
                   EvaluateCombinations(values, index + 1, currentResult * nextValue, testValue);
        }



        Console.WriteLine($"Part 1 solution is: {part1Result}");
    }

    public void SolvePart2()
    {
        long part2Result = 0;

        long testValue = 0;
        long[] values;
        foreach (var line in _lines)
        {
            string[] parsedLine = line.Split(":");
            testValue = long.Parse(parsedLine[0]);
            values = parsedLine[1].Trim().Split(" ").Select(long.Parse).ToArray();

            if (CheckIfLineIsValid(testValue, values))
            {
                part2Result += testValue;
            };

        }

        bool CheckIfLineIsValid(long testValue, long[] values)
        {
            return EvaluateCombinations(values, 1, values[0], testValue);
        }

        bool EvaluateCombinations(long[] values, int index, long currentResult, long testValue)
        {
            if (index == values.Length)
            {
                return currentResult == testValue;
            }

            // Get the next value to consider
            long nextValue = values[index];

            // Explore all three operators: +, *, and |
            return EvaluateCombinations(values, index + 1, currentResult + nextValue, testValue) ||  // Addition
                   EvaluateCombinations(values, index + 1, currentResult * nextValue, testValue) ||  // Multiplication
                   EvaluateCombinations(values, index + 1, CombineAsText(currentResult, nextValue), testValue);  // Text concatenation
        }

        // Combine two values as text, only left-right
        long CombineAsText(long left, long right)
        {
            string combined = left.ToString() + right.ToString();
            return long.Parse(combined);
        }

        Console.WriteLine($"Part 2 solution is: {part2Result} ");
    }

}