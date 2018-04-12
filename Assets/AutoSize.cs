using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class AutoSize : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var chaperone = OpenVR.Chaperone;
		HmdQuad_t quad_T = new HmdQuad_t();
		chaperone.GetPlayAreaRect(ref quad_T);

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
