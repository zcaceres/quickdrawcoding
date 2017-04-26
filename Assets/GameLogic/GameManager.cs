using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public string codeBlock;
	public string[] currentCodeBlock;
	public int letterPointer = 0;
	public GameObject letterPrefab;
	public GameObject mainCamera;
	public GameObject myTestLetter;
	private int CAMERA_ROTATION_SPEED = 5;

	void Start () {
		mainCamera = GameObject.FindWithTag("MainCamera");
		codeBlock = "app Component";
		RetrieveRandomCodeblock();
		// get codeBlock from DB
		currentCodeBlock = SplitCodeblockIntoLetters();
		CreateGalleryOfLetters(currentCodeBlock);
		StartRound();
	}

	public void StartRound() {
		// letters lerp from ground
		// play starting sound
		//
	}

	private void CreateGalleryOfLetters(string[] codeBlock) {
		foreach (string s in codeBlock) {
			var letter = UnityEngine.Object.Instantiate(letterPrefab);
			letter.GetComponent<TextMesh>().text = s;
		}
		// instantiate a prefab for each letter
		// set transforms for each letter at gallery starting points
	}


	private void RetrieveRandomCodeblock() {
		// search library of code to retrieve a component
	}

	private string[] SplitCodeblockIntoLetters() {
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

	private void CheckKeyboardInput () {
			if (letterPointer >= currentCodeBlock.Length) {
				// TODO: Load a new string and keep going
				return;
			}
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

	void snapCameraToNextLetter() {
		mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation,
		Quaternion.LookRotation(myTestLetter.transform.position - mainCamera.transform.position),
		CAMERA_ROTATION_SPEED*Time.deltaTime);
	}

	void Update () {
		snapCameraToNextLetter();
		if(Input.anyKeyDown) {
			CheckKeyboardInput();
			// if (asciiCodeOfKeyPressed == (int)currentTargetCharCode) {
				// Debug.Log("shot hit!");
				// codeLine.pop();
				// destroyLetter()
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
