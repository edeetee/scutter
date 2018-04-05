using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using XInputDotNetPure; // Required in C#
using VRTK;


public class SentientListener : MonoBehaviour{
    bool playerIndexSet = false;
    PlayerIndex playerIndex;

    public VRTK_ControllerReference controller;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Sentient"){
            float strength = getStrength(collision)*0.01f;
            Debug.Log("Velocity Strength: " + strength);
            pulse(strength);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if(collision.collider.tag == "Sentient"){
            float strength = (collision.impulse/collision.collider.GetComponent<Rigidbody>().mass/Time.fixedDeltaTime).magnitude*0.01f;
            Debug.Log("Collision Strength: " + strength);
            pulse(strength);
        }
    }

    float getStrength(Collision collision){
        // return (collision.impulse/Time.fixedDeltaTime).magnitude;
        return (collision.impulse/collision.collider.GetComponent<Rigidbody>().mass/Time.fixedDeltaTime).magnitude;
    }

    void OnCollisionExit(Collision collision)
    {
        pulse(0);
    }

    void pulse(float strength){
        // GamePad.SetVibration(playerIndex, strength, strength);
        VRTK_SDK_Bridge.HapticPulse(controller, strength);
    }
    
    void Update(){
        if (!playerIndexSet && GamePad.GetState(playerIndex).IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }
    }
}