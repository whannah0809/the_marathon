using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Utility : MonoBehaviour
{
    [SerializeField] float move_speed;
    [SerializeField] float rotation_speed;

    public void SetSpeeds(Vector3 target_pos, Vector3 target_rot, float targetTime) {
        // Calculate the distance to move
        float distance = Vector3.Distance(transform.position, target_pos);

        // Calculate the angle to rotate
        Quaternion targetRotation = Quaternion.Euler(target_rot);
        float angle = Quaternion.Angle(transform.rotation, targetRotation);

        // Set speeds based on targetTime
        move_speed = distance / targetTime;
        rotation_speed = angle / targetTime;
    }

    public IEnumerator TranslateCamera(Vector3 target_pos) {
        while (Vector3.Distance(transform.position, target_pos) > 0.1f) { 
            transform.position = Vector3.MoveTowards(transform.position, target_pos, move_speed * Time.deltaTime);
            yield return null;
        }
        transform.position = target_pos; // Ensure final position is accurate
    }

    public IEnumerator RotateCamera(Vector3 target_rot) {
        Quaternion targetRotation = Quaternion.Euler(target_rot);

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotation_speed * Time.deltaTime);
            yield return null;
        }
        transform.rotation = targetRotation; // Ensure final rotation is accurate
    }
}
