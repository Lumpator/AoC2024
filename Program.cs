using System.Diagnostics;
using AoC2024.Days;

class Program
{
    static void Main(string[] args)
    {
        var stopwatch = Stopwatch.StartNew();
        var day = new Day20();
        stopwatch.Stop();
        if (stopwatch.Elapsed.TotalSeconds > 1)
        {
            Console.WriteLine($"Preparations elapsed time: {stopwatch.Elapsed.TotalSeconds:F2} s");
        }
        else
        {
            Console.WriteLine($"Preparations elapsed time: {stopwatch.Elapsed.TotalMilliseconds:F5} ms");
        }
        Console.WriteLine("----------------------------------------");

        // Measure Part 1 execution time
        stopwatch.Restart();
        day.SolvePart1();
        stopwatch.Stop();
        if (stopwatch.Elapsed.TotalSeconds > 1)
        {
            Console.WriteLine($"Elapsed time: {stopwatch.Elapsed.TotalSeconds:F2} s");
        }
        else
        {
            Console.WriteLine($"Elapsed time: {stopwatch.Elapsed.TotalMilliseconds:F5} ms");
        }
        Console.WriteLine("----------------------------------------");

        // Measure Part 2 execution time
        stopwatch.Restart();
        day.SolvePart2();
        stopwatch.Stop();
        if (stopwatch.Elapsed.TotalSeconds > 1)
        {
            Console.WriteLine($"Elapsed time: {stopwatch.Elapsed.TotalSeconds:F5} s");
        }
        else
        {
            Console.WriteLine($"Elapsed time: {stopwatch.Elapsed.TotalMilliseconds:F5} ms");
        }
        Console.WriteLine("----------------------------------------");
    }
}