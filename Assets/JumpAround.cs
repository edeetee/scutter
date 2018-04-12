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
            yield return new WaitForSeconds(Random.Range(0.01f, 0.6f));
            var angle = Random.onUnitSphere;
            angle.y = Mathf.Abs(angle.y);
            var scale = Random.Range(0.1f, 2);
            angle.Scale(new Vector3(scale, scale, scale));
            SentientListener.randomProp().GetComponent<Rigidbody>().AddForce(angle, ForceMode.VelocityChange);
        }
    }
}
