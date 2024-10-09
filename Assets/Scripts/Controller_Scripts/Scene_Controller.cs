using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Controller : MonoBehaviour
{
    [SerializeField] private UI_Controller ui;
    [SerializeField] private Fade_Controller fade;

    public void ChangeScene(int MPID){
        if(MPID != SceneManager.GetActiveScene().buildIndex){
            ui.DeactivateAll();
            StartCoroutine(SceneRoutine(MPID));
        }
        else{
            ui.DeactivateMap();
        }
    }

    private IEnumerator SceneRoutine(int MPID)
    {
        Coroutine fade_to_black = StartCoroutine(fade.FadeToBlack());
        yield return fade_to_black;

        SceneManager.LoadScene(MPID);
        yield return new WaitForSeconds(1f);

        Coroutine fade_from_black = StartCoroutine(fade.FadeFromBlack());
        yield return fade_from_black;

        ui.ActivateGameplay();
        yield return null;
    }
}
