using AoC2024.Interfaces;
namespace AoC2024.Days;

public class Day23 : IDay
{
    private readonly string[] _lines;
    private readonly Dictionary<string, List<string>> _computers;
    public Day23()
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Inputs", "inputDay23.txt");
        _lines = File.ReadAllLines(filePath);
        _computers = new Dictionary<string, List<string>>();
        foreach (var line in _lines)
        {
            var parts = line.Split('-');
            string first = parts[0];
            string second = parts[1];

            if (!_computers.ContainsKey(first))
            {
                _computers[first] = new List<string>();
            }
            if (!_computers[first].Contains(second))
            {
                _computers[first].Add(second);
            }

            if (!_computers.ContainsKey(second))
            {
                _computers[second] = new List<string>();
            }
            if (!_computers[second].Contains(first))
            {
                _computers[second].Add(first);
            }
        }
    }

    public void SolvePart1()
    {
        int part1Result = 0;
        var keys = new List<string>(_computers.Keys);

        HashSet<string> uniqueTriplets = new HashSet<string>();

        for (int i = 0; i < keys.Count; i++)
        {
            for (int j = i + 1; j < keys.Count; j++)
            {
                string first = keys[i];
                string second = keys[j];

                if (!_computers[first].Contains(second) || !_computers[second].Contains(first))
                {
                    continue;
                }
                var sharedComputers = new HashSet<string>(_computers[first]);
                sharedComputers.IntersectWith(_computers[second]);

                foreach (var shared in sharedComputers)
                {
                    var triplet = new List<string> { first, second, shared };
                    triplet.Sort();
                    string tripletString = string.Join(",", triplet);
                    uniqueTriplets.Add(tripletString);
                }
            }
        }

        foreach (var triplet in uniqueTriplets)
        {
            var tripletParts = triplet.Split(",");
            if (tripletParts.Any(part => part.StartsWith("t")))
            {
                part1Result++;
            }
        }

        Console.WriteLine($"Part 1 solution is: {part1Result}");
    }

    public void SolvePart2()
    {
        HashSet<string> uniqueComputersConnectedWithEachOther = new HashSet<string>();

        var keys = new List<string>(_computers.Keys);
        for (int i = 0; i < keys.Count; i++)
        {
            for (int j = i + 1; j < keys.Count; j++)
            {
                string first = keys[i];
                string second = keys[j];

                if (!_computers[first].Contains(second) || !_computers[second].Contains(first))
                {
                    continue;
                }

                var sharedComputers = new HashSet<string>(_computers[first]);
                sharedComputers.IntersectWith(_computers[second]);

                if (CheckIfAllComputersAreConnected(sharedComputers))
                {
                    var pairWithShared = new List<string> { first, second };
                    pairWithShared.AddRange(sharedComputers);
                    pairWithShared.Sort();
                    string pairWithSharedString = string.Join(",", pairWithShared);
                    uniqueComputersConnectedWithEachOther.Add(pairWithSharedString);
                }
            }
        }

        string longestItem = uniqueComputersConnectedWithEachOther.OrderByDescending(s => s.Length).FirstOrDefault() ?? string.Empty;
        Console.WriteLine($"Part 2 solution is: {longestItem}");
    }

    private bool CheckIfAllComputersAreConnected(HashSet<string> sharedComputers)
    {
        bool allSharedConnectedToEachOther = true;
        var sharedList = new List<string>(sharedComputers);
        for (int m = 0; m < sharedList.Count; m++)
        {
            for (int n = m + 1; n < sharedList.Count; n++)
            {
                string sharedFirst = sharedList[m];
                string sharedSecond = sharedList[n];
                if (!_computers[sharedFirst].Contains(sharedSecond) || !_computers[sharedSecond].Contains(sharedFirst))
                {
                    allSharedConnectedToEachOther = false;
                    break;
                }
            }
            if (!allSharedConnectedToEachOther)
            {
                break;
            }
        }

        return allSharedConnectedToEachOther;
    }
}