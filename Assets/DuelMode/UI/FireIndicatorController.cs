using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FireIndicatorController : MonoBehaviour {
	private Text textDisplay;

	void Awake () {
		textDisplay = gameObject.GetComponentInChildren<Text>();
	}

	public void DisplayFireText(bool toDisplay) {
		textDisplay.enabled = toDisplay;
	}

}
