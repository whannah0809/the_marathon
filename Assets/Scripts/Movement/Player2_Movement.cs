using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Function:   Moves player 2 character based on player 1 position
    Usage:      Move player 2 in the station scene
*/
public class Player2_Movement : MonoBehaviour
{
    [SerializeField] Transform p2_target; 
    [SerializeField] Transform p1;
    [SerializeField] Player_Movement p_movement;
    
    [Header("Movement settings")]
    [SerializeField] private float activation_threshold = 0.5f; 
    [SerializeField] private float stopping_threshold = 0.01f;
    [SerializeField] private float acceleration = 2f; 
    [SerializeField] private float slowSpeedFactor = 0.3f;

    [SerializeField] private Animator anim;

    private float move_speed; 
    private float rotation_speed; 
    private bool isMoving = false; 
    private float currentSpeed = 0f; 

    void Start()
    {
        //Set the movement and rotation speed to same as player 1
        move_speed = p_movement.speed;
        rotation_speed = p_movement.rotation_speed;
    }

    void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, p2_target.position);

        // Check if the object should start moving
        if (!isMoving && distanceToTarget > activation_threshold)
        {
            Debug.Log(p2_target.parent.position.x);
            isMoving = true;
        }

        // If moving, handle movement and rotation. Update animation states
        if (isMoving && p2_target.parent.position.x < 36.1f)
        {
            anim.SetBool("Walking", true);
            MoveTowardsTarget(distanceToTarget);
        }
        else
        {
            RotateTowardsP1();
            anim.SetBool("Walking", false);
        }
    }

    private void RotateTowardsP1(){
        Vector3 target_forward = new Vector3(p1.position.x, transform.position.y, p1.position.z);
        Vector3 target_dir = target_forward - transform.position;
        if((transform.forward - target_dir.normalized).sqrMagnitude >= 0.0001f){
            transform.forward = Vector3.Slerp(transform.forward, target_dir.normalized, rotation_speed * Time.deltaTime);
        }
    }

    private void MoveTowardsTarget(float distanceToTarget)
    {
        if (distanceToTarget > stopping_threshold)
        {
            //Determine the target speed based on proximity to the target
            float targetSpeed = move_speed;
            if (distanceToTarget < activation_threshold)
            {
                //Reduce speed linearly as the object approaches the stopping threshold
                float normalizedDistance = Mathf.Clamp01((distanceToTarget - stopping_threshold) / (activation_threshold - stopping_threshold));
                targetSpeed = Mathf.Lerp(move_speed * slowSpeedFactor, move_speed, normalizedDistance);
            }

            //Adjust current speed towards target speed
            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime);

            // Rotate the forward vector using Slerp
            Vector3 direction = (p2_target.position - transform.position).normalized;
            if (direction.sqrMagnitude > 0.01f)
            {
                Vector3 newForward = Vector3.Slerp(transform.forward, direction, rotation_speed * Time.deltaTime);
                transform.forward = newForward;
            }

            // Move towards the target position
            transform.position += transform.forward * currentSpeed * Time.deltaTime;
        }
        else
        {
            // Stop moving if close enough
            isMoving = false;
            currentSpeed = 0f;
        }
    }
}
