using AoC2024.Interfaces;

namespace AoC2024.Days;

public class Day25 : IDay
{
    private readonly string[] _lines;
    public Day25()
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Inputs", "test.txt");
        _lines = File.ReadAllLines(filePath);
    }

    public void SolvePart1()
    {
        int part1Result = 0;
        Console.WriteLine($"Part 1 solution is: {part1Result}");
    }

    public void SolvePart2()
    {
        int part2Result = 0;
        Console.WriteLine($"Part 2 solution is: {part2Result} ");
    }

}