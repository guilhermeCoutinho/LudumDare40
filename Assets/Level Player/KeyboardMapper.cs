using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMapper  {

	static string[] keyboardMapBrazil = {
		"1234567890",
		"qwertyuiop",
		"asdfghjklç",
		"zxcvbnm,.;"
	};

    static string[] keyboardMapEUA = {
        "1234567890",
        "qwertyuiop",
        "asdfghjkl;",
        "zxcvbnm,./"
    };

	public static Vector2Int getPositionInMap (char c) {
		c = c.ToString().ToLower()[0];
		for (int i=0;i<keyboardMapBrazil.Length;i++){
			for (int j=0;j<keyboardMapBrazil[i].Length;j++){
				if (c == keyboardMapBrazil[i][j]){
					return new Vector2Int (i+1,j+1);
				}
			}
		}
		return null;
	}
    public static char getCharInPosition(Vector2Int positionInMap)
    {
		return keyboardMapBrazil[positionInMap.x][positionInMap.y];
    }
}
