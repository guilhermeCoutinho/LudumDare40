using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    LevelData levelData;

    void Start ()
    {
        levelData = LevelLoader.Instance.LoadedLevelData;
        UpdateGameState();
    }
    
    void Update ()
    {
        if (Input.anyKeyDown ){
            print (Input.inputString);
        }
    }

    void MovePlayer (Vector2Int direction)
    {
        levelData.MovePlayer(direction);
        UpdateGameState();
    }

    void UpdateGameState ()
    {
    }

}
