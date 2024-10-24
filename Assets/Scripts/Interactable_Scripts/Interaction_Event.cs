using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction_Event : MonoBehaviour
{
    public abstract void InvokeEvent();

    public IEnumerator LookAtObject(Transform target, float rotation_speed){
        Transform player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        Vector3 target_forward = new Vector3(target.position.x, player.position.y, target.position.z);
        Vector3 target_dir = target_forward - player.position;
        while(player.forward != target_dir.normalized){
            player.forward = Vector3.Slerp(player.forward, target_dir.normalized, rotation_speed * Time.deltaTime);
            yield return null;
        }
        Debug.Log("Completed rotation");

        yield return null;
    }
}
