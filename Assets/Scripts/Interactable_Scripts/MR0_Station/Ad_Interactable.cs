using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ad_Interactable : Interaction_Event
{
    [SerializeField] Transform look_at;
    [SerializeField] float rotation_speed;
    [SerializeField] Dialogue_Asset ad_dialogue;
    [SerializeField] Player_Movement p1;
    [SerializeField] GameObject p2;
    [SerializeField] Animator p2_anim;

    private Dialogue_Controller dialogue;
    private Input_Controller input;
    private UI_Controller ui;

    void Awake(){
        dialogue = GameObject.FindGameObjectWithTag("Dialogue Manager").GetComponent<Dialogue_Controller>();
        input = GameObject.FindGameObjectWithTag("Input Manager").GetComponent<Input_Controller>();
    }

    public override void InvokeEvent(){
        StartCoroutine(EventSequence());
    }

    private IEnumerator EventSequence(){
        input.DisableDefault();
        p1.TakeAnimControl();

        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(LookAtObject(look_at, rotation_speed));
        yield return new WaitForSeconds(1f);

        p2.GetComponent<Player2_Movement>().enabled = false;

        Transform p2_trans = p2.GetComponent<Transform>();
        yield return StartCoroutine(RotateTarget(p2_trans, look_at, rotation_speed));
        p2_anim.SetBool("Left", true);

        dialogue.StartDialogue(ad_dialogue);
        dialogue.dialogue_ended.AddListener(EventHandler);
        yield return StartCoroutine(WaitForDialogueEnd());

        p2_anim.SetBool("Left", false);
        p2.GetComponent<Player2_Movement>().enabled = true;

        input.EnableDefault();
        p1.GiveAnimControl();

        yield return null;
    }
}
