using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScreen : SingletonMonoBehaviour<LevelScreen> {
	public GameObject levelScreen;
	public float tempo;

	public IEnumerator BlinkScreen(){
		UnityEngine.UI.Text levelname;
		levelname =  levelScreen.GetComponentInChildren<UnityEngine.UI.Text>();
		levelname.text = "LEVEL "+(GameManager.Instance.currentLevel+1).ToString();
		levelScreen.SetActive(true);
		updateLives();
		yield return new WaitForSeconds(tempo); //black magic
		levelScreen.SetActive(false);
	}

	public void updateLives(){
		switch(GameManager.Instance.playerLifes){
			case 1:
				levelScreen.transform.GetChild(0).gameObject.SetActive(true);
				levelScreen.transform.GetChild(1).gameObject.SetActive(false);
				levelScreen.transform.GetChild(2).gameObject.SetActive(false);
			break;
			case 2:
				levelScreen.transform.GetChild(0).gameObject.SetActive(true);
				levelScreen.transform.GetChild(1).gameObject.SetActive(true);
				levelScreen.transform.GetChild(2).gameObject.SetActive(false);
			break;
			case 3:
				levelScreen.transform.GetChild(0).gameObject.SetActive(true);
				levelScreen.transform.GetChild(1).gameObject.SetActive(true);
				levelScreen.transform.GetChild(2).gameObject.SetActive(true);
			break;
			default:
			break;
		}
	}
}