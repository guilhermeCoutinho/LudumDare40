using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {
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

	public void Initialize (Vector2Int position) {
		if (pressurePlates == null)
			pressurePlates = new List<PressurePlate>();
		pressurePlates.Add(this);
		this.position = position;
		
	}

	void Update( ) { 
		RaycastHit hit;
		hasSomethingOnTop =  Physics.Raycast(
			transform.position,Vector3.back, out hit,Mathf.Infinity) ;

	}

}