using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour {
	Text timerText;
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

	public void ResetTimerAndStart(int duration) {
		ClearTime();
		timeRemaining = duration;
		StartTime(duration);
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
