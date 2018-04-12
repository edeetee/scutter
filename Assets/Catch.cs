using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Catch : MonoBehaviour {
	List<Controller> controllers;
	Camera vrCamera;

	// Use this for initialization
	void Start () {
		vrCamera = Player.instance.hmdTransform.GetComponent<Camera> ();
		controllers = GetComponentsInChildren<Controller> ();
	}
	
	// Update is called once per frame
	void Update () {
		foreach(var controller in controllers){
			Vector2 pointOnScreen = vrCamera.WorldToScreenPoint (controller.target);
			var onScreen = new Rect (0, 0, 1f, 1f).Contains (pointOnScreen);
			var dist = Vector3.Distance (vrCamera.transform.position, controller.target.transform.position);
			if (onScreen && dist < 1)
				Destroy (Controller);
		}
	}
}
