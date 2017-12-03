using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {
	Vector2Int position;
	
	public void Initialize (Vector2Int position ) {
		int vLeft, vRight, vTop, vBottom;
        vLeft = vRight = vTop = vBottom = 0;
        Level model = LevelLoader.Instance.LoadedLevel;
		Vector2Int topPosition = position + Vector2Int.up;
		Vector2Int rightPosition = position + Vector2Int.right;
		Vector2Int bottomPosition = position + Vector2Int.down;
		Vector2Int leftPosition = position + Vector2Int.left;
		if (model.insideGrid(topPosition))
			vTop=model.Grid[topPosition.x,topPosition.y] != LevelLoader.Instance.wallId ? 1 : 0;
		if (model.insideGrid(rightPosition))
            vRight = model.Grid[rightPosition.x, rightPosition.y]!= LevelLoader.Instance.wallId? 1 : 0;
		if (model.insideGrid(bottomPosition))
            vBottom = model.Grid[bottomPosition.x, bottomPosition.y]!= LevelLoader.Instance.wallId? 1 : 0;
		if (model.insideGrid(leftPosition))
            vLeft = model.Grid[leftPosition.x, leftPosition.y]!= LevelLoader.Instance.wallId? 1 : 0;
		int spriteIndex = vTop *8 + vRight * 4 + vBottom * 2 + vLeft ;
		GetComponentInChildren<SpriteRenderer>().sprite = 
			WallSprites.Instance.wallSpriteBindings[spriteIndex].sprite;
	}
}
