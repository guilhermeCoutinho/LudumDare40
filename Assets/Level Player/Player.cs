using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Level level;
    public Vector2Int currentPosition;
	public int onTopOf;
    public List<int> keyRing;

    void Start() {
        level = LevelLoader.Instance.LoadedLevel;
        keyRing = new List<int>();
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
					if (onTopOf == LevelLoader.Instance.keyId) {
						onTopOf = LevelLoader.Instance.floorId;
                        Key keyComponnent = Key.getKeyByPosition(currentPosition);
                        if(keyComponnent!=null){
                            keyRing.Add(keyComponnent.id);
                            Destroy(keyComponnent.gameObject);
                        }
					}
				}
			}
        }
    }

    void MovePlayer (Vector2Int playerInput)
    {
        level.MovePlayer(playerInput);
    }
}
