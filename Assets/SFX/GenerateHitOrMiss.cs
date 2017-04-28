using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateHitOrMiss : MonoBehaviour {
	public GameObject hitPrefab;
	public GameObject missPrefab;
	private Vector3 generationPoint;

	void Start () {
		generationPoint = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
	}

	public void GenerateHitPrefab() {
		Object.Instantiate(hitPrefab, generationPoint, transform.rotation);
		transform.DetachChildren();
	}

	public void GenerateMissPrefab() {
		Object.Instantiate(missPrefab, generationPoint, transform.rotation);
		transform.DetachChildren();
	}

}
