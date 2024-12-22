using AoC2024.Interfaces;

namespace AoC2024.Days;

public class Day22 : IDay
{
    private readonly string[] _lines;
    private readonly long[] _secrets;
    public Day22()
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Inputs", "inputDay22.txt");
        _lines = File.ReadAllLines(filePath);
        _secrets = new long[_lines.Length];
        for (int i = 0; i < _lines.Length; i++)
        {
            _secrets[i] = long.Parse(_lines[i]);
        }
    }

    public void SolvePart1()
    {
        long part1Result = 0;
        foreach (var secret in _secrets)
        {
            var newSecret = secret;
            for (int i = 0; i < 2000; i++)
            {
                newSecret = NextSecret(newSecret);
            }
            part1Result += newSecret;
        }

        Console.WriteLine($"Part 1 solution is: {part1Result}");
    }

    public void SolvePart2()
    {
        int part2Result = 0;
        Dictionary<string, int> sequencesOfAllSecrets = new();
        foreach (var secret in _secrets)
        {
            var newSecret = secret;
            Dictionary<string, int> sequencesOfCurrentSecret = new();
            List<int> sequence = new List<int>(4);
            int price = (int)(newSecret % 10);

            for (int i = 0; i < 1999; i++)
            {
                newSecret = NextSecret(newSecret);
                int newPrice = (int)(newSecret % 10);
                int change = newPrice - price;
                price = newPrice;
                if (sequence.Count == 4)
                {
                    sequence.RemoveAt(0);
                    sequence.Add(change);
                    var sequenceCopy = sequence.ToArray();
                    var sequenceKey = string.Join(",", sequenceCopy);
                    if (!sequencesOfCurrentSecret.ContainsKey(sequenceKey))
                    {
                        sequencesOfCurrentSecret.Add(sequenceKey, price);
                    }
                }
                else
                {
                    sequence.Add(change);
                }

            }

            foreach (var item in sequencesOfCurrentSecret)
            {
                if (!sequencesOfAllSecrets.ContainsKey(item.Key))
                {
                    sequencesOfAllSecrets.Add(item.Key, item.Value);
                }
                else
                {
                    sequencesOfAllSecrets[item.Key] += item.Value;
                }
            }
        }

        var highestValueSequence = sequencesOfAllSecrets.OrderByDescending(x => x.Value).First();
        part2Result = highestValueSequence.Value;

        Console.WriteLine($"Part 2 solution is: {part2Result} ");
    }


    long Mix(long secret, long value)
    {
        return value ^ secret;
    }
    long Prune(long secret)
    {
        return secret % 16777216;
    }
    long NextSecret(long secret)
    {
        long newSecret = Prune(Mix(secret, secret * 64));
        newSecret = Prune(Mix(newSecret, newSecret / 32));
        newSecret = Prune(Mix(newSecret, newSecret * 2048));
        return newSecret;
    }
}