using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] float speed = 50f;

    private bool can_move = true;

    void Update(){
        if(can_move){
            float input = Input.GetAxis("Horizontal");

            Vector3 movement = new Vector3(speed * input, 0, 0);
            movement *= Time.deltaTime;

            transform.Translate(movement);
        }
    }

    public void DisableMovement(){
        can_move = false;
    }

    public void EnableMovement(){
        can_move = true;
    }

}
