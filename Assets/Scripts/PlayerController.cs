﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    // Local rigidbody variable to hold a reference to the attached Rigidbody2D component
    new Rigidbody2D rigidbody_;

    public float movementSpeed = 1000.0f;

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
    }

    void Move(Vector2 targetVelocity)
    {
        // Set rigidbody velocity
        rigidbody_.velocity = (targetVelocity * movementSpeed) * Time.deltaTime; // Multiply the target by deltaTime to make movement speed consistent across different framerates
    }
}
