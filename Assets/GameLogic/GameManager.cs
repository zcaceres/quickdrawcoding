using System;
﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

public class GameManager : MonoBehaviour {

	public string codeBlock;
	public string[] currentCodeBlock;
	public int letterPointer = 0;
	public int lettersDestroyed = 0;
	public GameObject letterPrefab;
	public GameObject mainCamera;
	private const int CAMERA_ROTATION_SPEED = 5;
	private const float PLAYER_MOVEMENT_SPEED = 2f;
	private AudioSource cameraSoundPlayer;
	public Transform[] galleryTransforms;
	public List<GameObject> lettersForRound = new List<GameObject>();
	public GameObject currentLetter;
	public bool roundStarted;
	private List<List<string>> allRounds;
	public AudioClip[] shotClips;
	private EZCameraShake.CameraShaker cameraShaker;
	private AnnouncementManager announcer;

	private Text UICodeDisplay;
	private ReloadController reloadNotifier;
	private StreakController streakNotifier;
	private ScoreController scoreController;

	private MuzzleFlash muzzleFlasher;

	private FiringPositionManager firingPositionManager;
	private Transform[] firingPositions;

	void Start () {
		SetUpComponents();
		codeBlock = "private void void void void void void void void void void void void void void void void void void void void void ";
		// get codeBlock from DB
		RetrieveRandomCodeblock();
		currentCodeBlock = SplitCodeblockIntoLetters();
		allRounds = SetUpGallery(currentCodeBlock);
		SetUpRound(allRounds);
		Debug.Log("ROUNDS " + allRounds.Count() + " for codeblock " + codeBlock);
		StartRound();
	}

	void Update () {
		if (!roundStarted) return;

		SnapCameraToNextLetter();
		MovePlayer();

		if(Input.anyKeyDown) {
			CheckKeyboardInput();
		}
	}

	void MovePlayer() {
		var targetPosition = firingPositionManager.firingPositions[firingPositionManager.currentPositionIndex].position;
		mainCamera.transform.parent.transform.position =
			Vector3.Slerp(mainCamera.transform.parent.transform.position,
				targetPosition,
				PLAYER_MOVEMENT_SPEED*Time.deltaTime); // Must refer to PARENT of mainCamera for shake to work!!
	}

	// GALLERY
	private string[] SplitCodeblockIntoLetters() {
		string[] chars = new string[codeBlock.Length];
		for (var i = 0; i < codeBlock.Length; i++) {
			chars[i] = codeBlock[i].ToString();
		}
		return chars;
	}

	private List<List<string>> SetUpGallery(string[] codeBlock) {
		scoreController.ClearPoints();
		var rounds = new List<List<string>>();
		List<string> round = new List<string>();
		var i = 0;
		var totalLettersProcessed = 0;
		while (i < codeBlock.Length) {
			if (round.Count() < galleryTransforms.Length) {
				round.Add(codeBlock[i]);
			} else {
				rounds.Add(round);
				round = new List<string>();
				round.Add(codeBlock[i]);
			}
			i++;
			totalLettersProcessed++;
		}
		/*
			Handles edge case where codeblock string is smaller than a single round
			through all transforms in the gallery.
		*/
		if (totalLettersProcessed < galleryTransforms.Length) {
			rounds.Add(round);
		}
		return rounds;
	}

	private void EndGallery() {
		// roundStarted = false;
		announcer.PlayWinSound();
		// display accuracy
		// display final program
	}

	// ROUND
	private void SetUpRound(List<List<string>> rounds) {
		if(rounds.Count() > 0 && rounds.First() != null) {
			InstantiateLetters(rounds.First());
			rounds.RemoveAt(0);
		}
	}

	private void InstantiateLetters(List<string> round) {
		var i = 0;
		foreach (string s in round) {
			var letter = UnityEngine.Object.Instantiate(letterPrefab, galleryTransforms[i++]);
			letter.GetComponent<TextMesh>().text = s;
			lettersForRound.Add(letter);
		}
		currentLetter = lettersForRound[0];
		SetCurrentLetterColor(currentLetter);
	}

	public void StartRound() {
		roundStarted = true;
		RenderLetters();
		// letters lerp from ground
		// play starting sound BEGIN!!
	}


	void EndRound() {
		roundStarted = false;
		if (allRounds.Count() > 0) {
			SetUpRound(allRounds);
			StartRound();
			firingPositionManager.ToggleNextPosition(); // Go to next position!
		} else {
			EndGallery();
		}
	}

