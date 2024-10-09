using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class camera_target : MonoBehaviour
{
    void Awake(){
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");

        camera.GetComponent<CinemachineVirtualCamera>().Follow = transform;
        camera.GetComponent<CinemachineVirtualCamera>().LookAt = transform;
    }
}
