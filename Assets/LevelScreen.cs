using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScreen : SingletonMonoBehaviour<LevelScreen> {
	public GameObject[] partsOfScreen;
	public float tempo;

	void Awake(){
		int parts=transform.childCount;
		partsOfScreen = new GameObject[parts];
		for(int i=0; i<parts; i++){
			partsOfScreen[i]=transform.GetChild(i).gameObject;
		}
	}

	public IEnumerator BlinkScreen(){
		UnityEngine.UI.Text levelname;
		levelname =  GetComponentInChildren<UnityEngine.UI.Text>(true);
		levelname.text = "LEVEL "+(GameManager.Instance.currentLevel+1).ToString();
		foreach(GameObject g in partsOfScreen){
			g.SetActive(true);
		}
		Debug.Log("blinking");
		yield return new WaitForSeconds(tempo); //black magic
		Debug.Log("blinking out");
		foreach(GameObject g in partsOfScreen){
			g.SetActive(false);
		}
	}
}