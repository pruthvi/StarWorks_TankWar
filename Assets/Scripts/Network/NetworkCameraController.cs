using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkCameraController : MonoBehaviour {


    private Transform playerPosition;


    public void setTarget(Transform target)
    {
        playerPosition = target;
    }


    void Update () {

        this.transform.position = new Vector3(playerPosition.position.x, playerPosition.position.y, this.transform.position.z);

    }



}
