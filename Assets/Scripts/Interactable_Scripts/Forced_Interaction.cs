using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forced_Interaction : MonoBehaviour
{
    private UI_Controller ui;
    private Input_Controller input;

    private bool can_interact = true;

    private void OnTriggerEnter(Collider collider){
        if(collider.gameObject.CompareTag("Player") && can_interact){
            this.gameObject.GetComponent<Interaction_Event>().InvokeEvent();
            can_interact = false;
        }
    }
}
