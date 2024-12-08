using AoC2024.Interfaces;

namespace AoC2024.Days;

public class Day8 : IDay
{
    private readonly string[] _lines;
    private readonly char[][] _grid;
    public Day8()
    {
        _lines = File.ReadAllLines("Inputs/inputDay8.txt");
        _grid = new char[_lines.Length][];
        for (int i = 0; i < _lines.Length; i++)
        {
            _grid[i] = _lines[i].ToCharArray();
        }

    }

    public void SolvePart1()
    {
        int part1Result = 0;

        List<(char, (int, int))> Antenas = new();
        for (int row = 0; row < _grid.Length; row++)
        {
            for (int col = 0; col < _grid[row].Length; col++)
            {
                if (_grid[row][col] != '.')
                {
                    Antenas.Add((_grid[row][col], (row, col)));
                }
            }
        }

        // Iterate over all unique antena pairs with same characcter and count their difference of x and y
        var processedPairs = new HashSet<(char, (int, int), (int, int))>();
        var signalLocations = new HashSet<(int, int)>();
        foreach (var antena in Antenas)
        {
            var sameAntenas = Antenas.Where(x => x.Item1 == antena.Item1).ToList();
            for (int i = 0; i < sameAntenas.Count; i++)
            {
                for (int j = i + 1; j < sameAntenas.Count; j++)
                {
                    var pair = (sameAntenas[i].Item1, sameAntenas[i].Item2, sameAntenas[j].Item2);
                    var reversePair = (sameAntenas[i].Item1, sameAntenas[j].Item2, sameAntenas[i].Item2);
                    if (!processedPairs.Contains(pair) && !processedPairs.Contains(reversePair))
                    {
                        processedPairs.Add(pair);
                        bool xLower = sameAntenas[i].Item2.Item1 < sameAntenas[j].Item2.Item1;
                        bool yLower = sameAntenas[i].Item2.Item2 < sameAntenas[j].Item2.Item2;

                        var xDiff = Math.Abs(sameAntenas[i].Item2.Item1 - sameAntenas[j].Item2.Item1);
                        var yDiff = Math.Abs(sameAntenas[i].Item2.Item2 - sameAntenas[j].Item2.Item2);

                        var signalLocation1 = (-1, -1);
                        var signalLocation2 = (-1, -1);

                        if (xLower)
                        {
                            signalLocation1.Item1 = sameAntenas[i].Item2.Item1 - xDiff;
                            signalLocation2.Item1 = sameAntenas[j].Item2.Item1 + xDiff;
                        }
                        else
                        {
                            signalLocation1.Item1 = sameAntenas[i].Item2.Item1 + xDiff;
                            signalLocation2.Item1 = sameAntenas[j].Item2.Item1 - xDiff;
                        }

                        if (yLower)
                        {
                            signalLocation1.Item2 = sameAntenas[i].Item2.Item2 - yDiff;
                            signalLocation2.Item2 = sameAntenas[j].Item2.Item2 + yDiff;
                        }
                        else
                        {
                            signalLocation1.Item2 = sameAntenas[i].Item2.Item2 + yDiff;
                            signalLocation2.Item2 = sameAntenas[j].Item2.Item2 - yDiff;
                        }



                        if (CheckIfLocationIsInGridBoundary(signalLocation1))
                        {
                            signalLocations.Add(signalLocation1);
                        }
                        if (CheckIfLocationIsInGridBoundary(signalLocation2))
                        {
                            signalLocations.Add(signalLocation2);
                        }

                    }
                }
            }
        }

        /* // print grid
        for (int i = 0; i < _grid.Length; i++)
        {
            Console.WriteLine(_grid[i]);
        }
 */
        part1Result = signalLocations.Count;
        Console.WriteLine($"Part 1 solution is: {part1Result}");
    }

