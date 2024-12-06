using System.Security.Cryptography.X509Certificates;
using AoC2024.Interfaces;

namespace AoC2024.Days;

public class Day6 : IDay
{
    private readonly string[] _lines;
    private string _currentDirection;

    private (int, int) _currentLocation;
    private (int, int) _nextLocation;

    private readonly char[][] _grid;
    public Day6()
    {
        _lines = File.ReadAllLines("Inputs/inputDay6.txt");
        _grid = new char[_lines.Length][];
        for (int i = 0; i < _lines.Length; i++)
        {
            _grid[i] = _lines[i].ToCharArray();
        }

        _currentDirection = FindStartingDirectionAndLocation(_grid).Item1;
        _currentLocation = FindStartingDirectionAndLocation(_grid).Item2;

    }

    public void SolvePart1()
    {
        int part1Result = 0;
        Console.WriteLine(_currentLocation);

        int MoveUntilLeavingGrid()
        {
            int steps = 0;

            while (_currentLocation.Item1 >= 0 && _currentLocation.Item1 < _grid.Length && _currentLocation.Item2 >= 0 && _currentLocation.Item2 < _grid[0].Length)
            {
                _grid[_currentLocation.Item1][_currentLocation.Item2] = 'X';
                switch (_currentDirection)
                {
                    case "up":
                        _nextLocation = (_currentLocation.Item1 - 1, _currentLocation.Item2);
                        if (CheckIfNextLocationIsInGridBoundary(_nextLocation) == false)
                        {
                            return steps;
                        }
                        if (_grid[_nextLocation.Item1][_nextLocation.Item2] == '#')
                        {
                            TurnRight();
                        }
                        else
                        {
                            _currentLocation = _nextLocation;
                        }
                        break;
                    case "down":
                        _nextLocation = (_currentLocation.Item1 + 1, _currentLocation.Item2);
                        if (CheckIfNextLocationIsInGridBoundary(_nextLocation) == false)
                        {
                            return steps;
                        }
                        if (_grid[_nextLocation.Item1][_nextLocation.Item2] == '#')
                        {
                            TurnRight();
                        }
                        else
                        {
                            _currentLocation = _nextLocation;
                        }
                        break;
                    case "left":
                        _nextLocation = (_currentLocation.Item1, _currentLocation.Item2 - 1);
                        if (CheckIfNextLocationIsInGridBoundary(_nextLocation) == false)
                        {
                            return steps;
                        }
                        if (_grid[_nextLocation.Item1][_nextLocation.Item2] == '#')
                        {
                            TurnRight();
                        }
                        else
                        {
                            _currentLocation = _nextLocation;
                        }
                        break;
                    case "right":
                        _nextLocation = (_currentLocation.Item1, _currentLocation.Item2 + 1);
                        if (CheckIfNextLocationIsInGridBoundary(_nextLocation) == false)
                        {
                            return steps;
                        }
                        if (_grid[_nextLocation.Item1][_nextLocation.Item2] == '#')
                        {
                            TurnRight();
                        }
                        else
                        {
                            _currentLocation = _nextLocation;
                        }
                        break;
                }
                if (_grid[_currentLocation.Item1][_currentLocation.Item2] != 'X')
                {
                    steps++;
                }

            }
            return steps;
        }

        part1Result += MoveUntilLeavingGrid() + 1;
        Console.WriteLine($"Part 1 solution is: {part1Result}");
    }

    public void SolvePart2()
    {
        int part2Result = 0;
        Console.WriteLine($"Part 2 solution is: {part2Result} ");
    }

    public (string, (int, int)) FindStartingDirectionAndLocation(char[][] grid)
    {
        (int, int) currentLocation = (-1, -1);
        // Find the starting direction
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                if (grid[i][j] == '>')
                {
                    currentLocation = (i, j);
                    return ("right", currentLocation);
                }
                else if (grid[i][j] == '<')
                {
                    currentLocation = (i, j);
                    return ("left", currentLocation);
                }
                else if (grid[i][j] == '^')
                {
                    currentLocation = (i, j);
                    return ("up", currentLocation);
                }
                else if (grid[i][j] == 'v')
                {
                    currentLocation = (i, j);
                    return ("down", currentLocation);
                }
            }
        }
        return ("", currentLocation);
    }

    public void TurnRight()
    {
        if (_currentDirection == "up")
        {
            _currentDirection = "right";
        }
        else if (_currentDirection == "right")
        {
            _currentDirection = "down";
        }
        else if (_currentDirection == "down")
        {
            _currentDirection = "left";
        }
        else if (_currentDirection == "left")
        {
            _currentDirection = "up";
        }
    }

    public bool CheckIfNextLocationIsInGridBoundary((int, int) nextLocation)
    {
        return nextLocation.Item1 >= 0 && nextLocation.Item1 < _grid.Length && nextLocation.Item2 >= 0 && nextLocation.Item2 < _grid[0].Length;
    }

}