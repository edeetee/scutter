using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ControllerSetup : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        foreach (var hand in Player.instance.hands)
        {
            if (hand.enabled)
            {
                var attach = hand.transform.Find("Attach_ControllerTip") ?? hand.transform;
                hand.gameObject.AddComponent<Controller>();
            }
        }
    }
}
