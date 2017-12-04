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
		yield return new WaitForSeconds(tempo); //black magic
		levelScreen.SetActive(false);
	}
}