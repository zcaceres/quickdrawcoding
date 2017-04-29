using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RoleIndicatorController : MonoBehaviour {
	private Text displayText;
	
	void Awake () {
		displayText = GetComponent<Text>();
	}

	public void ShowPrepareToType() {
		displayText.text = "Prepare to Type";
		displayText.enabled = true;
	}

	public void ShowPrepareToFire() {
		displayText.text = "Prepare to Fire";
		displayText.enabled = true;
	}

	public void HideRoleText() {
		displayText.enabled = false;
	}

}
