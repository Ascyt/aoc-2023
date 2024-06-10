using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Numerics;

class Programm
{
    // Activate examples 
    static bool activeExample = true;

    // Write your solution here. Return any type you want. 
    public static object? GetSolution(string[] a, string[][][] parsedData3Dim, string[] parsedDataReplcaChar) // You don't have to use everything 
    {
        Array.Resize(ref a, a.Length - 1);

        int dirs = 4;
        int[] dRow = { -1, 0, +1, 0 };
        int[] dCol = { 0, +1, 0, -1 };
        char[] dName = { '^', '>', 'v', '<' };

        int rows = a.Length;
        int cols = a[0].Length;

        var special = new Dictionary<(int, int), int>();
        var specialList = new List<(int, int)>();
        int n = 0;

        void addToList(int row, int col)
        {
            var cur = (row, col);
            specialList.Add(cur);
            special[cur] = n++;
        }

        addToList(0, 1);
        addToList(rows - 1, cols - 2);
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (a[row][col] == '#')
                    continue;
                int num = 0;
                for (int dir = 0; dir < dirs; dir++)
                {
                    int nRow = row + dRow[dir];
                    int nCol = col + dCol[dir];
                    if (0 <= nRow && nRow < rows &&
                        0 <= nCol && nCol < cols &&
                        dName.Contains(a[nRow][nCol]))
                        num += 1;
                }
                if (num > 1)
                    addToList(row, col);
            }
        }

        var adj = new List<(int, int)>[n];
        for (int i = 0; i < n; i++)
        {
            adj[i] = new List<(int, int)>();
            var d = new int[rows, cols];
            var q = new Queue<(int, int)>();

            void add(int row, int col, int dist)
            {
                d[row, col] = dist;
                q.Enqueue((row, col));
            }

            add(specialList[i].Item1, specialList[i].Item2, 1);
            while (q.Count > 0)
            {
                var cur = q.Dequeue();
                int dist = d[cur.Item1, cur.Item2];
                if (special.ContainsKey(cur) && special[cur] != i)
                {
                    adj[i].Add((special[cur], dist - 1));
                    continue;
                }
                for (int dir = 0; dir < dirs; dir++)
                {
                    int nRow = cur.Item1 + dRow[dir];
                    int nCol = cur.Item2 + dCol[dir];
                    if (0 <= nRow && nRow < rows &&
                        0 <= nCol && nCol < cols &&
                        a[nRow][nCol] != '#' &&
                        d[nRow, nCol] == 0)
                        add(nRow, nCol, dist + 1);
                }
            }
        }

        int res = 0;
        var used = new bool[n];

        void recur(int u, int d)
        {
            if (u == 1)
            {
                res = Math.Max(res, d);
                return;
            }
            used[u] = true;
            foreach (var e in adj[u])
                if (!used[e.Item1])
                    recur(e.Item1, d + e.Item2);
            used[u] = false;
        }

        recur(0, 0);
        return res;
    }

    [STAThread]
    static void Main(string[] args)
    {
        // Pfad zur Textdatei
        string filePath = @"..\..\..\..\..\files\input.txt";

        // Pfad zum .json file
        string jsonFilePath = @"..\..\..\..\..\files\examples.json";
        string jsonContent = File.ReadAllText(jsonFilePath);

        // Optional: Definieren Sie ein Trennzeichen
        string delimiter = " "; // Beispiel für ein Komma als Trennzeichen

        // Den Parser aufrufen und das Ergebnis erhalten
        string[] lines = activeExample ? GetFirstStringElement(jsonContent).Split('\n') : File.ReadAllLines(filePath);
        string[][][] parsedData3Dim = TextFileParser.ParseFile(filePath, delimiter);
        string[] parsedDataReplcaChar = ReplaceCharacterParser.Parser(filePath);
        //int[,,] parsedDataBinary = ByteParser.ParseInputFile(filePath, 1000, 1000);

        // Die Lösung berechnen
        object? solution = GetSolution(lines, parsedData3Dim, parsedDataReplcaChar);

        if (solution is null)
        {
            Console.WriteLine("{null}");
            return;
        }

        // Die Lösung ausgeben
        Console.WriteLine(solution);

        // Die Lösung in die Zwischenablage kopieren
        Clipboard.SetText(solution.ToString());
        Console.WriteLine("Copied solution to clipboard.");
    }

    public static string GetFirstStringElement(string jsonContent)
    {
        JsonDocument jsonDocument = JsonDocument.Parse(jsonContent);
        JsonElement rootElement = jsonDocument.RootElement;

        if (rootElement.ValueKind == JsonValueKind.Array)
        {
            JsonElement firstElement = rootElement.EnumerateArray().FirstOrDefault();

            if (firstElement.ValueKind == JsonValueKind.String)
            {
                return firstElement.GetString();
            }
            else
            {
                Console.WriteLine("first element is not a string.");
            }
        }
        else
        {
            Console.WriteLine("JSON parsing failed");
        }

        return "";
    }
}

