﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAround : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(jumper());
	}
	
    IEnumerator jumper(){
        while(true){
            yield return new WaitForSeconds(Random.Range(0.01f, 0.6f));
			var angle = Extensions.GetPointOnUnitSphereCap(Quaternion.identity, 45);
            var scale = Random.Range(0.1f, 1.5f);
			angle.Scale(Extensions.All(scale));
            SentientListener.randomProp().GetComponent<Rigidbody>().AddForce(angle, ForceMode.VelocityChange);
        }
    }
}
