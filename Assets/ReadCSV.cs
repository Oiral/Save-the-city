using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadCSV : MonoBehaviour{

    public TextAsset csvFile;

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
                outputGrid[x, y] = outputGrid[x, y].Replace("\"\"", "\"");
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

    
}
