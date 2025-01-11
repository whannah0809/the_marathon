using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction_Event : MonoBehaviour
{
    private bool dialogue_continue;

    public abstract void InvokeEvent();

    public void EventHandler(){
        dialogue_continue = true;
    }

    public IEnumerator WaitForDialogueEnd() {
        yield return new WaitUntil(() => dialogue_continue);
        dialogue_continue = false;
        yield return null;
    }

    public IEnumerator LookAtObject(Transform target, float rotation_speed){
        Debug.Log("Rotate called");

        Transform player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        yield return StartCoroutine(RotateTarget(player, target, rotation_speed));
    
        yield return null;
    }

    public IEnumerator RotateTarget(Transform to_rotate, Transform target, float rotation_speed){
        Vector3 target_forward = new Vector3(target.position.x, to_rotate.position.y, target.position.z);
        Vector3 target_dir = target_forward - to_rotate.position;
        while((to_rotate.forward - target_dir.normalized).sqrMagnitude >= 0.0001f){
            to_rotate.forward = Vector3.Slerp(to_rotate.forward, target_dir.normalized, rotation_speed * Time.deltaTime);
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
