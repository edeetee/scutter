using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAround : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(jumper());
	}
	
    IEnumerator jumper(){
        while(true){
            yield return new WaitForSeconds(Random.Range(0.3f, 2f));
			var angle = Random.insideUnitSphere;
            var scale = Random.Range(0.0001f, 0.01f);
			angle.Scale(scale);
            Rigidbody push = SentientListener.randomProp().GetComponent<Rigidbody>();
            push.AddForce(angle, ForceMode.VelocityChange);
            push.AddTorque(Random.insideUnitSphere.Scale(Random.Range(0, 0.3f)), ForceMode.VelocityChange);
        }
    }
}
