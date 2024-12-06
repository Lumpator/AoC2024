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

        var startingInfo = FindStartingDirectionAndLocation(_grid);
        _currentDirection = startingInfo.Item1;
        _currentLocation = startingInfo.Item2;
    }

    public void SolvePart1()
    {
        int part1Result = 0;
        Console.WriteLine(_currentLocation);

        int MoveUntilLeavingGrid()
        {
            int steps = 1;

            while (_currentLocation.Item1 >= 0 && _currentLocation.Item1 < _grid.Length && _currentLocation.Item2 >= 0 && _currentLocation.Item2 < _grid[0].Length)
            {
                _grid[_currentLocation.Item1][_currentLocation.Item2] = 'X';
                switch (_currentDirection)
                {
                    case "up":
                        _nextLocation = (_currentLocation.Item1 - 1, _currentLocation.Item2);
                        if (!CheckIfNextLocationIsInGridBoundary(_nextLocation))
                        {
                            return steps;
                        }
                        Move(); // Go straight if possible or change your direction right
                        break;
                    case "down":
                        _nextLocation = (_currentLocation.Item1 + 1, _currentLocation.Item2);
                        if (!CheckIfNextLocationIsInGridBoundary(_nextLocation))
                        {
                            return steps;
                        }
                        Move();
                        break;
                    case "left":
                        _nextLocation = (_currentLocation.Item1, _currentLocation.Item2 - 1);
                        if (!CheckIfNextLocationIsInGridBoundary(_nextLocation))
                        {
                            return steps;
                        }
                        Move();
                        break;
                    case "right":
                        _nextLocation = (_currentLocation.Item1, _currentLocation.Item2 + 1);
                        if (!CheckIfNextLocationIsInGridBoundary(_nextLocation))
                        {
                            return steps;
                        }
                        Move();
                        break;
                }
                if (_grid[_currentLocation.Item1][_currentLocation.Item2] != 'X')
                {
                    steps++;
                }

            }
            return steps;
        }

        part1Result += MoveUntilLeavingGrid();
        Console.WriteLine($"Part 1 solution is: {part1Result}");
    }



    public void SolvePart2()
    {
        int part2Result = 0;
        Console.WriteLine($"Part 2 solution is: {part2Result} ");
    }

    private (string, (int, int)) FindStartingDirectionAndLocation(char[][] grid)
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

    private void TurnRight()
    {
        switch (_currentDirection)
        {
            case "up":
                _currentDirection = "right";
                break;
            case "right":
                _currentDirection = "down";
                break;
            case "down":
                _currentDirection = "left";
                break;
            case "left":
                _currentDirection = "up";
                break;
        }
    }

    private void Move()
    {
        if (_grid[_nextLocation.Item1][_nextLocation.Item2] == '#')
        {
            TurnRight();
        }
        else
        {
            _currentLocation = _nextLocation;
        }
    }

    private bool CheckIfNextLocationIsInGridBoundary((int, int) nextLocation)
    {
        return nextLocation.Item1 >= 0 && nextLocation.Item1 < _grid.Length && nextLocation.Item2 >= 0 && nextLocation.Item2 < _grid[0].Length;
    }

}