using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMapper  {

	static string[] keyboardMap = {
		"1234567890",
		"qwertyuiop",
		"asdfghjkl",
		"zxcvbnm"
	};


	public static Vector2Int getPositionInMap (char c) {
		for (int i=0;i<keyboardMap.Length;i++){
			for (int j=0;j<keyboardMap[i].Length;j++){
				if (c == keyboardMap[i][j]){
					return new Vector2Int (i,j);
				}
			}
		}
		return null;
	}
}
