using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure; // Required in C#

public class SentientListener : MonoBehaviour{
    bool playerIndexSet = false;
    PlayerIndex playerIndex;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Sentient"){
            float strength = collision.relativeVelocity.magnitude*1f;
            Debug.Log("Collidion Strength: " + strength);
            GamePad.SetVibration(playerIndex, strength, strength);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if(collision.collider.tag == "Sentient"){
            float strength = (collision.impulse/Time.fixedDeltaTime).magnitude*0.01f;
            Debug.Log("Collidion Strength: " + strength);
            GamePad.SetVibration(playerIndex, strength, strength);
        }
    }

    float getStrength(Collision collision){
        // return (collision.impulse/Time.fixedDeltaTime).magnitude;
        return collision.relativeVelocity.magnitude;
    }

    void OnCollisionExit(Collision collision)
    {
        GamePad.SetVibration(playerIndex, 0, 0);
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