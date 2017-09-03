using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputHandler : MonoBehaviour {

    private CharacterMotor characterMotor;

    private void Start()
    {
        characterMotor = GetComponent<CharacterMotor>();
    }

    // Update is called once per frame
    void Update () {
        Vector3 movementInput = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        characterMotor.UpdateLastInput(movementInput);
    }
}
