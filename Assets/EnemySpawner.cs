using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

public class EnemySpawner : MonoBehaviour {
	public static string path = "Build/LevelData/";

	public GameObject enemyPrefab;
	int rowCount , colCount;

	public void SpawnEnemies (string fileName , int rowCount,int colCount) {
		this.rowCount = rowCount;
		this.colCount = colCount;
		Parse (fileName);
	}

    void Parse(string fileName)
    {
        fileName = fileName + "_es.txt";
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
						string[] inp = line.Split(' ');
						Vector2Int enemyOrigin = new Vector2Int (
							int.Parse(inp[0]),int.Parse(inp[1])
						);
                        Vector2Int enemyDestiny =new Vector2Int(
                            int.Parse(inp[2]), int.Parse(inp[3])
                        );
                        GameObject clone = Instantiate(enemyPrefab,
						LevelLoader.Instance.getWorldPosition(
							enemyOrigin.x,enemyOrigin.y,rowCount,colCount, -4),
						enemyPrefab.transform.rotation);
						clone.GetComponent<Enemy>().Initialize(enemyOrigin,enemyDestiny,enemyOrigin);
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
    }
}
