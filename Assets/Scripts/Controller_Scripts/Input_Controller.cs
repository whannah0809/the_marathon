using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Controller : MonoBehaviour
{
    [SerializeField] UI_Controller ui;

    public void DisableDefault(){
        Player_Movement movement = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>();

        movement.DisableMovement();
    }

    public void EnableDefault(){
        Player_Movement movement = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>();

        movement.EnableMovement();
    }
}
