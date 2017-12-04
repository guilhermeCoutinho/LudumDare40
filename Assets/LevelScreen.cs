using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelScreen : SingletonMonoBehaviour<LevelScreen> {
	public float tempo;

	public IEnumerator BlinkScreen(){
		Text levelname;
		levelname =  GetComponentInChildren<Text>(true);
		levelname.text = "LEVEL "+(GameManager.Instance.currentLevel+1).ToString();
		yield return new WaitForSeconds(tempo); //black magic
		gameObject.SetActive(false);
	}
}