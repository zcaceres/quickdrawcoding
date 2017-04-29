using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypewriterSoundController : MonoBehaviour {
	public AudioClip[] typewriterClips;

	public AudioClip GetTypewriterClip() {
		return typewriterClips[Random.Range(0, typewriterClips.Length)];
	}

}
