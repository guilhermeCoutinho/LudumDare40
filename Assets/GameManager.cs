using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager> {
	public string[] levelSequences;
	private LevelLoader levelLoader;

	int playerLifes;
	int currentLevel ;
	bool gameRunning = false;

	void Awake () {
		levelLoader = GetComponent<LevelLoader> ();
	}

	void Start () {
		StartGame (3);
	}

	void Update () {
		if (!gameRunning)
			return;
		if (Input.GetKeyDown(KeyCode.Backspace)){
			PlayerDied ();
		}
		if (Input.GetKeyDown(KeyCode.Return)){
			OpenNextLevel ();
		}
		if (Input.GetKeyDown(KeyCode.LeftShift)){
			GoUpMap ();
		}
		if (Input.GetKeyDown(KeyCode.RightShift)){
			GoDownMap ();
		}
	}

	public void StartGame (int lifes) {
		gameRunning = true;
		playerLifes = lifes;
		currentLevel = 0;
		OpenLevel();
	}

	void OpenLevel () {
		levelLoader.LoadMap(
			levelSequences[currentLevel] );
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
			gameRunning = false;
			levelLoader.destroyContent ();
			Time.timeScale = 0;
			return;
		}
		OpenLevel ();
	}
}
