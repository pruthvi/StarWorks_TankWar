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
	public GameObject barrel;
	public GameObject bulletFrefab;
	public GameObject tankShotFrefab;
	public Transform shootingPointLeft;
	public Transform shootingPointRight;
	public Transform TankShotPositionLeft;
	public Transform TankShotPositionRight;
	public float bulletForce = 700.0f;



	private Rigidbody2D rBody;
	private SpriteRenderer sRend;
	private Animator animator;
	private GameObject exhaustFume;


	private AudioSource tankMove;
	private AudioSource barrelMove;

	void Start () {
		rBody = this.GetComponent<Rigidbody2D>();
		sRend = this.GetComponent<SpriteRenderer>();
		animator = this.GetComponent<Animator>();	
		exhaustFume = Instantiate (exhaustFumeFrefab, exhaustPointLeft.position, exhaustPointLeft.rotation);			

		AudioSource[] allMyAudioSources = GetComponents<AudioSource>();
		tankMove = allMyAudioSources[0];
		barrelMove = allMyAudioSources[1];
	}
	

	void FixedUpdate () {
		//Moving tank with left/right keys
		float moveHoriz = Input.GetAxis("Horizontal");

		rBody.velocity = new Vector2(moveHoriz * maxSpeed, rBody.velocity.y);	

		animator.SetFloat("Speed", Mathf.Abs(moveHoriz));
		exhaustFume.GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(moveHoriz));

		if (moveHoriz > 0) {						
			changeDirrection (Direction.left);
			displayExhauseFume (exhaustPointLeft);
			if (!tankMove.isPlaying) tankMove.Play ();
			
		} else if (moveHoriz < 0) {						
			changeDirrection (Direction.right);
			displayExhauseFume(exhaustPointRight);
			if (!tankMove.isPlaying) tankMove.Play ();
		} else {
			if (tankMove.isPlaying) tankMove.Stop ();
			exhaustFume.GetComponent<SpriteRenderer> ().enabled = false;
		}

		// Moving the barrel with up/down keys
		float moveVertical = Input.GetAxis("Vertical");

		if (direction == Direction.left) 
			barrel.transform.Rotate (new Vector3 (0, 0, moveVertical));
		else 
			barrel.transform.Rotate (new Vector3 (0, 0, -moveVertical));				

		if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.DownArrow)) {
			if (!barrelMove.isPlaying) barrelMove.Play ();			
		} else {
			barrelMove.Stop ();
		}

		if (Input.GetButtonDown ("Fire1") || Input.GetKeyDown (KeyCode.Space))
			shoot ();
	}

	void shoot(){		
		Transform shootingPoint = direction == Direction.left ? shootingPointLeft : shootingPointRight;
		Transform tankShotPoint = direction == Direction.left ? TankShotPositionLeft : TankShotPositionRight;
		GameObject bullet = Instantiate (bulletFrefab, shootingPoint.position, shootingPoint.rotation);		
		GameObject tankShot = Instantiate (tankShotFrefab, tankShotPoint.position, tankShotPoint.rotation);		
		Destroy (tankShot, 0.4f);
		bullet.GetComponent<Rigidbody2D> ().AddForce (bullet.transform.right * bulletForce);
	}

	void changeDirrection(Direction facingDirection){
		if (direction != facingDirection) {
			barrel.transform.Rotate(new Vector3(0,0,-barrel.transform.eulerAngles.z*2));
		}
		direction = facingDirection;
		bool flip = direction == Direction.left ? false : true;
		sRend.flipX = flip;
		barrel.GetComponent<SpriteRenderer> ().flipX = flip;
	}

	void displayExhauseFume(Transform exhausePoint){
		bool flip = direction == Direction.left ? false : true;
		exhaustFume.GetComponent<SpriteRenderer> ().flipX = flip;
		exhaustFume.GetComponent<SpriteRenderer> ().enabled = true;
		exhaustFume.transform.position = exhausePoint.position;
		exhaustFume.transform.rotation = exhausePoint.rotation;
	}
}
