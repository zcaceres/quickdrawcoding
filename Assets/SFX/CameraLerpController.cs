using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerpController : MonoBehaviour {
	public Transform lerpStart;
	public Transform lerpEnd;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		var targetPosition = lerpEnd.position;
		transform.position =
			Vector3.Lerp(transform.position,
				targetPosition,
				.1f*Time.deltaTime);
	}
}
