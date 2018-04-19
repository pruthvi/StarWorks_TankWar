using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSpeed : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.gameObject.GetComponent<TankController>().tankConfig.maxSpeed += 2;
            Destroy(this.gameObject);
            //Debug.Log("Speed Increased by 2");
        }
    }
}
