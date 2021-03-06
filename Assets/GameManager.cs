﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager> {
	public enum GameState {
		START_SCREEN ,
		GAME_RUNNING ,
		GAME_OVER ,
		END_GAME
	};
	public GameState gameState;
	Timer timer;
	public GameObject startScreen ;
	public LevelData[] levelSequences;
	public LevelScreen levelScreen ;
	public GameObject gameOverScreen;
	public GameObject endGameScreen;
    public GameObject guiHUD;
    private LevelLoader levelLoader;

	public LifeCounter[] lifeCounters;
	public int playerLifes;
	public int currentLevel;

	void Awake () {
		levelLoader = GetComponent<LevelLoader> ();
		timer = GetComponent<Timer>();
	}

	void Update ()
    {

		if (gameState == GameState.START_SCREEN)
        {

			if (Input.GetKeyDown(KeyCode.Escape))
            {
				startScreen.SetActive(false);

                StartGame(3);

			}
			return;

		}
		else if (gameState == GameState.GAME_RUNNING)
        {
			if (Input.GetKeyDown(KeyCode.Escape))
            {
				Sound.Instance.Play(3, (int)Sound.soundEvents.RESET);
				PlayerDied ();
			}

		}
        else if (gameState == GameState.GAME_OVER)
        {
			if (Input.anyKeyDown)
            {
				gameOverScreen.SetActive(false);
				gameState = GameState.GAME_RUNNING;
				playerLifes = 3;
				SetupLifeCounters(3);
				OpenLevel();
			}

		}
        else if  (gameState == GameState.END_GAME)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }

		}
	}

	public void StartGame (int lifes)
    {
		gameState = GameState.GAME_RUNNING;
        playerLifes = lifes;
		currentLevel = 0;
		OpenLevel();
        SetupLifeCounters(lifes);
	}

	void OpenLevel ()
    {
        Debug.Log ("OPEN LEVEL CALLED");
		levelLoader.LoadMap(levelSequences[currentLevel]);

        guiHUD.SetActive(true);
        levelScreen.gameObject.SetActive(true);

		StartCoroutine(levelScreen.BlinkScreen());
		timer.StartTimer(levelSequences[currentLevel].timeToComplete , OnTimeRunOunt);
		//Sound.Instance.PlayBGM(0);
		Sound.Instance.Play(0, (int)Sound.soundEvents.START);
		Sound.Instance.Play(3, (int)Sound.soundEvents.START);
	}

    void OnTimeRunOunt()
    {
        if (gameState == GameState.GAME_RUNNING)
        {
            PlayerDied();
        }
		
	}

    public void OpenNextLevel()
    {
        currentLevel++;

        if (ThereIsNoNextLevel())
        {
            gameState = GameState.END_GAME;
            guiHUD.SetActive(false);
            endGameScreen.SetActive(true);
            timer.StopTimer();
            return;
        }

        OpenLevel();

    }

    private bool ThereIsNoNextLevel()
    {
        return currentLevel >= levelSequences.Length;
    }

    public void PlayerReachedGoal ()
    {
		Sound.Instance.Play(3, (int)Sound.soundEvents.FINISH);
		OpenNextLevel();
	}

	public void PlayerDied ()
    {
        guiHUD.SetActive(false);
        playerLifes -- ;
		if (playerLifes == 0){
			GameOver ();
			return;
		}
		OpenLevel ();
	}

	void GameOver ()
    {
		Sound.Instance.Play(3, (int)Sound.soundEvents.GAMEOVER);
        gameOverScreen.SetActive(true);
        guiHUD.SetActive(false);

        gameState = GameState.GAME_OVER;
		timer.StopTimer ();
        levelLoader.destroyContent();
	}

	void SetupLifeCounters (int n)
    {
		foreach (LifeCounter LifeCounter in lifeCounters)
		{
			LifeCounter.SetupLifeCounter(n);
		}
	}
}
