using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MR00_Station : Scene_Event
{
    [Header("Scene Objects")]
    [SerializeField] GameObject train;

    [Header("Move Targets")]
    [SerializeField] Transform train_target;

    [Header("Scene Variables")]
    [SerializeField] float train_move_speed;
    [SerializeField] Player2_Movement player2_move;
    [SerializeField] Animator player2_anim;

    [Header("Dialogue")]
    [SerializeField] Dialogue_Asset forgot_map;

    private Input_Controller input;
    private Dialogue_Controller dialogue;

    public override IEnumerator ExecuteSceneCode(){
        Debug.Log("Executing MR0");
        input = GameObject.FindGameObjectWithTag("Input Manager").GetComponent<Input_Controller>();
        dialogue = GameObject.FindGameObjectWithTag("Dialogue Manager").GetComponent<Dialogue_Controller>();

        input.DisableDefault();

        //Move train
        yield return StartCoroutine(TranslateObject(train, train_target, train_move_speed));

        player2_anim.SetBool("Left", true);

        dialogue.StartDialogue(forgot_map);
        dialogue.dialogue_ended.AddListener(EventHandler);
        yield return StartCoroutine(WaitForDialogueEnd());
        
        player2_move.enabled = true;
        player2_anim.SetBool("Left", false);

        yield return new WaitForSeconds(0.5f);

        input.EnableDefault();
    }
}
