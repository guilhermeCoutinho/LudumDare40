using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {
	public int id;
    public static List<Key> keyList;
	public Vector2Int currentPosition;

	public static void ClearList()
    {
        if (keyList == null)
            keyList = new List<Key>();
        keyList.Clear();
    }
	public static Key getKeyByPosition(Vector2Int position) {
		for (int i=0;i<keyList.Count;i++){
			if (keyList[i].currentPosition.isEqual(position) )
				return keyList[i];
		}
		return null;
	}

	public void Initialize(Vector2Int position){
		if(keyList==null){
			keyList = new List<Key>();
		}
		keyList.Add(this);
		currentPosition = position;
	}
}
