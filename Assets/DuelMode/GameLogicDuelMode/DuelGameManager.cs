using System;
﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using EZCameraShake;

public class DuelGameManager : MonoBehaviour {
  public int letterPointer = 0;
	public int lettersDestroyed = 0;
  private int currentTyperPlayerId = 0;
  public GameObject[] playerUIPanels;
  private List<List<string>> allRounds;
  public GameObject letterPrefab;
  private bool isFiringOpportunityForFirer;
  private bool isFiringOpportunityForTyper;


  // CODEBLOCK
  public string codeBlock;
  private TextManager textManager;
  public GameObject currentLetter;
  public string[] currentCodeBlock;
  public GameObject UITextBlock;

  // UI & TYPER DISPLAY
  private TyperDisplayController typerDisplayController;
  private TyperCurrentLettersController typerCurrentLettersController;
  public Transform[] typerTransforms;
  public List<GameObject> lettersForRound = new List<GameObject>();
  public FireIndicatorController[] fireIndicatorControllers;



  void Awake() {
    typerDisplayController = UITextBlock.GetComponent<TyperDisplayController>();
    typerCurrentLettersController = GameObject.Find("CurrentLettersDisplay").GetComponent<TyperCurrentLettersController>();
    typerTransforms = typerCurrentLettersController.roundLetterTransforms;

  }

  void Start() {
    // GetCodeBlockFromFile();
    codeBlock = "private void void void void void void void void void void void void void void void void void void "; // textManager.GetCleanCodeFileAsString();
    currentCodeBlock = SplitCodeblockIntoLetters();
    allRounds = SetUpDuel(currentCodeBlock);
    SetUpRound(allRounds);
  }

  private string[] SplitCodeblockIntoLetters() {
		Debug.Log("inside split code block into letters");
		string[] chars = new string[codeBlock.Length];
		for (var i = 0; i < codeBlock.Length; i++) {
			chars[i] = codeBlock[i].ToString();
		}
		return chars;
	}

