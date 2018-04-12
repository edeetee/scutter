using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
using System.Collections.Generic;

class Enlarge : MonoBehaviour {
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

		switch (DeathType.Rise) {
		case DeathType.Enlarge:
			transform.DOScale (UnityEngine.Random.Range (1.5f, 2.5f) * transform.localScale.magnitude, time);
			break;
		case DeathType.Hover:
			rigidBody.useGravity = false;
			DOTween.To (() => Physics.gravity, grav => gravity = grav, Vector3.zero, time);
			break;
		case DeathType.Rise:
			rigidBody.useGravity = false;
			Vector3 dir = (new Vector3[] { Vector3.left, -Vector3.left, Vector3.forward, -Vector3.forward }).random ();
			dir.y += 0.6f;
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