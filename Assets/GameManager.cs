using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager> {
	public string[] levelSequences;
	private LevelLoader levelLoader;


	int playerLifes;
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
		playerLifes = lifes;
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
	
	public void PlayerReachedGoal () {
		OpenNextLevel ();
	}

	public void PlayerDied () {
		playerLifes -- ;
		if (playerLifes == 0){
			print ("AGORA VOCE MORREU MESMO LIXO");
			Time.timeScale = 0;
		}
		OpenLevel ();
	}

}
