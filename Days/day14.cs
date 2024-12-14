using AoC2024.Interfaces;
using AoC2024.Utils;

namespace AoC2024.Days;

public class Day14 : IDay
{
    private readonly string[] _lines;
    private readonly List<List<(int, int)>> _robots = new List<List<(int, int)>>();

    public Day14()
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Inputs", "inputDay14.txt");
        _lines = File.ReadAllLines(filePath);
        foreach (var line in _lines)
        {
            var parts = line.Split(new[] { ' ', ',', '=' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 6)
            {
                List<(int, int)> robot = new List<(int, int)>();

                int p1 = int.Parse(parts[1]);
                int p2 = int.Parse(parts[2]);
                int v1 = int.Parse(parts[4]);
                int v2 = int.Parse(parts[5]);

                robot.Add((p1, p2));
                robot.Add((v1, v2));

                _robots.Add(robot);
            }
        }

    }

    public void SolvePart1()
    {
        long part1Result = 0;
        int gridRowLimit = 103; // y
        int gridColumnLimit = 101; // x

        int quadrantRows = gridRowLimit / 2;
        int quadrantColumns = gridColumnLimit / 2;

        int q1 = 0;
        int q2 = 0;
        int q3 = 0;
        int q4 = 0;


        (int, int) RobotMove((int, int) currentLocation, (int, int) velocity)
        {
            return (currentLocation.Item1 + velocity.Item1, currentLocation.Item2 + velocity.Item2);
        }

        foreach (var robot in _robots)
        {
            for (int i = 0; i < 100; i++)
            {
                robot[0] = RobotMove(robot[0], robot[1]);
            }
            if (Math.Abs(robot[0].Item1) >= gridColumnLimit)
            {
                robot[0] = (robot[0].Item1 % gridColumnLimit, robot[0].Item2);
            }
            if (robot[0].Item1 < 0)
            {
                robot[0] = (robot[0].Item1 + gridColumnLimit, robot[0].Item2);
            }
            if (Math.Abs(robot[0].Item2) >= gridRowLimit)
            {
                robot[0] = (robot[0].Item1, robot[0].Item2 % gridRowLimit);
            }
            if (robot[0].Item2 < 0)
            {
                robot[0] = (robot[0].Item1, robot[0].Item2 + gridRowLimit);
            }

            if (robot[0].Item2 < quadrantRows)
            {
                if (robot[0].Item1 < quadrantColumns)
                {
                    q1++;
                }
                else if (robot[0].Item1 >= quadrantColumns + 1)
                {
                    q2++;
                }
            }
            else if (robot[0].Item2 >= quadrantRows + 1)
            {
                if (robot[0].Item1 < quadrantColumns)
                {
                    q3++;
                }
                else if (robot[0].Item1 >= quadrantColumns + 1)
                {
                    q4++;
                }
            }
        }
        // print grid with each robot position
        /*         for (int i = 0; i < gridRowLimit; i++)
                {
                    for (int j = 0; j < gridColumnLimit; j++)
                    {
                        if (_robots.Any(r => r[0].Item1 == j && r[0].Item2 == i))
                        {
                            Console.Write("R");
                        }
                        else
                        {
                            Console.Write(".");
                        }
                    }
                    Console.WriteLine();
                } */

        part1Result += q1 * q2 * q3 * q4;
        Console.WriteLine($"Part 1 solution is: {part1Result}");
    }






    public void SolvePart2()
    {
        int part2Result = 0;
        Console.WriteLine($"Part 2 solution is: {part2Result} ");
    }



}