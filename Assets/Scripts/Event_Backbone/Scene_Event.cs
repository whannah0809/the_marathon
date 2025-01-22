using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Function:   An abstract scene event class that is inherited by one script per event. Handles the sequence
                of events to occur in a given scene
    Usage:      Used by the event backbone when entering the scene
*/
public abstract class Scene_Event : MonoBehaviour
{
    private bool dialogue_continue;

    /*
    Function:   Abstract scene code to be overriden with scene event sequence
    Usage:      Called by the event backbone when entering the scene
    */
    public abstract IEnumerator ExecuteSceneCode();

    /*
    Function:   Moves specified object to a specified location 
    Usage:      Utility code used by scene event code that inherit this class
    Input:      g_object    -> The game object to translate
                target      -> The target location identified by a transform component
                speed       -> The speed of the translation action 
    */
    public IEnumerator TranslateObject(GameObject g_object, Transform target, float speed){
        while (Vector3.Distance(g_object.transform.position, target.position) > 0.01f) { 
            g_object.transform.position = Vector3.MoveTowards(g_object.transform.position, target.position, speed * Time.deltaTime);
            yield return null;
        }
    }

    /*
    Function:   Rotates specified object to a specified xyz rotation 
    Usage:      Utility code used by scene event code that inherit this class
    Input:      g_object    -> The game object to translate
                target      -> The target rotation identified by a vector3
                speed       -> The speed of the rotation action 
    */
    public IEnumerator RotateObject(GameObject g_object, Vector3 target, float speed){
        Quaternion target_Rotation = Quaternion.Euler(target);

        while (Quaternion.Angle(g_object.transform.rotation, target_Rotation) > 0.1f) {
            g_object.transform.rotation = Quaternion.RotateTowards(g_object.transform.rotation, target_Rotation, speed * Time.deltaTime);
            yield return null;
        }
        g_object.transform.rotation = target_Rotation;
    }

    /*
    Function:   Coroutine used to block methods until the current dialogue terminates
    Usage:      Utility code used by scene event code that inherit this class
    */
    public void EventHandler(){
        dialogue_continue = true;
    }

    public IEnumerator WaitForDialogueEnd() {
        yield return new WaitUntil(() => dialogue_continue);
        dialogue_continue = false;
        yield return null;
    }
}