public static class Tools {

    public static void QuickSort(long[] arr, long low, long high)
    {
        if (low < high)
        {
            long partitionIndex = Partition(arr, low, high);

            QuickSort(arr, low, partitionIndex - 1);
            QuickSort(arr, partitionIndex + 1, high);
        }
    }

    static long Partition(long[] arr, long low, long high)
    {
        long pivot = arr[high];
        long i = low - 1;

        for (long j = low; j < high; j++)
        {
            if (arr[j] < pivot)
            {
                i++;

                // Swap arr[i] and arr[j]
                long temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }

        // Swap arr[i+1] and arr[high] (or pivot)
        long temp1 = arr[i + 1];
        arr[i + 1] = arr[high];
        arr[high] = temp1;

        return i + 1;
    }
}


class TextFileParser
{
    public static string[][][] ParseFile(string filePath, string delimiter)
    {
        string[] lines = File.ReadAllLines(filePath);
        List<string[][]> blocks = new List<string[][]>();
        List<string[]> currentBlock = new List<string[]>();
        int maxBlockLength = 0;
        int maxLineLength = 0;

        // Erste Durchlauf, um die maximale Größe zu bestimmen
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                if (currentBlock.Count > 0)
                {
                    maxBlockLength = Math.Max(maxBlockLength, currentBlock.Count);
                    blocks.Add(currentBlock.ToArray());
                    currentBlock.Clear();
                }
            }
            else
            {
                string[] segments = line.Split(delimiter);
                maxLineLength = Math.Max(maxLineLength, segments.Length);
                currentBlock.Add(segments);
            }
        }
        if (currentBlock.Count > 0)
        {
            maxBlockLength = Math.Max(maxBlockLength, currentBlock.Count);
            blocks.Add(currentBlock.ToArray());
        }

        // Zweiter Durchlauf, um das 3D-Array zu erstellen
        string[][][] result = new string[blocks.Count][][];
        for (int i = 0; i < blocks.Count; i++)
        {
            result[i] = new string[maxBlockLength][];
            for (int j = 0; j < maxBlockLength; j++)
            {
                result[i][j] = new string[maxLineLength];
                if (j < blocks[i].Length)
                {
                    for (int k = 0; k < maxLineLength; k++)
                    {
                        result[i][j][k] = k < blocks[i][j].Length ? blocks[i][j][k] : string.Empty;
                    }
                }
                else
                {
                    for (int k = 0; k < maxLineLength; k++)
                    {
                        result[i][j][k] = string.Empty;
                    }
                }
            }
        }

        return result;
    }

    private static void UpdateMaxLengths(List<string[]> currentBlock, ref int maxBlockLength, ref int maxLineLength)
    {
        maxBlockLength = Math.Max(maxBlockLength, currentBlock.Count);
        foreach (var line in currentBlock)
        {
            foreach (var element in line)
            {
                maxLineLength = Math.Max(maxLineLength, element.Length);
            }
        }
    }

    private static string[][][] ConvertTo3DArray(List<string[][]> blocks, int maxBlockLength, int maxLineLength)
    {
        string[][][] result = new string[blocks.Count][][];
        for (int i = 0; i < blocks.Count; i++)
        {
            result[i] = new string[maxBlockLength][];
            for (int j = 0; j < maxBlockLength; j++)
            {
                result[i][j] = j < blocks[i].Length ? new string[blocks[i][j].Length] : new string[0];
                for (int k = 0; k < result[i][j].Length; k++)
                {
                    if (j < blocks[i].Length && k < blocks[i][j].Length)
                    {
                        result[i][j][k] = blocks[i][j][k];
                    }
                    else
                    {
                        result[i][j][k] = string.Empty; // Oder ein anderer Standardwert
                    }
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

