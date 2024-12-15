using AoC2024.Interfaces;
using static AoC2024.Utils.Utilities;

namespace AoC2024.Days;

public class Day15 : IDay
{
    private readonly string[] _lines;

    private readonly char[][] _grid;

    private readonly List<char> _instructions;

    public Day15()
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Inputs", "test.txt");
        _lines = File.ReadAllLines(filePath);

        int emptyLineIndex = Array.IndexOf(_lines, "");

        _grid = new char[emptyLineIndex][];
        for (int i = 0; i < emptyLineIndex; i++)
        {
            _grid[i] = _lines[i].ToCharArray();
        }


        _instructions = new List<char>();
        for (int i = emptyLineIndex + 1; i < _lines.Length; i++)
        {
            foreach (char instruction in _lines[i])
            {
                _instructions.Add(instruction);
            }
        }


    }

    public void SolvePart1()
    {
        long part1Result = 0;
        //copy grid
        char[][] grid = new char[_grid.Length][];
        for (int i = 0; i < _grid.Length; i++)
        {
            grid[i] = new char[_grid[i].Length];
            Array.Copy(_grid[i], grid[i], _grid[i].Length);
        }
        (int, int) currentLocation = FindStartingLocation(grid);
        grid[currentLocation.Item1][currentLocation.Item2] = '.';

        foreach (char instruction in _instructions)
        {
            var nextLocation = FindNextLocation(currentLocation, instruction);
            if (grid[nextLocation.Item1][nextLocation.Item2] == '.')
            {
                currentLocation = nextLocation;
                continue;
            }
            if (grid[nextLocation.Item1][nextLocation.Item2] == '#')
            {
                continue;
            }
            if (grid[nextLocation.Item1][nextLocation.Item2] == 'O')
            {
                if (CheckDirectionUntilStop(grid, currentLocation, instruction))
                {
                    currentLocation = nextLocation;
                    //PrintGrid(grid);
                }
            }
        }
        //print grid
        part1Result += SumBoxesGPSCoordinates(grid);


        Console.WriteLine($"Part 1 solution is: {part1Result}");
    }



    public void SolvePart2()
    {
        int part2Result = 0;


        Console.WriteLine($"Part 2 solution is: {part2Result} ");
    }

    private (int, int) FindStartingLocation(char[][] grid)
    {
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                if (grid[i][j] == '@')
                {
                    return (i, j);
                }

            }
        }
        return (-1, -1);
    }

    private (int, int) FindNextLocation((int, int) currentLocation, char direction)
    {
        return direction switch
        {
            '^' => (currentLocation.Item1 - 1, currentLocation.Item2),
            'v' => (currentLocation.Item1 + 1, currentLocation.Item2),
            '<' => (currentLocation.Item1, currentLocation.Item2 - 1),
            '>' => (currentLocation.Item1, currentLocation.Item2 + 1),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private bool CheckDirectionUntilStop(char[][] grid, (int, int) location, char direction)
    {
        int row = location.Item1;
        int column = location.Item2;
        List<(int, int)> boxes = new List<(int, int)>();

        while (true)
        {
            switch (direction)
            {
                case '^':
                    row--;
                    break;
                case 'v':
                    row++;
                    break;
                case '<':
                    column--;
                    break;
                case '>':
                    column++;
                    break;
            }

            if (grid[row][column] == '#')
            {
                return false;
            }
            else if (grid[row][column] == '.')
            {
                MoveBoxes(boxes, direction, grid);
                return true;
            }
            else
            {
                boxes.Add((row, column));
            }
        }
    }
    private void MoveBoxes(List<(int, int)> boxes, char direction, char[][] grid)
    {
        foreach (var box in boxes)
        {
            grid[box.Item1][box.Item2] = '.';
        }
        foreach (var box in boxes)
        {
            switch (direction)
            {
                case '^':
                    grid[box.Item1 - 1][box.Item2] = 'O';
                    break;
                case 'v':
                    grid[box.Item1 + 1][box.Item2] = 'O';
                    break;
                case '<':
                    grid[box.Item1][box.Item2 - 1] = 'O';
                    break;
                case '>':
                    grid[box.Item1][box.Item2 + 1] = 'O';
                    break;
            }
        }
    }

    private long SumBoxesGPSCoordinates(char[][] grid)
    {
        long result = 0;
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                if (grid[i][j] == 'O')
                {
                    result += 100 * i + j;
                }

            }
        }
        return result;
    }
}

