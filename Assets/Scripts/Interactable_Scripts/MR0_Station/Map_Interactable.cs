using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Interactable : Interaction_Event
{
    [SerializeField] Dialogue_Asset map_dialogue;
    [SerializeField] float rotation_speed;
    [SerializeField] Transform look_at;

    private Dialogue_Controller dialogue;
    private UI_Controller ui;
    private Input_Controller input;

    void Awake(){
        dialogue = GameObject.FindGameObjectWithTag("Dialogue Manager").GetComponent<Dialogue_Controller>();
        ui = GameObject.FindGameObjectWithTag("UI Manager").GetComponent<UI_Controller>();
        input = GameObject.FindGameObjectWithTag("Input Manager").GetComponent<Input_Controller>();
    }

    public override void InvokeEvent(){
        StartCoroutine(EventSequence());
    }

    private IEnumerator EventSequence(){

        input.DisableDefault();

        yield return new WaitForSeconds(0.2f);

        dialogue.StartDialogue(map_dialogue);
        dialogue.dialogue_ended.AddListener(EventHandler);
        yield return StartCoroutine(WaitForDialogueEnd());

        yield return StartCoroutine(LookAtObject(look_at, rotation_speed));

        this.gameObject.GetComponent<Interactable_Object>().EndInteractionEvent();

        input.EnableDefault();
        ui.ActivateMap();
    }
}
