using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseController : MonoBehaviour {


	private Canvas canvas;
	// Use this for initialization
	void Start () {
		canvas = GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			// PAUSE THE GAME
			Pause();
		}
	}

	public void Pause()
	{
		canvas.enabled = !canvas.enabled; // Toggle
		Time.timeScale = Time.timeScale == 0 ? 1 : 0; // Toggle
		//ChangeSound();
	}

	public void Restart(){
		Time.timeScale = 1;
		SceneManager.LoadScene("Start");
	}

	public void Quit()
	{
		#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}
