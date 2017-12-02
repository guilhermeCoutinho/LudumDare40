using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ContextualMenu : SingletonMonoBehaviour<ContextualMenu> {
	GameObject child ;
	[HideInInspector]public GridObject currentSelectedObject ;

	void Awake () {
		SetupSize ();
		SetupChildren ();
		instance = this;
	}

	void Start( ) {
		gameObject.SetActive (false);
	}
    
	void SetupChildren () {
		child = transform.GetChild (0).gameObject;
		setNameNColor (child, 0);
		int qtd = GridBuilder.Instance.cellTypeColor.Length;
		for (int i = 1; i < qtd; i++) {
			GameObject clone = (GameObject)Instantiate (child, transform);
			setNameNColor (clone, i);
		}
	}
	void setNameNColor (GameObject clone , int i)
	{
		clone.name = GridBuilder.Instance.cellTypeColor [i].type.ToString ();
		clone.GetComponent<Image> ().color = GridBuilder.Instance.cellTypeColor [i].color;
		SetClickListener (clone, (int)GridBuilder.Instance.cellTypeColor[i].type);
	}

	void SetClickListener (GameObject clone, int i)
	{
		EventTrigger trigger = clone.GetComponent<EventTrigger> ();
		EventTrigger.Entry entry = new EventTrigger.Entry ();
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback.AddListener (delegate {
			click (i);
		});
		trigger.triggers.Add (entry);
	}

	void SetupSize (){
		RectTransform rect = GetComponent<RectTransform> ();
		float qtd = GridBuilder.Instance.cellTypeColor.Length;
		Vector2 size = new Vector2 (qtd * 160 + 40 + (qtd - 1) * 20, rect.sizeDelta.y);
		rect.sizeDelta = size;
	}

	public void click (int position){
		if (currentSelectedObject == null)
			Debug.Log ("null");
		if (currentSelectedObject != null)
			currentSelectedObject.ChangeCellType (position);
		close ();
	}

	void close () {
		gameObject.SetActive (false);
	}
}
