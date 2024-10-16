using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Controller : MonoBehaviour
{
    [SerializeField] UI_Controller ui;

    private bool can_interact = true;

    public void DisableDefault(){
        Debug.Log("Disabled control");
        Player_Movement movement = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>();
        can_interact = false;

        movement.DisableMovement();
    }

    public void EnableDefault(){
        Debug.Log("Enabled control");
        Player_Movement movement = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>();
        can_interact = true;

        movement.EnableMovement();

        recalculateCollisions();
    }

    public void DelayedEnable(){
        StartCoroutine(WaitAndEnable());
    }

    public bool QueryInteractable(){
        return can_interact;
    }

    private void recalculateCollisions(){
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        player.GetComponent<Collider>().enabled = false;
        player.GetComponent<Collider>().enabled = true;
    }

    private IEnumerator WaitAndEnable(){
        yield return new WaitForSeconds(0.2f);
        EnableDefault();
    }
}
