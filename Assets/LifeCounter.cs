using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCounter : MonoBehaviour {
	public GameObject lifeIconPrefab;
	int maxLifes;
	int playerCurrentHp = -1;

	void Update () {
		if (GameManager.Instance.playerLifes != playerCurrentHp){
            playerCurrentHp = GameManager.Instance.playerLifes;
			UpdateLifes();
		}
	}

	void UpdateLifes () {
		for (int i = transform.childCount - 1; i >= playerCurrentHp; i--) {
            transform.GetChild(i).GetComponent<RawImage>().enabled = false;
        }
	}

	public void SetupLifeCounter (int maxLifes){
		this.maxLifes = maxLifes;
		for(int i = transform.childCount-1;i>=maxLifes;i--){
			Destroy(transform.GetChild(i).gameObject);
		}
		for (int i=transform.childCount; i < maxLifes;i++){
			Instantiate (lifeIconPrefab,transform);
		}
	}
}
