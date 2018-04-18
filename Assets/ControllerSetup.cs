using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ControllerSetup : MonoBehaviour {
    List<uint> controllers = new List<uint>();

    // Use this for initialization
    void Start()
    {
        foreach (var hand in Player.instance.hands)
        {
            if (hand.controller != null && hand.controller.connected)
            {
                hand.gameObject.AddComponent<Controller>();
            }
        }
    }

	private void Update()
	{
        foreach (var hand in Player.instance.hands)
        {
            if(hand.controller != null){
                var controller = hand.controller;
                if(!controllers.Contains(controller.index) && controller.connected){
                    Debug.Log("Controller " + controller.index + " added.");
                    controllers.Add(controller.index);
                    hand.gameObject.AddComponent<Controller>();
                }
            }
        }
	}
}