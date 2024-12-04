using AoC2024.Interfaces;

namespace AoC2024.Days;

public class Day4 : IDay
{
    private readonly string[] _lines;
    private readonly char[][] _grid;
    public Day4()
    {
        _lines = File.ReadAllLines("Inputs/inputDay4.txt");
        // Create the char[][] from the lines
        _grid = new char[_lines.Length][];
        for (int i = 0; i < _lines.Length; i++)
        {
            _grid[i] = _lines[i].ToCharArray();
        }
    }

    public void SolvePart1()
    {
        int part1Result = 0;
        static List<(int, int)> FindXMAS(char[][] grid)
        {
            // Define all 8 possible directions
            int[][] directions = new int[][]
            {
            new int[] { -1, 0 },  // Up
            new int[] { 1, 0 },   // Down
            new int[] { 0, -1 },  // Left
            new int[] { 0, 1 },   // Right
            new int[] { -1, -1 }, // Up-Left
            new int[] { -1, 1 },  // Up-Right
            new int[] { 1, -1 },  // Down-Left
            new int[] { 1, 1 }    // Down-Right
            };

            List<(int, int)> results = new List<(int, int)>();

            int rows = grid.Length;
            int cols = grid[0].Length;

            // Iterate through each character in the grid
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    // If the character is 'X', check all 8 directions
                    if (grid[i][j] == 'X')
                    {
                        for (int d = 0; d < directions.Length; d++)
                        {
                            int x = i, y = j;
                            bool found = true;

                            // Check the word "XMAS"
                            string word = "XMAS";
                            for (int k = 1; k < word.Length; k++)
                            {
                                x += directions[d][0];
                                y += directions[d][1];

                                // Check bounds and character match
                                if (x < 0 || x >= rows || y < 0 || y >= cols || grid[x][y] != word[k])
                                {
                                    found = false;
                                    break;
                                }
                            }

                            // If the word is found, add it to the results
                            if (found)
                            {
                                results.Add((i, j));
                            }
                        }
                    }
                }
            }
            return results;
        }

        part1Result += FindXMAS(_grid).Count();

        Console.WriteLine($"Part 1 solution is: {part1Result}");
    }

    public void SolvePart2()
    {
        int part2Result = 0;

        static int FindPattern(char[][] grid)
        {
            int matchCount = 0;

            int rows = grid.Length;
            int cols = grid[0].Length;

            // Iterate through the grid to find potential starting points for the pattern
            for (int i = 0; i < rows - 2; i++) // Stop 2 rows before the end
            {
                for (int j = 0; j < cols - 2; j++) // Ensure valid range for M.S horizontally
                {
                    // Check the first "M.S" in row `i`
                    matchCount += CheckFromTopToBot(grid, i, j);
                    matchCount += CheckFromBotToTop(grid, i, j);
                    matchCount += CheckFromLeftToRight(grid, i, j);
                    matchCount += CheckFromRightToLeft(grid, i, j);
                }
            }

            return matchCount;
        }
        part2Result += FindPattern(_grid);

        Console.WriteLine($"Part 2 solution is: {part2Result} ");
    }

    private static int CheckFromTopToBot(char[][] grid, int i, int j)
    {
        int matchCount = 0;
        if (grid[i][j] == 'M' && grid[i][j + 2] == 'S')
        {
            // Check the middle "A" in row `i + 1`
            if (grid[i + 1][j + 1] == 'A')
            {
                // Check the second "M.S" in row `i + 2`
                if (grid[i + 2][j] == 'M' && grid[i + 2][j + 2] == 'S')
                {
                    matchCount++;
                }
            }
        }

        return matchCount;
    }
    private static int CheckFromBotToTop(char[][] grid, int i, int j)
    {
        int matchCount = 0;
        if (grid[i][j] == 'S' && grid[i][j + 2] == 'M')
        {
            // Check the middle "A" in row `i + 1`
            if (grid[i + 1][j + 1] == 'A')
            {
                // Check the second "M.S" in row `i + 2`
                if (grid[i + 2][j] == 'S' && grid[i + 2][j + 2] == 'M')
                {
                    matchCount++;
                }
            }
        }

        return matchCount;
    }

    private static int CheckFromLeftToRight(char[][] grid, int i, int j)
    {
        int matchCount = 0;
        if (grid[i][j] == 'S' && grid[i][j + 2] == 'S')
        {
            // Check the middle "A" in row `i + 1`
            if (grid[i + 1][j + 1] == 'A')
            {
                // Check the second "M.S" in row `i + 2`
                if (grid[i + 2][j] == 'M' && grid[i + 2][j + 2] == 'M')
                {
                    matchCount++;
                }
            }
        }

        return matchCount;
    }

    private static int CheckFromRightToLeft(char[][] grid, int i, int j)
    {
        int matchCount = 0;
        if (grid[i][j] == 'M' && grid[i][j + 2] == 'M')
        {
            // Check the middle "A" in row `i + 1`
            if (grid[i + 1][j + 1] == 'A')
            {
                // Check the second "M.S" in row `i + 2`
                if (grid[i + 2][j] == 'S' && grid[i + 2][j + 2] == 'S')
                {
                    matchCount++;
                }
            }
        }

        return matchCount;
    }
}