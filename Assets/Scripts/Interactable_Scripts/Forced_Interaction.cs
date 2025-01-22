using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Function:   A class for mandatory events that do not require the user interacting with an object
    Usage:      Assigned to a collider. On collision, calls the interaction event on the gameobject
*/
public class Forced_Interaction : MonoBehaviour
{
    private UI_Controller ui;
    private Input_Controller input;

    private bool can_interact = true;

    //Detect collision 
    private void OnTriggerEnter(Collider collider){
        if(collider.gameObject.CompareTag("Player") && can_interact){
            this.gameObject.GetComponent<Interaction_Event>().InvokeEvent();
            can_interact = false;
        }
    }
}
