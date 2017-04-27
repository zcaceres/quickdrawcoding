using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour {

	private Light flash;
	// Use this for initialization
	void Start () {
		flash = GetComponent<Light>();
	}

	// TODO: Make Muzzle Flash
	public void TriggerFlash() {
		Debug.Log("flashing");
		flash.enabled = true;
		// yield return new WaitForSeconds(.2f);
		flash.enabled = false;
	}

}
