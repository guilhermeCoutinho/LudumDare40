using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMapper  {

    static string[] keyboardMapEUA = {
        "1234567890",
        "qwertyuiop",
        "asdfghjkl;",
        "zxcvbnm,./"
    };

	public static Vector2Int getPositionInMap (char c) {
		c = c.ToString().ToLower()[0];
		for (int i=0;i<keyboardMapEUA.Length;i++){
			for (int j=0;j<keyboardMapEUA[i].Length;j++){
				if (c == keyboardMapEUA[i][j]){
					return new Vector2Int (i+1,j+1);
				}
			}
		}
		return null;
	}
    public static char getCharInPosition(Vector2Int positionInMap)
    {
		return keyboardMapEUA[positionInMap.x][positionInMap.y];
    }
}
