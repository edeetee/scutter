using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {
    new public Camera camera;
	
	// Update is called once per frame
	void Update () {
        transform.forward = camera.transform.forward;
	}
}
