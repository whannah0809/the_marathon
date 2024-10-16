using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Object : MonoBehaviour
{
    [SerializeField] private float interaction_cooldown = 1f;

    private UI_Controller ui;
    private Input_Controller input;

    private bool can_interact = false;
    private bool interacting = false;

    void Awake(){
        ui = GameObject.FindGameObjectWithTag("UI Manager").GetComponent<UI_Controller>();
        input = GameObject.FindGameObjectWithTag("Input Manager").GetComponent<Input_Controller>();

        StartCoroutine(OnInteraction());
    }
    
    private void OnTriggerEnter(Collider collider){
        if(collider.gameObject.CompareTag("Player")){
            ui.ActivateInteractable();
            can_interact = true;
        }
    }

    private void OnTriggerExit(Collider collider){
        if(collider.gameObject.CompareTag("Player")){
            ui.DeactivateInteractable();
            can_interact = false;
        }
    }

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

    public void EndInteractionEvent(){
        StartCoroutine(RestartInteractable());
    }

    private IEnumerator RestartInteractable(){
        yield return new WaitForSeconds(interaction_cooldown);
        Debug.Log("Object Interactable");
        ui.ActivateInteractable();
        interacting = false;
    }
}
