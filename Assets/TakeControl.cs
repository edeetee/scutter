using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeControl : MonoBehaviour {

	float top = 0;
	Rigidbody me;
	Rigidbody target;

	Bounds bounds;

	// Use this for initialization
	void Start () {
		me = GetComponent<Rigidbody>();
		var spring = GetComponent<SpringJoint>();
		target = spring.connectedBody;
		
		foreach (var collider in target.GetComponents<Collider>()) {
			var newTop = collider.bounds.size.y / target.transform.localScale.y / 2;
			if(top < newTop){
				top = newTop;
				bounds = collider.bounds;
			}
		}

		spring.autoConfigureConnectedAnchor = false;
		spring.connectedAnchor = new Vector3(0, top, 0);
	}

	void OnDrawGizmos()
	{
		if(UnityEditor.EditorApplication.isPlaying){
			Gizmos.color = Color.cyan;
			Gizmos.DrawWireSphere(target.transform.TransformPoint(new Vector3(0, top, 0)), 0.1f);
			if(bounds != null){
				Gizmos.DrawWireCube(target.position, bounds.size);
			}
		}
	}

	// void FixedUpdate () {
	// 	var delta = me.position - (target.position + target.centerOfMass);
	// 	target.AddForce(delta);
	// }
}
