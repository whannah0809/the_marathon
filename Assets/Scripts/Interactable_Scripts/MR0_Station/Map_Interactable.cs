using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Interactable : MonoBehaviour
{
    [SerializeField] Dialogue_Asset map_dialogue;

    private void OnTriggerEnter(Collider collider){
        if(collider.gameObject.CompareTag("Player")){
            Dialogue_Controller dialogue = GameObject.FindGameObjectWithTag("Dialogue Manager").GetComponent<Dialogue_Controller>();
            dialogue.StartDialogue(map_dialogue);
        }
    }
}
