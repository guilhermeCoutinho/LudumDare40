﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
	public Text timerText;
	Action callback;

	Coroutine timerCoroutine; 

	public void StartTimer (int time,Action  callback){
		this.callback = callback;
		if (timerCoroutine != null)
			StopCoroutine(timerCoroutine);
		timerCoroutine = StartCoroutine(countDown(time));
	}
	
	public void StopTimer () {
        if (timerCoroutine != null)
            StopCoroutine(timerCoroutine);
	}

	IEnumerator countDown (int totalTimer) {
        int currentTime = totalTimer;
		while (currentTime >= 0){
			int minutos = currentTime / 60;
			int segundos = currentTime - minutos * 60;
			string text = segundos >= 10 ?
				minutos.ToString() + ':' + segundos :
				minutos.ToString() + ":0" + segundos;
			timerText.text = text;
            yield return new WaitForSeconds(1f);
            currentTime--;
		}
		callback ();
	}


}
