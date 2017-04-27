using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGalleryAsParent : MonoBehaviour {

	void Start () {
		transform.parent = transform.parent.parent;
	}

}
