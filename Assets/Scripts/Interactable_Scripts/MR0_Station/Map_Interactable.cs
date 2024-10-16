using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Interactable : Interaction_Event
{
    [SerializeField] Dialogue_Asset map_dialogue;

    private Dialogue_Controller dialogue;
    private UI_Controller ui;

    void Awake(){
        dialogue = GameObject.FindGameObjectWithTag("Dialogue Manager").GetComponent<Dialogue_Controller>();
        ui = GameObject.FindGameObjectWithTag("UI Manager").GetComponent<UI_Controller>();
    }

    public override void InvokeEvent(){
        dialogue.StartDialogue(map_dialogue);
        dialogue.dialogue_ended.AddListener(EndInvokedAction);
    }

    private void EndInvokedAction(){
        dialogue.dialogue_ended.RemoveListener(EndInvokedAction);
        this.gameObject.GetComponent<Interactable_Object>().EndInteractionEvent();

        ui.ActivateMap();
    }
}
