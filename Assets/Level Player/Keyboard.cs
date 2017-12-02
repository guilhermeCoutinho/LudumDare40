using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMapper : MonoBehaviour {

	string[] keyboardMap = {
		"1234567890",
		"qwertyuiop",
		"asdfghjkl",
		"zxcvbnm"
	};


	Vector2Int getPositionInMap (char c) {
		for (int i=0;i<keyboardMap.Length;i++){
			for (int j=0;j<keyboardMap[i].Length;j++){
				if (c == keyboardMap[i][j])
					return new Vector2Int (i,j);
			}
		}
		return null;
	}

	void Update () {
		if (Input.anyKeyDown){
			string rawInput = Input.inputString;
			if (rawInput.Length > 0){
				Vector2Int playerInput = getPositionInMap(rawInput[0]);
			}
		}
	}

}
