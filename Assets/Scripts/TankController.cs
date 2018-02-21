using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour {

	public enum Direction {left, right };

	public float maxSpeed = 10f;
	public Direction direction = 0;
	public Transform exhaustPointLeft;
	public Transform exhaustPointRight;
	public GameObject exhaustFumeFrefab;

	private Rigidbody2D rBody;
	private SpriteRenderer sRend;
	private Animator animator;

	private GameObject exhaustFume;

	void Start () {
		rBody = this.GetComponent<Rigidbody2D>();
		sRend = this.GetComponent<SpriteRenderer>();
		animator = this.GetComponent<Animator>();	
		exhaustFume = Instantiate (exhaustFumeFrefab, exhaustPointLeft.position, exhaustPointLeft.rotation);		
	}
	

	void FixedUpdate () {
		float moveHoriz = Input.GetAxis("Horizontal");

		rBody.velocity = new Vector2(moveHoriz * maxSpeed, rBody.velocity.y);	

		animator.SetFloat("Speed", Mathf.Abs(moveHoriz));
		exhaustFume.GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(moveHoriz));

		if (moveHoriz > 0) {
			sRend.flipX = false;
			direction = Direction.left;
			displayExhauseFume (exhaustPointLeft);
		} else if (moveHoriz < 0) {			
			sRend.flipX = true;	
			direction = Direction.right;
			displayExhauseFume(exhaustPointRight);
		} else {
			exhaustFume.GetComponent<SpriteRenderer> ().enabled = false;
		}
	}

	void displayExhauseFume(Transform exhausePoint){
		bool flip = direction == Direction.left ? false : true;
		exhaustFume.GetComponent<SpriteRenderer> ().flipX = flip;
		exhaustFume.GetComponent<SpriteRenderer> ().enabled = true;
		exhaustFume.transform.position = exhausePoint.position;
		exhaustFume.transform.rotation = exhausePoint.rotation;
	}
}
