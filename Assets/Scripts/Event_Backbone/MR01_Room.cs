using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MR01_Room : Scene_Event
{
    [SerializeField] Transform camera_target;
    [SerializeField] Transform camera_origin;
    [SerializeField] GameObject main_camera;
    [SerializeField] Input_Controller input;

    public override IEnumerator ExecuteSceneCode(){
        Debug.Log("Executing MR1");
        input = GameObject.FindGameObjectWithTag("Input Manager").GetComponent<Input_Controller>();
        input.DisableDefault();

        //Player 1 and Player 2 walk in
        //Small talk dialogue

        //Player 1 turns around
        //Sit here dont touch anything Im going to go get the list

        //Move Camera
        CinemachineVirtualCamera cvc = main_camera.GetComponent<CinemachineVirtualCamera>();
        cvc.enabled = false;

        Camera_Utility cu = main_camera.GetComponent<Camera_Utility>();
        cu.SetSpeeds(camera_target.position, new Vector3(0f, -90f, 0f), 1f);
        StartCoroutine(cu.TranslateCamera(camera_target.position));
        StartCoroutine(cu.RotateCamera(new Vector3(0f, -90f, 0f)));

        yield return new WaitForSeconds(4f);

        //Player 1 goes to the next room
        //You see the pile of dirty laundry

        //Move Camera back
        Coroutine t_back = StartCoroutine(cu.TranslateCamera(camera_origin.position));
        Coroutine r_back = StartCoroutine(cu.RotateCamera(new Vector3(0f, 0f, 0f)));

        yield return t_back;
        yield return r_back;

        cvc.enabled = true;
        input.EnableDefault();

        //Set timer: 1 minute
    }
}
