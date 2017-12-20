using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using Assets;

public static class MDParser {
#if UNITY_EDITOR
    public static string path = "LevelData/";
#else
     public static string path = "LevelData/";
#endif

    public static void Parse(string fileName, ref LevelMetaData data)
    {

        fileName = fileName + "_md";

        try
        {

            List<string> textLines
                = ParseUtils.TextAssetToList(Resources.Load(path + fileName) as TextAsset);

            int doorIndex=0;
            int id=0;
            int elementIndex=0;
            data.requirements = new List<RequirementsMetaData>();
                
            foreach(string line in textLines)
            {

                if (line != null)
                {
                    if(line.Split(' ').Length==2){
                        int x = int.Parse(line.Split(' ')[0]);
                        int y = int.Parse(line.Split(' ')[1]);
                        data.requirements.Add(new RequirementsMetaData());
                        data.requirements[doorIndex].doorToOpen=new Vector2(x,y);
                        data.requirements[doorIndex].requirements=new List<RequirementMetaData>();
                        doorIndex++;
                        elementIndex =0;
                    }else if(line.Split(' ').Length==3){
                        int a = int.Parse(line.Split(' ')[0]);
                        int b = int.Parse(line.Split(' ')[1]);
                        int c = int.Parse(line.Split(' ')[2]);
                        RequirementsMetaData temp = data.requirements[doorIndex-1];
                        temp.requirements.Add(new RequirementMetaData());
                        temp.requirements[elementIndex].positionInGrid=new Vector2(a,b);
                        temp.requirements[elementIndex].type = c;
                        temp.requirements[elementIndex].id = id;
                        id++;
                        elementIndex++;
                    }
                }
            }
                
            
        }
        catch (Exception e)
        {
            Debug.Log("Unable to Load file " + fileName + "\n"
                + e.Message);
            return;
        }
        //setupGrid(data, colCount);
    }


}
