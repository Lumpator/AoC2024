using AoC2024.Interfaces;

namespace AoC2024.Days;

public class Day17 : IDay
{
    private readonly string[] _lines;
    private readonly Computer _computer1;
    private readonly Computer _computer2;
    public Day17()
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Inputs", "inputDay17.txt");
        _lines = File.ReadAllLines(filePath);
        int registerA = int.Parse(_lines[0].Split(':')[1].Trim());
        int registerB = int.Parse(_lines[1].Split(':')[1].Trim());
        int registerC = int.Parse(_lines[2].Split(':')[1].Trim());

        string[] programParts = _lines[4].Split(':')[1].Trim().Split(',');
        int[] instructions = Array.ConvertAll(programParts, int.Parse);

        _computer1 = new Computer(registerA, registerB, registerC, instructions);
        _computer2 = new Computer(registerA, registerB, registerC, instructions);
    }

    public void SolvePart1()
    {
        _computer1.Work();
        Console.WriteLine($"Part 1 solution is: {_computer1.output}");
    }

    public void SolvePart2()
    {
        long part2Result = 0;

        // keep increasing registerA until the output is the same as instructions
        while (true)
        {
            //make a copy of _computer2
            var computer = _computer2.Clone();
            computer.registerA += part2Result;
            string output = computer.Work();
            if (output == _lines[4].Split(':')[1].Trim())
            {
                part2Result += _computer2.registerA;
                break;
            }
            part2Result++;
            if (part2Result % 1000000 == 0)
            {
                Console.WriteLine(part2Result);
            }
        }
        Console.WriteLine(part2Result);

    }

}

public class Computer
{
    public long registerA { get; set; } = 0;
    int registerB = 0;
    int registerC = 0;

    int pointer;
    int[] instructions;

    public string output;

    public Computer(long registerA, int registerB, int registerC, int[] instructions)
    {
        this.registerA = registerA;
        this.registerB = registerB;
        this.registerC = registerC;
        this.instructions = instructions;
        this.pointer = 0;
        this.output = "";
    }
    public Computer Clone()
    {
        return new Computer(this.registerA, this.registerB, this.registerC, this.instructions);
    }

    public void Instruction0(int operand)
    {
        registerA = (int)(registerA / Math.Pow(2, ComboValue(operand)));
    }

    public void Instruction1(int operand)
    {
        registerB = operand ^ registerB;
    }
    public void Instruction2(int operand)
    {
        registerB = ComboValue(operand) % 8;
    }
    public void Instruction3(int operand)
    {
        if (registerA != 0)
        {
            pointer = operand - 2;
        }
    }
    public void Instruction4(int operand)
    {
        registerB = registerB ^ registerC;
    }
    public string Instruction5(int operand)
    {
        return (ComboValue(operand) % 8).ToString() + ",";
    }
    public void Instruction6(int operand)
    {
        registerB = (int)(registerA / Math.Pow(2, ComboValue(operand)));
    }
    public void Instruction7(int operand)
    {
        registerC = (int)(registerA / Math.Pow(2, ComboValue(operand)));
    }

    public int ComboValue(int operand)
    {
        switch (operand)
        {
            case 1:
            case 2:
            case 3:
                return operand;
            case 4:
                return (int)registerA;
            case 5:
                return registerB;
            case 6:
                return registerC;
            default:
                return 0;
        }
    }
    public string Work()
    {
        while (pointer < instructions.Length)
        {
            int operand = instructions[pointer + 1];
            switch (instructions[pointer])
            {
                case 0:
                    Instruction0(operand);
                    break;
                case 1:
                    Instruction1(operand);
                    break;
                case 2:
                    Instruction2(operand);
                    break;
                case 3:
                    Instruction3(operand);
                    break;
                case 4:
                    Instruction4(operand);
                    break;
                case 5:
                    output += Instruction5(operand);
                    break;
                case 6:
                    Instruction6(operand);
                    break;
                case 7:
                    Instruction7(operand);
                    break;
            }
            pointer += 2;
        }

        // remove last comma from string
        output = output.Remove(output.Length - 1);
        return output;
    }

}