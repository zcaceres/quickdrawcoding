using System;
﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using EZCameraShake;

public class DuelGameManager : MonoBehaviour {
  private const float PROBABILITY_OF_FIRE_OPPORTUNITY = 0.6f;
  private const int DURATION_OF_TURN = 8;
  public int letterPointer = 0;
	public int lettersDestroyed = 0;
  private int currentTyperPlayerId = 0;
  public GameObject[] playerUIPanels;
  private List<List<string>> allRounds;
  public GameObject letterPrefab;
  private bool isFiringOpportunityForFirer;
  private bool isFiringOpportunityForTyper;
  public bool roundStarted;
  private bool gameOver;
  private bool listenForGameOverInput;

  // CODEBLOCK
  public string codeBlock;
  private DuelTextManager textManager;
  public GameObject currentLetter;
  public string[] currentCodeBlock;
  public GameObject UITextBlock;

  // AUDIO
  public DuelAudioManager duelAudioManager;
  public AudioSource ambientMusic;

  // UI & TYPER DISPLAY
  private TyperDisplayController typerDisplayController;
  private TyperCurrentLettersController typerCurrentLettersController;
  public Transform[] typerTransforms;
  public List<GameObject> lettersForRound = new List<GameObject>();
  public FireIndicatorController[] fireIndicatorControllers;
  public RoleIndicatorController[] roleIndicatorControllers;
  public DeathIndicatorController[] deathIndicatorControllers;
  public WinIndicatorController[] winIndicatorControllers;
  private ReloadController reloadNotifier;

  // TIMER
  private TimerController timerController;


  void Awake() {
    textManager = GetComponent<DuelTextManager>();
    typerDisplayController = UITextBlock.GetComponent<TyperDisplayController>();
    typerCurrentLettersController = GameObject.Find("CurrentLettersDisplay").GetComponent<TyperCurrentLettersController>();
    typerTransforms = typerCurrentLettersController.roundLetterTransforms;
    reloadNotifier = GameObject.Find("Canvas/BottomNotification").GetComponentInChildren<ReloadController>();
    timerController = GameObject.Find("Canvas/Timer").GetComponent<TimerController>();
  }

  void Start() {
    roundStarted = false;
    codeBlock = textManager.GetCleanCodeFileAsString(); //"private void void void void void void void void void void void void void void void void void void ";
    currentCodeBlock = SplitCodeblockIntoLetters();
    allRounds = SetUpDuel(currentCodeBlock);
    StartCoroutine(GetReady());
  }



  IEnumerator GetReady() {
    ambientMusic.Play();
    roleIndicatorControllers[currentTyperPlayerId].ShowPrepareToType();
    roleIndicatorControllers[GetFirerPlayerId()].ShowPrepareToFire();
    yield return new WaitForSeconds(5);
    roleIndicatorControllers[currentTyperPlayerId].HideRoleText();
    roleIndicatorControllers[GetFirerPlayerId()].HideRoleText();
    yield return new WaitForSeconds(2);
    DisplayCodeOnTyperUI(currentTyperPlayerId);
    SetUpRound(allRounds);
    StartRound();
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

  void StartRound() {
    roundStarted = true;
    timerController.ResetTimerAndStart(DURATION_OF_TURN);
  }

  void EndRound() {
    roundStarted = false;
    timerController.StopTime();
    if (allRounds.Count() > 0) {
      SetUpRound(allRounds);
      StartCoroutine("SwitchTurns");
    } else {
      // StartCoroutine(EndGame()); // END GAME HERE
    }
  }

  private IEnumerator SwitchTurns() {
    HideCodeOnTyperUI(currentTyperPlayerId);
    SetCurrentTyperPlayerId();
    roleIndicatorControllers[currentTyperPlayerId].ShowPrepareToType();
    roleIndicatorControllers[GetFirerPlayerId()].ShowPrepareToFire();
    yield return new WaitForSeconds(5);
    roleIndicatorControllers[currentTyperPlayerId].HideRoleText();
    roleIndicatorControllers[GetFirerPlayerId()].HideRoleText();
    DisplayCodeOnTyperUI(currentTyperPlayerId);
    StartRound();
  }


  private void SetCurrentTyperPlayerId() {
    currentTyperPlayerId = currentTyperPlayerId == 0 ? 1 : 0;
  }

  private int GetFirerPlayerId() {
    return currentTyperPlayerId == 0 ? 1 : 0;
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
      textComponent.text = " ";
    }
    else if (textComponent.text == "\n") {
      Debug.Log("NEWLINE CHAR!");
      reloadNotifier.DisplayReload();
    }
    // set current letter to lerp up and down a little!
  }


