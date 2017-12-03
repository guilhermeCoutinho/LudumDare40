using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : SingletonMonoBehaviour<Sound> {
	public enum soundEvents{
		BGM,
		PUSHBOX,
		STEPS,
		GRAB,
		FANFARE,
		DOOR,
		START,
		FINISH,
		DEATH,
		RESET,
		HOLE,
		GAMEOVER
	}

	public static void play(int layer, int sound){
		switch (sound){
			case 0: //bgm
			Debug.Log(layer + ": STARTING BGM NOW");

			break;
			case 1: //pushbox
			Debug.Log(layer + ": BOX IS PUSHED");

			break;
			case 2: //steps
			Debug.Log(layer + ": step");

			break;
			case 3: //grab
			Debug.Log(layer + ": ITEM ACQUIRED");
			
			break;
			case 4: //fanfare
			Debug.Log(layer + ": CONGRATULATIONS");

			break;
			case 5: //door
			Debug.Log(layer + ": DOOR HAS OPENED");
			
			break;
			case 6: //start
			Debug.Log(layer + ": LEVEL HAS STARTED");

			break;
			case 7: //finish
			Debug.Log(layer + ": LEVEL COMPLETE");
			
			break;
			case 8: //death
			Debug.Log(layer + ": YOU DIED");

			break;
			case 9: //reset
			Debug.Log(layer + ": LEVEL RESET");
			
			break;
			case 10: //hole
			Debug.Log(layer + ": YOU FELL IN A HOLE");

			break;
			case 11: //gameover
			Debug.Log(layer + ": GAME OVER :(");

			break;
			default:
				Debug.LogError("UNIDENTIFIED SOUND ID " + sound + "ON LAYER" + layer);
			break;
		}
	}
}
