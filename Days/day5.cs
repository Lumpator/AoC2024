using AoC2024.Interfaces;

namespace AoC2024.Days;

public class Day5 : IDay
{
    private readonly string[] _lines;
    private readonly List<Rule> _rules;
    int i = 0;
    public Day5()
    {
        _lines = File.ReadAllLines("Inputs/test.txt");
        _rules = new List<Rule>();
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
    }

    public void SolvePart1()
    {
        int part1Result = 0;
        int currentIndex = i;

        // Read the tickets
        for (; currentIndex < _lines.Length; currentIndex++)
        {
            var line = _lines[currentIndex];
            if (CheckIfLineIsValidAndReturnMiddleValue(line) != -1)
            {
                part1Result += CheckIfLineIsValidAndReturnMiddleValue(line);
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
        int currentIndex = i;

        int SortAndReturnMiddleValue(string line)
        {
            int[] numbers = line.Split(',').Select(int.Parse).ToArray();

            // Function to check if the array is valid
            bool IsLineValid(int[] arr)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    var rule = _rules.FirstOrDefault(r => r.Number == arr[i]);
                    if (rule != null)
                    {
                        // Check LowerIndexNumbers
                        if (i > 0 && !rule.LowerIndexNumbers.Contains(arr[i - 1]) && rule.LowerIndexNumbers.Any())
                        {
                            return false;
                        }
                        // Check HigherIndexNumbers
                        if (i < arr.Length - 1 && !rule.HigherIndexNumbers.Contains(arr[i + 1]) && rule.HigherIndexNumbers.Any())
                        {
                            return false;
                        }
                    }
                }
                return true;
            }

            // Bubble sort-like approach to fix the line
            bool swapped;
            do
            {
                swapped = false;
                for (int i = 0; i < numbers.Length - 1; i++)
                {
                    int current = numbers[i];
                    int next = numbers[i + 1];

                    var currentRule = _rules.FirstOrDefault(r => r.Number == current);
                    var nextRule = _rules.FirstOrDefault(r => r.Number == next);

                    // Check if swapping is needed
                    if ((currentRule != null && currentRule.LowerIndexNumbers.Contains(next)) ||
                        (nextRule != null && nextRule.HigherIndexNumbers.Contains(current)))
                    {
                        // Swap the numbers
                        numbers[i] = next;
                        numbers[i + 1] = current;
                        swapped = true;
                    }
                }
            } while (swapped && !IsLineValid(numbers)); // Repeat until valid or no swaps

            // Debugging Output
            Console.WriteLine("Input Line: " + line);
            Console.WriteLine("Sorted Line: " + string.Join(",", numbers));
            Console.WriteLine("IsLineValid: " + IsLineValid(numbers));
            Console.WriteLine("Middle Value: " + numbers[numbers.Length / 2]);

            // Return middle value
            return numbers[numbers.Length / 2];
        }

        for (; currentIndex < _lines.Length; currentIndex++)
        {
            var line = _lines[currentIndex];
            if (CheckIfLineIsValidAndReturnMiddleValue(line) == -1)
            {
                part2Result += SortAndReturnMiddleValue(line);
            }
        }

        Console.WriteLine($"Part 2 solution is: {part2Result} ");
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


