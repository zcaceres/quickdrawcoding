using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnouncementManager : MonoBehaviour {
	private AudioSource announcer;
	public AudioClip[] victorySounds;
	public AudioClip[] defeatSounds;
	public AudioClip[] streakSounds;
	public AudioClip getReadyClip;
	public AudioClip beginClip;

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

	public void PlayStreakSound(int index) {
		announcer.clip = streakSounds[index];
		PlaySound();
	}

	public void PlayGetReadySound() {
		announcer.clip = getReadyClip;
		PlaySound();
	}

	public void PlayBeginSound() {
		announcer.clip = beginClip;
		PlaySound();
	}

	private void PlaySound() {
		announcer.Play();
	}
}
