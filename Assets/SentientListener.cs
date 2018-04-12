using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Valve.VR;

public class SentientListener : MonoBehaviour{
	// public SteamVR_Controller.Device controller;
    public Controller controller;
    new Rigidbody rigidbody;

    GameObject next;
    float collisionPulse = 0;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        next = randomProp();
    }


    public static GameObject randomProp(){
        //random free prop
        return Array.FindAll(GameObject.FindGameObjectsWithTag("Sentient"), obj => {
            return obj.GetComponent<SentientListener>() == null && obj.GetComponent<Enlarge>() == null;
        }).random();
    }

    // float calcStrength(Collision collision){
    //     // return (collision.impulse/Time.fixedDeltaTime).magnitude;
    //     // return collision.relativeVelocity.magnitude;
    //     return ;
    //     // return (collision.impulse/collision.collider.GetComponent<Rigidbody>().mass/Time.fixedDeltaTime).magnitude;
    // }

    float startCollision = 0;
    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject == next){

        } else if(collision.collider.tag == "Sentient"){
            startCollision = Time.time;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        //hold loud collision for a time
        if(collision.collider.tag == "Sentient"){
            var mod = 0.2 < Time.time - startCollision ? 0.12f : 0.25f;
            collisionPulse = Math.Min(rigidbody.velocity.sqrMagnitude/2, 1.0f)*mod;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        collisionPulse = 0;
    }

    void OnDisable()
    {
        controller.vibrate(0);
    }

    // const float pulseProp
    float lastGeiger = 0;
    static public float triggerDist = 0.1f;
    // Update is called once per frame
    void Update()
    {
        //distance from controller
		var targetDist = Math.Min(1, Vector3.Distance(next.transform.position, controller.transform.position)/3);

		//squared
		targetDist *= targetDist;

        if(targetDist < triggerDist){
            controller.setTarget(next);
            return;
        }

        float interval = Mathf.Lerp(0.01f, 2f, targetDist);
        interval *= interval;

        //geiger counter
        //if interval has passed
        if (interval < Time.time - lastGeiger)
            lastGeiger = Time.time;

        //if still inside vibrate window at start of interval
        //Math.Lerp = time in each interval to vibrate
        bool geiger = Time.time - lastGeiger < Mathf.Lerp(0.01f, 0.2f, targetDist);
        // var geigerVibration = (float)Math.Sin(Math.PI*2*Time.time/)*2-1;
        
        controller.vibrate(geiger ? 0.6f : collisionPulse);
    }
}