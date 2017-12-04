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
		BGMLayer.volume = 0.5f;
	}

	public void StopBGM(){
		BGMLayer.Stop();
	}

	public void Play(int layer, int sound){
		return;
		switch (sound){
			case 0: //pushbox
			layers[layer].clip = sounds[30];
			layers[layer].Play();
			break;
			case 1: //steps
			layers[layer].clip = sounds[4];
			layers[layer].Play();
			break;
			case 2: //grab
			layers[layer].clip = sounds[3];
			layers[layer].Play();
			break;
			case 3: //fanfare
			break;
			case 4: //door
			break;
			case 5: //start
			layers[1].clip = sounds[28];
			layers[1].Play();
			layers[0].clip = bgms[0];
			layers[0].PlayDelayed(1.2f);
			break;
			case 6: //finish			
			break;
			case 7: //death
			layers[layer].clip = sounds[6];
			layers[layer].Play();
			break;
			case 8: //reset
			//layers[layer].clip = sounds[1];
			//layers[layer].Play();
			break;
			case 9: //hole
			layers[layer].clip = sounds[1];
			layers[layer].Play();
			break;
			case 10: //gameover
			break;
			default:
				Debug.LogError("UNIDENTIFIED SOUND ID " + sound + "ON LAYER" + layer);
			break;
		}
	}
}
