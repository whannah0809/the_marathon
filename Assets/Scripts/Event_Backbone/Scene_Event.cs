using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Scene_Event : MonoBehaviour
{
    public abstract IEnumerator ExecuteSceneCode();

    public IEnumerator TranslateObject(GameObject g_object, Transform target, float speed){
        while (Vector3.Distance(g_object.transform.position, target.position) > 0.01f) { 
            g_object.transform.position = Vector3.MoveTowards(g_object.transform.position, target.position, speed * Time.deltaTime);
            yield return null;
        }
        //g_object.transform.position = target.position; // Ensure final position is accurate
    }

    public IEnumerator RotateObject(GameObject g_object, Vector3 target, float speed){
        Quaternion target_Rotation = Quaternion.Euler(target);

        while (Quaternion.Angle(g_object.transform.rotation, target_Rotation) > 0.1f) {
            g_object.transform.rotation = Quaternion.RotateTowards(g_object.transform.rotation, target_Rotation, speed * Time.deltaTime);
            yield return null;
        }
        g_object.transform.rotation = target_Rotation; // Ensure final rotation is accurate
    }
}
