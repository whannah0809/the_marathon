using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Function:   Input controller controls when the user is able to control the player
Usage:      Called by event and interaction scripts
*/
public class Input_Controller : MonoBehaviour
{
    [SerializeField] UI_Controller ui;

    private bool can_interact = true;

    /*
    Function:   Diables all user input
    Usage:      Called by event and interaction scripts to disable user input
    */
    public void DisableDefault(){
        Debug.Log("Disabled control");
        Player_Movement movement = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>();
        can_interact = false;

        movement.DisableMovement();
    }

    /*
    Function:   Enables all user input
    Usage:      Called by event and interaction scripts to Enable user input
    */
    public void EnableDefault(){
        Debug.Log("Enabled control");
        Player_Movement movement = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>();
        can_interact = true;

        movement.EnableMovement();

        recalculateCollisions();
    }

    /*
    Function:   Diables all user input with a slight delay
    Usage:      Called when there needs to be a slight delay after event/interaction code terminates
    */
    public void DelayedEnable(){
        StartCoroutine(WaitAndEnable());
    }
    private IEnumerator WaitAndEnable(){
        yield return new WaitForSeconds(0.2f);
        EnableDefault();
    }

    /*
    Function:   Returns whether the user can currently control the player or not
    Usage:      Called when event/interaction scripts need to check if the user can interact
    Return:     Whether the user can interact or not
    */
    public bool QueryInteractable(){
        return can_interact;
    }

    private void recalculateCollisions(){
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        player.GetComponent<Collider>().enabled = false;
        player.GetComponent<Collider>().enabled = true;
    }
}
