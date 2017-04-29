using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TyperDisplayController : MonoBehaviour {
	private Text textDisplay;
	public Transform[] roundLetterTransforms;

	void Start () {
		textDisplay = GetComponent<Text>();
	}


	// Refactor to add one letter at a time to 'final program typed'
	public void SetTyperTextContent(string textContent) {
		textDisplay.text = textContent;
	}

}
