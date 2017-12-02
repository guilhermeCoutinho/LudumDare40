using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Level level;
    public Vector2Int currentPosition;
	public int onTopOf;

    void Start() {
        level = LevelLoader.Instance.LoadedLevel;
	}

	public void Initialize(Vector2Int pos, int startingId) {
		currentPosition = pos;
		onTopOf = startingId;
	}

	void Update ()
    {
        if (Input.anyKeyDown)
        {
            string rawInput = Input.inputString;
            if (rawInput.Length > 0)
            {
                Vector2Int playerInput = KeyboardMapper.getPositionInMap(rawInput[0]);
                if (playerInput != null) {
                    MovePlayer(playerInput);
                }
            }
        }
    }

    void MovePlayer (Vector2Int playerInput)
    {
        level.MovePlayer(playerInput);
    }
}
