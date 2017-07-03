using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour {
	private GameObject LanguageButtons;
	private GameObject Title;
	private GameObject MainMenu;

	void Awake()
	{
		LanguageButtons = gameObject.transform.Find("MenuPanel/LanguageButtons").gameObject;
		Title = gameObject.transform.Find("MenuPanel/Title").gameObject;
		MainMenu = gameObject.transform.Find("MenuPanel/MenuButtons").gameObject;
	}

	public void ShowLanguageMenu()
	{
		LanguageButtons.SetActive(true);
		HideMainMenu();
	}

	void HideLanguageMenu()
	{
		LanguageButtons.SetActive(false);
		ShowMainMenu();
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

	void SetLanguagePreference(string languagePref)
	{

	}


}
