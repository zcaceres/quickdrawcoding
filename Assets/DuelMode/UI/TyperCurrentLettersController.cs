using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TyperCurrentLettersController : MonoBehaviour {
	public Transform[] roundLetterTransforms;

	void Awake() {
		Transform[] childTransforms = gameObject.GetComponentsInChildren<Transform>();
		roundLetterTransforms = new Transform[childTransforms.Length - 1];
		for (var i = 0; i < childTransforms.Length - 1; i++) {
			roundLetterTransforms[i] = childTransforms[i + 1];
		}
	}

}
