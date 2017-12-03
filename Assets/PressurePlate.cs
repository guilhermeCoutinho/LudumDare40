using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {
	public static List<PressurePlate> pressurePlates;

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
		this.position = position;
	}
}