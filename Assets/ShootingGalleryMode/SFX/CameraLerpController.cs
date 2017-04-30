using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerpController : MonoBehaviour {
	public Transform lerpStart;
	public Transform lerpEnd;
	private bool atEnd;

	void Start () {
		atEnd = false;
	}



	// Update is called once per frame
	void Update () {
		var targetPosition = atEnd ? lerpStart.position : lerpEnd.position;
		if (Mathf.Abs(transform.position.x - lerpEnd.position.x) < .1f) {
			atEnd = true;
		} else if (Mathf.Abs(transform.position.x - lerpStart.position.x) < .1f) {
			atEnd = false;
		}
		transform.position =
			Vector3.Lerp(transform.position,
				targetPosition,
				.2f*Time.deltaTime);
	}
}
