using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCamera : MonoBehaviour {

    public Transform target;
    public float behind;
    public float over;

    public float startHeight;

	// Use this for initialization
	void Start () {
        startHeight = target.position.y + over;

    }
	
	// Update is called once per frame
	void Update () {
        Vector3 targetPos = new Vector3();
        targetPos.z = target.position.z - behind;
        targetPos.y = startHeight;
        transform.position = targetPos;
	}
}
