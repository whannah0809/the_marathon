using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] float rotation_speed = 1f;
    [SerializeField] Transform player_parent;

    private bool can_move = false;

    void Start(){
        //transform.forward = transform.right;
    }

    void Update(){
        if(can_move){
            float input = Input.GetAxis("Horizontal");

            Vector3 movement = new Vector3(speed * input, 0, 0);
            movement *= Time.deltaTime;

            player_parent.Translate(movement);
            transform.forward = Vector3.Slerp(transform.forward, movement, rotation_speed * Time.deltaTime);
        }
    }

    public void DisableMovement(){
        can_move = false;
    }

    public void EnableMovement(){
        can_move = true;
    }

}
