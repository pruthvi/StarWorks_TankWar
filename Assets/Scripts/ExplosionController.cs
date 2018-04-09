using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour {

	public GameObject gameManager;
	private GamePlayManager gamePlayManger;
	void Start () {
		gamePlayManger = gameManager.GetComponent<GamePlayManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void deleteObject(){
		Destroy (this.gameObject);
		gamePlayManger.loadNextPlayer ();
	}
}
