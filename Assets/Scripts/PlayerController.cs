using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    // Local rigidbody variable to hold a reference to the attached Rigidbody2D component
    new Rigidbody2D rigidbody_;

    public float movementSpeed = 1000.0f;

    public GameObject bomb;

    private float timer;
    private bool timerBool = false;

    void Awake()
    {
        // Setup Rigidbody for frictionless top down movement and dynamic collision
        rigidbody_ = GetComponent<Rigidbody2D>();

        rigidbody_.isKinematic = false;
        rigidbody_.angularDrag = 0.0f;
        rigidbody_.gravityScale = 0.0f;
    }

    void Update()
    {
        // Handle user input
        Vector2 targetVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        Move(targetVelocity);
        
        if (Input.GetKeyDown(KeyCode.Space) && timerBool == false)
        {
            PlacingBomb();
            timerBool = true;
        }

        if (timerBool)
        {
            timer += Time.deltaTime;

            if (timer >= 3f)
            {
                timerBool = false;
                timer = 0f;
            }
        }
        
        
    }

    void Move(Vector2 targetVelocity)
    {
        // Set rigidbody velocity
        rigidbody_.velocity = (targetVelocity * movementSpeed) * Time.deltaTime; // Multiply the target by deltaTime to make movement speed consistent across different framerates
    }

    void PlacingBomb()
    {
        Instantiate(bomb, transform.position, transform.rotation);
    }
}
