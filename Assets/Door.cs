using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	public bool firstTimeOpen=true;
	public bool isOpen;
	public static List<Door> doorList;
	Vector2Int playerStandPosition;
	Vector2Int position;
	public bool openAutomatic;
	public List<int> keyIds;
	public List<PressurePlate> pressurePlates;

	private Player getPlayer {
		get {
			return LevelLoader.Instance.LoadedLevel.player;
		}
	}

    public static void ClearList()
    {
        if (doorList == null)
            doorList = new List<Door>();
        doorList.Clear();
    }

	public static Door getDoorByPosition(Vector2Int position)
    {
        for (int i = 0; i < doorList.Count; i++)
        {
            if (doorList[i].position.isEqual(position))
                return doorList[i];
        }
        return null;
    }

	public void Initialize (Vector2Int position) {
		this.position = position;
		if (doorList == null)
			doorList = new List<Door>();
		doorList.Add(this);
		firstTimeOpen=true;
	}

	public void Setup (RequirementsMetaData requirements , Vector2Int playerPosition,
		bool openAutomatically) {
		this.playerStandPosition = playerPosition;
		this.openAutomatic = openAutomatically;
		keyIds = new List<int>();
		pressurePlates = new List<PressurePlate>();
		foreach (RequirementMetaData requirement in requirements.requirements)
		{
			if (requirement.type == 0) // ITS A KEY
				keyIds.Add (requirement.id);
			else 
				pressurePlates.Add( PressurePlate.GetPressurePlateByPosition ( new Vector2Int( requirement.positionInGrid)  ));
		}
	} 

    public bool hasAllKeys()
    {
		for (int i = 0;i<keyIds.Count;i++)
			if (!getPlayer.keyRing.Contains(keyIds[i]))
			return false;
		return true;
    }

	public bool hasAllPressurePlates () {
		for (int i = 0; i < pressurePlates.Count; i++)
            if ( !pressurePlates[i].hasSomethingOnTop )
                return false;
        return true;
	}

    void Update () {
		toggleActive (hasAllKeys() && hasAllPressurePlates());
	}

	void toggleActive (bool active) {
		if (isOpen == active)
			return;
		isOpen = active;
		if (isOpen){
			GetComponentInChildren<Renderer>().enabled = false;
			LevelLoader.Instance.LoadedLevel.setCell
			(position.x,position.y,LevelLoader.Instance.floorId);
			Sound.play(1, (int)Sound.soundEvents.DOOR);
			if(firstTimeOpen)Sound.play(0, (int)Sound.soundEvents.FANFARE);
			firstTimeOpen=false;
		}else {
                GetComponentInChildren<Renderer>().enabled = true;
                LevelLoader.Instance.LoadedLevel.setCell
                (position.x, position.y, LevelLoader.Instance.doorId);
		}
	}
}
