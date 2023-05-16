using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    Transform playerTr;

    Vector3 diff;

    void GetPlayer()
    {
        playerTr = Main.instance.player.gameObject.transform;

        diff = playerTr.transform.position - transform.position;
    }

    void Update()
    {
        if(playerTr != null){
            transform.position = playerTr.position - diff;
        }
        else{
            GetPlayer();
        }
    }
}
