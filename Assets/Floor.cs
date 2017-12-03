using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {
	public Sprite[] floorSprites;
	void OnEnable () {
		GetComponentInChildren<SpriteRenderer>().sprite = 
		floorSprites[Random.Range(0,floorSprites.Length)];
	}
}
