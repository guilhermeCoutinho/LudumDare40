using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;

public static class MDParser {
    public static string path = "Build/LevelData/";
    public static void Parse(string fileName, ref LevelMetaData data) {
        fileName = fileName + "_md.txt";
        try
        {
            string line;
            StreamReader streamReader = new StreamReader(
                path + fileName, Encoding.Default);
            using (streamReader)
            {   
                int doorIndex=0;
                int id=0;
                data.requirements = new List<RequirementsMetaData>();
                do
                {
                    line = streamReader.ReadLine();
                    if (line != null)
                    {
                        if(line.Split(' ').Length==2){
                            int x = int.Parse(line.Split(' ')[0]);
                            int y = int.Parse(line.Split(' ')[1]);
                            data.requirements.Add(new RequirementsMetaData());
                            data.requirements[doorIndex].doorToOpen=new Vector2(x,y);
                            data.requirements[doorIndex].requirements=new List<RequirementMetaData>();
                            doorIndex++;
                            id =0;
                        }else if(line.Split(' ').Length==3){
                            int a = int.Parse(line.Split(' ')[0]);
                            int b = int.Parse(line.Split(' ')[1]);
                            int c = int.Parse(line.Split(' ')[2]);
                            RequirementsMetaData temp = data.requirements[doorIndex-1];
                            temp.requirements.Add(new RequirementMetaData());
                            temp.requirements[id].positionInGrid=new Vector2(a,b);
                            temp.requirements[id].type = c;
                            temp.requirements[id].id = id;
                            id++;
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
            return;
        }
        //setupGrid(data, colCount);
    }


}