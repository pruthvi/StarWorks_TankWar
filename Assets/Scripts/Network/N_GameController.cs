using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class N_GameController : MonoBehaviour {

    
    public GameObject heart;
    public GameObject ammo;

    public GameObject[] selectorArr = new GameObject[10];

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

        healthText.text = "Health : " + Health;
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
        UpdateHealth();

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

    void UpdateHealth()
    {
        switch (Health)
        {
            case 90:
                selectorArr[0].SetActive(false);
                break;
            case 80:
                selectorArr[1].SetActive(false);
                break;
            case 70:
                selectorArr[2].SetActive(false);
                break;
            case 60:
                selectorArr[3].SetActive(false);
                break;
            case 50:
                selectorArr[4].SetActive(false);
                break;
            case 40:
                selectorArr[5].SetActive(false);
                break;
            case 30:
                selectorArr[6].SetActive(false);
                break;
            case 20:
                selectorArr[7].SetActive(false);
                break;
            case 10:
                selectorArr[8].SetActive(false);
                break;
            case 0:
                selectorArr[9].SetActive(false);
                break;

            default:
                break;
        }






    }
}