using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : SingletonMonoBehaviour<Sound> {
	public AudioClip[] sounds;
	public AudioClip[] bgms;
	public AudioSource[] layers;
	public AudioSource BGMLayer;

	public void Awake(){
		for(int i=0;i<layers.Length;i++){
			layers[i]=gameObject.AddComponent<AudioSource>();
			layers[i].playOnAwake=false;
			layers[i].priority=i;
		}
		BGMLayer =gameObject.AddComponent<AudioSource>();
		BGMLayer.playOnAwake=false;
		BGMLayer.loop=true;
		BGMLayer.priority=-1;
	}

	public enum soundEvents{
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

	public void PlayBGM(int bgmId){
		BGMLayer.clip = bgms[bgmId];
		BGMLayer.Play();
	}

	public void StopBGM(){
		BGMLayer.Stop();
	}

	public void Play(int layer, int sound){
		switch (sound){
			case 0: //pushbox
			Debug.Log(layer + ": BOX IS PUSHED");

			break;
			case 1: //steps
			Debug.Log(layer + ": step");
			layers[layer].clip = sounds[4];
			layers[layer].Play();
			break;
			case 2: //grab
			Debug.Log(layer + ": ITEM ACQUIRED");
			layers[layer].clip = sounds[2];
			layers[layer].Play();
			break;
			case 3: //fanfare
			Debug.Log(layer + ": CONGRATULATIONS");

			break;
			case 4: //door
			Debug.Log(layer + ": DOOR HAS OPENED");
			
			break;
			case 5: //start
			Debug.Log(layer + ": LEVEL HAS STARTED");
			layers[layer].clip = sounds[6];
			layers[layer].Play();
			break;
			case 6: //finish
			Debug.Log(layer + ": LEVEL COMPLETE");
			
			break;
			case 7: //death
			Debug.Log(layer + ": YOU DIED");
			layers[layer].clip = sounds[1];
			layers[layer].Play();
			break;
			case 8: //reset
			Debug.Log(layer + ": LEVEL RESET");
			layers[layer].clip = sounds[1];
			layers[layer].Play();
			break;
			case 9: //hole
			Debug.Log(layer + ": YOU FELL IN A HOLE");
			layers[layer].clip = sounds[1];
			layers[layer].Play();
			break;
			case 10: //gameover
			Debug.Log(layer + ": GAME OVER :(");

			break;
			default:
				Debug.LogError("UNIDENTIFIED SOUND ID " + sound + "ON LAYER" + layer);
			break;
		}
	}
}
