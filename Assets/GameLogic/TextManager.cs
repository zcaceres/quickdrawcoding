using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour {
	public TextAsset[] codeFiles;
	private string codeFileContents;

	// Must use awake so script references are available for Game Manager before called in its Start(); 
	void Awake () {
		codeFileContents = SelectCodeFileAsText();
	}

	private string SelectCodeFileAsText() {
		return codeFiles[0].text;
	}

	public string GetCleanCodeFileAsString() {
		Debug.Log("Returned from clean coe file as string");
		return codeFileContents;
	}

}
