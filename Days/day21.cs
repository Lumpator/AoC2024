using AoC2024.Interfaces;
using static AoC2024.Utils.Utilities;

// 155578 => too High

namespace AoC2024.Days;

public class Day21 : IDay
{
    private readonly string[] _lines;
    public Day21()
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Inputs", "test.txt");
        _lines = File.ReadAllLines(filePath);
    }

    public void SolvePart1()
    {
        int part1Result = 0;
        /* int test179A_1 = MoveAfterClick(Direction.Up) + Move(Direction.Up, Direction.Left) + Move(Direction.Left, Direction.Left) + Click(Direction.Left);
int test179A_7 = MoveAfterClick(Direction.Up) + Move(Directi<on.Up, Direction.Up) + Click(Direction.Up);
int test179A_9 = MoveAfterClick(Direction.Right) + Move(Direction.Right, Direction.Right) + Click(Direction.Right);
int test179A_A = MoveAfterClick(Direction.Down) + Move(Direction.Down, Direction.Down) + Move(Direction.Down, Direction.Down) + Click(Direction.Down);
Console.WriteLine("Test179A = " + (test179A_1 + test179A_7 + test179A_9 + test179A_A));

int test980A_9 = MoveAfterClick(Direction.Up) + Move(Direction.Up, Direction.Up) + Move(Direction.Up, Direction.Up) + Click(Direction.Up);
int test980A_8 = MoveAfterClick(Direction.Left) + Click(Direction.Left);
int test980A_0 = MoveAfterClick(Direction.Down) + Move(Direction.Down, Direction.Down) + Move(Direction.Down, Direction.Down) + Click(Direction.Down);
int test980A_A = MoveAfterClick(Direction.Right) + Click(Direction.Right);
Console.WriteLine("Test980A = " + (test980A_9 + test980A_8 + test980A_0 + test980A_A));
} */


        string[] codes = { "208A", "586A", "341A", "463A", "593A" };
        int test208A_2 = MoveAfterClick(Direction.Left) + Move(Direction.Left, Direction.Up) + Click(Direction.Up); // OK
        int test208A_0 = MoveAfterClick(Direction.Down) + Click(Direction.Down); // OK
        int test208A_8 = MoveAfterClick(Direction.Up) + Move(Direction.Up, Direction.Up) + Move(Direction.Up, Direction.Up) + Click(Direction.Up); // OK
        int test208A_A = MoveAfterClick(Direction.Down) + Move(Direction.Down, Direction.Down) + Move(Direction.Down, Direction.Down) + Move(Direction.Down, Direction.Right) + Click(Direction.Right); // OK?

        int score208A = (test208A_2 + test208A_0 + test208A_8 + test208A_A) * 208;

        int test586A_5 = MoveAfterClick(Direction.Left) + Move(Direction.Left, Direction.Up) + Move(Direction.Up, Direction.Up) + Click(Direction.Up); // OK
        int test586A_8 = MoveAfterClick(Direction.Up) + Click(Direction.Up); // OK
        int test586A_6 = MoveAfterClick(Direction.Down) + Move(Direction.Down, Direction.Right) + Click(Direction.Right); // OK ?
        int test586A_A = MoveAfterClick(Direction.Down) + Move(Direction.Down, Direction.Down) + Click(Direction.Down); // OK


        int score586A = (test586A_5 + test586A_8 + test586A_6 + test586A_A) * 586;

        int test341A_3 = MoveAfterClick(Direction.Up) + Click(Direction.Up); // OK
        int test341A_4 = MoveAfterClick(Direction.Left) + Move(Direction.Left, Direction.Left) + Move(Direction.Left, Direction.Up) + Click(Direction.Up); // OK ?
        int test341A_1 = MoveAfterClick(Direction.Down) + Click(Direction.Down); // OK
        int test341A_A = MoveAfterClick(Direction.Right) + Move(Direction.Right, Direction.Right) + Move(Direction.Right, Direction.Down) + Click(Direction.Down); // OK ?

        int score341A = (test341A_3 + test341A_4 + test341A_1 + test341A_A) * 341;

        int test463A_4 = MoveAfterClick(Direction.Up) + Move(Direction.Up, Direction.Up) + Move(Direction.Up, Direction.Left) + Move(Direction.Left, Direction.Left) + Click(Direction.Left); // OK ??
        int test463A_6 = MoveAfterClick(Direction.Right) + Move(Direction.Right, Direction.Right) + Click(Direction.Right); // OK
        int test463A_3 = MoveAfterClick(Direction.Down) + Click(Direction.Down); // OK
        int test463A_A = MoveAfterClick(Direction.Down) + Click(Direction.Down); // OK

        int score463A = (test463A_4 + test463A_6 + test463A_3 + test463A_A) * 463;

        int test593A_5 = MoveAfterClick(Direction.Left) + Move(Direction.Left, Direction.Up) + Move(Direction.Up, Direction.Up) + Click(Direction.Up); // OK
        int test593A_9 = MoveAfterClick(Direction.Right) + Move(Direction.Right, Direction.Up) + Click(Direction.Up); // OK ??
        int test593A_3 = MoveAfterClick(Direction.Down) + Move(Direction.Down, Direction.Down) + Click(Direction.Down);  // OK
        int test593A_A = MoveAfterClick(Direction.Down) + Click(Direction.Down); // OK

        int score593A = (test593A_5 + test593A_9 + test593A_3 + test593A_A) * 593;

        int totalScore = score208A + score586A + score341A + score463A + score593A;
        part1Result = totalScore;

        //Console.WriteLine(test593A_9A);
        //Console.WriteLine(test593A_9B);










        Console.WriteLine($"Part 1 solution is: {part1Result} ");
    }

    public void SolvePart2()
    {
        int part2Result = 0;
        Console.WriteLine($"Part 2 solution is: {part2Result} ");
    }


    public int MoveAfterClick(Direction directionTo) => directionTo switch
    {
        Direction.Up => 8,
        Direction.Down => 9,
        Direction.Left => 10,
        Direction.Right => 6,
        _ => 0,
    };
    public int Click(Direction directionLast) => directionLast switch
    {
        Direction.Up => 4,
        Direction.Down => 7,
        Direction.Left => 8,
        Direction.Right => 4,
        _ => 0,
    };
    public int Move(Direction directionLast, Direction directionTo) => (directionLast, directionTo) switch
    {
        (Direction.Up, Direction.Up) => 1,
        (Direction.Down, Direction.Down) => 1,
        (Direction.Left, Direction.Left) => 1,
        (Direction.Right, Direction.Right) => 1,

        (Direction.Up, Direction.Left) => 9,
        (Direction.Up, Direction.Right) => 7,

        (Direction.Down, Direction.Left) => 8,
        (Direction.Down, Direction.Right) => 4,

        (Direction.Left, Direction.Up) => 7,
        (Direction.Left, Direction.Down) => 4,

        (Direction.Right, Direction.Up) => 9,
        (Direction.Right, Direction.Down) => 8,

        _ => 0,
    };

}




