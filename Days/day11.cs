using AoC2024.Interfaces;

namespace AoC2024.Days;

public class Day11 : IDay
{
    private readonly string[] _lines;
    private readonly Dictionary<long, long> _baseStones; // stones with values 0-9 and its partners to form a base stone
    private readonly Dictionary<long, long> _nonBaseStones; // stones that are processed before becaming base stones
    public Day11()
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Inputs", "inputDay11.txt");
        _lines = File.ReadAllLines(filePath);

        _baseStones = new Dictionary<long, long>();
        _nonBaseStones = new Dictionary<long, long>();
    }


    public void SolvePart1()
    {
        long part1Result = 0;

        foreach (var line in _lines)
        {
            var values = line.Split(' ');
            foreach (var value in values)
            {
                int key = int.Parse(value);
                _baseStones[key] = 1;
            }
        }

        for (int i = 0; i < 25; i++)
        {
            Blink();
        }

        foreach (var kvp in _baseStones)
        {
            part1Result += kvp.Value;
        }

        Console.WriteLine($"Part 1 solution is: {part1Result}");
    }


    public void SolvePart2()
    {
        long part2Result = 0;

        _baseStones.Clear();
        foreach (var line in _lines)
        {
            var values = line.Split(' ');
            foreach (var value in values)
            {
                int key = int.Parse(value);
                _baseStones[key] = 1;
            }
        }

        for (int i = 0; i < 75; i++)
        {
            Blink();
        }

        foreach (var kvp in _baseStones)
        {
            part2Result += kvp.Value;
        }

        Console.WriteLine($"Part 2 solution is: {part2Result} ");
    }

    private void Blink()
    {
        var newBaseStones = new Dictionary<long, long>();

        foreach (var kvp in _baseStones)
        {
            long key = kvp.Key;
            long value = kvp.Value;

            // list most of the patterns from BaseStones to avoid processing them
            switch (key)
            {
                case 0:
                    AddToDictionary(newBaseStones, 1, value);
                    break;
                case 1:
                    AddToDictionary(newBaseStones, 2024, value);
                    break;
                case 2024:
                    AddToDictionary(newBaseStones, 20, value);
                    AddToDictionary(newBaseStones, 24, value);
                    break;
                case 20:
                    AddToDictionary(newBaseStones, 0, value);
                    AddToDictionary(newBaseStones, 2, value);
                    break;
                case 24:
                    AddToDictionary(newBaseStones, 2, value);
                    AddToDictionary(newBaseStones, 4, value);
                    break;
                case 2:
                    AddToDictionary(newBaseStones, 4048, value);
                    break;
                case 4048:
                    AddToDictionary(newBaseStones, 40, value);
                    AddToDictionary(newBaseStones, 48, value);
                    break;
                case 40:
                    AddToDictionary(newBaseStones, 0, value);
                    AddToDictionary(newBaseStones, 4, value);
                    break;
                case 48:
                    AddToDictionary(newBaseStones, 4, value);
                    AddToDictionary(newBaseStones, 8, value);
                    break;
                case 3:
                    AddToDictionary(newBaseStones, 6072, value);
                    break;
                case 6072:
                    AddToDictionary(newBaseStones, 60, value);
                    AddToDictionary(newBaseStones, 72, value);
                    break;
                case 60:
                    AddToDictionary(newBaseStones, 0, value);
                    AddToDictionary(newBaseStones, 6, value);
                    break;
                case 72:
                    AddToDictionary(newBaseStones, 7, value);
                    AddToDictionary(newBaseStones, 2, value);
                    break;
                case 4:
                    AddToDictionary(newBaseStones, 8096, value);
                    break;
                case 8096:
                    AddToDictionary(newBaseStones, 80, value);
                    AddToDictionary(newBaseStones, 96, value);
                    break;
                case 80:
                    AddToDictionary(newBaseStones, 0, value);
                    AddToDictionary(newBaseStones, 8, value);
                    break;
                case 96:
                    AddToDictionary(newBaseStones, 9, value);
                    AddToDictionary(newBaseStones, 6, value);
                    break;
                case 5:
                    AddToDictionary(newBaseStones, 10120, value);
                    break;
                case 10120:
                    AddToDictionary(newBaseStones, 20482880, value);
                    break;
                case 20482880:
                    AddToDictionary(newBaseStones, 2048, value);
                    AddToDictionary(newBaseStones, 2880, value);
                    break;
                case 2048:
                    AddToDictionary(newBaseStones, 20, value);
                    AddToDictionary(newBaseStones, 48, value);
                    break;
                case 2880:
                    AddToDictionary(newBaseStones, 28, value);
                    AddToDictionary(newBaseStones, 80, value);
                    break;
                case 28:
                    AddToDictionary(newBaseStones, 2, value);
                    AddToDictionary(newBaseStones, 8, value);
                    break;
                case 6:
                    AddToDictionary(newBaseStones, 12144, value);
                    break;
                case 12144:
                    AddToDictionary(newBaseStones, 24579456, value);
                    break;
                case 24579456:
                    AddToDictionary(newBaseStones, 2457, value);
                    AddToDictionary(newBaseStones, 9456, value);
                    break;
                case 2457:
                    AddToDictionary(newBaseStones, 24, value);
                    AddToDictionary(newBaseStones, 57, value);
                    break;
                case 57:
                    AddToDictionary(newBaseStones, 5, value);
                    AddToDictionary(newBaseStones, 7, value);
                    break;
                case 9456:
                    AddToDictionary(newBaseStones, 94, value);
                    AddToDictionary(newBaseStones, 56, value);
                    break;
                case 94:
                    AddToDictionary(newBaseStones, 9, value);
                    AddToDictionary(newBaseStones, 4, value);
                    break;
                case 56:
                    AddToDictionary(newBaseStones, 5, value);
                    AddToDictionary(newBaseStones, 6, value);
                    break;
                case 7:
                    AddToDictionary(newBaseStones, 14168, value);
                    break;
                case 14168:
                    AddToDictionary(newBaseStones, 28676032, value);
                    break;
                case 28676032:
                    AddToDictionary(newBaseStones, 2867, value);
                    AddToDictionary(newBaseStones, 6032, value);
                    break;
                case 2867:
                    AddToDictionary(newBaseStones, 28, value);
                    AddToDictionary(newBaseStones, 67, value);
                    break;
                case 6032:
                    AddToDictionary(newBaseStones, 60, value);
                    AddToDictionary(newBaseStones, 32, value);
                    break;
                case 8:
                    AddToDictionary(newBaseStones, 16192, value);
                    break;
                case 16192:
                    AddToDictionary(newBaseStones, 32772608, value);
                    break;
                case 32772608:
                    AddToDictionary(newBaseStones, 3277, value);
                    AddToDictionary(newBaseStones, 2608, value);
                    break;
                case 3277:
                    AddToDictionary(newBaseStones, 32, value);
                    AddToDictionary(newBaseStones, 77, value);
                    break;
                case 2608:
                    AddToDictionary(newBaseStones, 26, value);
                    AddToDictionary(newBaseStones, 08, value);
                    break;
                case 9:
                    AddToDictionary(newBaseStones, 18216, value);
                    break;
                case 18216:
                    AddToDictionary(newBaseStones, 36869184, value);
                    break;
                case 36869184:
                    AddToDictionary(newBaseStones, 3686, value);
                    AddToDictionary(newBaseStones, 9184, value);
                    break;
                case 3686:
                    AddToDictionary(newBaseStones, 36, value);
                    AddToDictionary(newBaseStones, 86, value);
                    break;
                case 9184:
                    AddToDictionary(newBaseStones, 91, value);
                    AddToDictionary(newBaseStones, 84, value);
                    break;
                default:
                    AddToDictionary(_nonBaseStones, key, value);
                    break;
            }
        }

        ProcessNonBaseStones(newBaseStones);

        _baseStones.Clear();
        foreach (var kvp in newBaseStones)
        {
            _baseStones[kvp.Key] = kvp.Value;
        }
    }

    void ProcessNonBaseStones(Dictionary<long, long> newBaseStones)
    {
        var newNonBaseStones = new Dictionary<long, long>();

        foreach (var stone in _nonBaseStones)
        {

            if (stone.Key.ToString().Length % 2 == 0)
            {
                string valueStr = stone.Key.ToString();
                int mid = valueStr.Length / 2;
                int leftValue = int.Parse(valueStr.Substring(0, mid));
                int rightValue = int.Parse(valueStr.Substring(mid));
                AddToDictionary(newNonBaseStones, leftValue, stone.Value);
                AddToDictionary(newNonBaseStones, rightValue, stone.Value);
            }
            else
            {
                long newValue = stone.Key * 2024;
                AddToDictionary(newNonBaseStones, newValue, stone.Value);
            }
        }

        // add newNonBaseStones to newBaseStones
        foreach (var kvp in newNonBaseStones)
        {
            if (newBaseStones.ContainsKey(kvp.Key))
            {
                newBaseStones[kvp.Key] += kvp.Value;
            }
            else
            {
                newBaseStones[kvp.Key] = kvp.Value;
            }
        }

        _nonBaseStones.Clear();
    }


    private void AddToDictionary(Dictionary<long, long> dictionary, long key, long value)
    {
        if (dictionary.ContainsKey(key))
        {
            dictionary[key] += value;
        }
        else
        {
            dictionary[key] = value;
        }
    }

}