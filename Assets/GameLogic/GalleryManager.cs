using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryManager : MonoBehaviour {
	public Transform[] allGalleryTransforms;
	// Use this for initialization
	void Start () {
		Transform[] childTransforms = gameObject.GetComponentsInChildren<Transform>();
		allGalleryTransforms = new Transform[childTransforms.Length - 1];
		Debug.Log("CHILDTANS LENGTH" + childTransforms.Length);
		Debug.Log("allGalleryLength " + allGalleryTransforms.Length);
		for (var i = 0; i < childTransforms.Length - 1; i++) {
			allGalleryTransforms[i] = childTransforms[i + 1];
		}
		Debug.Log("allGalleryLength " + allGalleryTransforms.Length);

	}
}
