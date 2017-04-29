using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelAudioManager : MonoBehaviour {
	private AudioSource bellSound;
	private AudioSource gunshotSound;
	private AudioSource typewriterSound;
	private TypewriterSoundController typewriterSoundController;

	void Awake () {
		bellSound = transform.Find("Bell").GetComponent<AudioSource>();
		gunshotSound = transform.Find("GunShot").GetComponent<AudioSource>();
		typewriterSound = transform.Find("Typewriter").GetComponent<AudioSource>();
		typewriterSoundController = typewriterSound.GetComponent<TypewriterSoundController>();
	}

	public void PlayGunshot() {
		gunshotSound.Play();
	}

	public void PlayBell() {
		bellSound.Play();
	}

	public void PlayTypewriter() {
		typewriterSound.clip = typewriterSoundController.GetTypewriterClip();
		typewriterSound.Play();
	}
}
