using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public GameObject explosion;
	public float collisionRadius = 0.1f;
	public LayerMask WhatToCollideWith;
	public float damageAmt = 10.0f;

	//private bool collided = false;
	private Collider2D col2D;

	void Start () {
		
	}
	
	//Update is called once per frame
	void Update () {

		col2D = Physics2D.OverlapCircle(gameObject.transform.position, collisionRadius, WhatToCollideWith);


		if(col2D != null)
        {			
			GameObject ex = Instantiate (explosion, gameObject.transform.position, gameObject.transform.rotation) as GameObject;

			Destroy (gameObject);

            if (col2D.tag == "Player")
            {				
				col2D.GetComponent<TankController> ().Damage (damageAmt);
			}
		};	
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Dobject")
        {
            GameObject ex = Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation) as GameObject;

            Destroy(this.gameObject);
        }
    }



    //	void OnCollisionEnter(Collision collision)
    //	{
    //		Debug.Log ("aaa");
    //		GameObject ex = Instantiate (explosion, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
    //		Destroy (ex, 0.8f);
    //		Destroy (gameObject);
    //	}
}
