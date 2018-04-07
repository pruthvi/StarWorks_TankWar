using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GamePlayManager : MonoBehaviour {
	
	public Transform tankPosition1;
	public GameObject currentPlayer;
	public GameObject mainCamera;
	private int numberOfPlayer = 1;
	private int selectedTank;
	private GameObject player1;

	public List<GameObject> players;

	public Text countDownTimer;
	public Transform[] spawnPositions;

	public float turnDuration = 10.0f;
	private float timeLeft;
	private TankController currentTankController;

	private int turn = 0;

	// Use this for initialization
	void Start () {
		string frefab;
		for (int i = 1; i < 4; i++) {
			selectedTank = PlayerPrefs.GetInt ("Player" + i.ToString() + "_Tank");
			if (selectedTank > 0) {
				frefab = "Prefabs/Tank_" + selectedTank;
				Debug.Log (frefab);
				GameObject tank1Prefab = Resources.Load(frefab) as GameObject;

				player1 = Instantiate (tank1Prefab, spawnPositions[i-1].position, spawnPositions[i-1].rotation);			
				players.Add (player1);
			}
		}

		currentPlayer = players [0];
		countDownTimer.text = timeLeft.ToString();
		timeLeft = turnDuration;
	}

	// Update is called once per frame
	void Update () {

		currentTankController = currentPlayer.GetComponent<TankController> ();

		// Shoot
		if (Input.GetButtonDown ("Fire1") || Input.GetKeyDown (KeyCode.Space))
			currentTankController.shoot ();
		// Move
		float moveHoriz = Input.GetAxis("Horizontal");	
		currentTankController.move (moveHoriz);
		// adjustBarrel
		float moveVertical = Input.GetAxis("Vertical");
		currentTankController.adjustBarrel (moveVertical);

		// Move camera following current player
		mainCamera.transform.position = new Vector3 (currentPlayer.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);

		// Time Count Down
		TimeCountDown ();
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
			timeLeft = turnDuration;
			turn += 1;
			currentPlayer = players [turn % players.Count];
		}
	}
}
