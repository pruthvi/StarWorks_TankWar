using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour {

	public float maxHealth = 100;
	public Slider healthBar;

	private float currentHealth;

	void Start () {
		currentHealth = maxHealth;	
		UpdateHealth ();
	}

//	public void Damage (float damageAmt) {
//		currentHealth -= damageAmt;
//		updateHealth ();
//	}
//
//	public void NewFunction{
//		Debug.Logger("ABC");
//	}

	public void Damage (float damageAmt) {
		currentHealth -= damageAmt;
		//update heathbar slider
		UpdateHealth();
	}

	void UpdateHealth(){
		if (currentHealth < 0) {			
			die ();
		} else {
			healthBar.value = currentHealth / maxHealth;
		}
	}

	void die(){
		Debug.Log ("DIE....!!!");
	}
}