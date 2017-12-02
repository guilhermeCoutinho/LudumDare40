using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Level level;
    public Vector2Int currentPosition;

    void Start() {
        level = LevelLoader.Instance.LoadedLevel;
    }

    void Update ()
    {
        if (Input.anyKeyDown ){
            print (Input.inputString);
        }
    }

    void MovePlayer (Vector2Int playerInput)
    {
        level.MovePlayer(playerInput);
    }
}
