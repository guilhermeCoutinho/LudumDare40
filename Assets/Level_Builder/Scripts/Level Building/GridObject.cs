using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridObject : MonoBehaviour {
    int x, y ;
	public ContextualMenu contextualMenu;
    Renderer rend;
	CellElement cellType ;

    void Awake() {
        rend = GetComponentInChildren<Renderer>();
		contextualMenu = ContextualMenu.Instance;
    }

	public void setIndexes(int x, int y , int value){
        this.x = x;
        this.y = y;
		cellType = GridBuilder.Instance.cellTypeColor [value];
		setupCellGfx ();
    }
		
	void OnMouseOver () {
		if (Input.GetMouseButtonDown(0)) {
			if (contextualMenu.isActiveAndEnabled)
				return;
			Vector3 position =  Camera.main.ScreenToWorldPoint (Input.mousePosition);
			position.z = 0;
			contextualMenu.transform.position = position;
			contextualMenu.gameObject.SetActive (true);
			contextualMenu.currentSelectedObject = this;
		}
	}

	public void ChangeCellType (int index) {
		cellType = GridBuilder.Instance.cellTypeColor [index];
		contextualMenu.currentSelectedObject = null;
		GridBuilder.Instance.changeElement (x, y, index);
		setupCellGfx ();
	}

	void setupCellGfx () {
		rend.material.color = cellType.color;
	}
}
