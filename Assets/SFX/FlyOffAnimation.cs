using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyOffAnimation : MonoBehaviour {
	private Transform myTransform;
	private Color myTextColor;
	private float startingTime;
	private float waitValue;
	// Use this for initialization
	void Start () {
		myTransform = gameObject.transform;
		myTextColor = gameObject.GetComponent<TextMesh>().color;
		waitValue = 10f;
	}

	// Update is called once per frame
	void Update () {
		Fly();
		Decay();
	}

	void Fly() {
		float translation = Time.deltaTime * 1;
		transform.Translate(0, translation, 0);
	}

	void Decay() {
		waitValue -= .5f;
		if(waitValue <= 0) {
			Die();
		}
	}

	void Die() {
		Destroy(gameObject);
	}
}
