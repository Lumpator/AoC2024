using System.Text.RegularExpressions;
using AoC2024.Interfaces;

namespace AoC2024.Days;

public class Day3 : IDay
{
    private readonly string[] _lines;
    public Day3()
    {
        _lines = File.ReadAllLines("Inputs/inputday3.txt");
    }

    public void SolvePart1()
    {
        long part1Result = 0;

        string pattern = @"mul\s*\(\s*(\d{1,3})\s*,\s*(\d{1,3})\s*\)";

        foreach (string line in _lines)
        {
            MatchCollection matches = Regex.Matches(line, pattern);
            foreach (Match match in matches)
            {
                int x = int.Parse(match.Groups[1].Value);
                int y = int.Parse(match.Groups[2].Value);

                part1Result += x * y;
            }
        }
        Console.WriteLine($"Part 1 solution is: {part1Result}");
    }

    public void SolvePart2()
    {
        int part2Result = 0;

        string pattern = @"mul\s*\(\s*(\d{1,3})\s*,\s*(\d{1,3})\s*\)|don't\(\)|do\(\)";
        bool enabled = true;

        foreach (string line in _lines)
        {
            MatchCollection matches = Regex.Matches(line, pattern);
            foreach (Match match in matches)
            {
                ToggleEnabledState(ref enabled, match.Value);

                if (match.Groups[1].Success && match.Groups[2].Success) // Check if it's a valid mul(x,y)
                {
                    if (enabled)
                    {
                        int x = int.Parse(match.Groups[1].Value);
                        int y = int.Parse(match.Groups[2].Value);

                        part2Result += x * y;
                    }
                }
            }
        }

        Console.WriteLine($"Part 2 solution is: {part2Result} ");
    }

    void ToggleEnabledState(ref bool enabled, string matchValue)
    {
        if (matchValue == "do()")
        {
            enabled = true;
        }
        else if (matchValue == "don't()")
        {
            enabled = false;
        }
    }

}