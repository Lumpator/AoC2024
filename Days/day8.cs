using AoC2024.Interfaces;

namespace AoC2024.Days;

public class Day8 : IDay
{
    private readonly string[] _lines;
    private readonly char[][] _grid;

    private readonly List<(char, (int, int))> _antenas;
    public Day8()
    {
        _lines = File.ReadAllLines("Inputs/inputDay8.txt");
        _grid = new char[_lines.Length][];
        for (int i = 0; i < _lines.Length; i++)
        {
            _grid[i] = _lines[i].ToCharArray();
        }
        _antenas = new List<(char, (int, int))>();
        for (int row = 0; row < _grid.Length; row++)
        {
            for (int col = 0; col < _grid[row].Length; col++)
            {
                if (_grid[row][col] != '.')
                {
                    _antenas.Add((_grid[row][col], (row, col)));
                }
            }
        }
    }

    public void SolvePart1()
    {
        int part1Result = 0;
        var processedPairs = new HashSet<(char, (int, int), (int, int))>();
        var signalLocations = new HashSet<(int, int)>();

        // Iterate over all unique antena pairs with same characcter and count their difference of x and y
        foreach (var antena in _antenas)
        {
            var sameAntenas = _antenas.Where(x => x.Item1 == antena.Item1).ToList();

            for (int i = 0; i < sameAntenas.Count; i++)
            {
                for (int j = i + 1; j < sameAntenas.Count; j++)
                {
                    if (CheckIfPairIsAlreadyProcessed(processedPairs, sameAntenas[i], sameAntenas[j]))
                        continue;

                    processedPairs.Add((sameAntenas[i].Item1, sameAntenas[i].Item2, sameAntenas[j].Item2));

                    var signalLocation1 = CalculateSignalLocation(sameAntenas[i].Item2, sameAntenas[j].Item2, true);
                    var signalLocation2 = CalculateSignalLocation(sameAntenas[i].Item2, sameAntenas[j].Item2, false);

                    AddIfValid(signalLocations, signalLocation1);
                    AddIfValid(signalLocations, signalLocation2);
                }
            }
        }


        part1Result = signalLocations.Count;
        Console.WriteLine($"Part 1 solution is: {part1Result}");
    }

    public void SolvePart2()
    {
        int part2Result = 0;
        var processedPairs = new HashSet<(char, (int, int), (int, int))>();
        var signalLocations = new HashSet<(int, int)>();

        // Iterate over all unique antena pairs with same characcter and count their difference of x and y
        foreach (var antena in _antenas)
        {
            var sameAntenas = _antenas.Where(x => x.Item1 == antena.Item1).ToList();

            for (int i = 0; i < sameAntenas.Count; i++)
            {
                for (int j = i + 1; j < sameAntenas.Count; j++)
                {
                    if (CheckIfPairIsAlreadyProcessed(processedPairs, sameAntenas[i], sameAntenas[j]))
                        continue;

                    processedPairs.Add((sameAntenas[i].Item1, sameAntenas[i].Item2, sameAntenas[j].Item2));
                    bool xLower = sameAntenas[i].Item2.Item1 < sameAntenas[j].Item2.Item1;
                    bool yLower = sameAntenas[i].Item2.Item2 < sameAntenas[j].Item2.Item2;

                    var xDiff = Math.Abs(sameAntenas[i].Item2.Item1 - sameAntenas[j].Item2.Item1);
                    var yDiff = Math.Abs(sameAntenas[i].Item2.Item2 - sameAntenas[j].Item2.Item2);

                    // creat firs signal location in position of antena (it doesnt matter which one)
                    var signalLocation = sameAntenas[i].Item2;

                    // Iterating in first direction to find all signals inside grid boundary
                    while (true)
                    {
                        var newSignalLocation = (
                            signalLocation.Item1 + (xLower ? -xDiff : xDiff),
                            signalLocation.Item2 + (yLower ? -yDiff : yDiff)
                        );

                        if (!CheckIfLocationIsInGridBoundary(newSignalLocation))
                        {
                            break;
                        }
                        signalLocations.Add(newSignalLocation);
                        signalLocation = newSignalLocation;
                    }

                    // Iterating in second direction to find all signals inside grid boundary
                    while (true)
                    {
                        var newSignalLocation = (
                            signalLocation.Item1 + (xLower ? xDiff : -xDiff),
                            signalLocation.Item2 + (yLower ? yDiff : -yDiff)
                        );

                        if (!CheckIfLocationIsInGridBoundary(newSignalLocation))
                        {
                            break;
                        }
                        signalLocations.Add(newSignalLocation);
                        signalLocation = newSignalLocation;
                    }

                    // add location of both antenas into HashSet
                    signalLocations.Add(sameAntenas[i].Item2);
                    signalLocations.Add(sameAntenas[j].Item2);

                }
            }
        }

        part2Result = signalLocations.Count;
        Console.WriteLine($"Part 2 solution is: {part2Result} ");
    }

    private bool CheckIfLocationIsInGridBoundary((int, int) nextLocation)
    {
        return nextLocation.Item1 >= 0 && nextLocation.Item1 < _grid.Length && nextLocation.Item2 >= 0 && nextLocation.Item2 < _grid[0].Length;
    }

    void AddIfValid(HashSet<(int, int)> locations, (int, int) location)
    {
        if (CheckIfLocationIsInGridBoundary(location))
        {
            locations.Add(location);
        }
    }
    private bool CheckIfPairIsAlreadyProcessed(HashSet<(char, (int, int), (int, int))> processedPairs, (char, (int, int)) antena1, (char, (int, int)) antena2)
    {
        var pair = (antena1.Item1, antena1.Item2, antena2.Item2);
        var reversePair = (antena1.Item1, antena2.Item2, antena1.Item2);
        return processedPairs.Contains(pair) || processedPairs.Contains(reversePair);
    }

    private (int, int) CalculateSignalLocation((int, int) antena1, (int, int) antena2, bool firstLocation)
    {
        int xDiff = Math.Abs(antena1.Item1 - antena2.Item1);
        int yDiff = Math.Abs(antena1.Item2 - antena2.Item2);

        bool xLower = antena1.Item1 < antena2.Item1;
        bool yLower = antena1.Item2 < antena2.Item2;

        int x = firstLocation ?
                (xLower ? antena1.Item1 - xDiff : antena1.Item1 + xDiff) :
                (xLower ? antena2.Item1 + xDiff : antena2.Item1 - xDiff);

        int y = firstLocation ?
                (yLower ? antena1.Item2 - yDiff : antena1.Item2 + yDiff) :
                (yLower ? antena2.Item2 + yDiff : antena2.Item2 - yDiff);

        return (x, y);
    }
}