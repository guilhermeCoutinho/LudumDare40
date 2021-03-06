﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : SingletonMonoBehaviour<LevelLoader>   {

    public int borderAroundLevel = 2;
    const string FILE_NAME = "Level ";

    string MapName = "level5";

    public Transform contentParent;
    public int floorId;

    public int playerId;
    public GameObject playerPrefab;

	public int wallId;

	public int keyId;
    public GameObject keyPrefab;

    public int boxId;
    public GameObject boxPrefab;

    public int doorId;
    public GameObject doorPrefab;
    
    public int pressurePlateId;
    public GameObject pressurePlatePrefab;

    public int victoryPlateId;
    public GameObject victoryPlatePrefab;

    public ObjectPool pool;
    public ObjectPool wallPool;
    public ObjectPool keyBoardOverlay;

    public Sprite borderSprite;

    Level model;
    public Level LoadedLevel
    {
        get { return model; }
    }
    public void LoadMap (LevelData levelData )
    {
        destroyContent ();
        this.MapName = levelData.name;
        LoadMap();
        LevelMetaData metadata = gameObject.AddComponent(typeof(LevelMetaData)  ) as LevelMetaData;
        MDParser.Parse(levelData.name, ref metadata);
        metadata.LoadMetaData();
        GetComponent<EnemySpawner>().SpawnEnemies(levelData.name,model.Height ,model.Width);
        model.levelInfo = levelData;
        DisplayOverlays ();
    }

    void DisplayOverlays () {
        if (!model.levelInfo.showOnlyAdjacentToPlayerKeys){
            for (int i=0;i<model.Height;i++){
                for (int j=0;j<model.Width;j++){
                    if (model.Grid[i,j] == floorId){
                        GameObject go = keyBoardOverlay.getObject();
                        go.transform.position = getWorldPosition(i,j,model.Height,model.Width,-2);
                        go.GetComponent<KeyboardKey>().UpdateText(new Vector2Int(i-1,j-1),
                        model.levelInfo.shouldHide );
                    }
                }
            }
        }
        else {
            Vector2Int[] adjacentTiles = {Vector2Int.up,Vector2Int.down,
                Vector2Int.right,Vector2Int.left};
            for (int i = 0 ; i <adjacentTiles.Length;i++){
                GameObject go = keyBoardOverlay.getObject();
                int x = model.player.currentPosition.x + adjacentTiles[i].x;
                int y = model.player.currentPosition.y + adjacentTiles[i].y;
                go.transform.position = getWorldPosition(x, y, model.Height, model.Width, -2);
                go.GetComponent<KeyboardKey>().UpdateText(new Vector2Int(x - 1, y - 1),
                model.levelInfo.shouldHide);
            }
        }
    }

    void LoadMap ()
    {
        int colCount= 0;
        List<int> data;
        data = CSVParser.ParseCSV ( MapName, out colCount);
        int rowCount = (data.Count / colCount ) ;
        model = new Level(rowCount , colCount);
        int rowCountWithBorder = rowCount + borderAroundLevel * 2;
        int colCountWithBorder = colCount + borderAroundLevel * 2;
        for (int i = 0; i < rowCountWithBorder; i++)
        {
            for (int j = 0; j < colCountWithBorder ; j++)
            {
                int actualI = i - borderAroundLevel;
                int actualJ = j - borderAroundLevel;
                if (actualI >= 0 && actualJ >= 0 
                    && actualI < rowCount && actualJ < colCount) {
                    int value = data[actualI * colCount + actualJ];
                    model.setCell(actualI, actualJ, value);
                    GameObject go = pool.getObject();
                    go.transform.position = getWorldPosition(actualI, actualJ, rowCount, colCount, 0);
                }else {
                    GameObject wallGO = wallPool.getObject();
                    wallGO.transform.position = getWorldPosition(i, j, rowCountWithBorder,
                     colCountWithBorder, -2);
                     wallGO.GetComponentInChildren<SpriteRenderer>().sprite =borderSprite;
                }
			}
		}
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                int value = model.Grid[i,j];
                if (value == playerId)
                {
                    InstantiatePlayer(new Vector2Int(i, j), colCount, rowCount);
                }
                if (value == wallId)
                    InstantiateWall(new Vector2Int(i, j), colCount, rowCount);
                if (value == keyId)
                    InstantiateKey(new Vector2Int(i, j), colCount, rowCount);
                if (value == boxId)
                    InstantiateBox(new Vector2Int(i, j), colCount, rowCount);
                if (value == doorId)
                    InstantiateDoor(new Vector2Int(i, j), colCount, rowCount);
                if (value == pressurePlateId)
                    InstantiatePressurePlate(new Vector2Int(i, j), colCount, rowCount);
                if (value == victoryPlateId)
                    InstantiateVictoryPlate(new Vector2Int(i, j), colCount, rowCount);
            }
        }
    }

    public Vector3 getWorldPosition(int i, int j, int rowCount, int colCount, int z){
       return new Vector3( j - (colCount-1)/2f , (-1)*(i - (rowCount-1)/2f) , z);
    }

    void InstantiatePlayer (Vector2Int position , int colCount , int rowCount) {
        GameObject playerGO = Instantiate(playerPrefab,
                getWorldPosition(position.x, position.y, rowCount, colCount, -3),
                                    playerPrefab.transform.rotation,contentParent);
        Player playerComponent = playerGO.GetComponent<Player>();
		playerComponent.Initialize(new Vector2Int(position.x, position.y), floorId);
        LoadedLevel.setPlayer(playerComponent);
    }

	void InstantiateWall(Vector2Int position, int colCount, int rowCount) {
		GameObject wallGO = wallPool.getObject();
        wallGO.transform.position = getWorldPosition(position.x, position.y, rowCount, colCount, -2);
        Wall wallcomponent = wallGO.GetComponent<Wall>();
        wallcomponent.Initialize(
            new Vector2Int(position.x,position.y));
	}

	void InstantiateKey(Vector2Int position, int colCount, int rowCount) {
		GameObject keyGO = Instantiate(keyPrefab,
                getWorldPosition(position.x, position.y, rowCount, colCount, -2),
				keyPrefab.transform.rotation,contentParent);
        Key keyComponent = keyGO.GetComponent<Key>();
        keyComponent.Initialize(position);
	}

    void InstantiateBox(Vector2Int position, int colCount, int rowCount)
    {
        GameObject boxGO = Instantiate(boxPrefab,
                getWorldPosition(position.x, position.y, rowCount, colCount, -3),
                boxPrefab.transform.rotation,contentParent);
        Box boxComponnent = boxGO.GetComponent<Box>();
        boxComponnent.Initialize(position);
    }
    
    void InstantiateDoor(Vector2Int position, int colCount, int rowCount)
    {
        GameObject doorGO = Instantiate(doorPrefab,
                getWorldPosition(position.x, position.y, rowCount, colCount, -2),
                doorPrefab.transform.rotation,contentParent);
        Door doorComponnent = doorGO.GetComponent<Door>();
        doorComponnent.Initialize(position);
    }

    void InstantiatePressurePlate(Vector2Int position, int colCount, int rowCount)
    {
        GameObject pressurePlateGO = Instantiate(pressurePlatePrefab,
                getWorldPosition(position.x, position.y, rowCount, colCount, -1),
                pressurePlatePrefab.transform.rotation,contentParent);
        PressurePlate pressureComponent = pressurePlateGO.GetComponent<PressurePlate>();
        pressureComponent.Initialize(position);
    }

    void InstantiateVictoryPlate(Vector2Int position, int colCount, int rowCount)
    {
       Instantiate(victoryPlatePrefab,
                getWorldPosition(position.x, position.y, rowCount, colCount, -1),
                victoryPlatePrefab.transform.rotation, contentParent);
    }

    public void destroyContent () {
        pool.returnAllObjects ();
        wallPool.returnAllObjects () ;
        keyBoardOverlay.returnAllObjects();
        foreach ( Transform t  in contentParent)
        {
            Destroy (t.gameObject);
        }
        Box.ClearList();
        Key.ClearList();
        Door.ClearList ();
        PressurePlate.ClearList ();
        LevelMetaData toDestroy = GetComponent<LevelMetaData> ();
        if (toDestroy != null)
            DestroyImmediate(toDestroy);
    }
}