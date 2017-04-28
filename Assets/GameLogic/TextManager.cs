using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour {
	public TextAsset[] codeFiles;
	private string codeFileContents;
	void Start () {
		codeFileContents = SelectCodeFileAsText();
	}

	private string SelectCodeFileAsText() {
		return codeFiles[0].text;
	}

	public string GetCleanCodeFileAsString() {
		return codeFileContents;
	}

}
