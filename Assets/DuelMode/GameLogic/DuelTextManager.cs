using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelTextManager : MonoBehaviour {
	public TextAsset[] codeFiles;
	private string codeFileContents;

	// Must use awake so script references are available for Game Manager before called in its Start();
	void Awake () {
		codeFileContents = SelectCodeFileAsText();
	}

	private string SelectCodeFileAsText() {
		var myString = codeFiles[0].text.Replace(System.Environment.NewLine, " ");
		return myString;
	}

	public string GetCleanCodeFileAsString() {
		Debug.Log("Returned from clean coe file as string");
		return codeFileContents;
	}
}
