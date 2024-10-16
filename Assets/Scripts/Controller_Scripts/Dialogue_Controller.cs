using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Dialogue_Controller : MonoBehaviour
{
    [Header("Dialogue Panel pop up parameters")]
    [SerializeField] private GameObject dialogue_panel;
    [SerializeField] private float translate_speed = 2;
    [SerializeField] private int target_y;

    [Header("Text parameters")]
    [SerializeField] private float text_speed;
    [SerializeField] private TextMeshProUGUI text_field;
    [SerializeField] private TextMeshProUGUI next_symbol;

    [Header("Other parameters")]
    [SerializeField] private Input_Controller input;
    [SerializeField] private UI_Controller ui;
    
    public UnityEvent dialogue_started;
    public UnityEvent dialogue_ended;

    private int cur_line = 0;

    public void StartDialogue(Dialogue_Asset dialogue){
        cur_line = 0;
        StartCoroutine(DialogueRoutine(dialogue));
    }

    private IEnumerator DialogueRoutine(Dialogue_Asset dialogue){
        input.DisableDefault();
        ui.DeactivateAll();

        text_field.text = string.Empty;

        dialogue_started.Invoke();

        Coroutine initiate = StartCoroutine(MovePanel(target_y));
        yield return initiate;

        while(cur_line < dialogue.lines.Length){
            Coroutine type = StartCoroutine(TypeLine(dialogue));
            Coroutine quick_end = StartCoroutine(QuickEnd(type, dialogue));
            yield return type;

            StopCoroutine(quick_end);

            next_symbol.text += "▼";

            Coroutine next = StartCoroutine(NextLine());
            yield return next;

            cur_line++;
        }

        Coroutine terminate = StartCoroutine(MovePanel(-600f));
        yield return terminate;

        input.EnableDefault();
        ui.ActivateGameplay();

        dialogue_ended.Invoke();
    }

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
    }

    private IEnumerator NextLine(){
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

    private IEnumerator MovePanel(float target){
        RectTransform rect = dialogue_panel.GetComponent<RectTransform>();
        float cur_time = 0f;
        float cur_position = rect.localPosition.y;
        float lerped_position;

        while(cur_time < translate_speed){
            cur_time += Time.deltaTime;

            lerped_position = Mathf.Lerp(cur_position, target, cur_time/translate_speed);
            rect.localPosition = new Vector3(rect.localPosition.x, lerped_position, rect.localPosition.z);

            yield return null;
        }

        yield return null;
    }
}
