using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHealth : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.gameObject.GetComponent<TankController>().currentHealth += 50;
            Destroy(this.gameObject);
            //Debug.Log("Health Added");


        }
    }


}