  void DisplayCodeOnTyperUI(int playerId) {
    var panelToDisplayCode = playerUIPanels[playerId];
    UITextBlock.transform.SetParent(panelToDisplayCode.transform, false);
    panelToDisplayCode.SetActive(true);
    panelToDisplayCode.GetComponentInChildren<Image>().enabled = true;
    panelToDisplayCode.transform.Find("TypingBackground").GetComponent<Image>().enabled = true;
    typerCurrentLettersController.transform.SetParent(panelToDisplayCode.transform, false);
    typerCurrentLettersController.gameObject.SetActive(true);
  }

  void HideCodeOnTyperUI(int playerId) {
    var panelToDisplayCode = playerUIPanels[playerId];
    typerCurrentLettersController.gameObject.SetActive(false);
    panelToDisplayCode.GetComponentInChildren<Image>().enabled = false;
    panelToDisplayCode.transform.Find("TypingBackground").GetComponent<Image>().enabled = false;
    panelToDisplayCode.SetActive(false);
  }

  void EndGame() {
    gameOver = true;
    timerController.StopTime();
    Debug.Log("GAME OVER " + gameOver);
    StartCoroutine(GameOver());
  }

  IEnumerator GameOver() {
    yield return new WaitForSeconds(3);
    listenForGameOverInput = true;
  }

  void Update() {
    if(Input.GetKeyDown(KeyCode.Escape)) {
      SceneManager.LoadSceneAsync(0);
    }

    if(listenForGameOverInput && Input.GetKeyDown(KeyCode.Space)) {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    if(gameOver) { return; }
    if(!roundStarted) return;

    if(timerController.timeRemaining == 0) {
      gameOver = true;
      KillTyper();
    }

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
      if (Input.GetKeyDown(KeyCode.Space) && isFiringOpportunityForTyper) {
        KillFirer();
      }
      duelAudioManager.PlayTypewriter();
    	var targetLetter = currentCodeBlock[letterPointer];
    	if (Input.inputString == targetLetter) {
    		if (Input.inputString == "\n") {
          // TODO: DEAL WITH NEW LINES
          CorrectLetter();
    		} else {
    			CorrectLetter();
    		}
    	} else {
        if(UnityEngine.Random.Range(0.0f, 1.0f) <= PROBABILITY_OF_FIRE_OPPORTUNITY) {
          IncorrectLetter();
        }
    	}
    return;
  }

  void CorrectLetter() {
    AddCurrentLetterToTyperDisplayBlock(currentLetter.GetComponent<Text>().text);
    if (reloadNotifier.isDisplayed()) reloadNotifier.HideReload(); // Makes sure Reload is toggled off after hitting a space
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
    StartCoroutine("PlayDeathSequence", currentTyperPlayerId);
  }

  private void KillFirer() {
    StartCoroutine("PlayDeathSequence", GetFirerPlayerId());
  }

  IEnumerator PlayDeathSequence(int playerId) {
    ambientMusic.Stop();
    duelAudioManager.PlayGunshot();
    yield return new WaitForSeconds(.2f);
    duelAudioManager.PlayBell();
    deathIndicatorControllers[playerId].ShowDeathScreen();
    var otherPlayer = playerId == 0 ? 1 : 0;
    HideCodeOnTyperUI(currentTyperPlayerId);
    winIndicatorControllers[otherPlayer].ShowWinText();
    EndGame();
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
    CheckWhoFired(playerId);
    DisplayFireText(playerId, true);
    yield return new WaitForSeconds(.1f); // TODO: optimize display time here
    ResetFiringOpportunities();
    DisplayFireText(playerId, false);
  }

  void CheckWhoFired(int playerId) {
    if (playerId != currentTyperPlayerId) {
      isFiringOpportunityForFirer = true;
    } else {
      isFiringOpportunityForTyper = true;
    }
  }

  void ResetFiringOpportunities() {
    isFiringOpportunityForFirer = false;
    isFiringOpportunityForTyper = false;
  }

  void DisplayFireText(int playerId, bool toDisplay) {
    fireIndicatorControllers[playerId].DisplayFireText(toDisplay);
  }


}
