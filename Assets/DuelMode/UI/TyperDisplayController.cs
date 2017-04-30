using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TyperDisplayController : MonoBehaviour {
	private Text textDisplay;

	void Start () {
		textDisplay = GetComponent<Text>();
	}

	// Refactor to add one letter at a time to 'final program typed'
	public void SetTyperTextContent(string letter) {
		textDisplay.text += letter;
	}

}
