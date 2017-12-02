using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {
    public static List<Box> boxList;

	public static Box getBoxByPosition(Vector2Int position) {
		for (int i=0;i<boxList.Count;i++){
			if (boxList[i].currentPosition == position)
				return boxList[i];
		}
		return null;
	}

	public int onTopOf;
	public Vector2Int currentPosition;
}
