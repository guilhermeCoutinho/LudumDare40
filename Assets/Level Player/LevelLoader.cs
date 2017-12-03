using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : SingletonMonoBehaviour<LevelLoader>   {

    const string FILE_NAME = "Level ";
    //public int level;
    public string levelName ;

    public int floorId;

    public int playerId;
    public GameObject playerPrefab;

	public int wallId;
    public GameObject wallPrefab;

	public int keyId;
    public GameObject keyPrefab;

    public int boxId;
    public GameObject boxPrefab;

    public int doorId;
    public GameObject doorPrefab;
    
    public int pressurePlateId;
    public GameObject pressurePlatePrefab;

    public ObjectPool pool;

    Level model;
    public Level LoadedLevel
    {
        get { return model; }
    }
    void Start ()
    {
        LoadLevel();
        LevelMetaData metadata = gameObject.AddComponent(typeof(LevelMetaData)  ) as LevelMetaData;
        MDParser.Parse(levelName, ref metadata);
        metadata.LoadMetaData();
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
                go.transform.position = getWorldPosition(i, j, rowCount, colCount, 0);
                if (value == playerId){
                    InstantiatePlayer (new Vector2Int(i,j),colCount,rowCount);
                }
				if (value == wallId)
					InstantiateWall (new Vector2Int(i, j), colCount, rowCount);
				if (value == keyId) 
					InstantiateKey(new Vector2Int(i, j), colCount, rowCount);
                if (value == boxId)
                    InstantiateBox(new Vector2Int(i, j), colCount, rowCount);
                if (value == doorId)
                    InstantiateDoor(new Vector2Int(i, j), colCount, rowCount);
                    if (value == pressurePlateId)
                    InstantiatePressurePlate (new Vector2Int(i, j), colCount, rowCount);
			}
		}
        Camera.main.orthographicSize=rowCount*4/5;
    }

    Vector3 getWorldPosition(int i, int j, int rowCount, int colCount, int z){
       return new Vector3( j - (colCount-1)/2f , (-1)*(i - (rowCount-1)/2f) , z);
    }

    void InstantiatePlayer (Vector2Int position , int colCount , int rowCount) {
        GameObject playerGO = Instantiate(playerPrefab,
                getWorldPosition(position.x, position.y, rowCount, colCount, -3),
                                    playerPrefab.transform.rotation);
        Player playerComponent = playerGO.GetComponent<Player>();
		playerComponent.Initialize(new Vector2Int(position.x, position.y), floorId);
        LoadedLevel.setPlayer(playerComponent);
    }

	void InstantiateWall(Vector2Int position, int colCount, int rowCount) {
		GameObject wallGO = Instantiate(wallPrefab,
                getWorldPosition(position.x, position.y, rowCount, colCount, -2),
				wallPrefab.transform.rotation);
	}

	void InstantiateKey(Vector2Int position, int colCount, int rowCount) {
		GameObject keyGO = Instantiate(keyPrefab,
                getWorldPosition(position.x, position.y, rowCount, colCount, -2),
				keyPrefab.transform.rotation);
        Key keyComponent = keyGO.GetComponent<Key>();
        keyComponent.Initialize(position);
	}

    void InstantiateBox(Vector2Int position, int colCount, int rowCount)
    {
        GameObject boxGO = Instantiate(boxPrefab,
                getWorldPosition(position.x, position.y, rowCount, colCount, -3),
                boxPrefab.transform.rotation);
        Box boxComponnent = boxGO.GetComponent<Box>();
        boxComponnent.Initialize(position);
    }
    
    void InstantiateDoor(Vector2Int position, int colCount, int rowCount)
    {
        GameObject doorGO = Instantiate(doorPrefab,
                getWorldPosition(position.x, position.y, rowCount, colCount, -2),
                doorPrefab.transform.rotation);
        Door doorComponnent = doorGO.GetComponent<Door>();
        doorComponnent.Initialize(position);
    }

    void InstantiatePressurePlate(Vector2Int position, int colCount, int rowCount)
    {
        GameObject pressurePlateGO = Instantiate(pressurePlatePrefab,
                getWorldPosition(position.x, position.y, rowCount, colCount, -1),
                pressurePlatePrefab.transform.rotation);
        PressurePlate pressureComponent = pressurePlateGO.GetComponent<PressurePlate>();
        pressureComponent.Initialize(position);
    }

}