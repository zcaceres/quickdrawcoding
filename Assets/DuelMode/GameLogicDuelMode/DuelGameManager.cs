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
  private int currentTyperPlayerId = 0;
  public GameObject[] playerUIPanels;
  private List<List<string>> allRounds;

  // CODEBLOCK
  public string codeBlock;
  private TextManager textManager;
  public GameObject currentLetter;
  public string[] currentCodeBlock;
  public GameObject UITextBlock;

  // TYPER DISPLAY
  private TyperDisplayController typerDisplayController;
  public Transform[] typerTransforms;


  void Awake() {
    typerDisplayController = UITextBlock.GetComponent<TyperDisplayController>();
    typerTransforms = GameObject.Find("CurrentLettersDisplay")
      .GetComponent<TyperCurrentLettersController>().roundLetterTransforms;

  }

  void Start() {
    // GetCodeBlockFromFile();
    codeBlock = "private void void void "; // textManager.GetCleanCodeFileAsString();
    DisplayCodeOnTyperUI(currentTyperPlayerId);
    currentCodeBlock = SplitCodeblockIntoLetters();
    Debug.Log(currentCodeBlock.Length);
    allRounds = SetUpDuel(currentCodeBlock);
    Debug.Log("all rounds" + allRounds.Count());
    // Announce Typer Text at top of Typer's UI
    // Announce Firerer Text at top of Firer's UI
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


  void AddCurrentLetterToTyperDisplayBlock(string codeBlock) {
    typerDisplayController.SetTyperTextContent(codeBlock);
  }

  void DisplayCodeOnTyperUI(int playerId) {
    var panelToDisplayCode = playerUIPanels[playerId];
    UITextBlock.transform.SetParent(panelToDisplayCode.transform);
    DisableFirerPanel();
    // find Player's Livecode Transform
    // re-parent currentText block to their transform
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

  }


}
