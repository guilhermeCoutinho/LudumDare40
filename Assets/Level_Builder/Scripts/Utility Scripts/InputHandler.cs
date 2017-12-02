using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : SingletonMonoBehaviour<InputHandler> {

	public delegate void MouseClickDelegate ();
	public static event MouseClickDelegate OnMouseClick ;

	public delegate void MouseRightClickDelegate ();
	public static event MouseRightClickDelegate onMouseRightClick ;

	void Update () {
		if (Input.GetMouseButtonUp (0) && OnMouseClick != null)
				OnMouseClick ();
		if (Input.GetMouseButtonUp (1) && onMouseRightClick != null)
			onMouseRightClick ();
	}
}
