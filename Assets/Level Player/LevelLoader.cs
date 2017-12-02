using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : SingletonMonoBehaviour<LevelLoader>   {

    const string FILE_NAME = "Level ";
    //public int level;
    public string levelName ;
    public ObjectPool pool;

    Level model;
    public Level LoadedLevel
    {
        get { return model; }
    }
    void Start ()
    {
        LoadLevel();
    }

    void LoadLevel ()
    {
        int colCount= 0;
        List<int> data;
//        data = CSVParser.ParseCSV(FILE_NAME + level + ".csv", out colCount);
        data = CSVParser.ParseCSV (levelName + ".csv" , out colCount);

        int rowCount = data.Count / colCount;

        model = new Level(rowCount , colCount);
        

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                int value = data[i * colCount + j];
                model.setCell(i, j, value);
                GameObject go = pool.getObject();
                go.transform.name = "Cell_"+ i + "," + j + "_" + value;
                go.transform.position = new Vector3( j - (colCount-1)/2f , (rowCount-1- i) - rowCount/2f , 0);
            }
        }
        model.printGrid();
    }
    
}