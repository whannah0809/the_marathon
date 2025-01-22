using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/*
    Function:   Sets object as the follow target for the cvc
    Usage:      Used on player 1
*/
public class Camera_Target : MonoBehaviour
{
    void Awake(){
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");

        camera.GetComponent<CinemachineVirtualCamera>().Follow = transform;
        camera.GetComponent<CinemachineVirtualCamera>().LookAt = transform;
    }
}
