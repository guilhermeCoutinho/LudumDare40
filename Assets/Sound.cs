using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : SingletonMonoBehaviour<Sound> {
	public AudioClip[] sounds;
	public AudioClip[] bgms;
	public AudioSource[] layers;
	public AudioSource BGMLayer;

	public void Awake()
    {

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

	public enum soundEvents
    {
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
		GAMEOVER,
		BUTTOND,
		BUTTONU
	}

	public void PlayBGM(int bgmId)
    {
		BGMLayer.clip = bgms[bgmId];
		BGMLayer.volume = 0.15f;
	}

	public void StopBGM()
    {
		BGMLayer.Stop();
	}

	public void Play(int layer, int sound)
    {

		layers[layer].volume=0.15f;

		switch (sound){
			case 0: //pushbox
			    layers[layer].clip = sounds[30];
			    layers[layer].Play();
			    break;

			case 1: //steps
                layers[layer].volume = 0.15f;
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
                layers[layer].volume = 0.25f;
                layers[layer].clip = sounds[11];
			    layers[layer].Play();
			    break;

			case 5: //start
			    StartCoroutine(onStartLevel ());
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
			    layers[0].volume = 0f;
			    layers[1].volume = 0.5f;
			    layers[1].clip = sounds[0];// im afraid
			    layers[1].Play();
			    break;

			case (int)soundEvents.BUTTOND:
			    layers[layer].clip = sounds[12];
			    layers[layer].Play();
			    break;

			case (int)soundEvents.BUTTONU:
			    layers[layer].clip = sounds[13];
			    layers[layer].Play();
			    break;

			default:
				Debug.LogError("UNIDENTIFIED SOUND ID " + sound + "ON LAYER" + layer);
			    break;
		}
	}

	IEnumerator onStartLevel () {
        layers[0].volume = 0f;
        layers[2].volume = 0.08f;
        layers[2].clip = sounds[28];
        layers[2].Play();
        yield return new WaitForSeconds(1.6f);
        /*
        layers[1].volume = 0.25f;
        layers[1].clip = sounds[6];
        layers[1].Play();
        yield return new WaitForSeconds(.4f);
		 */
        layers[0].volume = 0.10f;
        if (!layers[0].isPlaying)
        {
            layers[0].clip = bgms[0];
            layers[0].loop = true;
            layers[0].Play();
        }
	}
}
