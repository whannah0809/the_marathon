using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

/*
Function:   The dialogue controller is a presistant manager in charge of rendering dialogue as UI in the scene
Usage:      Used by event scripts when dialogues are a part of event code
*/
public class Dialogue_Controller : MonoBehaviour
{
    [Header("Text parameters")]
    [SerializeField] private float text_speed;
    //UI objects
    [SerializeField] private TextMeshProUGUI text_field;
    [SerializeField] private TextMeshProUGUI next_symbol;

    [Header("Other parameters")]
    //Other controllers to coordinate with
    [SerializeField] private UI_Controller ui;
    
    //Events for coordination with in event scripts
    public UnityEvent dialogue_started;
    public UnityEvent dialogue_ended;

    private int cur_line = 0;

    /*
    Function:   Starts dialogue
    Usage:      Called by event code to initiate dialogue
    Input:      dialogue -> Dialogue Asset object (Scriptable Object) to render. The dialogue asset has an array of strings to be rendered 
                as dialogue
    */
    public void StartDialogue(Dialogue_Asset dialogue){
        cur_line = 0;
        StartCoroutine(DialogueRoutine(dialogue));
    }

    //Deactivate ui elements and initiate coroutines
    private IEnumerator DialogueRoutine(Dialogue_Asset dialogue){
        ui.DeactivateAll();

        text_field.text = string.Empty;

        dialogue_started.Invoke();

        StartCoroutine(InitiateLine(dialogue));

        yield return null;
    }

    //Start 2 coroutines: 1 for typing out the line another to listen for quick end input
    private IEnumerator InitiateLine(Dialogue_Asset dialogue){
        if(cur_line < dialogue.lines.Length){
            Coroutine type = StartCoroutine(TypeLine(dialogue));
            Coroutine quick_end = StartCoroutine(QuickEnd(type, dialogue));
            yield return type;

            StopLine(dialogue);
        } else {
            ui.ActivateGameplay();

            dialogue_ended.Invoke();
        }
    }

    private void StopLine(Dialogue_Asset dialogue){
        StopAllCoroutines();

        next_symbol.text += "▼";

        StartCoroutine(HandleNext(dialogue));
    }

    //Listen for input and start next line
    private IEnumerator HandleNext(Dialogue_Asset dialogue){
        Coroutine next = StartCoroutine(NextLine());
        yield return next;

        cur_line++;
        StartCoroutine(InitiateLine(dialogue));
    }

    //Skip typing animation
    private IEnumerator QuickEnd(Coroutine type, Dialogue_Asset dialogue){
        while(true){
            if (Input.GetKeyDown(KeyCode.Space))
            {
                text_field.text = dialogue.lines[cur_line].line;
                break;
            }

            yield return null;
        }

        yield return null;

        StopLine(dialogue);
    }

    private IEnumerator NextLine(){
        //Listen for space bar input
        while(true){
            if (Input.GetKeyDown(KeyCode.Space))
            {
                text_field.text = string.Empty;
                next_symbol.text = string.Empty;
                break;
            }

            yield return null;
        }

        yield return null;
    }

    private IEnumerator TypeLine(Dialogue_Asset dialogue){
        //Type current line character by character
        foreach(char c in dialogue.lines[cur_line].line){
            if(text_field.text != dialogue.lines[cur_line].line){
                text_field.text += c;
                yield return new WaitForSeconds(text_speed);
            }
            else{
                yield return null;
            }
        }
        yield return null;
    }
}
