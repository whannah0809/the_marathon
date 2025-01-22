using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
Function:   Sets the frame rate of the game and changes the scene to the first scene 
            once all presistant managers are in the scene.
*/
public class Initialize : MonoBehaviour
{
    [SerializeField] private Scene_Controller scene_cont;
    [SerializeField] private Material fade_material;

    void Awake(){
        QualitySettings.vSyncCount = 0; 
        Application.targetFrameRate = 20;
        
        fade_material.color = Color.black;
        scene_cont.QuickChange(1);
    }
}
