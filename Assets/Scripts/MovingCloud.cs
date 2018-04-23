using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCloud : MonoBehaviour {


	
	void Start () {

        float speed = Random.Range(0.0f, 2.0f);
        Vector2 movement = new Vector2(-1, 0);

        this.GetComponent<Rigidbody2D>().velocity = movement * speed ;

        //        Debug.Log("Cloud Speed :" + speed);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            Destroy(this.gameObject);
    }


}
