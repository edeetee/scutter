using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
using System.Collections.Generic;

class Zombify : MonoBehaviour {
	Rigidbody rigidBody;
	Vector3 gravity;

	enum DeathType {
		Enlarge,
		Hover,
		Rise
	}

	void Awake(){
		rigidBody = gameObject.GetComponent<Rigidbody>();

		var time = 10f;
		var deathType = ((DeathType[])Enum.GetValues (typeof(DeathType))).random ();

        switch (deathType) {
    		case DeathType.Enlarge:
    			transform.DOScale (UnityEngine.Random.Range (1.3f, 2f) * transform.localScale.magnitude, time);
    			break;
    		case DeathType.Hover:
    			rigidBody.useGravity = false;
    			DOTween.To (() => Physics.gravity, grav => gravity = grav, new Vector3(0, 0.01f), time);
    			break;
    		case DeathType.Rise:
    			rigidBody.useGravity = false;
    			Vector3 dir = (new Vector3[] { Vector3.left, -Vector3.left, Vector3.forward, -Vector3.forward }).random ();
                dir.Scale(0.05f);
    			dir.y += 1;
    			DOTween.To (() => Physics.gravity, grav => gravity = grav, dir, time);
    			break;
		}
    }

    void FixedUpdate()
    {
		if (!rigidBody.useGravity) {
			rigidBody.AddForce (gravity);
		}
    }
}