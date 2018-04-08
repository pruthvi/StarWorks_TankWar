using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectTank : MonoBehaviour {

	private AudioSource audioSource;
	private Animator animator;
	public int selectedTank = 0;

	public Sprite[] barrels;
	public SpriteRenderer tankBarrel;
	public GameObject shaddow;
	public string variableName;


	void Start () {		
		audioSource = GetComponent<AudioSource> ();
		animator = GetComponent<Animator> ();
		displayTank (selectedTank);	
	}
		
	public void nextTank(){	
		selectedTank = (selectedTank + 7) % 6;
		displayTank (selectedTank);
		audioSource.Play ();
	}
	public void previousTank(){
		selectedTank = (selectedTank + 5) % 6;
		displayTank (selectedTank);
		audioSource.Play ();
	}

	public void PlayButton(){
		PlaySound ();
		SceneManager.LoadScene("Scene1");
	}	

	public void displayTank(int selectedTank){
		Debug.Log (selectedTank);
		PlayerPrefs.SetInt (variableName, selectedTank);
		tankBarrel.sprite = (Sprite)barrels [selectedTank];
		animator.SetInteger ("selectedTank", selectedTank);
		if (selectedTank == 0){
			shaddow.SetActive (false);
		} else {
			shaddow.SetActive (true);
		}
	}

	void PlaySound(){		
		audioSource.Play ();
	}
}
