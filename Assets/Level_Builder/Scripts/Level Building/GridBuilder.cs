using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using System.Text;

public class GridBuilder : SingletonMonoBehaviour<GridBuilder> {
	public CellElement[] cellTypeColor;
	public Text fileText;
    ObjectPool pool;
	int[,] grid;

    void Start() {
        pool = GetComponent<ObjectPool>();
		resetGrid ();
    }

	public void changeElement (int i , int j, int value ){
		grid [i, j] = value;
	}

	public string parseGrid2CSV () {
		string output = "";
		for (int i = 0; i < grid.GetLength(0); i++) {
			for (int j = 0; j < grid.GetLength(1); j++){
				output += grid [i, j] + ",";
			}
			output = output.Remove (output.Length - 1);
			output += "\n";
		}
		if (output.Length > 1)
			output = output.Remove (output.Length - 1);
		return output;
	}
	public void resetGrid () {
		pool.returnAllObjects ();
		grid = new int[0, 0];
		grid = ResizeArray (grid, 1, 1);
	}

    public void changeRows(int size) {
		if (grid.GetLength (0) + size <= 0)
			return;
		grid = ResizeArray (grid, grid.GetLength (0) + size, grid.GetLength (1));
		CustomLogger.Instance.Log ( "(" + grid.GetLength(0) + "," + grid.GetLength(1) + ")");
    }
	public void changeColumns(int size) {
		if (grid.GetLength (1) + size <= 0)
			return;
		grid = ResizeArray (grid, grid.GetLength (0), grid.GetLength (1) + size);
		CustomLogger.Instance.Log ( "(" + grid.GetLength(0) + "," + grid.GetLength(1) + ")");
	}

	int [,] ResizeArray (int[,] original ,int x , int y){
		pool.returnAllObjects ();
		int [,] newArray = new int[x,y];
		int minRows = Math.Min (original.GetLength (0), x);
		int minCols = Math.Min (original.GetLength (1), y);
		for (int i = 0; i < x; i++) {
			for (int j = 0; j < y; j++) {
				if (i < minRows && j < minCols) {
					newArray [i, j] = original [i, j];
					InstantiateElement (i, j, original [i, j]);
				} else {
					newArray [i, j] = 0;
					InstantiateElement (i, j, 0);
				}
			}
		}
		return newArray;
	}

	void InstantiateElement (int i, int j , int value){
		GameObject quad = pool.getObject ();
		Vector3 position = new Vector3 (j, -i, 0);
		quad.transform.position = position;
		quad.GetComponent<GridObject> ().setIndexes (i, j,value);
	}

	public void SaveToCSV () {
		if (fileText == null)
			return;
		string saveThis = parseGrid2CSV();
		string fileName = fileText.text;
		string path = "LevelData/";
		File.WriteAllText (path + fileName + ".csv", saveThis);
		CustomLogger.Instance.Log ("Success on saving file " + fileName);
	}

	public void OpenFile (string fileName) {
        pool.returnAllObjects();
        List<int> data;
        int colCount =0;
        data = CSVParser.ParseCSV(fileName, out colCount);

        setupGrid (data, colCount);
	}

	void setupGrid (List<int> array, int colCount) {
		if (colCount == 0)
			return;
		grid = new int[array.Count/ colCount,colCount];
		for (int i = 0; i < grid.GetLength (0); i++) {
			for (int j = 0; j < grid.GetLength (1); j++) {
				grid [i, j] = array [i * colCount + j];
				InstantiateElement (i, j, grid[i,j] );
			}
		}
	}
		
}
[System.Serializable]
public class CellElement {
	public enum Type {
		EMPTY ,
		INITIAL_POSITION ,
		FLOOR,
		END_POSITION ,
		ENEMY_POSITION
	}

	public Type type ;
	public Color color ;
}