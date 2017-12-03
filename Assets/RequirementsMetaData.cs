using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RequirementsMetaData  {
	public Vector2 doorToOpen ;
	public bool openAutomatically;
	public Vector2 playerRequiredPosition;
	public List<RequirementMetaData> requirements;
}

[System.Serializable]
public class RequirementMetaData {
	public Vector2 positionInGrid;
	public int type; // key or pressureplayer
	public int id;
}