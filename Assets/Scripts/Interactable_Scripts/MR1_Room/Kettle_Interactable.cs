using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Function:   Defines event for when player interacts with the kettle
    Usage:      Used when player interacts with the kettle
*/
public class Kettle_Interactable : Interaction_Event
{
    [SerializeField] float rotation_speed;
    [SerializeField] Transform look_at;

    public override void InvokeEvent(){
        StartCoroutine(EventRoutine());
    }

    private IEnumerator EventRoutine(){
        Coroutine rotate = StartCoroutine(LookAtObject(look_at, rotation_speed));
        yield return rotate;
        
        this.gameObject.GetComponent<Interactable_Object>().EndInteractionEvent();
    }
}