    public void SolvePart2()
    {
        int part2Result = 0;

        List<(char, (int, int))> Antenas = new();
        for (int row = 0; row < _grid.Length; row++)
        {
            for (int col = 0; col < _grid[row].Length; col++)
            {
                if (_grid[row][col] != '.')
                {
                    Antenas.Add((_grid[row][col], (row, col)));
                }
            }
        }

        // Iterate over all unique antena pairs with same characcter and count their difference of x and y
        var processedPairs = new HashSet<(char, (int, int), (int, int))>();
        var signalLocations = new HashSet<(int, int)>();
        foreach (var antena in Antenas)
        {
            var sameAntenas = Antenas.Where(x => x.Item1 == antena.Item1).ToList();
            for (int i = 0; i < sameAntenas.Count; i++)
            {
                for (int j = i + 1; j < sameAntenas.Count; j++)
                {
                    var pair = (sameAntenas[i].Item1, sameAntenas[i].Item2, sameAntenas[j].Item2);
                    var reversePair = (sameAntenas[i].Item1, sameAntenas[j].Item2, sameAntenas[i].Item2);
                    if (!processedPairs.Contains(pair) && !processedPairs.Contains(reversePair))
                    {
                        processedPairs.Add(pair);
                        bool xLower = sameAntenas[i].Item2.Item1 < sameAntenas[j].Item2.Item1;
                        bool yLower = sameAntenas[i].Item2.Item2 < sameAntenas[j].Item2.Item2;

                        var xDiff = Math.Abs(sameAntenas[i].Item2.Item1 - sameAntenas[j].Item2.Item1);
                        var yDiff = Math.Abs(sameAntenas[i].Item2.Item2 - sameAntenas[j].Item2.Item2);

                        var signalLocation = sameAntenas[i].Item2;

                        while (true)
                        {
                            var newSignalLocation = (-1, -1);

                            if (xLower)
                            {
                                newSignalLocation.Item1 = signalLocation.Item1 - xDiff;
                            }
                            else
                            {
                                newSignalLocation.Item1 = signalLocation.Item1 + xDiff;
                            }

                            if (yLower)
                            {
                                newSignalLocation.Item2 = signalLocation.Item2 - yDiff;
                            }
                            else
                            {
                                newSignalLocation.Item2 = signalLocation.Item2 + yDiff;
                            }

                            if (!CheckIfLocationIsInGridBoundary(newSignalLocation))
                            {
                                break;
                            }

                            signalLocations.Add(newSignalLocation);
                            signalLocation = newSignalLocation;
                        }

                        signalLocation = sameAntenas[j].Item2;

                        while (true)
                        {
                            var newSignalLocation = (-1, -1);

                            if (xLower)
                            {
                                newSignalLocation.Item1 = signalLocation.Item1 + xDiff;
                            }
                            else
                            {
                                newSignalLocation.Item1 = signalLocation.Item1 - xDiff;
                            }

                            if (yLower)
                            {
                                newSignalLocation.Item2 = signalLocation.Item2 + yDiff;
                            }
                            else
                            {
                                newSignalLocation.Item2 = signalLocation.Item2 - yDiff;
                            }

                            if (!CheckIfLocationIsInGridBoundary(newSignalLocation))
                            {
                                break;
                            }

                            signalLocations.Add(newSignalLocation);
                            signalLocation = newSignalLocation;
                        }


                        // add location of both antenas into signallocations
                        signalLocations.Add(sameAntenas[i].Item2);
                        signalLocations.Add(sameAntenas[j].Item2);

                    }
                }
            }
        }
        /*         // add # in grid for each signallocation
                foreach (var signalLocation in signalLocations)
                {
                    _grid[signalLocation.Item1][signalLocation.Item2] = '#';
                }


                // print grid
                for (int i = 0; i < _grid.Length; i++)
                {
                    Console.WriteLine(_grid[i]);
                } */

        part2Result = signalLocations.Count;
        Console.WriteLine($"Part 2 solution is: {part2Result} ");
    }

    private bool CheckIfLocationIsInGridBoundary((int, int) nextLocation)
    {
        return nextLocation.Item1 >= 0 && nextLocation.Item1 < _grid.Length && nextLocation.Item2 >= 0 && nextLocation.Item2 < _grid[0].Length;
    }


}