using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class NetworkTankController : NetworkBehaviour {

	public enum Direction {left, right };

	public float maxSpeed = 10f;
	public Direction direction = 0;

	public GameObject barrel;

    public GameObject bulletPrefab;
	public Transform shootingPointLeft;
	public Transform shootingPointRight;
	public float bulletForce = 700.0f;
    

	private Rigidbody2D rBody;
	private Animator animator;

	private AudioSource tankMove;
	private AudioSource tankShoot;

	void Start () {

		rBody = this.GetComponent<Rigidbody2D>();

		animator = this.GetComponent<Animator>();

     
        AudioSource[] allMyAudioSources = GetComponents<AudioSource>();
		tankMove = allMyAudioSources[0];
		tankShoot = allMyAudioSources[1];
	}


    void Update()
    {

    }

    public override void OnStartLocalPlayer()
    {
        Camera.main.GetComponent<NetworkCameraController>().setTarget(this.gameObject.transform);
    }

    void FixedUpdate () {

        if (!isLocalPlayer)
        {
            return;
        }


        //Moving tank with left/right keys
        float moveHoriz = Input.GetAxis("Horizontal");

		rBody.velocity = new Vector2(moveHoriz * maxSpeed, rBody.velocity.y);	

		animator.SetFloat("Speed", Mathf.Abs(moveHoriz));

		if (moveHoriz > 0) {						
			if (!tankMove.isPlaying) tankMove.Play ();			
		} else if (moveHoriz < 0) {						
			if (!tankMove.isPlaying) tankMove.Play ();
		} else {
			if (tankMove.isPlaying) tankMove.Stop ();
		}

		// Moving the barrel with up/down keys
		float moveVertical = Input.GetAxis("Vertical");

		if (direction == Direction.left) 
			barrel.transform.Rotate (new Vector3 (0, 0, moveVertical));
		else 
			barrel.transform.Rotate (new Vector3 (0, 0, - moveVertical));		

		if (Input.GetButtonDown ("Fire1") || Input.GetKeyDown (KeyCode.Space))
            Cmd_shoot();
	}

    [Command]
	void Cmd_shoot(){		
		Transform shootingPoint = direction == Direction.left ? shootingPointLeft : shootingPointRight;
		GameObject bullet = Instantiate (bulletPrefab, shootingPoint.position, shootingPoint.rotation);		
		bullet.GetComponent<Rigidbody2D> ().AddForce (bullet.transform.right * bulletForce);
		tankShoot.Play();

        NetworkServer.Spawn(bullet);
	}

}
