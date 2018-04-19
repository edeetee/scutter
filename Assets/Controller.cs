#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
#define WIN
#endif

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using DG.Tweening;
using UnityEngine.UI;
#if WIN
using XInputDotNetPure; // Required in C#
#endif

public class Controller : MonoBehaviour
{
    public bool drawDebug = true;

    Vector3 top = Vector3.zero;
    public GameObject target;
    GameObject next;

    SpringJoint spring;

    Hand hand;

    public Text scoreObj;
    public SpriteRenderer highlight;
    int score = 0;

    //TODO geiger
    void Awake()
    {
        spring = gameObject.AddComponent<SpringJoint>();
        spring.spring = 30f;
        spring.damper = 10f;
        spring.minDistance = 0.01f;
        spring.autoConfigureConnectedAnchor = false;
        GetComponent<Rigidbody>().isKinematic = true;

        hand = transform.GetComponentInParent<Hand>() ?? transform.GetComponent<Hand>();

        GameObject randObj = SentientListener.randomProp();
        setTarget(randObj);
    }

#if WIN
    //gamepad player
    PlayerIndex playerIndex = PlayerIndex.One;
#endif

    float triggerStart = 0;
    Tweener shake;
    const float triggerTime = 1f;

	void OnDisable()
	{
        if (target)
            Destroy(target.GetComponent<SentientListener>());
	}

	void Update()
    {
        //debug update target
        if (spring.connectedBody.gameObject != target)
        {
            setTarget(spring.connectedBody.gameObject);
        }

        if(isConnected()){
            if (hand.controller.GetHairTriggerDown()){
                triggerStart = Time.time;
                shake = target.transform.DOShakeRotation(triggerTime, 40);
            }

            var progress = (Time.time - triggerStart) / triggerTime;

            if(hand.controller.GetHairTrigger()){
                hand.controller.TriggerHapticPulse((ushort)(progress * 700));

                if(1f < progress){
                    Destroy(this);

                    var startScale = target.transform.localScale;
                    //DOTween.To(() => 1f, scale => {
                    //    Debug.Log(scale);
                    //    target.transform.localScale = startScale.Scale(scale);
                    //}, 0f, 1f)
                    target.transform.DOScale(Vector3.zero, 0.3f).OnComplete(() => {
                        Destroy(target.gameObject);
                        Debug.Log("destroyed");
                    });
                }
            } else if(hand.controller.GetHairTriggerUp()){
                shake.Kill();
            }
        }

#if WIN
        // sets up gamepad for vibration testing
        if (!isGamepadConnected())
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                }
            }
        }
#endif
    }

    public void setTarget(GameObject target)
    {
        if (this.target != null){
            score += 1;
            scoreObj.text = score.ToString();
            Destroy(this.target.GetComponent<SentientListener>());
            //this.target.AddComponent<Zombify>();
        }

        if (this.target == target)
        {
            Debug.LogError("tried to re-set a target prop");
            return;
        }

        this.target = target;

        highlight.transform.parent = target.transform;
        highlight.transform.localPosition = Vector3.zero;

        spring.connectedBody = target.GetComponent<Rigidbody>();

        top = Vector3.zero;
        foreach (var collider in target.GetComponents<Collider>())
        {
            var newTop = collider.getTop();

            if (top == Vector3.zero || top.y < newTop.y)
            {
                top = newTop;
            }
        }

        spring.connectedAnchor = top;

        SentientListener listener = target.gameObject.AddComponent<SentientListener>();
        listener.controller = this;
    }

    private bool isConnected(){
        return hand.controller != null && hand.controller.connected;
    }

    public void vibrate(float strength)
    {
        // GamePad.SetVibration(playerIndex, strength, strength);
        if (isConnected()){
            hand.controller.TriggerHapticPulse((ushort)(strength * 1000));
        }
        // for testing
#if WIN
        if(isGamepadConnected())
            GamePad.SetVibration(playerIndex, strength, strength);
#endif
    }

#if WIN
    bool isGamepadConnected(){
        return GamePad.GetState(playerIndex).IsConnected;
    }
#endif
	
	void OnDrawGizmos()
	{
        if(drawDebug){
			Gizmos.DrawWireSphere(transform.position, SentientListener.triggerDist);

			Gizmos.color = Color.cyan;
			// Gizmos.DrawWireSphere(worldTop, 0.05f);
			Vector3 worldTop = target.transform.TransformPoint(top);
			Gizmos.DrawLine(worldTop, new Ray(worldTop, target.transform.up).GetPoint(0.08f));
		}
	}
}
