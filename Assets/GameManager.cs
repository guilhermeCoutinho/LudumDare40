using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager> {
	public enum GameState {
		START_SCREEN ,
		GAME_RUNNING ,
		GAME_OVER
	};
	public GameState gameState;
	Timer timer;
	public GameObject startScreen ;
	public LevelData[] levelSequences;
	public LevelScreen levelScreen ;
	public GameObject gameOverScreen;
	private LevelLoader levelLoader;

	public LifeCounter[] lifeCounters;
	public int playerLifes;
	public int currentLevel;

	void Awake () {
		levelLoader = GetComponent<LevelLoader> ();
		timer = GetComponent<Timer>();
	}

	void Update () {
		if (gameState == GameState.START_SCREEN){
			if (Input.GetKeyDown(KeyCode.Escape)){
				startScreen.SetActive (false);
				StartGame(3);
			}
			return;
		}
		else if (gameState == GameState.GAME_RUNNING){
			if (Input.GetKeyDown(KeyCode.Backspace)){
				Sound.Instance.Play(0, (int)Sound.soundEvents.RESET);
				PlayerDied ();
			}
			if (Input.GetKeyDown(KeyCode.Return)){
				Debug.LogWarning ("REMOVE THIS BEFORE BUILDDDDDDDDDDDDDDDDDDDDDDDD");
				OpenNextLevel ();
			}
		}else if (gameState == GameState.GAME_OVER){
			if (Input.anyKeyDown){
				startScreen.SetActive(true);
				gameOverScreen.SetActive(false);
				gameState = GameState.START_SCREEN;
			}
		}
	}

	public void StartGame (int lifes) {
		gameState = GameState.GAME_RUNNING;
		playerLifes = lifes;
		currentLevel = 0;
		OpenLevel();
        SetupLifeCounters(lifes);
	}

	void OpenLevel () {
		Debug.Log ("OPEN LEVEL CALLED");
		levelLoader.LoadMap(levelSequences[currentLevel] );
		levelScreen.gameObject.SetActive(true);
		StartCoroutine(levelScreen.BlinkScreen());
		timer.StartTimer(levelSequences[currentLevel].timeToComplete , OnTimeRunOunt);
		Sound.Instance.PlayBGM(0);
		Sound.Instance.Play(0, (int)Sound.soundEvents.START);
	}

	void OnTimeRunOunt () {
		PlayerDied();
	}

	public void OpenNextLevel () {
		try{
			currentLevel ++ ;
			OpenLevel();
		}catch(System.IndexOutOfRangeException){
			Debug.LogError("VICTORY");
		}
	}

	public void PlayerReachedGoal () {
		Sound.Instance.Play(0, (int)Sound.soundEvents.FINISH);
		OpenNextLevel ();
	}

	public void PlayerDied () {
		playerLifes -- ;
		if (playerLifes == 0){
			GameOver ();
			return;
		}
		OpenLevel ();
	}

	void GameOver () {
		Sound.Instance.Play(0, (int)Sound.soundEvents.GAMEOVER);
        gameOverScreen.SetActive(true);
        gameState = GameState.GAME_OVER;
		timer.StopTimer ();
        levelLoader.destroyContent();
	}

	void SetupLifeCounters (int n) {
		foreach (LifeCounter LifeCounter in lifeCounters)
		{
			LifeCounter.SetupLifeCounter(n);
		}
	}
}
