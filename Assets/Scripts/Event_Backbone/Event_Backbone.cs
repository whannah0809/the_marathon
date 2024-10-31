using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Backbone : MonoBehaviour
{
    public void CallSceneEvent(int sceneID){
        Scene_Event se = GameObject.FindGameObjectWithTag("Event Bone").GetComponent<Scene_Event>();
        StartCoroutine(se.ExecuteSceneCode());
    }
}