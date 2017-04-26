using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public string codeBlock;
	public string[] currentCodeBlock;
	public int letterPointer = 0;

	void Start () {
		codeBlock = "app Component";
		retrieveRandomCodeblock();
		// get codeBlock from DB
		currentCodeBlock = splitCodeblockIntoLetters();
		createGalleryOfLetters();
		startRound();
	}

	public void startRound() {
		// letters lerp from ground
		// play starting sound
		//
	}

	private void createGalleryOfLetters() {
		// instantiate a prefab for each letter
		// set transforms for each letter at gallery starting points
	}


	private void retrieveRandomCodeblock() {
		// search library of code to retrieve a component
	}

	private string[] splitCodeblockIntoLetters() {
		string[] chars = new string[codeBlock.Length];
		for (var i = 0; i < codeBlock.Length; i++) {
			chars[i] = codeBlock[i].ToString();
		}
		return chars;
	}



		// Debug.Log("codeLine Length " + codeLine.Length);
		// codeLine = codeBlock.Split(separator);
		// for (var i = 0; i < codeBlock.Length; i++) {
		// 	codeLine[i] = codeBlock[i].ToString();
		// }
		// Debug.Log("codeLine " + codeLine);
		// string manipulation on codeblock to split into array of chars
		// watch for spaces
	// }

	private void checkKeyboardInput () {
			if (letterPointer >= currentCodeBlock.Length) { return; }
			if (Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift)) { return; }
			Debug.Log(Input.inputString);
			var targetLetter = currentCodeBlock[letterPointer];
			if (Input.inputString == targetLetter) {
				Debug.Log("Shot hit!");
				letterPointer++;
			} else {
				Debug.Log("missed!");
			}
		return;
	}

	void Update () {
		if(Input.anyKeyDown) {
			checkKeyboardInput();
			// if (asciiCodeOfKeyPressed == (int)currentTargetCharCode) {
				// Debug.Log("shot hit!");
				// codeLine.pop();
				// destroyLetter()
				// snapCameraToNextLetter();
				// incrementPoints()
				// increment points
			// } else {
				// destroyStreak
			// }
		}
	}

	private void incrementPoints() {
		// incrementPoints
	}
}
