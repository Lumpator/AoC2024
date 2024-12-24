using AoC2024.Interfaces;

namespace AoC2024.Days;

public class Day24 : IDay
{
    private readonly string[] _lines;
    private readonly List<(string, bool)> _gates = new List<(string, bool)>();

    public Day24()
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Inputs", "test.txt");
        _lines = File.ReadAllLines(filePath);
        var emptyLineIndex = Array.IndexOf(_lines, string.Empty);
        for (int i = 0; i < emptyLineIndex; i++)
        {
            var parts = _lines[i].Split(':');
            string gate = parts[0].Trim();
            bool isOn = int.Parse(parts[1].Trim()) != 0;
            _gates.Add((gate, isOn));
        }

        List<int> linesToProcessAgain = new List<int>();
        for (int i = emptyLineIndex + 1; i < _lines.Length; i++)
        {
            linesToProcessAgain.Add(i);
        }

        while (linesToProcessAgain.Count > 0)
        {
            List<int> NewLinesToProcessAgain = new List<int>();
            foreach (var lineIndex in linesToProcessAgain)
            {
                var parts = _lines[lineIndex].Split(' ');
                if (!_gates.Any(x => x.Item1 == parts[0]) || !_gates.Any(x => x.Item1 == parts[2]))
                {
                    NewLinesToProcessAgain.Add(lineIndex);
                    continue;
                }
                bool gate1 = _gates.First(x => x.Item1 == parts[0]).Item2;
                bool gate2 = _gates.First(x => x.Item1 == parts[2]).Item2;
                string operation = parts[1];
                bool result = operation switch
                {
                    "AND" => gate1 && gate2,
                    "OR" => gate1 || gate2,
                    "XOR" => gate1 ^ gate2,
                    _ => throw new NotImplementedException()
                };
                _gates.Add((parts[4], result));
            }
            linesToProcessAgain = NewLinesToProcessAgain;
        }
    }

    public void SolvePart1()
    {
        long part1Result = 0;

        var zGates = _gates.Where(x => x.Item1.StartsWith("z")).OrderByDescending(x => x.Item1);
        string byteStr = "";
        foreach (var zGate in zGates)
        {
            byteStr += zGate.Item2 switch
            {
                true => "1",
                false => "0"
            };
        }
        part1Result = Convert.ToInt64(byteStr, 2);
        Console.WriteLine($"Part 1 solution is: {part1Result}");

    }

    public void SolvePart2()
    {
        int part2Result = 0;
        var xGates = _gates.Where(x => x.Item1.StartsWith("x")).OrderByDescending(x => x.Item1);
        var yGates = _gates.Where(x => x.Item1.StartsWith("y")).OrderByDescending(x => x.Item1);
        var zGates = _gates.Where(x => x.Item1.StartsWith("z")).OrderByDescending(x => x.Item1);
        string xByteStr = "";
        foreach (var xGate in xGates)
        {
            xByteStr += xGate.Item2 switch
            {
                true => "1",
                false => "0"
            };
        }
        string yByteStr = "";
        foreach (var yGate in yGates)
        {
            yByteStr += yGate.Item2 switch
            {
                true => "1",
                false => "0"
            };
        }
        string zByteStr = "";
        foreach (var zGate in zGates)
        {
            zByteStr += zGate.Item2 switch
            {
                true => "1",
                false => "0"
            };
        }
        string expectedResult = "";
        for (int i = 0; i < xByteStr.Length; i++)
        {
            expectedResult += xByteStr[i] == '1' && yByteStr[i] == '1' ? "1" : "0";
        }
        Console.WriteLine($"{expectedResult}");
        Console.WriteLine($"{zByteStr}");

        // compare expected result and zbytestr and check indexes where they differ
        for (int i = 0; i < expectedResult.Length; i++)
        {
            if (expectedResult[i] != zByteStr[i])
            {
                Console.WriteLine($"Index: {i} Expected: {expectedResult[i]} Actual: {zByteStr[i]}");
            }
        }








        Console.WriteLine($"Part 2 solution is: {part2Result} ");
    }

}