using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour {
	private static GameObject LanguageButtons;
	private static GameObject Title;
	private static GameObject MainMenu;
	private static DifficultyManager difficultyManager;
	private static bool isActive = false;

	void Awake()
	{
		LanguageButtons = gameObject.transform.Find("MenuPanel/LanguageButtons").gameObject;
		Title = gameObject.transform.Find("MenuPanel/Title").gameObject;
		MainMenu = gameObject.transform.Find("MenuPanel/MenuButtons").gameObject;
		difficultyManager = GetComponent<DifficultyManager>();
		isActive = false;
	}

	public void GoToLanguages()
	{
		ShowLanguageMenu();
		HideMainMenu();
	}

	public void GoToMainMenu()
	{
		ShowMainMenu();
		HideLanguageMenu();
	}

	public void ShowLanguageMenu()
	{
		LanguageButtons.SetActive(true);
		isActive = true;
	}

	public void HideLanguageMenu()
	{
		LanguageButtons.SetActive(false);
		isActive = false;
	}

	void ShowMainMenu()
	{
		Title.SetActive(true);
		MainMenu.SetActive(true);
	}

	void HideMainMenu()
	{
		Title.SetActive(false);
		MainMenu.SetActive(false);
	}



}
