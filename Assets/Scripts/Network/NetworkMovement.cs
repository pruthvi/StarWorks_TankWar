using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkMovement : MonoBehaviour {
    public float speed;
    private Rigidbody2D rBody;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rBody = GetComponent<Rigidbody2D>();
        if (moveHorizontal > 0)
        {
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            rBody.velocity = movement * speed;
        }
        else if (moveHorizontal < 0)
        {
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            rBody.velocity = movement * speed;
        }
        else
        {
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            rBody.velocity = movement * speed;
        }
    }
}
