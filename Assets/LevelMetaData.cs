using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMetaData : MonoBehaviour {
	public string fileName;
	public RequirementsMetaData[] requirements;

	public void LoadMetaData () {
		foreach ( RequirementsMetaData metaData in requirements)
		{
			Door doorComponent = Door.getDoorByPosition ( new Vector2Int( metaData.doorToOpen) );
			doorComponent.Setup(metaData , new Vector2Int (metaData.playerRequiredPosition ),
			metaData.openAutomatically);
		}
	}

}
