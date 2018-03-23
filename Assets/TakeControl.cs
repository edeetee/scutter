using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeControl : MonoBehaviour {

	float top = 0;
	Rigidbody target;

	SpringJoint spring;
	Bounds bounds;

	// Use this for initialization
	void Start () {
		spring = GetComponent<SpringJoint>();
		UpdateTarget(spring.connectedBody);
	}

	void Update() {
		if(spring.connectedBody != target){
			UpdateTarget(spring.connectedBody);
		}
	}

	void UpdateTarget(Rigidbody target){
		this.target = target;
		
		foreach (var collider in target.GetComponents<Collider>()) {
			var newTop = collider.bounds.size.y / target.transform.localScale.y / 2;

			// if(collider is BoxCollider)
			// 	newTop = ((BoxCollider)collider).size.y/2 + ((BoxCollider)collider).center.y;
			// else if(collider is CapsuleCollider)
			// 	newTop = ((CapsuleCollider)collider).height/2 + ((CapsuleCollider)collider).center.y;
			// else if(collider is SphereCollider)
			// 	newTop = ((SphereCollider)collider).radius + ((SphereCollider)collider).center.y;
			
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
			Gizmos.DrawWireSphere(target.transform.TransformPoint(new Vector3(0, top, 0)), 0.05f);
			if(bounds != null){
				Gizmos.DrawWireCube(target.position, bounds.size);
			}
		}
	}
}
