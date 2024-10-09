using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade_Controller : MonoBehaviour
{
    [SerializeField] private float fade_time;
    [SerializeField] private Material fade_material;

    private Color black = Color.black;
    private Color transp = new Color(0, 0, 0, 0);

    public IEnumerator FadeFromBlack(){
        Coroutine faded = StartCoroutine(Fade(transp));
        yield return faded;

        yield return null;
    }

    public IEnumerator FadeToBlack(){
        Coroutine faded = StartCoroutine(Fade(black));
        yield return faded;

        yield return null;
    }

    private IEnumerator Fade(Color target_col){
        Color cur_color = fade_material.color;
        Color lerp_color;
        float cur_time = 0f;

        while(cur_time < fade_time){
            cur_time += Time.deltaTime;

            lerp_color = Color.Lerp(cur_color, target_col, cur_time/fade_time);
            fade_material.color = lerp_color;

            yield return null;
        }

        fade_material.color = target_col;
        Debug.Log(fade_material.color);
        yield return null;
    }
}