﻿using System.Collections;
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
			Sound.play(0, (int)Sound.soundEvents.RESET);
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
			levelSequences[currentLevel] );
		Sound.play(2, (int)Sound.soundEvents.BGM);
		Sound.play(1, (int)Sound.soundEvents.START);
	}

	public void OpenNextLevel () {
		currentLevel ++ ;
		OpenLevel();
	}

	public void PlayerReachedGoal () {
		Sound.play(0, (int)Sound.soundEvents.FINISH);
		OpenNextLevel ();
	}

	public void PlayerDied () {
		playerLifes -- ;
		if (playerLifes == 0){
			Sound.play(0, (int)Sound.soundEvents.GAMEOVER);
			gameRunning = false;
			levelLoader.destroyContent ();
			Time.timeScale = 0;
			return;
		}
		OpenLevel ();
	}
}
