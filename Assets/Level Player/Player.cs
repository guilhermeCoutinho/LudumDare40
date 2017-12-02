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
        if (Input.anyKeyDown)
        {
            string rawInput = Input.inputString;
            if (rawInput.Length > 0)
            {
                Vector2Int playerInput = KeyboardMapper.getPositionInMap(rawInput[0]);
                if (playerInput != null) 
                    MovePlayer(playerInput);
            }
        }
    }

    void MovePlayer (Vector2Int playerInput)
    {
        level.MovePlayer(playerInput);
    }
}
