using AoC2024.Interfaces;

namespace AoC2024.Days;

public class Day5 : IDay
{
    private readonly string[] _lines;
    private readonly List<Rule> _rules;
    public Day5()
    {
        _lines = File.ReadAllLines("Inputs/inputDay5.txt");
        _rules = new List<Rule>();
    }

    public void SolvePart1()
    {
        int part1Result = 0;
        int i = 0;

        // Read the rules
        while (i < _lines.Length && !string.IsNullOrWhiteSpace(_lines[i]))
        {
            var line = _lines[i];
            var parts = line.Split('|');
            int left = int.Parse(parts[0]);
            int right = int.Parse(parts[1]);

            AddOrUpdateRule(left, right, true);
            AddOrUpdateRule(right, left, false);

            i++;
        }
        i++; // Skip the empty line

        // Read the tickets
        for (; i < _lines.Length; i++)
        {
            var line = _lines[i];
            if (CheckIfLineIsValidAndReturnMiddleValue(line) != -1)
            {
                part1Result += CheckIfLineIsValidAndReturnMiddleValue(line);
            }
        }


        void AddOrUpdateRule(int number, int relatedNumber, bool IsHigherIndex)
        {
            var rule = _rules.FirstOrDefault(r => r.Number == number);
            if (rule == null)
            {
                rule = new Rule { Number = number };
                _rules.Add(rule);
            }

            if (IsHigherIndex)
            {
                if (!rule.HigherIndexNumbers.Contains(relatedNumber))
                {
                    rule.HigherIndexNumbers.Add(relatedNumber);
                }
            }
            else
            {
                if (!rule.LowerIndexNumbers.Contains(relatedNumber))
                {
                    rule.LowerIndexNumbers.Add(relatedNumber);
                }
            }
        }

        Console.WriteLine($"Part 1 solution is: {part1Result}");
    }

    private int CheckIfLineIsValidAndReturnMiddleValue(string line)
    {
        int[] numbers = line.Split(',')
                     .Select(int.Parse)
                     .ToArray();
        bool valid = true;
        for (int i = 1; i < numbers.Length - 1; i++)
        {
            if (i == numbers.Length - 1)
            {
                if (!_rules.Any(r => r.Number == numbers[i] && r.HigherIndexNumbers.Count == 0))
                {
                    valid = false;
                    return -1;
                }
            }
            else
            {
                if (!_rules.Any(r => r.Number == numbers[i] && r.LowerIndexNumbers.Contains(numbers[i - 1]) && r.HigherIndexNumbers.Contains(numbers[i + 1])))
                {
                    valid = false;
                    return -1;
                }
            }
        }
        return valid ? numbers[numbers.Length / 2] : -1;

    }

    public void SolvePart2()
    {
        int part2Result = 0;
        Console.WriteLine($"Part 2 solution is: {part2Result} ");
    }


}

public class Rule
{
    public int Number { get; set; }
    public List<int> LowerIndexNumbers { get; set; }
    public List<int> HigherIndexNumbers { get; set; }
    public Rule()
    {
        LowerIndexNumbers = new List<int>();
        HigherIndexNumbers = new List<int>();
    }

}


