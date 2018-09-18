using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadCSV : MonoBehaviour{

    public TextAsset csvFile;

    private void Start()
    {
        string[,] grid = SplitGrid(csvFile.text);
        grid = TrimArray(0, grid, 0);
        DebugArray(grid);

        //The position of each event within the file
        int[] eventNumber = GetCollumnItems(0,grid);
        //Debug.Log(eventNumber[2]);
    }

    static public string[,] SplitGrid(string csvText)
    {
        string[] lines = csvText.Split("\n"[0]);

        // finds the max width of row
        int width = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            string[] row = SplitCsvLine(lines[i]);
            width = Mathf.Max(width, row.Length);
        }

        // creates new 2D string grid to output to
        string[,] outputGrid = new string[width + 1, lines.Length + 1];
        for (int y = 0; y < lines.Length; y++)
        {
            string[] row = SplitCsvLine(lines[y]);
            for (int x = 0; x < row.Length; x++)
            {
                outputGrid[x, y] = row[x];

                // This line was to replace "" with " in my output. 
                // Include or edit it as you wish.
                //outputGrid[x, y] = outputGrid[x, y].Replace("\"\"", "\"");
            }
        }

        return outputGrid;
    }

    static public string[] SplitCsvLine(string line)
    {
        return line.Split(',');
    }

    static public void DebugArray(string[,] array)
    {
        string textOutput = "";
        for (int y = 0; y < array.GetUpperBound(1); y++)
        {
            for (int x = 0; x < array.GetUpperBound(0); x++)
            {

                textOutput += array[x, y];
                textOutput += "|";
            }
            textOutput += "\n";
        }
        Debug.Log(textOutput);
    }

    static public string[,] TrimArray(int rowToRemove, string[,] original, int collumnToRemove)
    {
        string[,] result = new string[original.GetLength(0) - 1, original.GetLength(1) - 1];
        for (int i = 0, r = 0; i < original.GetLength(0); i++)
        {
            if (i == rowToRemove)
            {
                continue;
            }
            for (int j = 0, c = 0; j < original.GetLength(1); j++)
            {
                if (j == collumnToRemove)
                {
                    continue;
                }
                result[r, c] = original[i, j];
                c++;
            }
            r++;
        }
        return result;
    }
    static public string[,] TrimArray(int rowToRemove,string[,] original)
    {
        string[,] result = new string[original.GetLength(0) - 1, original.GetLength(1)];
        for (int i = 0,r = 0; i < original.GetLength(0); i++)
        {
            if (i == rowToRemove)
            {
                continue;
            }
            for (int j = 0; j < original.GetLength(1); j++)
            {
                result[r, j] = original[i, j];
            }
            r++;
        }
        return result;
    }
    static public string[,] TrimArray(string[,] original, int collumnToRemove)
    {
        string[,] result = new string[original.GetLength(0) - 1, original.GetLength(1) - 1];
        for (int i = 0; i < original.GetLength(0); i++)
        {
            for (int j = 0, c = 0; j < original.GetLength(1); j++)
            {
                if (j == collumnToRemove)
                {
                    continue;
                }
                result[i, c] = original[i, j];
                c++;
            }
        }
        return result;
    }

    static public int[] GetCollumnItems(int collumn, string[,] array)
    {
        List<int> TempList = new List<int>();

        for (int i = 0; i < array.GetLength(1) - 1; i++)
        {
            if (array[collumn, i] != "")
            {
                TempList.Add(i);
            }
        }

        return (TempList.ToArray());

    }

    static public int[] GetCollumnItemBetween(string[,] array,int collumn,int rowStart,int rowFinish)
    {
        List<int> TempList = new List<int>();

        //Check if we are check past the end of the array
        if (rowFinish > array.GetLength(1))
        {
            Debug.LogError("Array index out of range to check");
            return null;
        }

        for (int i = rowStart; i < rowFinish; i++)
        {
            if (array[collumn, i] != "")
            {
                TempList.Add(i);
            }
        }
        return (TempList.ToArray());
    }
}

