using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Function:   Specified the sequence of events to occur in scene 1
    Usage:      Used by the event backbone when entering scene 1
*/
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
    [SerializeField] Dialogue_Asset forgot_list;

    private Input_Controller input;
    private Dialogue_Controller dialogue;

    public override IEnumerator ExecuteSceneCode(){
        //The scene starts with the train moving. Find appropriate managers and disable input 
        input = GameObject.FindGameObjectWithTag("Input Manager").GetComponent<Input_Controller>();
        dialogue = GameObject.FindGameObjectWithTag("Dialogue Manager").GetComponent<Dialogue_Controller>();

        input.DisableDefault();

        //Move train
        yield return StartCoroutine(TranslateObject(train, train_target, train_move_speed));

        //Player 2 looks to the left
        player2_anim.SetBool("Left", true);

        //Dialogue starts about player 2 forgetting his list
        dialogue.StartDialogue(forgot_list);
        dialogue.dialogue_ended.AddListener(EventHandler);
        yield return StartCoroutine(WaitForDialogueEnd());
        
        //After dialogue finishes, player 2 looks forward
        player2_anim.SetBool("Left", false);

        //Player 2 starts moving towards the exit
        yield return new WaitForSeconds(0.4f);
        player2_move.enabled = true;

        //After a slight delay, player 1 is allowed to move via player input
        yield return new WaitForSeconds(0.5f);
        input.EnableDefault();
    }
}
