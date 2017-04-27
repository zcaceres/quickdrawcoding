using System;
﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public string codeBlock;
	public string[] currentCodeBlock;
	public int letterPointer = 0;
	public GameObject letterPrefab;
	public GameObject mainCamera;
	private int CAMERA_ROTATION_SPEED = 5;
	private AudioSource cameraSoundPlayer;
	public Transform[] galleryTransforms;
	public List<GameObject> lettersForRound = new List<GameObject>();
	public GameObject currentLetter;
	private bool roundStarted;

	void Start () {
		mainCamera = GameObject.FindWithTag("MainCamera");
		cameraSoundPlayer = mainCamera.GetComponent<AudioSource>();
		codeBlock = "app Component";
		RetrieveRandomCodeblock();
		// get codeBlock from DB
		currentCodeBlock = SplitCodeblockIntoLetters();
		CreateGalleryOfLetters(currentCodeBlock);
		StartRound();
	}

	public void StartRound() {
		roundStarted = true;
		// letters lerp from ground
		// play starting sound
	}

	private void CreateGalleryOfLetters(string[] codeBlock) {
		var i = 0;
		foreach (string s in codeBlock) {
			var letter = UnityEngine.Object.Instantiate(letterPrefab, galleryTransforms[i++]);
			letter.GetComponent<TextMesh>().text = s;
			lettersForRound.Add(letter);
		}
		currentLetter = lettersForRound[0];
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

	private void CheckKeyboardInput () {
			if (letterPointer >= currentCodeBlock.Length) {
				// if new string
				// TODO: Load a new string and keep going
				// else
				// EndRound();
				return;
			}
			if (Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift)) { return; }
			Debug.Log(Input.inputString);
			var targetLetter = currentCodeBlock[letterPointer];
			if (Input.inputString == targetLetter) {
					ShotHit();
			} else {
				Debug.Log("missed!");
			}
		return;
	}


	void ShotHit() {
		PlayShotSound();
		Destroy(currentLetter);
		letterPointer++;
		lettersForRound.RemoveAt(0);
		if(lettersForRound.Count() != 0) {
			currentLetter = lettersForRound.First();
		} else {
			EndRound();
		}
	}

	void PlayShotSound() {
		cameraSoundPlayer.Play();
	}

	void EndRound() {
		Debug.Log("Round is over!");
		roundStarted = false;
	}

	void snapCameraToNextLetter() {
		mainCamera.transform.rotation =
			Quaternion.Slerp(mainCamera.transform.rotation,
				Quaternion.LookRotation(currentLetter.transform.position - mainCamera.transform.position),
				CAMERA_ROTATION_SPEED*Time.deltaTime);
	}

	void Update () {
		if (!roundStarted) return;

		snapCameraToNextLetter();
		if(Input.anyKeyDown) {
			CheckKeyboardInput();
		}
	}

	private void incrementPoints() {
		// incrementPoints
	}
}
