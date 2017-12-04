using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {
	public Sprite pressuredSprite; 
	public Sprite releasedSprite;
	public static List<PressurePlate> pressurePlates;
	public int id;
	public Vector2Int position ;
	public bool hasSomethingOnTop ;

	public static PressurePlate GetPressurePlateByPosition (Vector2Int position) {
		for (int i = 0; i < pressurePlates.Count; i++)
        {
            if (pressurePlates[i].position.isEqual(position))
                return pressurePlates[i];
        }
        return null;
	}

	public static void ClearList()
    {
        if (pressurePlates == null)
            pressurePlates = new List<PressurePlate>();
        pressurePlates.Clear();
    }

	public void Initialize (Vector2Int position) {
		if (pressurePlates == null)
			pressurePlates = new List<PressurePlate>();
		pressurePlates.Add(this);
		this.position = position;
		
	}

	void Update( ) { 
		RaycastHit hit;
		ToggleSprites ( Physics.Raycast(
			transform.position,Vector3.back, out hit,Mathf.Infinity)) ;
	}

	void ToggleSprites (bool newState) {
		if (newState == hasSomethingOnTop)
			return;
		hasSomethingOnTop = newState;
		GetComponentInChildren<SpriteRenderer>().sprite =
		hasSomethingOnTop ? pressuredSprite : releasedSprite;
		if(hasSomethingOnTop){
			Sound.Instance.Play(3, (int)Sound.soundEvents.BUTTOND);
		}else{
			Sound.Instance.Play(3, (int)Sound.soundEvents.BUTTONU);
		}
    }

}