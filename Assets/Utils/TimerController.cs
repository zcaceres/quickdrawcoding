using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour {
	Text timerText;
	private const int DURATION_FOR_HARD = 8;
	private const int DURATION_FOR_MEDIUM = 12;
	private const int DURATION_FOR_EASY = 15;
	public int timeRemaining;


	void Awake() {
		timeRemaining = 10;
	}

	void Start () {
		timerText = transform.Find("Time").GetComponent<Text>();
	}

	public void StartTime(int duration) {
		// Must use named coroutine so StopCoroutine() invokes on correct IEnumerator instance below!
		StartCoroutine("Countdown", duration);
	}

	public void ResetTimerAndStart() {
		ClearTime();
		var duration =  GetRoundTimeFromDifficulty(PlayerPrefs.GetString("difficulty"));
		timeRemaining = duration;
		StartTime(duration);
	}

	public int GetRoundTimeFromDifficulty(string difficulty)
	{
		switch(difficulty) {
			case "easy":
				return DURATION_FOR_EASY;
			case "medium":
				return DURATION_FOR_MEDIUM;
			case "hard":
				return DURATION_FOR_HARD;
			default:
				return 12;
		}
	}

	public void ClearTime () {
		StopTime();
		timerText.text = "0";
		timerText.color = Color.white;
	}

	public void StopTime() {
		timeRemaining = 1; // Prevents defeat from playing on a win!
		StopCoroutine("Countdown");
	}

	IEnumerator Countdown(int duration) {
		for (var i = duration; i >= 0; i--) {
			timerText.text = i.ToString();
			if (i <= 2) {
				timerText.color = Color.red;
			}
			timeRemaining = i;
			yield return new WaitForSecondsRealtime(1);
		}
	}

}
