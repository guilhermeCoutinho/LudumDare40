using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager> {
	public string[] levelSequences;
	public LevelScreen levelScreen ;
	private LevelLoader levelLoader;

	public LifeCounter[] lifeCounters;
	public int playerLifes;
	public int currentLevel ;
	public bool gameRunning = false;

	void Awake () {
		levelLoader = GetComponent<LevelLoader> ();
	}

	void Start () {
		StartGame (5);
	}

	void Update () {
		if (!gameRunning)
			return;
		if (Input.GetKeyDown(KeyCode.Backspace)){
			Sound.Instance.Play(0, (int)Sound.soundEvents.RESET);
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
        SetupLifeCounters(lifes);
	}

	void OpenLevel () {
		levelLoader.LoadMap(levelSequences[currentLevel] );
		levelScreen.gameObject.SetActive(true);
		StartCoroutine(levelScreen.BlinkScreen());
		Sound.Instance.PlayBGM(0);
		Sound.Instance.Play(1, (int)Sound.soundEvents.START);
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
			Sound.Instance.Play(0, (int)Sound.soundEvents.GAMEOVER);
			gameRunning = false;
			levelLoader.destroyContent ();
			StartGame(3);
			return;
		}
		OpenLevel ();
	}

	void SetupLifeCounters (int n) {
		foreach (LifeCounter LifeCounter in lifeCounters)
		{
			LifeCounter.SetupLifeCounter(n);
		}
	}
}
