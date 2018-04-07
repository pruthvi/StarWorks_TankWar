using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuHandler : MonoBehaviour {

	public void StartButton()
	{		
		SceneManager.LoadScene("Select_Tank");
	}

	public void SettingButton()
	{
		Debug.Log("Setting Button Clicked");
		//Application.Quit(); // Quits your game.
	}
}
