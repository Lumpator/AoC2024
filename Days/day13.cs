using AoC2024.Interfaces;

namespace AoC2024.Days;

public class Day13 : IDay
{
    private readonly string[] _lines;
    private readonly List<List<(int, int)>> _clawMachines;
    public Day13()
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Inputs", "inputDay13.txt");
        _lines = File.ReadAllLines(filePath);
        _clawMachines = new List<List<(int, int)>>();
        List<(int, int)> currentClawMachine = new List<(int, int)>();

        for (int i = 0; i < _lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(_lines[i]))
            {
                if (currentClawMachine.Count > 0)
                {
                    _clawMachines.Add(new List<(int, int)>(currentClawMachine));
                    currentClawMachine.Clear();
                }
                continue;
            }

            var parts = _lines[i].Split(new[] { ' ', ',', ':' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 4)
            {
                int x = int.Parse(parts[2].Substring(2));
                int y = int.Parse(parts[3].Substring(2));
                currentClawMachine.Add((x, y));
            }
            else
            {
                int x = int.Parse(parts[1].Substring(2));
                int y = int.Parse(parts[2].Substring(2));
                currentClawMachine.Add((x, y));
            }
        }

        // add last claw machine
        if (currentClawMachine.Count > 0)
        {
            _clawMachines.Add(currentClawMachine);
        }

    }

    public void SolvePart1()
    {
        int part1Result = 0;

        foreach (var clawMachine in _clawMachines)
        {
            var clawMachineResult = SolveLinearEquations(clawMachine);
            if (!(clawMachineResult.Item1 == -1))
            {
                part1Result += 3 * clawMachineResult.Item1 + clawMachineResult.Item2;
            }

        }

        (int, int) SolveLinearEquations(List<(int, int)> clawMachine)
        {
            int a1 = clawMachine[0].Item1;
            int a2 = clawMachine[0].Item2;
            int b1 = clawMachine[1].Item1;
            int b2 = clawMachine[1].Item2;
            int c1 = clawMachine[2].Item1;
            int c2 = clawMachine[2].Item2;

            int determinant = a1 * b2 - a2 * b1;
            if (determinant == 0)
            {
                return (-1, -1);
            }

            int AButtonCount = (c1 * b2 - c2 * b1) / determinant;
            int BButtonCount = (a1 * c2 - a2 * c1) / determinant;

            if (AButtonCount * a1 + BButtonCount * b1 != c1 || AButtonCount * a2 + BButtonCount * b2 != c2)
            {
                return (-1, -1);
            }

            if (AButtonCount > 100 || BButtonCount > 100)
            {
                return (-1, -1);
            }
            return (AButtonCount, BButtonCount);

        }

        Console.WriteLine($"Part 1 solution is: {part1Result}");
    }

    public void SolvePart2()
    {
        long part2Result = 0;

        foreach (var clawMachine in _clawMachines)
        {
            var clawMachineResult = SolveLinearEquations(clawMachine);
            if (!(clawMachineResult.Item1 == -1))
            {
                part2Result += 3 * clawMachineResult.Item1 + clawMachineResult.Item2;
            }

        }

        (long, long) SolveLinearEquations(List<(int, int)> clawMachine)
        {
            long a1 = clawMachine[0].Item1;
            long a2 = clawMachine[0].Item2;
            long b1 = clawMachine[1].Item1;
            long b2 = clawMachine[1].Item2;
            long c1 = clawMachine[2].Item1 + 10000000000000;
            long c2 = clawMachine[2].Item2 + 10000000000000;

            long determinant = a1 * b2 - a2 * b1;
            if (determinant == 0)
            {
                return (-1, -1);
            }

            long AButtonCount = (c1 * b2 - c2 * b1) / determinant;
            long BButtonCount = (a1 * c2 - a2 * c1) / determinant;

            if (AButtonCount * a1 + BButtonCount * b1 != c1 || AButtonCount * a2 + BButtonCount * b2 != c2)
            {
                return (-1, -1);
            }
            return (AButtonCount, BButtonCount);

        }
        Console.WriteLine($"Part 2 solution is: {part2Result} ");
    }

}