using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
Function:   UI controller handles UI elements overlayed on to the scene
Usage:      Called when appropriate UI elements need to be rendered over the scene
*/
public class UI_Controller : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField] private GameObject map_UI;
    [SerializeField] private GameObject game_PlayUI;
    [SerializeField] private GameObject interactable_UI;
    [SerializeField] private Input_Controller input_Manager;

    [Header("Others")]
    [SerializeField] private TextMeshProUGUI interactable_affordance; 

    /*
    Function:   Renders the map UI element overlayed to the scene
    Usage:      Called when the map needs to become visible
    */
    public void ActivateMap(){
        map_UI.SetActive(true);
        game_PlayUI.SetActive(false);
        interactable_UI.SetActive(false);
        input_Manager.DisableDefault();
    }

    /*
    Function:   Removes the map UI element
    Usage:      Called when the map is no longer needed
    */
    public void DeactivateMap(){
        map_UI.SetActive(false);
        game_PlayUI.SetActive(true);
        input_Manager.DelayedEnable();
    }

    /*
    Function:   Removes all UI elements
    Usage:      Called during dialogue or times when user input needs to be controlled
    */
    public void DeactivateAll(){
        Debug.Log("Deactivating all");
        map_UI.SetActive(false);
        game_PlayUI.SetActive(false);
        interactable_UI.SetActive(false);
    }

    /*
    Function:   Activates UI elements needed during gameplay
    Usage:      Called when user input is enabled
    */
    public void ActivateGameplay(){
        game_PlayUI.SetActive(true);
    }

    /*
    Function:   Activates an affordance to tell users interaction is available
    Usage:      Called by interaction scripts when interaction is possible
    */
    public void ActivateInteractable(string line){
        if(!map_UI.active){
            interactable_affordance.text = line;
            interactable_UI.SetActive(true);
        }
    }

    /*
    Function:   Deactivates the affordance to tell users interaction is available
    Usage:      Called by interaction scripts when interaction is no longer possible
    */
    public void DeactivateInteractable(){
        interactable_UI.SetActive(false);
        interactable_affordance.text = string.Empty;
    }
}
