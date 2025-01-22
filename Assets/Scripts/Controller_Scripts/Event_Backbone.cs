using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Function:   Finds the event bone object in the scene and calls the event code
Usage:      Used by the scene manager to call the appropriate scene code on scene change
*/
public class Event_Backbone : MonoBehaviour
{
    public void CallSceneEvent(int sceneID){
        Scene_Event se = GameObject.FindGameObjectWithTag("Event Bone").GetComponent<Scene_Event>();
        StartCoroutine(se.ExecuteSceneCode());
    }
}