using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Weapon {
	public string weaponName;
	public GameObject wrapper;
}


public class WeaponSelection : MonoBehaviour {

	public Weapon[] weapons;
	public Sprite[] weaponBackgrounds;

	public GameObject gameManager;


	private int currentWeapon = 0;
	private AudioSource audioSource;
	private GamePlayManager gamePlayManager;

	void Start () {
		audioSource = GetComponent<AudioSource> ();
		gamePlayManager = gameManager.GetComponent<GamePlayManager> ();
		selectWeapon (0, false);

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("1"))
			selectWeapon (0);		
		if (Input.GetKeyDown ("2"))
			selectWeapon (1);		
		if (Input.GetKeyDown ("3"))
			selectWeapon (2);		
		if (Input.GetKeyDown ("4"))
			selectWeapon (3);		
	}

	void selectWeapon(int index, bool sound = true){
		currentWeapon = index;
		gamePlayManager.setWeapon (index, weapons [index].weaponName);
		if (sound)			
			audioSource.Play ();
		for(int i=0; i<4; i++){
			if (i == index) {				
				weapons[i].wrapper.GetComponent<SpriteRenderer> ().sprite = weaponBackgrounds [1];
			} else {
				weapons[i].wrapper.GetComponent<SpriteRenderer> ().sprite = weaponBackgrounds [0];
			}
		}
	}

	public void setWeapon(int index){		
		for(int i=0; i<4; i++){
			if (i == index) {				
				weapons[i].wrapper.GetComponent<SpriteRenderer> ().sprite = weaponBackgrounds [1];
			} else {
				weapons[i].wrapper.GetComponent<SpriteRenderer> ().sprite = weaponBackgrounds [0];
			}
		}
	}
}
