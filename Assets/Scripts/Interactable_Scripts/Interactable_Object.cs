using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Function:   A class for events that require the user interacting with an object
    Usage:      Assigned to a collider. On collision, calls the ui manager to render an interaction affordance.
                On user interaction (Space bar input), calls the gameobject's interactione event code..
*/
public class Interactable_Object : MonoBehaviour
{
    [SerializeField] private string interactable_affordance;

    private UI_Controller ui;
    private Input_Controller input;

    private bool can_interact = false;
    private bool interacting = false;

    void Awake(){
        ui = GameObject.FindGameObjectWithTag("UI Manager").GetComponent<UI_Controller>();
        input = GameObject.FindGameObjectWithTag("Input Manager").GetComponent<Input_Controller>();

        StartCoroutine(OnInteraction());
    }
    
    /*
    Function:   Renders the affordance while the player is colliding with the interaction space dictated by the collider on the object
    */
    private void OnTriggerEnter(Collider collider){
        if(collider.gameObject.CompareTag("Player") && input.QueryInteractable()){
            ui.ActivateInteractable(interactable_affordance);
            can_interact = true;
        }
    }
    private void OnTriggerExit(Collider collider){
        if(collider.gameObject.CompareTag("Player")){
            ui.DeactivateInteractable();
            can_interact = false;
        }
    }

    /*
    Function:   If the player presses the space key while interaction is possible, call the gameobject's interaction event code
    */
    private IEnumerator OnInteraction(){
        while(true){
            if (Input.GetKeyDown(KeyCode.Space) && can_interact && !interacting && input.QueryInteractable())
            {
                Debug.Log("Interaction");
                interacting = true;
                ui.DeactivateInteractable();
                this.gameObject.GetComponent<Interaction_Event>().InvokeEvent();
            }

            yield return null;
        }
    }

    //Called by the interaction event if the object is interactable multiple times
    public void EndInteractionEvent(){
        if(input.QueryInteractable()){
            ui.ActivateInteractable(interactable_affordance);
        }
        interacting = false;
    }
}
