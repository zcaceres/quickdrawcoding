using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour {
	public TextAsset[] javaScriptFiles;
	public TextAsset[] HTMLFiles;
	public TextAsset[] cPlusPlusFiles;
	public TextAsset[] cFiles;
	public TextAsset[] goFiles;
	public TextAsset[] rubyFiles;
	public TextAsset[] pythonFiles;
	private string codeFileContents;

	// Must use awake so script references are available for Game Manager before called in its Start();
	void Awake () {
		codeFileContents = SelectCodeFileAsText();
	}

	private string SelectCodeFileAsText() {
		var language = PlayerPrefs.GetString("language");
		var difficulty = PlayerPrefs.GetString("difficulty");
		var difficultyIdx = GetDifficultyIdx(difficulty);
		var fileSource = GetFileSource(language);
		Debug.Log("Settings: " + language + " " + difficulty);

		return fileSource[difficultyIdx].text;
	}

	// Get text file of set difficulty arranged easy = 0, medium = 1, hard = 2
	int GetDifficultyIdx(string difficulty)
	{
		if (difficulty == "easy") {
			return 0;
		} else if (difficulty == "medium") {
			return 1;
		} else {
			return 2;
		}
	}

	// Fetch proper language file source
	TextAsset[] GetFileSource (string language)
	{
		switch(language)
		{
			case "javascript":
				return javaScriptFiles;
			case "cplusplus":
				return cPlusPlusFiles;
			case "c":
				return cFiles;
			case "ruby":
				return rubyFiles;
			case "python":
				return pythonFiles;
			case "go":
				return goFiles;
			case "html":
				return HTMLFiles;
			default:
				return javaScriptFiles;
		}
	}

	public string GetCleanCodeFileAsString() {
		return codeFileContents;
	}

}
