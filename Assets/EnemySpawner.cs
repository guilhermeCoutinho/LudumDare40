using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using Assets;

public class EnemySpawner : MonoBehaviour
{

    public static string path = "LevelData/";

    public Transform contentParent;
	public GameObject enemyPrefab;
	int rowCount , colCount;

	public void SpawnEnemies (string fileName , int rowCount,int colCount) {
		this.rowCount = rowCount;
		this.colCount = colCount;
		Parse (fileName);
	}

    void Parse(string fileName)
    {

        fileName = fileName + "_es";

        try
        {

            List<string> textLines
                = ParseUtils.TextAssetToList(Resources.Load(path + fileName) as TextAsset);

            foreach (string line in textLines)
            {
                if (line != null)
                {
                    string[] inp = line.Split(' ');
                    Vector2Int enemyOrigin = new Vector2Int(
                        int.Parse(inp[0]), int.Parse(inp[1])
                    );
                    Vector2Int enemyDestiny = new Vector2Int(
                        int.Parse(inp[2]), int.Parse(inp[3])
                    );
                    GameObject clone = Instantiate(enemyPrefab,
                    LevelLoader.Instance.getWorldPosition(
                        enemyOrigin.x, enemyOrigin.y, rowCount, colCount, -4),
                    enemyPrefab.transform.rotation,
                    contentParent);
                    clone.GetComponent<Enemy>().Initialize(enemyOrigin, enemyDestiny, enemyOrigin);
                }

            }
                
        }
        catch (Exception e)
        {
            Debug.Log("Unable to Load file " + fileName + "\n"
                + e.Message);
            return;
        }
    }
}
