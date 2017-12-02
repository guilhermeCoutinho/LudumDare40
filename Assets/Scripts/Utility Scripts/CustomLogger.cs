using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomLogger : SingletonMonoBehaviour<CustomLogger> {
    Text logText;

    void Awake() {
        logText = GetComponentInChildren<Text>();
        instance = this;
    }

    public void Log (string text)    {
		Instance.logText.text = text;
    }

}
