using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObjects : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Weapon")
        {
           // Debug.Log("Destructible Object Destroyed!");
            Destroy(this.gameObject);
        }
    }
}
