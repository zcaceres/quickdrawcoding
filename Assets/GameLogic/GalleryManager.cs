using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryManager : MonoBehaviour {
	public Transform[] allGalleryTransforms;

	// Gets transforms in Gallery and preps them for GameManager
	void Start () {
		Transform[] childTransforms = gameObject.GetComponentsInChildren<Transform>();
		allGalleryTransforms = new Transform[childTransforms.Length - 1];
		for (var i = 0; i < childTransforms.Length - 1; i++) {
			allGalleryTransforms[i] = childTransforms[i + 1];
		}
	}
}
