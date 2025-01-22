using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Function:   Fade controller fades out the scene by changing the colour of a panel parented by the camera.
Usage:      Used by the scene manager for fading to and from black
*/
public class Fade_Controller : MonoBehaviour
{
    [Header("Fade parameters")]
    [SerializeField] private float fade_time;
    [SerializeField] private Material fade_material;

    private Color black = Color.black;
    private Color transp = new Color(0, 0, 0, 0);

    /*
    Function:   Sets the colour of the material used by the panel on the camera to black
    Usage:      Fades the camera view to black
    */
    public IEnumerator FadeFromBlack(){
        Coroutine faded = StartCoroutine(Fade(transp));
        yield return faded;

        yield return null;
    }

    /*
    Function:   Sets the colour of the material used by the panel on the camera to transparent
    Usage:      Fades the camera view to render the scene
    */
    public IEnumerator FadeToBlack(){
        Coroutine faded = StartCoroutine(Fade(black));
        yield return faded;

        yield return null;
    }

    private IEnumerator Fade(Color target_col){
        Color cur_color = fade_material.color;
        Color lerp_color;
        float cur_time = 0f;

        //Lerp the colour from current to target in the specified fade time
        while(cur_time < fade_time){
            cur_time += Time.deltaTime;

            lerp_color = Color.Lerp(cur_color, target_col, cur_time/fade_time);
            fade_material.color = lerp_color;

            yield return null;
        }

        fade_material.color = target_col;
        yield return null;
    }
}