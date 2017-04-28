using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnouncementManager : MonoBehaviour {

	private AudioSource announcer;
	public AudioClip[] victorySounds;
	public AudioClip[] defeatSounds;
	public AudioClip[] streakSounds;

	// Use this for initialization
	void Start () {
		announcer = gameObject.GetComponent<AudioSource>();
	}

	public void PlayWinSound() {
		announcer.clip = victorySounds[Random.Range(0, victorySounds.Length)];
		PlaySound();
	}

	public void PlayLoseSound() {
		announcer.clip = defeatSounds[Random.Range(0, defeatSounds.Length)];
		PlaySound();
	}

	public void PlayStreakSound() {
		announcer.clip = streakSounds[Random.Range(0, streakSounds.Length)];
		PlaySound();
	}

	private void PlaySound() {
		announcer.Play();
	}
}
