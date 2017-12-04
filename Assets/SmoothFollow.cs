using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : SingletonMonoBehaviour<SmoothFollow> {
	public float speed = 5;
	SpriteRenderer sprite;
	[HideInInspector]public GameObject target;
	Vector2 position ;

	void Start () {
		sprite = GetComponent<SpriteRenderer> ();
	}

	void Update () {
		if (target == null){
			sprite.enabled = false;
			return;
		}
		sprite.enabled = true;
		position = Vector2.Lerp (transform.position,target.transform.position,Time.deltaTime * speed);
		transform.position = new Vector3 (position.x,position.y,transform.position.z);
	}
}
