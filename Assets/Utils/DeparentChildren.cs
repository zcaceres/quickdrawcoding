using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeparentChildren : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// transform.DetachChildren();
		Destroy(gameObject);
	}

	// Update is called once per frame
	void Update () {

	}
}
