using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneControls : MonoBehaviour {

    private enum FlightMode
    {
        Cinematic, Flight, Combat
    }

    public Transform model;
    public float turnEffect;
    public float rollMagnitude;
    public float pitchMagnitude;
    public float acceleration;
    public float flyspeed;

    public GameObject followCamera, turretCamera;
    public Transform turret;

    public Vector4 maximums;

    private Vector3 turnTarget;
    private Vector3 virtualRotation;
    private float startHeight;
    private FlightMode flightMode;

    private Rigidbody body;

    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody>();
        startHeight = transform.position.y;

        flightMode = FlightMode.Cinematic;
    }

    private void Update()
    {
        if (turretCamera.activeSelf && flightMode != FlightMode.Combat)
        {
            SwitchToCombatMode();
        }
        if (followCamera.activeSelf && flightMode != FlightMode.Flight)
        {
            SwitchToFlightMode();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchToCombatMode();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            SwitchToFlightMode();
        }

        if (flightMode == FlightMode.Combat)
        {

            if (Input.GetAxis("Horizontal") > 0.0f)
            {
                turret.Rotate(turret.up, 100.0f * Time.deltaTime);
            }
            else if (Input.GetAxis("Horizontal") < 0.0f)
            {
                turret.Rotate(turret.up, -100.0f * Time.deltaTime);
            }

            turretCamera.transform.eulerAngles = turret.eulerAngles;

        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        if(flightMode == FlightMode.Flight)
        {
            Vector3 push = new Vector3();

            if (Input.GetAxis("Horizontal") > 0.0f)
            {
                turnTarget.z = -rollMagnitude;
                push.x += 50;
            }
            else if (Input.GetAxis("Horizontal") < 0.0f)
            {
                turnTarget.z = rollMagnitude;
                push.x -= 50;
            }
            else
            {
                turnTarget.z = 0.0f;
            }

            if (Input.GetAxis("Vertical") > 0.0f)
            {
                turnTarget.x = pitchMagnitude;
                push.y -= 50;
            }
            else if (Input.GetAxis("Vertical") < 0.0f)
            {
                turnTarget.x = -pitchMagnitude;
                push.y += 50;
            }
            else
            {
                turnTarget.x = 0.0f;
            }

            body.AddForce(push * acceleration);

            Vector3 resetPos = transform.position;
            Vector3 newVel = body.velocity;
            if (transform.position.x < maximums.x)
            {
                resetPos.x = maximums.x;
                newVel.x = 0.0f;
            }
            else if (transform.position.x > maximums.y)
            {
                resetPos.x = maximums.y;
                newVel.x = 0.0f;
            }
            if (transform.position.y - startHeight < maximums.z)
            {
                resetPos.y = maximums.z + startHeight;
                newVel.y = 0.0f;
            }
            else if (transform.position.y - startHeight > maximums.w)
            {
                resetPos.y = maximums.w + startHeight;
                newVel.y = 0.0f;
            }
            transform.position = resetPos;
            body.velocity = newVel;

            virtualRotation = Vector3.Lerp(virtualRotation, turnTarget, Time.fixedDeltaTime * turnEffect);
            model.localEulerAngles = virtualRotation;
        }
        else if (flightMode == FlightMode.Combat)
        {
            model.localEulerAngles = Vector3.zero;
        }


        Vector3 forceTemp = body.velocity;
        forceTemp.z = flyspeed * Time.fixedDeltaTime;
        body.velocity = forceTemp;

        
    }

    public void SwitchToCombatMode()
    {
        turretCamera.SetActive(true);
        followCamera.SetActive(false);

        body.velocity = Vector3.zero;
        turnTarget = Vector3.zero;
        transform.position = new Vector3(0.0f, startHeight, transform.position.z);

        flightMode = FlightMode.Combat;
    }

    public void SwitchToFlightMode()
    {
        turretCamera.SetActive(false);
        followCamera.SetActive(true);
        flightMode = FlightMode.Flight;
    }
}
