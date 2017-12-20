using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using Assets;

public static class CSVParser {

    public static string path = "LevelData/";

    public static List<int> ParseCSV(string fileName, out int colCount)
    {
        
        List<int> data = new List<int>();

        colCount = 0;

        try
        {

            List<string> textLines
                = ParseUtils.TextAssetToList(Resources.Load(path + fileName) as TextAsset);

            foreach (string line in textLines)
            {

                if (line != null)
                {
                    string[] entries = line.Split(',');

                    colCount = entries.Length;

                    foreach (var f in entries)
                    {
                        int parsedInt = 0;
                        int.TryParse(f, out parsedInt);
                        data.Add(parsedInt);
                    }

                }

            }
              
        }
        catch (Exception e)
        {
            Debug.Log("Unable to Load file " + fileName + "\n" + e.Message);
            return null;
        }
        //setupGrid(data, colCount);
        return data;
    }


}
