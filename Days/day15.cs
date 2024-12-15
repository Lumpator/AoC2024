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
        string filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Inputs", "inputDay15.txt");
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
        long part2Result = 0;
        //copy grid
        char[][] grid = new char[_grid.Length][];
        for (int i = 0; i < _grid.Length; i++)
        {
            grid[i] = new char[_grid[i].Length];
            Array.Copy(_grid[i], grid[i], _grid[i].Length);
        }

        //copy grid to new bigger one. Iterate over the grid and for each "char", add that char two times instead into new one
        char[][] newGrid = new char[grid.Length][];
        for (int i = 0; i < grid.Length; i++)
        {
            newGrid[i] = new char[grid[i].Length * 2];
            for (int j = 0; j < grid[i].Length; j++)
            {
                newGrid[i][j * 2] = grid[i][j];
                newGrid[i][j * 2 + 1] = grid[i][j];
            }
        }

        //find each pair of 'O' 'O' and replace it with '[' ']'
        for (int i = 0; i < newGrid.Length; i++)
        {
            for (int j = 0; j < newGrid[i].Length; j++)
            {
                if (newGrid[i][j] == 'O' && newGrid[i][j + 1] == 'O')
                {
                    newGrid[i][j] = '[';
                    newGrid[i][j + 1] = ']';
                }
            }
        }


        (int, int) currentLocation = FindStartingLocation(newGrid);
        newGrid[currentLocation.Item1][currentLocation.Item2] = '@';
        newGrid[currentLocation.Item1][currentLocation.Item2 + 1] = '.';

        int instructionCounter = 0;
        foreach (char instruction in _instructions)
        {
            newGrid[currentLocation.Item1][currentLocation.Item2] = '.';
            var nextLocation = FindNextLocation(currentLocation, instruction);
            if (newGrid[nextLocation.Item1][nextLocation.Item2] == '.')
            {
                currentLocation = nextLocation;
                instructionCounter++;
                continue;
            }
            if (newGrid[nextLocation.Item1][nextLocation.Item2] == '#')
            {
                instructionCounter++;
                continue;
            }
            if (newGrid[nextLocation.Item1][nextLocation.Item2] == '[' || newGrid[nextLocation.Item1][nextLocation.Item2] == ']')
            {
                if (CheckDirectionUntilStop2(newGrid, currentLocation, instruction))
                {
                    currentLocation = nextLocation;
                }
            }
            newGrid[currentLocation.Item1][currentLocation.Item2] = '@';
            //Console.WriteLine(instruction);
            //PrintGrid(newGrid);
        }

        part2Result += SumBoxesGPSCoordinates2(newGrid);


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
    private bool MoveBoxes2(HashSet<Box> boxes, char direction, char[][] grid)
    {
        foreach (var box in boxes)
        {
            if (!box.canMove(grid, direction))
            {
                return false;
            }
        }
        foreach (var box in boxes)
        {
            grid[box.leftPartLocation.Item1][box.leftPartLocation.Item2] = '.';
            grid[box.rightPartLocation.Item1][box.rightPartLocation.Item2] = '.';
        }
        foreach (var box in boxes)
        {
            switch (direction)
            {
                case '^':
                    box.MoveUp();
                    break;
                case 'v':
                    box.MoveDown();
                    break;
                case '<':
                    box.MoveLeft();
                    break;
                case '>':
                    box.MoveRight();
                    break;
            }
            grid[box.leftPartLocation.Item1][box.leftPartLocation.Item2] = '[';
            grid[box.rightPartLocation.Item1][box.rightPartLocation.Item2] = ']';
        }

        return true;
    }

    public class Box
    {
        public (int, int) leftPartLocation { get; set; }
        public (int, int) rightPartLocation { get; set; }

        public Box() { }
        public Box((int, int) partLocation, bool isLeftPart = true)
        {
            if (isLeftPart)
            {
                leftPartLocation = partLocation;
                rightPartLocation = (partLocation.Item1, partLocation.Item2 + 1);
            }
            else
            {
                rightPartLocation = partLocation;
                leftPartLocation = (partLocation.Item1, partLocation.Item2 - 1);
            }
        }

        public void MoveUp()
        {
            leftPartLocation = (leftPartLocation.Item1 - 1, leftPartLocation.Item2);
            rightPartLocation = (rightPartLocation.Item1 - 1, rightPartLocation.Item2);
        }
        public void MoveDown()
        {
            leftPartLocation = (leftPartLocation.Item1 + 1, leftPartLocation.Item2);
            rightPartLocation = (rightPartLocation.Item1 + 1, rightPartLocation.Item2);
        }
        public void MoveLeft()
        {
            leftPartLocation = (leftPartLocation.Item1, leftPartLocation.Item2 - 1);
            rightPartLocation = (rightPartLocation.Item1, rightPartLocation.Item2 - 1);
        }
        public void MoveRight()
        {
            leftPartLocation = (leftPartLocation.Item1, leftPartLocation.Item2 + 1);
            rightPartLocation = (rightPartLocation.Item1, rightPartLocation.Item2 + 1);
        }

        public HashSet<Box> CheckIfBoxAbove(char[][] grid)
        {
            HashSet<Box> boxesAbove = new HashSet<Box>();
            if (grid[leftPartLocation.Item1 - 1][leftPartLocation.Item2] == '[')
            {
                boxesAbove.Add(new Box((leftPartLocation.Item1 - 1, leftPartLocation.Item2), true));
            }
            if (grid[leftPartLocation.Item1 - 1][leftPartLocation.Item2] == ']')
            {
                boxesAbove.Add(new Box((leftPartLocation.Item1 - 1, leftPartLocation.Item2), false));
            }
            if (grid[rightPartLocation.Item1 - 1][rightPartLocation.Item2] == '[')
            {
                boxesAbove.Add(new Box((rightPartLocation.Item1 - 1, rightPartLocation.Item2), true));
            }
            if (grid[rightPartLocation.Item1 - 1][rightPartLocation.Item2] == ']')
            {
                boxesAbove.Add(new Box((rightPartLocation.Item1 - 1, rightPartLocation.Item2), false));
            }
            return boxesAbove;

        }
        public HashSet<Box> CheckIfBoxBelove(char[][] grid)
        {
            HashSet<Box> boxesBelow = new HashSet<Box>();
            if (grid[leftPartLocation.Item1 + 1][leftPartLocation.Item2] == ']')
            {
                boxesBelow.Add(new Box((leftPartLocation.Item1 + 1, leftPartLocation.Item2), false));
            }
            if (grid[leftPartLocation.Item1 + 1][leftPartLocation.Item2] == '[')
            {
                boxesBelow.Add(new Box((leftPartLocation.Item1 + 1, leftPartLocation.Item2), true));
            }
            if (grid[rightPartLocation.Item1 + 1][rightPartLocation.Item2] == ']')
            {
                boxesBelow.Add(new Box((rightPartLocation.Item1 + 1, rightPartLocation.Item2), false));
            }
            if (grid[rightPartLocation.Item1 + 1][rightPartLocation.Item2] == '[')
            {
                boxesBelow.Add(new Box((rightPartLocation.Item1 + 1, rightPartLocation.Item2), true));
            }
            return boxesBelow;

        }
        public bool canMove(char[][] grid, char direction)
        {
            switch (direction)
            {
                case '^':
                    if (grid[leftPartLocation.Item1 - 1][leftPartLocation.Item2] == '#' || grid[rightPartLocation.Item1 - 1][rightPartLocation.Item2] == '#')
                    {
                        return false;
                    }
                    break;
                case 'v':
                    if (grid[leftPartLocation.Item1 + 1][leftPartLocation.Item2] == '#' || grid[rightPartLocation.Item1 + 1][rightPartLocation.Item2] == '#')
                    {
                        return false;
                    }
                    break;
                case '<':
                    if (grid[leftPartLocation.Item1][leftPartLocation.Item2 - 1] == '#' || grid[rightPartLocation.Item1][rightPartLocation.Item2 - 1] == '#')
                    {
                        return false;
                    }
                    break;
                case '>':
                    if (grid[leftPartLocation.Item1][leftPartLocation.Item2 + 1] == '#' || grid[rightPartLocation.Item1][rightPartLocation.Item2 + 1] == '#')
                    {
                        return false;
                    }
                    break;
            }
            return true;
        }

    }
    public bool CheckDirectionUntilStop2(char[][] grid, (int, int) location, char direction)
    {
        int row = location.Item1;
        int column = location.Item2;
        HashSet<Box> boxes = new HashSet<Box>();

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
                if (MoveBoxes2(boxes, direction, grid))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else if (grid[row][column] == '[')
            {
                boxes.Add(new Box((row, column), true));
            }
            else if (grid[row][column] == ']')
            {
                boxes.Add(new Box((row, column), false));
            }
            if (direction == '^')
            {
                for (int i = 0; i < 10; i++)
                {
                    var newBoxes = new HashSet<Box>();
                    foreach (var box in boxes)
                    {
                        foreach (var b in box.CheckIfBoxAbove(grid))
                        {
                            newBoxes.Add(b);
                        }
                    }
                    foreach (var b in newBoxes)
                    {
                        if (!boxes.Any(existingBox => existingBox.leftPartLocation == b.leftPartLocation && existingBox.rightPartLocation == b.rightPartLocation))
                        {
                            boxes.Add(b);
                        }
                    }
                }
            }
            if (direction == 'v')
            {
                for (int i = 0; i < 10; i++)
                {
                    var newBoxes = new HashSet<Box>();
                    foreach (var box in boxes)
                    {
                        foreach (var b in box.CheckIfBoxBelove(grid))
                        {
                            newBoxes.Add(b);
                        }
                    }
                    foreach (var b in newBoxes)
                    {
                        if (!boxes.Any(existingBox => existingBox.leftPartLocation == b.leftPartLocation && existingBox.rightPartLocation == b.rightPartLocation))
                        {
                            boxes.Add(b);
                        }
                    }
                }
            }



        }
    }

    private long SumBoxesGPSCoordinates2(char[][] grid)
    {
        long result = 0;
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                if (grid[i][j] == '[')
                {
                    result += 100 * i + j;
                }

            }
        }
        return result;
    }
}
