using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PositionsConfig {
	public Transform exhaustPointLeft;
	public Transform exhaustPointRight;
	public Transform shootingPointLeft;
	public Transform shootingPointRight;
	public Transform tankShotPositionLeft;
	public Transform tankShotPositionRight;
}

[System.Serializable]
public class TankConfig {
	public float maxSpeed = 10f;
	public float maxMove = 2.0f;
}

public class TankController : MonoBehaviour {

	public PositionsConfig positionsConfig;
	public TankConfig tankConfig;

	public enum Direction {left, right };

	public Direction direction = 0;
	public GameObject barrel;


	public int currentWeapon;
	public string currentWeaponName;


	private float bulletForce = 1000.0f;
	private GameObject bulletFrefab;
	private GameObject tankShotFrefab;
	private GameObject exhaustFumeFrefab;
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
		exhaustFumeFrefab = Resources.Load("Prefabs/exhaustFume") as GameObject;
		tankShotFrefab = Resources.Load ("Prefabs/TankShot") as GameObject;
		exhaustFume = Instantiate (exhaustFumeFrefab, positionsConfig.exhaustPointLeft.position, positionsConfig.exhaustPointLeft.rotation);			
		AudioSource[] allMyAudioSources = GetComponents<AudioSource>();
		tankMove = allMyAudioSources[0];
		barrelMove = allMyAudioSources[1];
		setWeapon (0);
	}

	public void setWeapon(int index, string weaponName = "weapon_16_surprise_stones"){
		currentWeapon = index;
		currentWeaponName = weaponName;
		bulletFrefab = Resources.Load ("Prefabs/"+weaponName) as GameObject;
	}

	public void adjustBarrel(float moveVertical){	
		if (direction == Direction.left) 
			barrel.transform.Rotate (new Vector3 (0, 0, moveVertical));
		else 
			barrel.transform.Rotate (new Vector3 (0, 0, -moveVertical));				

		if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.DownArrow)) {
			if (!barrelMove.isPlaying) 
				barrelMove.Play ();			
		} else {
			barrelMove.Stop ();
		}
	}

	public void move(float moveHoriz, float moveTime){
		bool moving = moveTime < tankConfig.maxMove;

		if (moving) {
			rBody.velocity = new Vector2 (moveHoriz * tankConfig.maxSpeed, rBody.velocity.y);	
			animator.SetFloat ("Speed", Mathf.Abs (moveHoriz));
			exhaustFume.GetComponent<Animator> ().SetFloat ("Speed", Mathf.Abs (moveHoriz));
		} else {
			animator.SetFloat ("Speed", 0.0f);
			exhaustFume.GetComponent<Animator> ().SetFloat ("Speed", 0.0f);
		}


		if (moveHoriz > 0) {						
			changeDirrection (Direction.left);
			if (moving) {
				displayExhauseFume (positionsConfig.exhaustPointLeft);
				if (!tankMove.isPlaying) tankMove.Play ();
			}


		} else if (moveHoriz < 0) {						
			changeDirrection (Direction.right);
			if (moving) {
				displayExhauseFume (positionsConfig.exhaustPointRight);
				if (!tankMove.isPlaying)
					tankMove.Play ();
			}
		} else {
			if (tankMove.isPlaying) tankMove.Stop ();
			exhaustFume.GetComponent<SpriteRenderer> ().enabled = false;
		}
	}

	public void shoot(float power){		
		Transform shootingPoint = direction == Direction.left ? positionsConfig.shootingPointLeft : positionsConfig.shootingPointRight;
		Transform tankShotPoint = direction == Direction.left ? positionsConfig.tankShotPositionLeft : positionsConfig.tankShotPositionRight;
		GameObject bullet = Instantiate (bulletFrefab, shootingPoint.position, shootingPoint.rotation);		
		GameObject tankShot = Instantiate (tankShotFrefab, tankShotPoint.position, tankShotPoint.rotation);		
		Destroy (tankShot, 0.4f);
		bullet.GetComponent<Rigidbody2D> ().AddForce (bullet.transform.right * bulletForce * power);
		//bullet.GetComponent<Rigidbody2D> ().velocity = Vector2.right * 10 * Time.deltaTime;
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
