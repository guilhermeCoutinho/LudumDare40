using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager> {
	public string[] levelSequences;
	private LevelLoader levelLoader;

	int playerLifes;
	int currentLevel ;
	int mapIndex;
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
		mapIndex = 0;
		levelLoader.LoadMap(
			levelSequences[currentLevel] + "_" + mapIndex);
	}

	public void OpenNextLevel () {
		currentLevel ++ ;
		OpenLevel();
	}

	public void GoUpMap() {
		mapIndex ++;
		levelLoader.LoadMap(
			levelSequences[currentLevel] + "_" + mapIndex);
	}

	public void GoDownMap () {
		mapIndex--;
		levelLoader.LoadMap(
			levelSequences[currentLevel] + "_" + mapIndex);
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
