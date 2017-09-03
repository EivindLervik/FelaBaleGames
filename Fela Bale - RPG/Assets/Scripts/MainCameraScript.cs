using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScript : MonoBehaviour {

    public Transform target;
    public float followSpeed;

    public Transform myCamera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        myCamera.LookAt(target);
        transform.position = Vector3.Lerp(transform.position, target.position, followSpeed * Time.deltaTime);
	}
}
