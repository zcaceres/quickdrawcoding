using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour {
	Text timerText;

	void Start () {
		timerText = transform.Find("Time").GetComponent<Text>();
	}

	public void StartTime(int duration) {
		StartCoroutine(Countdown(duration));
	}

	public void ResetTimerAndStart(int duration) {
		ClearTime();
		StartTime(duration);
	}

	public void ClearTime () {
		StopTime();
		timerText.text = "0";
		timerText.color = Color.white;
	}

	public void StopTime() {
		StopCoroutine("Countdown");
	}

	IEnumerator Countdown(int duration) {
		for (var i = duration; i >= 0; i--) {
			timerText.text = i.ToString();
			if (i <= 2) {
				timerText.color = Color.red;
			}
			yield return new WaitForSecondsRealtime(1);
		}
	}

}
