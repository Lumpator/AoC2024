using AoC2024.Interfaces;

namespace AoC2024.Days;

public class Day1 : IDay
{
    private readonly string[] _lines;
    private List<int> _leftValues;
    private List<int> _rightValues;

    public Day1()
    {
        _lines = Utils.Utilities.ReadLinesFromInputFIle("Inputs/inputDay1.txt");
        _leftValues = new List<int>();
        _rightValues = new List<int>();
        ParseInput();
    }


    public void SolvePart1()
    {
        int part1Result = 0;

        _leftValues.Sort();
        _rightValues.Sort();

        for (int i = 0; i < _leftValues.Count; i++)
        {
            part1Result += Math.Abs(_leftValues[i] - _rightValues[i]);
        }

        Console.WriteLine($"Part 1 solution is: {part1Result}");
    }

    public void SolvePart2()
    {
        long part2Result = 0;

        var rightValuesGrouped = _rightValues.GroupBy(num => num).ToDictionary(num => num.Key, num => num.Count());
        var leftValuesOccurences =

        _leftValues.Select(num => new
        {
            Value = num,
            Count = rightValuesGrouped.ContainsKey(num) ? rightValuesGrouped[num] : 0
        });

        foreach (var leftValueOccurence in leftValuesOccurences)
        {
            part2Result += leftValueOccurence.Value * leftValueOccurence.Count;
        }

        Console.WriteLine($"Part 2 solution is: {part2Result} ");
    }

    private void ParseInput()
    {
        foreach (string line in _lines)
        {
            string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 2)
            {
                _leftValues.Add(int.Parse(parts[0]));
                _rightValues.Add(int.Parse(parts[1]));
            }
            else
            {
                Console.WriteLine($"Invalid line format: {line}");
            }
        }
    }
}
