﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringPositionManager : MonoBehaviour {
	public Transform[] firingPositions;
	public int currentPositionIndex;
	public Transform mainCameraTransform;
	public GameManager gameManager;

	void Start () {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		mainCameraTransform = GameObject.FindWithTag("MainCamera").transform.parent.transform;
		firingPositions = this.GetComponentsInChildren<Transform>();
		currentPositionIndex = 0;
	}

	void Update () {
		// If we arrive at our destination, load next round!
		if (AtFiringPosition(mainCameraTransform.position, firingPositions[currentPositionIndex].position)) {
			if (!gameManager.roundStarted && gameManager.currentLetter != null) // Protects again resetting the round after gallery has ended
				gameManager.StartRound();
		}
	}

	public void ToggleNextPosition() {
		if (currentPositionIndex < firingPositions.Length - 1) {
			currentPositionIndex++;
		} else {
			currentPositionIndex = 0;
		}
	}

	private bool AtFiringPosition(Vector3 start, Vector3 end) {
		if (Mathf.Abs(start.x - end.x) < 0.5f && Mathf.Abs(start.y - end.y) < 0.5f && Mathf.Abs(start.z - end.z) < 0.5f) {
			return true;
		} else {
			return false;
		}
	}

}
