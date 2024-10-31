using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction_Event : MonoBehaviour
{
    public abstract void InvokeEvent();

    public IEnumerator LookAtObject(Transform target, float rotation_speed){
        Debug.Log("Rotate called");

        Transform player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        Vector3 target_forward = new Vector3(target.position.x, player.position.y, target.position.z);
        Vector3 target_dir = target_forward - player.position;
        while((player.forward - target_dir.normalized).sqrMagnitude >= 0.0001f){
            player.forward = Vector3.Slerp(player.forward, target_dir.normalized, rotation_speed * Time.deltaTime);
            yield return null;
        }
        Debug.Log("Completed rotation");

        yield return null;
    }

    public IEnumerator MoveToTarget(Transform target, float move_speed){
        Debug.Log("Move called");
        Transform player = GameObject.FindGameObjectWithTag("Player Parent").GetComponent<Transform>();

        Coroutine rotate = StartCoroutine(LookAtObject(target, 10));

        while(Mathf.Abs(player.position.x - target.position.x) > .1f || Mathf.Abs(player.position.z - target.position.z) > .1f) { 
            Vector3 direction = target.position - player.position;

            direction = direction.normalized;

            player.Translate(
                (direction.x * move_speed * Time.deltaTime),
                0,
                (direction.z * move_speed * Time.deltaTime)
            );

            yield return null;
        }

        yield return rotate;
        yield return null;
    }
}
