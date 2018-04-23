using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PositionsConfig {	
	public Transform shootingPoint;
	public Transform tankShotPosition;
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
	public int barrelAngle = 0;
	public int currentWeapon;
	public string currentWeaponName;
	public float maxHealth = 100;
	public Slider healthBar;
	public bool die = false;
	public string name;
	public bool active = false;
	public GameObject exhaustFume;

	//--------------------------
	public GameObject frontWheel;
	private Collider2D col2D;
	public LayerMask level;
	public float collisionRadius = 0.1f;

	//--------------------------

	public float currentHealth;
	private float bulletForce = 1000.0f;
	private GameObject bulletFrefab;
	private GameObject tankShotFrefab;

	private Rigidbody2D rBody;
	private SpriteRenderer sRend;
	private Animator animator;

	private AudioSource tankMove;
	private AudioSource barrelMove;



	private Quaternion barrelRotaton;

	void Start () {
		rBody = this.GetComponent<Rigidbody2D>();
		sRend = this.GetComponent<SpriteRenderer>();
		animator = this.GetComponent<Animator>();	
		tankShotFrefab = Resources.Load ("Prefabs/TankShot") as GameObject;
		AudioSource[] allMyAudioSources = GetComponents<AudioSource>();
		tankMove = allMyAudioSources[0];
		barrelMove = allMyAudioSources[1];
		setWeapon (0);
		currentHealth = maxHealth;	
		UpdateHealth ();
	}

	void UpdateHealth(){
		if (currentHealth <= 0) {
			currentHealth = 0;
			die = true;
		}		
		healthBar.value = currentHealth / maxHealth;
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(frontWheel.transform.position, collisionRadius);
	}

	void Update(){
		
		if (!active) {
			tankMove.Stop ();
			barrelMove.Stop();
			animator.SetFloat ("Speed", 0);
			exhaustFume.GetComponent<Animator> ().SetFloat ("Speed", 0);
			exhaustFume.GetComponent<SpriteRenderer> ().enabled = false;
		}

		Debug.Log (transform.rotation.z);
	
		while(transform.rotation.z < -0.39f)
		{
			transform.Rotate (0,0,1);
		}
	}

	public void Damage (float damageAmt) {
		currentHealth -= damageAmt;
		UpdateHealth();
	}

	public void setWeapon(int index, string weaponName = "weapon_16_surprise_stones"){
		currentWeapon = index;
		currentWeaponName = weaponName;
		bulletFrefab = Resources.Load ("Prefabs/"+weaponName) as GameObject;
	}

	public int adjustBarrel(float moveVertical){	
		if (active) {
			
			barrel.transform.Rotate (new Vector3 (0, 0, moveVertical));

			if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.DownArrow)) {
				if (!barrelMove.isPlaying)
					barrelMove.Play ();			
			} else {
				barrelMove.Stop ();
			}

			if (direction == Direction.right) {
				barrelAngle = 90 - Mathf.RoundToInt (barrel.transform.eulerAngles.z % 90);
			} else {			
				barrelAngle = Mathf.RoundToInt (barrel.transform.eulerAngles.z % 90);
			}

			barrelRotaton = barrel.transform.rotation;
		}
		return barrelAngle;
	}

	public void move(float moveHoriz, float moveTime){

		if (active) {
			bool can_move = moveTime < tankConfig.maxMove;
			if (can_move) {				

				animator.SetFloat ("Speed", Mathf.Abs (moveHoriz));
				exhaustFume.GetComponent<Animator> ().SetFloat ("Speed", Mathf.Abs (moveHoriz));

				col2D = Physics2D.OverlapCircle (frontWheel.transform.position, collisionRadius, level);
				if (col2D != null) {							
					//sRend.color = Color.red;				
					float m = moveHoriz * tankConfig.maxSpeed * Time.deltaTime;
					transform.Translate (new Vector3(m, Mathf.Abs(m),0));
				} else {
					//sRend.color = Color.white;
					rBody.velocity = transform.right * tankConfig.maxSpeed * moveHoriz;
				}					
			} else {
				animator.SetFloat ("Speed", 0.0f);
				exhaustFume.GetComponent<Animator> ().SetFloat ("Speed", 0.0f);
			}


			if (moveHoriz > 0) {						
				changeDirrection (Direction.left);
				if (can_move) {
					exhaustFume.GetComponent<SpriteRenderer> ().enabled = true;
					if (!tankMove.isPlaying)
						tankMove.Play ();
				}					
			} else if (moveHoriz < 0) {						
				changeDirrection (Direction.right);
				if (can_move) {
					exhaustFume.GetComponent<SpriteRenderer> ().enabled = true;
					if (!tankMove.isPlaying)
						tankMove.Play ();
				}
			} else {
				if (tankMove.isPlaying)
					tankMove.Stop ();
				exhaustFume.GetComponent<SpriteRenderer> ().enabled = false;
			}
		} 
	}

	public void shoot(float power){						
		GameObject bullet = Instantiate (bulletFrefab, positionsConfig.shootingPoint.position, positionsConfig.shootingPoint.rotation);		
		GameObject tankShot = Instantiate (tankShotFrefab, positionsConfig.tankShotPosition.position, positionsConfig.tankShotPosition.rotation);		
		Destroy (tankShot, 0.4f);
		float shootDirection = direction == Direction.left ? 1.0f : -1.0f;			
		bullet.GetComponent<Rigidbody2D> ().AddForce ( shootDirection * bullet.transform.right * bulletForce * power);
	}

	void changeDirrection(Direction facingDirection){
		direction = facingDirection;
		if (facingDirection == Direction.left) 	
			transform.localScale = new Vector3 (1, 1, 1);
		else 
			transform.localScale = new Vector3(-1, 1, 1);	
	}
}
