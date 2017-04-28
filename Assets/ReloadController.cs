using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadController : MonoBehaviour {
	private Text reloadText;
	// Use this for initialization
	void Start () {
		reloadText = gameObject.GetComponent<Text>();
	}

	public void DisplayReload () {
		reloadText.enabled = true;
	}

	public void HideReload() {
		reloadText.enabled = false;
	}

	public bool isDisplayed() {
		return reloadText.enabled;
	}
}
