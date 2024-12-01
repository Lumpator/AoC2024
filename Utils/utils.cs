namespace AoC2024.Utils;

public class Utilities
{
    public static string[] ReadLinesFromInputFIle(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        return lines;
    }
}