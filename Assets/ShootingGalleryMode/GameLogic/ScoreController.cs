using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {
	private Text scoreDisplay;
	private StreakController streakDisplay;
	private int score;

	void Start () {
		scoreDisplay = gameObject.GetComponent<Text>();
		score = 0;
	}

	public void AddPoint() {
		score += 1;
		RenderScore();
	}

	public void RemovePoint() {
		score -= 1;
		RenderScore();
	}

	public void ClearPoints() {
		score = 0;
		RenderScore();
	}

	public int GetPoints() {
		return score;
	}

	void RenderScore() {
		scoreDisplay.text = score.ToString();
	}


}
