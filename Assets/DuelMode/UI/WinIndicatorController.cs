using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinIndicatorController : MonoBehaviour {
	private GameObject displayText;

	void Awake () {
		displayText = transform.Find("Text").gameObject;
	}

	public void ShowWinText() {
		displayText.SetActive(true);
	}

	public void HideWinText() {
		displayText.SetActive(false);
	}
}
