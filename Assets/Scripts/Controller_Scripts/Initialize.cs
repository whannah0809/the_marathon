using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initialize : MonoBehaviour
{
    [SerializeField] private Scene_Controller scene_cont;
    [SerializeField] private Material fade_material;

    void Awake(){
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 25;
        
        fade_material.color = Color.black;
        scene_cont.QuickChange(1);
    }
}
