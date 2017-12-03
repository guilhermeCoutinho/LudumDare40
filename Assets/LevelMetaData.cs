﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMetaData : MonoBehaviour {
	public string fileName;
	public List<RequirementsMetaData> requirements;

	public void LoadMetaData () {
		foreach ( RequirementsMetaData metaData in requirements)
		{
			Door doorComponent = Door.getDoorByPosition ( new Vector2Int( metaData.doorToOpen) );
			doorComponent.Setup(metaData , new Vector2Int (metaData.playerRequiredPosition ),
			metaData.openAutomatically);
			foreach (RequirementMetaData data in metaData.requirements){
				if(data.type ==0){
					Key.getKeyByPosition( new Vector2Int( data.positionInGrid)).id = data.id;
				}
				else if (data.type==1){
					PressurePlate.GetPressurePlateByPosition
					(new Vector2Int(data.positionInGrid)).id =  data.id;
				}
			}
		}
	}

}