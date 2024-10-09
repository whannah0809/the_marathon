using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    [SerializeField] float speed = 50f;

    void Update(){
        float input = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(speed * input, 0, 0);

        movement *= Time.deltaTime;

        transform.Translate(movement);
    }

}
