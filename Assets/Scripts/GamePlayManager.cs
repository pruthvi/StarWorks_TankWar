using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GamePlayManager : MonoBehaviour {
	
	public GameObject currentPlayer;
	public GameObject mainCamera;
	public float turnDuration = 10.0f;
	public List<GameObject> players;
	public Slider powerSlider;
	public Slider moveSlider;
	public Text countDownTimer;
	public float moveTime = 2.0f;

	private int selectedTank;
	private float currentMoveTime = 0.0f;

	public Transform[] spawnPositions;

	private float timeLeft;
	private TankController currentTankController;

	private int turn = 0;
	private float shootingForce = 0.0f;
	private bool powerbarMoveUp = true;


	// Use this for initialization
	void Start () {
		string frefab;
		for (int i = 1; i < 4; i++) {
			selectedTank = PlayerPrefs.GetInt ("Player" + i.ToString() + "_Tank");
			if (selectedTank > 0) {
				frefab = "Prefabs/Tank_" + selectedTank;
				GameObject tank1Prefab = Resources.Load(frefab) as GameObject;
				GameObject player1 = Instantiate (tank1Prefab, spawnPositions[i-1].position, spawnPositions[i-1].rotation);			
				players.Add (player1);
			}
		}

		powerSlider.value = 0.0f;

		currentPlayer = players [0];
		countDownTimer.text = timeLeft.ToString();
		timeLeft = turnDuration;
	}

	// Update is called once per frame
	void Update () {

		currentTankController = currentPlayer.GetComponent<TankController> ();

		// Shoot
		if (Input.GetKey(KeyCode.Space)){
			if (shootingForce > 2.0f) {
				powerbarMoveUp = false;
			} else if ( shootingForce < 0.0f){
				powerbarMoveUp = true;
			}

			if (powerbarMoveUp) {
				shootingForce += Time.deltaTime;		
			} else {
				shootingForce -= Time.deltaTime;		
			}
			powerSlider.value = shootingForce / 2.0f;
		}

		if (Input.GetKeyUp (KeyCode.Space)) {			
			currentTankController.shoot (shootingForce/2.0f);
			resetPowerSlider ();
		}


		// Move
		float moveHoriz = Input.GetAxis("Horizontal");	
		if (moveHoriz != 0) {
			currentMoveTime += Time.deltaTime;
			moveSlider.value = (currentMoveTime / moveTime);
		}
		currentTankController.move (moveHoriz, currentMoveTime);		


		// adjustBarrel
		float moveVertical = Input.GetAxis("Vertical");
		currentTankController.adjustBarrel (moveVertical);

		// Move camera following current player
		mainCamera.transform.position = new Vector3 (currentPlayer.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);

		// Time Count Down
		TimeCountDown ();
	}

	public void setWeapon(int index, string weaponName = "weapon_16_surprise_stones"){
		currentTankController.setWeapon (index, weaponName);
	}

	void TimeCountDown(){
		timeLeft -= Time.deltaTime;
		countDownTimer.text = (Mathf.Round(timeLeft)).ToString();

		if (timeLeft > 4.0f) {
			countDownTimer.color = Color.black;
		} else {
			countDownTimer.color = Color.red;
		}
			
		if (timeLeft < 0.0f) {			
			resetPowerSlider ();
			timeLeft = turnDuration;
			turn += 1;
			currentPlayer = players [turn % players.Count];
		}
	}

	void resetPowerSlider(){
		powerbarMoveUp = true;
		currentMoveTime = 0.0f;
		shootingForce = 0.0f;
		powerSlider.value = 0.0f;
		moveSlider.value = 0.0f;
	}
}
