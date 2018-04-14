using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCamera : MonoBehaviour {

    public Transform target;

    private Vector3 difference;

	// Use this for initialization
	void Start () {
        difference = target.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(transform.position, target.position - difference, Time.deltaTime * 100.0f);
	}
}
