using AoC2024.Interfaces;

namespace AoC2024.Days;

public class Day1 : IDay
{
    private readonly string[] lines;
    private List<int> leftValues;
    private List<int> rightValues;

    public Day1()
    {
        lines = Utils.Utilities.ReadLinesFromInputFIle("Inputs/inputDay1.txt");
        leftValues = new List<int>();
        rightValues = new List<int>();
        ParseInput();
    }


    public void SolvePart1()
    {
        int Part1Result = 0;

        leftValues.Sort();
        rightValues.Sort();

        for (int i = 0; i < leftValues.Count; i++)
        {
            int left = leftValues[i];
            int right = rightValues[i];

            Part1Result += Math.Abs(left - right);
        }

        Console.WriteLine($"Part 1 solution is: {Part1Result}");
    }

    public void SolvePart2()
    {
        long Part2Result = 0;

        var rightValuesGrouped = rightValues.GroupBy(num => num).ToDictionary(num => num.Key, num => num.Count());
        var leftValuesOccurences =

        leftValues.Select(num => new
        {
            Value = num,
            Count = rightValuesGrouped.ContainsKey(num) ? rightValuesGrouped[num] : 0
        });

        foreach (var leftValueOccurence in leftValuesOccurences)
        {
            Part2Result += leftValueOccurence.Value * leftValueOccurence.Count;
        }

        Console.WriteLine($"Part 2 solution is: {Part2Result} ");
    }

    private void ParseInput()
    {
        foreach (string line in lines)
        {
            string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 2)
            {
                leftValues.Add(int.Parse(parts[0]));
                rightValues.Add(int.Parse(parts[1]));
            }
            else
            {
                Console.WriteLine($"Invalid line format: {line}");
            }
        }
    }
}
