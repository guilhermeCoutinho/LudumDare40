using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class FileSelector : MonoBehaviour {
	//PUBLIC LINK VARIABLES
	public GameObject openButton ;
	public GameObject fileSelectionRootPanel;
	public GameObject fileButtonPrefab ;
	public GameObject saveButton ;

	//private rectTransform calculations
	RectTransform rectTransform ;
	float buttonHeight;
	float minHeight ;
	float fixedOffset ;
	float spacing ;

	void Start () {
		buttonHeight = fileButtonPrefab.GetComponent<RectTransform> ().sizeDelta.y;
		rectTransform = GetComponent<RectTransform> ();
		VerticalLayoutGroup layout = GetComponent<VerticalLayoutGroup> ();

		fixedOffset = layout.padding.bottom + layout.padding.top;
		spacing = layout.spacing;
		minHeight = rectTransform.sizeDelta.y;
	}
		
	void InstantiateChildren (FileInfo[] files) {
		int qtd = files.Length;
		if (qtd == 0)
			return;
		for (int i = 0; i < qtd; i++) {
			GameObject clone = (GameObject)Instantiate (fileButtonPrefab, this.transform);
			clone.transform.localScale = Vector3.one;
			string fileName = files [i].Name;
			clone.GetComponentInChildren<Text> ().text = fileName;
			clone.GetComponent<Button> ().onClick.AddListener (delegate {
				SelectFile (fileName);
			});
		}
	}

	void SetupRectTransformSize (int qtd)
	{
		float spacingValue = (qtd - 1) * spacing;
		float height = fixedOffset + buttonHeight * qtd + spacingValue;
		float anchorOffset = height / 2f;
		if (height > minHeight) {
			rectTransform.anchoredPosition = new Vector2 (0, -anchorOffset);
			rectTransform.sizeDelta = new Vector2 (rectTransform.sizeDelta.x, height);
		}
	}

	void flushButtons () {
		foreach (Transform t in transform.GetComponentInChildren<Transform>()) {
			Destroy (t.gameObject);
		}
	}

	public void PopulateFileList () {
		string path = "LevelData/";
		DirectoryInfo directoryInfo = new DirectoryInfo (path);
		flushButtons ();
		SetupRectTransformSize (directoryInfo.GetFiles ().Length);
		InstantiateChildren (directoryInfo.GetFiles ());
	}

	public void ClosePanel () {
		saveButton.GetComponent<Button> ().interactable = true;
		openButton.SetActive (true);
		fileSelectionRootPanel.SetActive (false);
	}

	public void SelectFile (string fileName) {
		GridBuilder.Instance.OpenFile (fileName);
		ClosePanel ();
	}

}
