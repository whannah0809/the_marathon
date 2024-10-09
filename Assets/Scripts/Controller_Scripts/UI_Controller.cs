using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] private GameObject mapUI;
    [SerializeField] private GameObject gamePlayUI;

    public void ActivateMap(){
        mapUI.SetActive(true);
        gamePlayUI.SetActive(false);
    }

    public void DeactivateMap(){
        mapUI.SetActive(false);
        gamePlayUI.SetActive(true);
    }

    public void DeactivateAll(){
        Debug.Log("Deactivating all");
        mapUI.SetActive(false);
        gamePlayUI.SetActive(false);
    }

    public void ActivateGameplay(){
        gamePlayUI.SetActive(true);
    }
}
