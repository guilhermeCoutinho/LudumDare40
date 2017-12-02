using UnityEngine;
using System.Collections;

public class ObjectPoolElement : MonoBehaviour {
	[HideInInspector]public bool beingUsed = false;

	public void activate () {
		gameObject.SetActive (true);
		beingUsed = true;
	}
	public void deactivate () {
		gameObject.SetActive (false);
		beingUsed = false;
	}
}
