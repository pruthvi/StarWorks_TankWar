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
	public Text currentAngle;
	public Text previousAngle;
	public Canvas winCanvas;
	public Text winner;
	public Transform[] spawnPositions;
	public WeaponSelection weaponSelection;
	public GameObject playerTurnBoard;
	public Text playerTurnTxt;
    public GameObject cloud;


    private int selectedTank;
	private float currentMoveTime = 0.0f;
	private float timeLeft;
	private TankController currentTankController;
	private int turn = -1;
	private float shootingForce = 0.0f;
	private bool powerbarMoveUp = true;


	public static GamePlayManager instance = null;

	// Use this for initialization
	void Start () {
		string frefab;
		for (int i = 1; i < 5; i++) {
			selectedTank = PlayerPrefs.GetInt ("Player" + i.ToString() + "_Tank");
			if (selectedTank > 0) {
				frefab = "Prefabs/Tank_" + selectedTank;
				GameObject tank1Prefab = Resources.Load(frefab) as GameObject;
				GameObject player1 = Instantiate (tank1Prefab, spawnPositions[i-1].position, spawnPositions[i-1].rotation);			
				players.Add (player1);
				player1.GetComponent<TankController> ().name = "Player" + i.ToString ();
			}
		}

		powerSlider.value = 0.0f;
        CreateCloud();

		loadNextPlayer ();
		countDownTimer.text = timeLeft.ToString();
		timeLeft = turnDuration;
	}

	// Update is called once per frame
	void Update () {

		currentTankController = currentPlayer.GetComponent<TankController> ();
		foreach (GameObject player in players) {
			TankController tankcontroller = player.GetComponent<TankController> ();
			if (tankcontroller.die) {	
				players.Remove (player);
				Destroy (player);	
			}
		}

		if (players.Count == 1) {
			winCanvas.enabled = true;
			Time.timeScale = 0;
			winner.text = currentTankController.name + " win";
		}


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
		currentAngle.text = currentTankController.adjustBarrel (moveVertical).ToString();

		// Move camera following current player
		if (currentPlayer.transform.position.x <0) {
			mainCamera.transform.position = new Vector3 (0f, mainCamera.transform.position.y, mainCamera.transform.position.z);	
		} else if (currentPlayer.transform.position.x > 23)  {
			mainCamera.transform.position = new Vector3 (23f, mainCamera.transform.position.y, mainCamera.transform.position.z);	
		}		
		else{
			mainCamera.transform.position = new Vector3 (currentPlayer.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);	
		}



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
			loadNextPlayer ();
		}
	}

	public void loadNextPlayer(){		
		turn += 1;
		timeLeft = turnDuration;
		if (currentTankController != null)
			currentTankController.active = false;
		
		currentPlayer = players [turn % players.Count];
		currentTankController = currentPlayer.GetComponent<TankController> ();
		currentTankController.active = true;
		//Set up UI for current player
		powerbarMoveUp = true;
		currentMoveTime = 0.0f;
		shootingForce = 0.0f;
		powerSlider.value = 0.0f;
		moveSlider.value = 0.0f;
		previousAngle.text = currentTankController.barrelAngle.ToString ();

		weaponSelection.setWeapon (currentTankController.currentWeapon);

		playerTurnBoard.SetActive (true);
		playerTurnTxt.text = currentPlayer.GetComponent<TankController> ().name + " Turn";

		StartCoroutine(ExecuteAfterTime(1));
	}

	IEnumerator ExecuteAfterTime(float time)
	{
		yield return new WaitForSeconds(time);

		playerTurnBoard.SetActive (false);
	}

    public void CreateCloud()
    {
        int No = Random.Range(8,15);

        for(int i=0;i<No;i++)
        {
            Vector2 position = new Vector2(Random.Range(-20, 100), Random.Range(5, 10));
            float scale = Random.Range(0.5f, 2.0f);
            // Debug.Log("Random Scale:" + scale);

            GameObject gb = Instantiate(cloud, position, Quaternion.identity);
            gb.transform.localScale = new Vector2 (scale, scale);
            // Debug.Log("Randomly Scaled!");

        }

    }
}