	// GameLogic inside round
	void SetCurrentLetterColor(GameObject letter) {
		var textMesh = letter.GetComponent<TextMesh>();
		textMesh.color = Color.red;
		textMesh.characterSize = .13f;
		if (textMesh.text == " ") {
			reloadNotifier.DisplayReload();
		}
		// set current letter to lerp up and down a little!
	}

	private void RetrieveRandomCodeblock() {
		// search library of code to retrieve a component
	}

	private void CheckKeyboardInput () {
			if (Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift)) { return; }
			var targetLetter = currentCodeBlock[letterPointer];
			if (Input.inputString == targetLetter) {
				ShotHit();
			} else {
				ShotMissed();
			}
		return;
	}

	void PlayShotShakeAnim() {
		cameraShaker.Shake(EZCameraShake.CameraShakePresets.Bump);
		// muzzleFlasher.TriggerFlash(); COME BACK TO THIS LATER...
	}

	void ShotMissed() {
		PlayShotShakeAnim();
		PlayShotMissedSound();
		scoreController.RemovePoint();
		currentLetter.GetComponent<GenerateHitOrMiss>().GenerateMissPrefab();
		// lose points
		// stop streak
	}

	void ShotHit() {
		PlayShotShakeAnim();
		PlayShotHitSound();
		scoreController.AddPoint();
		currentLetter.GetComponent<GenerateHitOrMiss>().GenerateHitPrefab();
		AddLetterToUI(currentLetter.GetComponent<TextMesh>().text);
		if (reloadNotifier.isDisplayed()) reloadNotifier.HideReload(); // Makes sure Reload is toggled off after hitting a space
		Destroy(currentLetter);
		letterPointer++;
		lettersDestroyed++;
		lettersForRound.RemoveAt(0);
		if(lettersForRound.Count() != 0) {
			currentLetter = lettersForRound.First();
			SetCurrentLetterColor(currentLetter);
			if (lettersDestroyed % 14 == 0) {
				RevealNextWave();
			}
		} else {
			EndRound();
		}
	}

	void RevealNextWave() {
		RenderLetters();
		PlayStreak();
		firingPositionManager.ToggleNextPosition();
	}

	void RenderLetters() {
		for (var i = 0; i < 14; i++) {
			if (lettersForRound.Count() > i)
				lettersForRound[i].GetComponent<MeshRenderer>().enabled = true;
		}
	}

	void AddLetterToUI(string letter) {
		UICodeDisplay.text += letter;
	}

	void PlayShotHitSound() {
		cameraSoundPlayer.clip = shotClips[UnityEngine.Random.Range(0, shotClips.Length - 1)];
		cameraSoundPlayer.Play();
	}

	void PlayShotMissedSound() {
		cameraSoundPlayer.clip = shotClips[shotClips.Length -1];
		cameraSoundPlayer.Play();
	}

	void SnapCameraToNextLetter() {
		if (currentLetter == null) return; // protects against snapping to null letter after galery is over
		mainCamera.transform.parent.transform.rotation =
			Quaternion.Slerp(mainCamera.transform.parent.transform.rotation,
				Quaternion.LookRotation(currentLetter.transform.position - mainCamera.transform.parent.transform.position),
				CAMERA_ROTATION_SPEED*Time.deltaTime); // Must refer to PARENT of mainCamera for shake to work!!
	}


	private void incrementPoints() {
		// incrementPoints
	}

	private void PlayStreak() {
		streakNotifier.DisplayStreakText();
	}

	private void PlayWin() {
		// play sound and play text animation
	}

	private void PlayLose() {

	}

	void SetUpComponents() {
		mainCamera = GameObject.FindWithTag("MainCamera");
		muzzleFlasher = mainCamera.GetComponentInChildren<MuzzleFlash>();
		announcer = mainCamera.GetComponentInChildren<AnnouncementManager>();
		galleryTransforms = GameObject.Find("Gallery").GetComponent<GalleryManager>().allGalleryTransforms;
		cameraShaker = mainCamera.GetComponent<CameraShaker>();
		cameraSoundPlayer = mainCamera.GetComponent<AudioSource>();
		firingPositionManager = GameObject.Find("FiringPositions").GetComponent<FiringPositionManager>();
		firingPositions = firingPositionManager.firingPositions;
		UICodeDisplay = GameObject.Find("Canvas/LiveCodeSnippet").GetComponentInChildren<Text>();
		reloadNotifier = GameObject.Find("Canvas/BottomNotification").GetComponentInChildren<ReloadController>();
		streakNotifier = GameObject.Find("Canvas/TopNotification/StartingPoint").GetComponent<StreakController>();
		scoreController = GameObject.Find("Canvas/ScoreTracker").GetComponent<ScoreController>();
	}

}
