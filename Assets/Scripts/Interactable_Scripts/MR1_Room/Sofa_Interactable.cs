using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Function:   Defines event for when player interacts with the sofa
    Usage:      Used when player interacts with the sofa
*/
public class Sofa_Interactable : Interaction_Event
{
    [SerializeField] Transform target_position_1;
    [SerializeField] Transform target_position_2;
    [SerializeField] Transform pills;
    [SerializeField] float move_speed;

    private Input_Controller input;

    //Disable user input and start event routine
    public override void InvokeEvent(){
        input = GameObject.FindGameObjectWithTag("Input Manager").GetComponent<Input_Controller>();
        input.DisableDefault();
        StartCoroutine(EventRoutine());
    }

    private IEnumerator EventRoutine(){
        //Move horizontally to align with sofa
        Coroutine movement_1 = StartCoroutine(MoveToTarget(target_position_1, move_speed));
        yield return movement_1;

        //Move in z direction to sitting position
        Coroutine movement_2 = StartCoroutine(MoveToTarget(target_position_2, move_speed));
        yield return movement_2;

        //Rotate towards the pills
        Coroutine pills_rotate = StartCoroutine(LookAtObject(pills, 4));
        yield return pills_rotate;

        Coroutine return_movement;

        //On horizontal input, return to original position 
        while(true){
            if (Input.GetAxis("Horizontal") != 0){
                return_movement = StartCoroutine(MoveToTarget(target_position_1, move_speed));
                break;
            }

            yield return null;
        }

        yield return return_movement;

        Debug.Log("Returned");
        this.gameObject.GetComponent<Interactable_Object>().EndInteractionEvent();
        input.EnableDefault();
    }
}
