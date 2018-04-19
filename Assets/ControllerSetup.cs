using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.UI;

public class ControllerSetup : MonoBehaviour {
    List<uint> controllers = new List<uint>();

    //MUST BE IN SAME ORDER
    public List<Text> scoreObjs;
    public List<SpriteRenderer> highlights;

	private void Update()
	{
        foreach (var hand in Player.instance.hands)
        {
            if(hand.controller != null){
                var controller = hand.controller;
                if(!controllers.Contains(controller.index) && controller.connected){
                    Debug.Log("Controller " + controller.index + " added.");
                    controllers.Add(controller.index);
                    var script = hand.gameObject.AddComponent<Controller>();
                    script.scoreObj = scoreObjs[0];
                    script.highlight = highlights[0];
                    scoreObjs.RemoveAt(0);
                    highlights.RemoveAt(0);
                }
            }
        }
	}
}