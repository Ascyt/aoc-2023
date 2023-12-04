using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;

class Programm
{
    // Write your solution here. Return any type you want. 
    public static object GetSolution(string[] lines, string[][][] parsedData3Dim, string[] parsedDataReplcaChar) // You don't have to use everything 
    {
        for (int i = 0; i < parsedData3Dim.Length; i++)
        {
            for (int j = 0; j < parsedData3Dim[i].Length; j++)
            {
                for (int k = 0; k < parsedData3Dim[i][j].Length; k++)
                {
                    Console.WriteLine($"Block {i + 1}, Zeile {j + 1}, Spalte {k + 1}: {parsedData3Dim[i][j][k].Length} {parsedData3Dim[i][j][k]}");
                }
            }
        }

        

        return parsedData3Dim;
    }

    [STAThread]
    static void Main(string[] args)
    {
        // Pfad zur Textdatei
        string filePath = @"..\..\..\..\..\files\input.txt";
        OpenFileInNotepad();

        // Optional: Definieren Sie ein Trennzeichen
        string delimiter = " "; // Beispiel für ein Komma als Trennzeichen

        // Den Parser aufrufen und das Ergebnis erhalten
        string[] lines = File.ReadAllLines(filePath);
        string[][][] parsedData3Dim = TextFileParser.ParseFile(filePath, delimiter);
        string[] parsedDataReplcaChar = ReplaceCharacterParser.Parser(filePath);
        //int[,,] parsedDataBinary = ByteParser.ParseInputFile(filePath, 1000, 1000);

        // Die Lösung berechnen
        object solution = GetSolution(lines, parsedData3Dim, parsedDataReplcaChar);

        // Die Lösung ausgeben
        Console.WriteLine(solution);

        // Die Lösung in die Zwischenablage kopieren
        Clipboard.SetText(solution.ToString());
        Console.WriteLine("Copied solution to clipboard.");
    }

    static void OpenFileInNotepad()
    {
        string filePath = @"..\..\..\..\..\files\input.txt";

        Process[] processes = Process.GetProcessesByName("notepad");

        if (processes.Length > 0)
        {
            return; // Notepad ist schon offen
        }

        // Öffne die Textdatei mit dem Standard-Texteditor
        try
        {
            System.Diagnostics.Process.Start("notepad.exe", filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler beim Öffnen der Datei: {ex.Message}");
        }
    }
}

class TextFileParser
{
    public static string[][][] ParseFile(string filePath, string delimiter)
    {
        string[] lines = File.ReadAllLines(filePath);
        List<string[][]> blocks = new List<string[][]>();
        List<string[]> currentBlock = new List<string[]>();

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                if (currentBlock.Count > 0)
                {
                    blocks.Add(currentBlock.ToArray());
                    currentBlock.Clear();
                }
            }
            else
            {
                if (string.IsNullOrEmpty(delimiter))
                {
                    currentBlock.Add(new string[] { line });
                }
                else
                {
                    currentBlock.Add(line.Split(new string[] { delimiter }, StringSplitOptions.None));
                }
            }
        }

        if (currentBlock.Count > 0)
        {
            blocks.Add(currentBlock.ToArray());
        }

        return ConvertTo3DArray(blocks);
    }

    private static string[][][] ConvertTo3DArray(List<string[][]> blocks)
    {
        string[][][] result = new string[blocks.Count][][];
        for (int i = 0; i < blocks.Count; i++)
        {
            result[i] = new string[blocks[i].Length][];
            for (int j = 0; j < blocks[i].Length; j++)
            {
                result[i][j] = new string[blocks[i][j].Length];
                for (int k = 0; k < blocks[i][j].Length; k++)
                {
                    result[i][j][k] = blocks[i][j][k];
                }
            }
        }

        return result;
    }

    
}


class ReplaceCharacterParser
{
    public static string[] Parser(string path)
    {
        List<string> cleanedLines = new List<string>();

        string[] lines = File.ReadAllLines(path);

        foreach (string line in lines)
        {

            string cleanedLine = Regex.Replace(line, @"[^a-zA-Z0-9\s]", "");


            if (!string.IsNullOrWhiteSpace(cleanedLine))
            {
                cleanedLines.Add(cleanedLine);
            }
        }

        string[] stringArray = cleanedLines.ToArray();

        return stringArray;
    }
 
    
}

/*
class ByteParser
{
    public static int[,,] ParseInputFile(string filePath, int numRows, int numCols)
    {
        int[,,] data = new int[numRows, numCols, 2];

        using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
        {
            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    int value = reader.ReadInt32();
                    data[row, col, 0] = row; // Zeilenindex
                    data[row, col, 1] = col; // Spaltenindex
                }
            }
        }

        return data;
    }
}*/

class DataStructure
{
    private char[] charArray;
    private char[,] twoDimensionalArray;

    public DataStructure(int charArrayLength, int rows, int cols)
    {
        charArray = new char[charArrayLength];
        twoDimensionalArray = new char[rows, cols];
    }
}

