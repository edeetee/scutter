using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(SpringJoint)), RequireComponent(typeof(SteamVR_TrackedObject))]
public class TakeControl : MonoBehaviour {
	public bool drawDebug = true;
    public int controllerIndex;
            
	Vector3 top = Vector3.zero;
	Rigidbody target;

	SpringJoint spring;
	Collider highestCol;

	SteamVR_Controller.Device controller;

	void Awake () {
		spring = GetComponent<SpringJoint>();

        int index = (int)GetComponent<SteamVR_TrackedObject>().index;
        controller = SteamVR_Controller.Input(index);

		GameObject randObj = GameObject.FindGameObjectsWithTag("Sentient").Shuffle()[0];
		SetTarget(randObj.GetComponent<Rigidbody>());
	}

	void FixedUpdate()
	{
        transform.position = controller.transform.pos;
	}

	void Update() {
		if(spring.connectedBody != target){
			SetTarget(spring.connectedBody);
		}
	}

	void SetTarget(Rigidbody target){
		this.target = target;
		if(spring.connectedBody != target)
			spring.connectedBody = target;

		top = Vector3.zero;
		
		foreach (var collider in target.GetComponents<Collider>()) {
			var newTop = collider.getTop();
			
			if(top == Vector3.zero || top.y < newTop.y){
				top = newTop;
				highestCol = collider;
			}
		}

		spring.autoConfigureConnectedAnchor = false;
		spring.connectedAnchor = top;

		SentientListener listener = target.GetComponent<SentientListener>() ?? target.gameObject.AddComponent<SentientListener>();
		listener.controller = controller;
	}
	
	void OnDrawGizmos()
	{
		if(spring.connectedBody != target){
			SetTarget(spring.connectedBody);
		}
		if(drawDebug){
			Matrix4x4 rotationMatrix = Matrix4x4.TRS(target.position, target.rotation, Vector3.one);
			// Gizmos.matrix = rotationMatrix;
			Gizmos.DrawWireSphere(transform.position, 0.05f);

			Gizmos.color = Color.cyan;
			// Gizmos.DrawWireSphere(worldTop, 0.05f);
			Vector3 worldTop = target.transform.TransformPoint(top);
			Gizmos.DrawLine(worldTop, new Ray(worldTop, target.transform.up).GetPoint(0.08f));
			// Gizmos.matrix = Matrix4x4.identity;
			// Gizmos.DrawWireCube(highestCol.bounds.center, highestCol.bounds.size);
		}
	}
}
