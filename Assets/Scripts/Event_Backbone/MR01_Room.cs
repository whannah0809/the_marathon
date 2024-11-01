using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MR01_Room : Scene_Event
{
    [Header("Camera")]
    [SerializeField] Transform camera_target;
    [SerializeField] Transform camera_origin;
    [SerializeField] GameObject main_camera;

    [Header("Other objects")]
    [SerializeField] GameObject red;
    [SerializeField] GameObject white;
    [SerializeField] GameObject door;

    [Header("Move Targets")]
    [SerializeField] Transform t_w_1;
    [SerializeField] Transform t_r_1;
    [SerializeField] Transform t_w_2;

    [Header("Rotation Targets")]
    [SerializeField] Vector3 r_w_1;
    [SerializeField] Vector3 r_w_2;

    [Header("Speed Parameters")]
    [SerializeField] float move_speed;
    [SerializeField] float rotation_speed;

    [Header("Dialogue Assets")]
    [SerializeField] Dialogue_Asset entrance_small_talk;
    [SerializeField] Dialogue_Asset sit_down;

    private Input_Controller input;
    private Dialogue_Controller dialogue;
    private bool dialogue_continue;

    private void EventHandler(){
        dialogue_continue = true;
    }

    IEnumerator WaitForDialogueEnd() {
        yield return new WaitUntil(() => dialogue_continue);
        dialogue_continue = false;
        yield return null;
    }

    public override IEnumerator ExecuteSceneCode(){
        Debug.Log("Executing MR1");
        input = GameObject.FindGameObjectWithTag("Input Manager").GetComponent<Input_Controller>();
        dialogue = GameObject.FindGameObjectWithTag("Dialogue Manager").GetComponent<Dialogue_Controller>();

        input.DisableDefault();

        //Player 1 and Player 2 walk in
        //Small talk dialogue happens simultaneously

        Coroutine move_white_1 = StartCoroutine(TranslateObject(white, t_w_1, move_speed + 0.3f));
        Coroutine move_red_1 = StartCoroutine(TranslateObject(red, t_r_1, move_speed));

        yield return new WaitForSeconds(1f);

        dialogue.StartDialogue(entrance_small_talk);
        dialogue.dialogue_ended.AddListener(EventHandler);

        yield return StartCoroutine(WaitForDialogueEnd());

        yield return move_red_1;
        yield return move_white_1;

        //Player 1 turns around
        Coroutine rotate_white_1 = StartCoroutine(RotateObject(white.transform.GetChild(0).gameObject, r_w_1, rotation_speed));
        yield return rotate_white_1;

        //Sit here dont touch anything Im going to go get the list
        dialogue.StartDialogue(sit_down);
        dialogue.dialogue_ended.AddListener(EventHandler);

        yield return StartCoroutine(WaitForDialogueEnd());

        //Move Camera
        CinemachineVirtualCamera cvc = main_camera.GetComponent<CinemachineVirtualCamera>();
        cvc.enabled = false;

        Camera_Utility cu = main_camera.GetComponent<Camera_Utility>();
        cu.SetSpeeds(camera_target.position, new Vector3(0f, -90f, 0f), 1f);
        StartCoroutine(cu.TranslateCamera(camera_target.position));
        StartCoroutine(cu.RotateCamera(new Vector3(0f, -90f, 0f)));

        //Player 1 goes to the next room

        Coroutine move_white_2 = StartCoroutine(TranslateObject(white, t_w_2, 3));
        Coroutine rotate_white_2 = StartCoroutine(RotateObject(white.transform.GetChild(0).gameObject, r_w_2, rotation_speed));

        yield return move_white_2;
        yield return rotate_white_2;

        //You see the pile of dirty laundry

        door.SetActive(false);

        yield return new WaitForSeconds(1f);

        door.SetActive(true);
        white.SetActive(false);

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
