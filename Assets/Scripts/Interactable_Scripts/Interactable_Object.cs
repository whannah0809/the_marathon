using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Object : MonoBehaviour
{
    [SerializeField] private float interaction_cooldown = 0.1f;
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
        if(input.QueryInteractable()){
            ui.ActivateInteractable(interactable_affordance);
        }
        interacting = false;
    }
}
