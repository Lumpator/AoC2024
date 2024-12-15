namespace AoC2024.Utils;

public static class Utilities
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    /// <summary>
    /// Finds the next location based on the current location and the given direction (4 ways).
    /// </summary>
    /// <param name="currentLocation">A tuple representing the current location (row, column).</param>
    /// <param name="direction">The direction to move from the current location.</param>
    /// <returns>A tuple representing the new location (row, column) after moving in the specified direction.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when an invalid direction is provided.</exception>
    public static (int, int) FindNextLocation((int, int) currentLocation, Direction direction) => direction switch
    {
        Direction.Up => (currentLocation.Item1 - 1, currentLocation.Item2),
        Direction.Down => (currentLocation.Item1 + 1, currentLocation.Item2),
        Direction.Left => (currentLocation.Item1, currentLocation.Item2 - 1),
        Direction.Right => (currentLocation.Item1, currentLocation.Item2 + 1),
        _ => throw new ArgumentOutOfRangeException()
    };

    /// <summary>
    /// Checks if the given location is within the boundaries of the grid.
    /// </summary>
    /// <param name="nextLocation">A tuple representing the location to check (row, column).</param>
    /// <param name="grid">A 2D character array representing the grid.</param>
    /// <returns>True if the location is within the grid boundaries, otherwise false.</returns>
    public static bool CheckIfLocationIsInGridBoundary((int, int) nextLocation, char[][] grid)
    {
        return nextLocation.Item1 >= 0 && nextLocation.Item1 < grid.Length && nextLocation.Item2 >= 0 && nextLocation.Item2 < grid[0].Length;
    }
    public static void PrintGrid(char[][] grid)
    {
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                Console.Write(grid[i][j]);
            }
            Console.WriteLine();
        }
    }
}