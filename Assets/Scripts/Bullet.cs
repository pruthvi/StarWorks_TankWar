using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public GameObject explosion;
	public float collisionRadius = 0.1f;
	public LayerMask WhatToCollideWith;

	private bool collided = false;

	void Start () {
		
	}
	
	//Update is called once per frame
	void Update () {

		collided = Physics2D.OverlapCircle(gameObject.transform.position, collisionRadius, WhatToCollideWith);
		if(collided) {			
			GameObject ex = Instantiate (explosion, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
			Destroy (ex, 0.8f);
			Destroy (gameObject);
		};	
	}

//	void OnCollisionEnter(Collision collision)
//	{
//		Debug.Log ("aaa");
//		GameObject ex = Instantiate (explosion, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
//		Destroy (ex, 0.8f);
//		Destroy (gameObject);
//	}
}
