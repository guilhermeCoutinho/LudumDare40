using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager> {
	public string[] levelSequences;
	private LevelLoader levelLoader;

	int currentLevel ;
	void Awake () {
		levelLoader = GetComponent<LevelLoader> ();
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Backspace)){
			OpenLevel();
		}
		if (Input.GetKeyDown(KeyCode.Return)){
			OpenNextLevel ();
		}
	}

	public void StartGame (int lifes) {
		currentLevel = 0;
		OpenLevel();
	}

	void OpenLevel () {
		levelLoader.LoadLevel(levelSequences[currentLevel]);
	}

	public void OpenNextLevel () {
		currentLevel ++ ;
		OpenLevel();
	}

}
