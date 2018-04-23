using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkBullet : NetworkBehaviour {

	public GameObject explosion;

    [Command]
    private void Cmd_explode()
    {
        GameObject ex = Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
        NetworkServer.Spawn(ex);

        Destroy(ex, 0.8f);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "Player")
        {
            Debug.Log("Player Collider!");

            GameObject.Find("GameController").GetComponent<N_GameController>().Health -= 10;
            Debug.Log("Health reduced!");
        }

        Cmd_explode();

    }

}
