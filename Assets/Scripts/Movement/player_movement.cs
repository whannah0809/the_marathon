using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] public float speed = 5;
    [SerializeField] public float rotation_speed = 1f;
    [SerializeField] private bool anim_control = true;
    [SerializeField] Transform player_parent;
    [SerializeField] Animator anim;

    [SerializeField] private float minX = -10f; // Minimum boundary for x
    [SerializeField] private float maxX = 35.2f; // Maximum boundary for x

    private bool can_move = false;

    public void GiveAnimControl()
    {
        anim_control = true;
    }

    public void TakeAnimControl()
    {
        anim_control = false;
        anim.SetBool("Walking", false);
    }

    void Update()
    {
        if (can_move)
        {
            float input = Input.GetAxis("Horizontal");

            if (input != 0 && anim_control)
            {
                anim.SetBool("Walking", true);
            }
            else
            {
                anim.SetBool("Walking", false);
            }

            Vector3 movement = new Vector3(speed * input, 0, 0);
            movement *= Time.deltaTime;

            // Move the player
            player_parent.Translate(movement);

            // Clamp the position to stay within boundaries
            Vector3 clampedPosition = player_parent.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
            player_parent.position = clampedPosition;

            // Update the forward direction
            if (movement != Vector3.zero)
            {
                transform.forward = Vector3.Slerp(transform.forward, movement, rotation_speed * Time.deltaTime);
            }
        }
    }

    public void DisableMovement()
    {
        can_move = false;
    }

    public void EnableMovement()
    {
        can_move = true;
    }
}
