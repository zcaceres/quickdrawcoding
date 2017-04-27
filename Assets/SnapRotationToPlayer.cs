using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapRotationToPlayer : MonoBehaviour {

	private Transform mainCameraTransform;

	void Start () {
		mainCameraTransform = GameObject.FindWithTag("MainCamera").transform;
	}

	void Update () {
		SnapToCamera();
	}

	// Make sure letter always faces player
	void SnapToCamera() {
		gameObject.transform.rotation = Quaternion.LookRotation(this.transform.position - mainCameraTransform.position);
	}
}
