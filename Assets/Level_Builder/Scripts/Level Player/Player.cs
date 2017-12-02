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
        if (Input.GetKeyDown(KeyCode.RightArrow))
            MovePlayer(Vector2Int.Right);
        if (Input.GetKeyDown(KeyCode.UpArrow))
            MovePlayer(Vector2Int.Up);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            MovePlayer(Vector2Int.Left);
        if (Input.GetKeyDown(KeyCode.DownArrow))
            MovePlayer(Vector2Int.Down);
    }

    void MovePlayer (Vector2Int direction)
    {
        levelData.MovePlayer(direction);
        UpdateGameState();
    }

    void UpdateGameState ()
    {
        levelData.playerPositionInGrid.print();
        transform.position = levelData.getWorldPosition(levelData.playerPositionInGrid);
    }

}
