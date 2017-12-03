using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager> {
	public LevelMapBinding[] levelSequences;
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
	}

	public void StartGame (int lifes) {
		gameRunning = true;
		playerLifes = lifes;
		currentLevel = 0;
		OpenLevel();
	}

	void OpenLevel () {
		levelLoader.LoadMap(
			levelSequences[currentLevel].levelName + "_" + mapIndex);
	}

	public void OpenNextLevel () {
		currentLevel ++ ;
		OpenLevel();
	}

	public void GoUpMap() {}

	public void GoDownMap () {

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

	[System.Serializable]
	public class LevelMapBinding {
		public string levelName ;
		public int mapCount;
	}
}
