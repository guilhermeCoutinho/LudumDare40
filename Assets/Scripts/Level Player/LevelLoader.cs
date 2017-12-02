using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : SingletonMonoBehaviour<LevelLoader>   {

    const string FILE_NAME = "Level ";
    public int level;
    public ObjectPool pool;
    public GameObject playerPrefab;

    LevelData model;
    public LevelData LoadedLevelData
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
        data = CSVParser.ParseCSV(FILE_NAME + level + ".csv", out colCount);

        int rowCount = data.Count / colCount;

        model = new LevelData(rowCount , colCount);
        
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                int value = data[i * colCount + j];
                model.setCell(i, j, value);
                if ( value > 0)
                {
                    GameObject go = pool.getObject();
                    go.transform.name = "Cell_"+ i + "," + j + "_" + value;
                    go.transform.position = new Vector3( j - colCount/2, (rowCount-1- i) -rowCount/2 , 0);
                }
                if (value == (int)CellElement.Type.INITIAL_POSITION)
                {
                    GameObject go = Instantiate(playerPrefab, transform.parent);
                    go.transform.name = "Player";
                    go.transform.position = new Vector3(j - colCount / 2, (rowCount - 1 - i) - rowCount / 2, 0);
                }
            }
        }
        model.printGrid();
    }
    
}