  private List<List<string>> SetUpDuel(string[] codeBlock) {
    Debug.Log("typer transform " + typerTransforms.Length);
    // scoreController.ClearPoints();
    var rounds = new List<List<string>>();
    List<string> round = new List<string>();
    var i = 0;
    var totalLettersProcessed = 0;
    while (i < codeBlock.Length) {
      if (round.Count() < typerTransforms.Length) {
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
    if (totalLettersProcessed < typerTransforms.Length) {
      rounds.Add(round);
    }
    return rounds;
  }

  /*  ROUND ROUND ROUND ROUND ROUND ROUND ROUND ROUND ROUND ROUND  */
  private void SetUpRound(List<List<string>> rounds) {
    if(rounds.Count() > 0 && rounds.First() != null) {
      InstantiateLetters(rounds.First());
      rounds.RemoveAt(0);
    }
  }

  void EndRound() {
    // roundStarted = false;
    if (allRounds.Count() > 0) {
      SetUpRound(allRounds);
      StartCoroutine("SwitchTurns");
    } else {
      // StartCoroutine(EndGame()); // END GAME HERE
    }
  }

  private IEnumerator SwitchTurns() {
    SetCurrentTyperPlayerId();
    // DISPLAY PLAYER ROLES
    yield return new WaitForSeconds(.01f); // REMOVE THIS
    DisplayCodeOnTyperUI(currentTyperPlayerId);
    // StartRound();
  }


  void SetCurrentTyperPlayerId() {
    currentTyperPlayerId = currentTyperPlayerId == 0 ? 1 : 0;
  }

  void DisplayPlayerRoles() {
    // Announce Typer Text at top of Typer's UI
    // Announce Firerer Text at top of Firer's UI
  }

  private void InstantiateLetters(List<string> round) {
    var i = 0;
    foreach (string s in round) {
      var letter = UnityEngine.Object.Instantiate(letterPrefab, typerTransforms[i++]);
      letter.GetComponent<Text>().text = s;
      lettersForRound.Add(letter);
    }
    currentLetter = lettersForRound[0];
    SetCurrentLetterColor(currentLetter);
  }

  void SetCurrentLetterColor(GameObject letter) {
    var textComponent = letter.GetComponent<Text>();
    textComponent.color = Color.red;
    textComponent.fontSize = 38;
    if (textComponent.text == " ") {
      textComponent.text = " "; // TODO: Change to a better character
    }
    else if (textComponent.text == "\n") {
      Debug.Log("NEWLINE CHAR!");
      // reloadNotifier.DisplayReload();
    }
    // set current letter to lerp up and down a little!
  }


  void DisplayCodeOnTyperUI(int playerId) {
    var panelToDisplayCode = playerUIPanels[playerId];
    UITextBlock.transform.SetParent(panelToDisplayCode.transform, false);
    panelToDisplayCode.GetComponent<Image>().enabled = true;
    typerCurrentLettersController.transform.SetParent(panelToDisplayCode.transform, false);
    DisableFirerPanel();
  }

  void DisableFirerPanel() {
    var panelToDisableIndex = (currentTyperPlayerId == 0 ? 1 : 0);
    playerUIPanels[panelToDisableIndex].GetComponent<Image>().enabled = false;
  }

  void EndGame() {
    //
    // if code block is complete, trigger DRAW

  }

  void Update() {
    // Exit KeyCode check here
    if(Input.anyKeyDown) {
      CheckMouseInput();
      CheckKeyboardInput();
    }

  }

  private void CheckMouseInput() {
    if(Input.GetKeyDown(KeyCode.Mouse0)) {
      if(isFiringOpportunityForFirer) {
        KillTyper();
      } else {
        IncorrectClick();
      }
    }
  }

  /* INPUT INPUT INPUT INPUT INPUT INPUT INPUT INPUT INPUT INPUT INPUT INPUT */
  private void CheckKeyboardInput () {
    	if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift)) { return; }
      if (Input.GetKeyDown(Key Code.Space) && isFiringOpportunityForTyper) {
        KillFirer();
      }
    	var targetLetter = currentCodeBlock[letterPointer];
    	if (Input.inputString == targetLetter) {
    		// if (Input.inputString == "\n") {
    		// 	PlayReloadSound();
    		// 	ShotHit();
    		// } else {
    		// 	PlayShotShakeAnim();
    		// 	PlayShotHitSound();
    			CorrectLetter();
    		// }
    	} else {
    		IncorrectLetter();
    	}
    return;
  }


  //If out of time, let other player execute typer :)

  void CorrectLetter() {
    // scoreController.AddPoint();
    // currentLetter.GetComponent<GenerateHitOrMiss>().GenerateHitPrefab();
    AddCurrentLetterToTyperDisplayBlock(currentLetter.GetComponent<Text>().text);
    // if (reloadNotifier.isDisplayed()) reloadNotifier.HideReload(); // Makes sure Reload is toggled off after hitting a space
    Destroy(currentLetter);
    letterPointer++;
    lettersDestroyed++;
    lettersForRound.RemoveAt(0);
    if(lettersForRound.Count() != 0) {
      currentLetter = lettersForRound.First();
      SetCurrentLetterColor(currentLetter);
    } else {
      EndRound();
    }
  }

  private void KillTyper() {
    Debug.Log("killed typer!");
  }

  private void KillFirer() {
    Debug.Log("Killed firer");
  }

  void AddCurrentLetterToTyperDisplayBlock(string letter) {
    typerDisplayController.SetTyperTextContent(letter);
  }

  void IncorrectLetter() {
    var firerID = currentTyperPlayerId == 0 ? 1 : 0;
    StartCoroutine("DisplayOpportunityToFire", firerID);
  }

  void IncorrectClick() {
    StartCoroutine("DisplayOpportunityToFire", currentTyperPlayerId);
  }

  IEnumerator DisplayOpportunityToFire(int playerId) {
    if (playerId != currentTyperPlayerId) {
      isFiringOpportunityForFirer = true;
    } else {
      isFiringOpportunityForTyper = true;
    }
    DisplayFireText(playerId, true);
    yield return new WaitForSeconds(.3f);
    isFiringOpportunityForFirer = false;
    isFiringOpportunityForTyper = false;
    DisplayFireText(playerId, false);
  }

  void DisplayFireText(int playerId, bool toDisplay) {
    fireIndicatorControllers[playerId].DisplayFireText(toDisplay);
  }


}
