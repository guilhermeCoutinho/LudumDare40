using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;

public static class CSVParser {
    public static string path = "LevelData/";
    public static List<int> ParseCSV(string fileName,
        out int colCount) {
        List<int> data = new List<int>();
        colCount = 0;

        try
        {
            string line;
            StreamReader streamReader = new StreamReader(
                path + fileName, Encoding.Default);
            using (streamReader)
            {
                do
                {
                    line = streamReader.ReadLine();
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
                } while (line != null);
                streamReader.Close();
                
            }
        }
        catch (Exception e)
        {
            Debug.Log("Unable to open file\n" 
                + e.Message);
            return null;
        }
        //setupGrid(data, colCount);
        return data;
    }


}
