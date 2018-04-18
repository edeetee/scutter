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
        next = randomProp(transform);
    }


    public static GameObject randomProp(){
        //random free prop
        return Array.FindAll(GameObject.FindGameObjectsWithTag("Sentient"), obj => {
            return !obj.HasComponent<SentientListener>() && !obj.HasComponent<Zombify>();
        }).random();
    }

    public static GameObject randomProp(Transform avoid)
    {
        //random free prop
        return Array.FindAll(GameObject.FindGameObjectsWithTag("Sentient"), obj => {
            //not sentient, not previously used, not within distance
            return !obj.HasComponent<SentientListener>() && !obj.HasComponent<Zombify>() && 2 < Vector3.Distance(avoid.position, obj.transform.position);
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
    static public float triggerDist = 0.3f;
    // Update is called once per frame
    void Update()
    {
        var dist = Vector3.Distance(next.transform.position, controller.transform.position);
        if (dist < triggerDist)
        {
            controller.setTarget(next);
            return;
        }

        //distance from controller
        var normDist = Math.Min(1, dist/3);

        //squared
        normDist *= normDist;
        //normDist = Mathf.Sqrt(normDist);

        float interval = Mathf.Lerp(0.01f, 1f, normDist);
        //interval *= interval;

        //geiger counter
        //if interval has passed
        if (interval < Time.time - lastGeiger)
            lastGeiger = Time.time;

        //if still inside vibrate window at start of interval
        //Math.Lerp = time in each interval to vibrate
        bool geiger = (Time.time - lastGeiger) < 0.03f;
        // var geigerVibration = (float)Math.Sin(Math.PI*2*Time.time/)*2-1;
        
        controller.vibrate(geiger ? Mathf.Lerp(0.6f, 0.2f, normDist) : collisionPulse);
    }
}