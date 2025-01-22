using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
Function:   Scene controller handles scene changes when appropriate
Usage:      Called when a scene change is necessary
*/
public class Scene_Controller : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField] private UI_Controller ui;
    [SerializeField] private Fade_Controller fade;
    [SerializeField] private Input_Controller input;
    [SerializeField] private Event_Backbone bone;

    /*
    Function:   Changes the scene to a specified scene identified by the build index
    Usage:      Called by external scripts when a scene change is appropriate
    Input:      MPID -> The build index of the scene to switch to
    */
    public void ChangeScene(int MPID){
        if(MPID != SceneManager.GetActiveScene().buildIndex){
            ui.DeactivateAll();
            input.DisableDefault();

            StartCoroutine(SceneRoutine(MPID));
        }
        else{
            ui.DeactivateMap();
        }
    }

    /*
    Function:   Changes the scene without the fade transition
    Usage:      Changes scene without transition
    Input:      MPID -> The build index of the scene to switch to
    */
    public void QuickChange(int MPID){
        StartCoroutine(QuickChangeRoutine(MPID));
    }

    private IEnumerator SceneRoutine(int MPID){
        //Fade to black
        Coroutine fade_to_black = StartCoroutine(fade.FadeToBlack());
        yield return fade_to_black;

        //Change scene
        SceneManager.LoadScene(MPID);
        yield return new WaitForSeconds(1f);

        //Fade to transparent
        Coroutine fade_from_black = StartCoroutine(fade.FadeFromBlack());

        //Call scene event
        bone.CallSceneEvent(MPID);
        yield return fade_from_black;

        yield return null;
    }

    private IEnumerator QuickChangeRoutine(int MPID){
        //change scene
        SceneManager.LoadScene(MPID);
        yield return new WaitForSeconds(1f);

        //Make material transparent
        Coroutine fade_from_black = StartCoroutine(fade.FadeFromBlack());

        //Call scene event
        bone.CallSceneEvent(MPID);
        yield return fade_from_black;

        yield return null;
    }
}
