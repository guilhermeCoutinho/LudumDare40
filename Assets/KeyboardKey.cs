using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardKey : MonoBehaviour {
	public TextMesh[] texts;
    Material material;

    void OnEnable () {
        material = GetComponent<MeshRenderer>().material;
    }

    public void UpdateText(Vector2Int position , bool hideWithTime)
    {
		char c = KeyboardMapper.getCharInPosition(position);
		foreach (var t in texts)
        {
            t.text = c.ToString();
        }
        if (hideWithTime)
            StartCoroutine(hide());
    }

    IEnumerator hide () {
        yield return new WaitForSeconds (3f);
        GetComponentInParent<ObjectPool>().returnObject(gameObject);
    }
}
