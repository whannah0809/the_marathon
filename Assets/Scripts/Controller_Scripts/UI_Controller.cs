using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] private GameObject map_UI;
    [SerializeField] private GameObject game_PlayUI;
    [SerializeField] private GameObject interactable_UI;
    [SerializeField] private Input_Controller input_Manager;
    [SerializeField] private TextMeshProUGUI interactable_affordance; 

    public void ActivateMap(){
        map_UI.SetActive(true);
        game_PlayUI.SetActive(false);
        interactable_UI.SetActive(false);
        input_Manager.DisableDefault();
    }

    public void DeactivateMap(){
        map_UI.SetActive(false);
        game_PlayUI.SetActive(true);
        input_Manager.DelayedEnable();
    }

    public void DeactivateAll(){
        Debug.Log("Deactivating all");
        map_UI.SetActive(false);
        game_PlayUI.SetActive(false);
        interactable_UI.SetActive(false);
    }

    public void ActivateGameplay(){
        game_PlayUI.SetActive(true);
    }

    public void ActivateInteractable(string line){
        if(!map_UI.active){
            interactable_affordance.text = line;
            interactable_UI.SetActive(true);
        }
    }

    public void DeactivateInteractable(){
        interactable_UI.SetActive(false);
        interactable_affordance.text = string.Empty;
    }
}
