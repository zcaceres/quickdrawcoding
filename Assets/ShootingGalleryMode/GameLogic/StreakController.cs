using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StreakController : MonoBehaviour {
	private Text streakText;
	private AnnouncementManager announcer;
	private int currentStreakAudioIndex = 0;

	public void Start () {
		streakText = gameObject.GetComponentInChildren<Text>();
		announcer = GameObject.FindWithTag("MainCamera").GetComponentInChildren<AnnouncementManager>();
	}

	public void DisplayStreakText() {
		streakText.text = SelectStreakText(GetValidAudioIndex());
		currentStreakAudioIndex++;
		StartCoroutine(FlashMessage());
	}

	public void DisplayTextOnTopOfScreen(string text, int duration) {
		StartCoroutine(FlashMessage(text, duration));
	}

	int GetValidAudioIndex() {
		if(currentStreakAudioIndex >= 7) {
			currentStreakAudioIndex = 0;
		}
		return currentStreakAudioIndex;
	}

	/*
	 Coroutine to handle timing of display text. Overloaded
	 for custom text at top of UI.
	*/
	IEnumerator FlashMessage() {
		streakText.enabled = true;
		yield return new WaitForSecondsRealtime(2);
		HideStreakText();
	}

	IEnumerator FlashMessage(string text, int duration) {
		streakText.text = text;
		streakText.enabled = true;
		yield return new WaitForSecondsRealtime(duration);
		HideStreakText();
	}

	public void HideStreakText() {
		streakText.enabled = false;
	}

	public bool isDisplayed() {
		return streakText.enabled;
	}

	private string SelectStreakText(int index) {
		PlayStreakSound(index);
		switch(index) {
			case 0:
				return "MODULAR!";
			case 1:
				return "THUNKIFIED!";
			case 2:
				return "TOTALLY FUNCTIONAL!";
			case 3:
				return "REFACTORED!";
			case 4:
				return "CLOSURED!";
			case 5:
				return "DISPATCHED!";
			case 6:
				return "GIT COMMITTED!";
			default:
				return "";
		}
	}

	private void PlayStreakSound(int index) {
		announcer.PlayStreakSound(index);
	}

}
