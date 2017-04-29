using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelAudioManager : MonoBehaviour {
	private AudioSource bellSound;
	private AudioSource gunshotSound;
	// Use this for initialization
	void Awake () {
		bellSound = transform.Find("Bell").GetComponent<AudioSource>();
		gunshotSound = transform.Find("GunShot").GetComponent<AudioSource>();
	}

	public void PlayGunshot() {
		gunshotSound.Play();
	}

	public void PlayBell() {
		bellSound.Play();
	}
}
