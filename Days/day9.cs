using System.Text;
using AoC2024.Interfaces;

namespace AoC2024.Days;

public class Day9 : IDay
{
    private readonly string[] _lines;
    public Day9()
    {
        _lines = File.ReadAllLines("Inputs/test.txt");

    }

    public void SolvePart1()
    {
        long part1Result = 0;
        int blockId = 0;
        int i = 0;
        List<(int id, int length)> blocks = new List<(int id, int length)>();
        List<int> gaps = new List<int>();

        foreach (var line in _lines)
        {
            foreach (var character in line)
            {
                int length = int.Parse(character.ToString());
                if (i % 2 == 0)
                {
                    blocks.Add((blockId, length));
                    blockId++;
                }
                else
                {
                    gaps.Add(length);
                }
                i++;
            }
        }


        var blockIndexToMove = blocks.Count - 1;
        var remainingInBlockToMove = blocks[blockIndexToMove].length;
        int currentPosition = 0;

        for (int index = 0; index <= blockIndexToMove; ++index)
        {
            var remainingItemsInBlock = index == blockIndexToMove ? remainingInBlockToMove : blocks[index].length;
            for (int blockItemIndex = 0; blockItemIndex < remainingItemsInBlock; ++blockItemIndex)
            {
                part1Result += currentPosition * blocks[index].id;
                currentPosition++;
            }

            for (var gapItemIndex = 0; index < blockIndexToMove && gapItemIndex < gaps[index]; ++gapItemIndex)
            {
                while (index < blockIndexToMove && remainingInBlockToMove == 0)
                {
                    blockIndexToMove--;
                    remainingInBlockToMove = blocks[blockIndexToMove].length;
                }

                if (index < blockIndexToMove)
                {
                    part1Result += currentPosition * blocks[blockIndexToMove].id;
                    remainingInBlockToMove--;
                    currentPosition++;
                }
            }
        }

        Console.WriteLine($"Part 1 solution is: {part1Result}");
    }

    public void SolvePart2()
    {
        long part2Result = 0;


        Console.WriteLine($"Part 2 solution is: {part2Result} ");
    }



}