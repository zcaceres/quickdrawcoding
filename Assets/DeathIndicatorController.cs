using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathIndicatorController : MonoBehaviour {
	private GameObject myDisplayImage;

	void Awake () {
		myDisplayImage = transform.Find("Image").gameObject;
	}

	public void ShowDeathScreen() {
		myDisplayImage.SetActive(true);
	}

	public void HideDeathScreen() {
		myDisplayImage.SetActive(false);
	}

}
