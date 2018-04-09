using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour {

	private GameObject gameManager;
	private GamePlayManager gamePlayManager;

	void Start () {
		gameManager = GameObject.Find("GameManager");
		gamePlayManager = gameManager.GetComponent<GamePlayManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void deleteObject(){
		
		gamePlayManager.loadNextPlayer ();
		Destroy (this.gameObject);
	}
}
