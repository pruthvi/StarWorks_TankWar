using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class N_GameController : MonoBehaviour {

    
    public GameObject heart;
    public GameObject ammo;

    public int Health = 50;
    public Text healthText;

    public int Ammo;
    public Text ammoText;

    // Use this for initialization
    void Start()
    {

        //GenerateRandomly(heart);
       // GenerateRandomly(ammo);

        healthText.text = "";
        ammoText.text = "";

        Health = 100;
        Ammo = 50;

    }

    // Update is called once per frame
    void Update()
    {

        healthText.text = "Health = " + Health;
        //ammoText.text = "Ammo = " + Ammo;

        CheckHealth();
    }


    void GenerateRandomly(GameObject gb)
    {
        Vector2 position = new Vector2(Random.Range(-50.0f, 42.0f), Random.Range(10.0f, 15.0f));
        Instantiate(gb, position, Quaternion.identity);
        Debug.Log(gb + "position :" + position);
    }

    private void CheckHealth()
    {
        if (Health <= 0)
        {
            Network.Disconnect();
            MasterServer.UnregisterHost();

            GameObject gb = GameObject.FindGameObjectWithTag("Player");
            Destroy(gb);
            Debug.Log("Player Died");
            return;
        }
    }
}
