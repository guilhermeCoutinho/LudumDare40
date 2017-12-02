﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : SingletonMonoBehaviour<LevelLoader>   {

    const string FILE_NAME = "Level ";
    //public int level;
    public string levelName ;
    public int playerId;
	public int wallId;
	public int floorId;
	public int keyId;
    public int boxId;
    public GameObject playerPrefab;
	public GameObject wallPrefab;
	public GameObject keyPrefab;
    public GameObject boxPrefab;

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
        data = CSVParser.ParseCSV ( levelName + ".csv" , out colCount);

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
                if (value == playerId){
                    InstantiatePlayer (new Vector2Int(i,j),colCount,rowCount);
                }
				if (value == wallId)
					InstantiateWall (new Vector2Int(i, j), colCount, rowCount);
				if (value == keyId) 
					InstantiateKey(new Vector2Int(i, j), colCount, rowCount);
                if (value == boxId)
                    InstantiateBox(new Vector2Int(i, j), colCount, rowCount);
			}
		}
    }

    void InstantiatePlayer (Vector2Int position , int colCount , int rowCount) {
        GameObject playerGO = Instantiate(playerPrefab,
                new Vector3(position.y - (colCount - 1) / 2f, (rowCount - 1 - position.x) - rowCount / 2f, -2),
                playerPrefab.transform.rotation);
        Player playerComponent = playerGO.GetComponent<Player>();
		playerComponent.Initialize(new Vector2Int(position.x, position.y), floorId);
        LoadedLevel.setPlayer(playerComponent);
    }

	void InstantiateWall(Vector2Int position, int colCount, int rowCount) {
		GameObject wallGO = Instantiate(wallPrefab,
				new Vector3(position.y - (colCount - 1) / 2f, (rowCount - 1 - position.x) - rowCount / 2f, -1),
				wallPrefab.transform.rotation);
	}

	void InstantiateKey(Vector2Int position, int colCount, int rowCount) {
		GameObject keyGO = Instantiate(keyPrefab,
				new Vector3(position.y - (colCount - 1) / 2f, (rowCount - 1 - position.x) - rowCount / 2f, -1),
				keyPrefab.transform.rotation);
	}

    void InstantiateBox(Vector2Int position, int colCount, int rowCount)
    {
        GameObject boxGO = Instantiate(boxPrefab,
                new Vector3(position.y - (colCount - 1) / 2f, (rowCount - 1 - position.x) - rowCount / 2f, -2),
                boxPrefab.transform.rotation);
        Box boxComponnent = boxGO.GetComponent<Box>();
        boxComponnent.Initialize(position);
    }

}