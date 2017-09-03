using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour {

    public Transform model;
    public float maximumSpeed;
    public float acceleration;

    private Rigidbody body;
    private Vector3 lastInput;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    public void UpdateLastInput(Vector3 lastInput)
    {
        this.lastInput = lastInput;
    }

    private void FixedUpdate()
    {
        // Initialize variables
        Vector3 appliedForce = lastInput * acceleration;
        Vector3 planeVelocity = body.velocity;
        planeVelocity.y = 0.0f;


        // Do rotation of object
        if (planeVelocity.magnitude > 0.01f)
        {
            model.transform.forward = planeVelocity.normalized;
        }


        // Add forces
        body.AddForce(appliedForce);

        // Limit the speed
        if(planeVelocity.magnitude > maximumSpeed)
        {
            body.velocity = Vector3.ClampMagnitude(body.velocity, maximumSpeed);
        }
    }

}
