using AoC2024.Interfaces;

namespace AoC2024.Days;

public class Day19 : IDay
{
    private readonly string[] _lines;
    public Day19()
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Inputs", "inputDay19.txt");
        _lines = File.ReadAllLines(filePath);
    }

    public void SolvePart1()
    {
        int part1Result = 0;
        string[] substringsArray = _lines[0].Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
        List<string> substrings = new List<string>(substringsArray);
        for (int i = 2; i < _lines.Length; i++)
        {
            string target = _lines[i];
            if (CanFormString(target, substrings))
            {
                part1Result++;
            }
        }

        Console.WriteLine($"Part 1 solution is: {part1Result}");
    }

    public void SolvePart2()
    {
        int part2Result = 0;
        Console.WriteLine($"Part 2 solution is: {part2Result} ");
    }

    private bool CanFormString(string target, List<string> substrings)
    {
        int towelPatternLength = target.Length;
        bool[] dp = new bool[towelPatternLength + 1];
        dp[0] = true;

        for (int i = 1; i <= towelPatternLength; i++)
        {
            foreach (string substring in substrings)
            {
                int len = substring.Length;
                if (i >= len && target.Substring(i - len, len) == substring)
                {
                    dp[i] = dp[i] || dp[i - len];
                }
            }
        }
        return dp[towelPatternLength];
    }

}