using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardKey : MonoBehaviour {
	public TextMesh[] texts;

    public void UpdateText(Vector2Int position)
    {
		char c = KeyboardMapper.getCharInPosition(position);
		foreach (var t in texts)
        {
            t.text = c.ToString();
        }
    }

}
