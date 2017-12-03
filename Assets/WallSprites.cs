using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// up ; right; down;left
public class WallSprites : SingletonMonoBehaviour<WallSprites> {

	public bool resetThis ;
	public WallSpriteBinding[] wallSpriteBindings;

/* 
	void Awake () {
		if (resetThis){
			for (int j=0;j<16;j++) {
				int i = j;
				int x, y ,z ,w;
				x = i / 8;
				i -= x * 8;
				y = i/4;
				i -= y*4;
				z = i/2;
				i -= z *2;
				w = i;
                Vector4 pos = new Vector4(x,y,z,w);
				wallSpriteBindings[j].position = pos;
			}
		}
	}
	*/

	[System.Serializable]
	public class WallSpriteBinding{
		public Vector4 position;
		public Sprite sprite;
	}
}
