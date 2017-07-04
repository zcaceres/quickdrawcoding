using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyManager : MonoBehaviour {
	private static GameObject DifficultyButtons;
	private static StartButton startButtonManager;
	public static bool isActive = false;

	void Awake()
	{
		DifficultyButtons = gameObject.transform.Find("MenuPanel/DifficultyButtons").gameObject;
		startButtonManager = GetComponent<StartButton>();
	}

	void ShowDifficultyButtons()
	{
		DifficultyButtons.SetActive(true);
		isActive = true;
	}

	public void GoToDifficulty()
	{
		ShowDifficultyButtons();
		startButtonManager.HideLanguageMenu();
	}

	public void GoToLanguages()
	{
		HideDifficultyButtons();
		startButtonManager.ShowLanguageMenu();
	}

	void HideDifficultyButtons()
	{
		DifficultyButtons.SetActive(false);
		isActive = false;
	}

	void SetLanguagePreference(string language)
	{

	}

}
