using System.Diagnostics;
using AoC2024.Days;

class Program
{
    static void Main(string[] args)
    {
        var day = new Day8();

        // Measure Part 1 execution time
        var stopwatch = Stopwatch.StartNew();
        day.SolvePart1();
        stopwatch.Stop();
        Console.WriteLine($"Execution time: {stopwatch.ElapsedMilliseconds} ms");

        // Measure Part 2 execution time
        stopwatch.Restart();
        day.SolvePart2();
        stopwatch.Stop();
        Console.WriteLine($"Execution time: {stopwatch.ElapsedMilliseconds} ms");
    }
}