using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public Transform platform;
    public float moveRate;
    public float pauseTime;
    public Rigidbody2D rb;
    Vector3 target;
    int targetID;
    bool pausing;

    void Start() {
    	platform.position = startPoint.position;
    	target = endPoint.position;
    	targetID = 1;
    	pausing=false;
    }

    void FixedUpdate() {
    	Vector2 moveVec = target-platform.position;
    	if (moveVec.magnitude > moveRate*Time.deltaTime) {
    		rb.MovePosition(rb.position+moveVec.normalized*moveRate*Time.deltaTime);
    	} else {
    		if (!pausing) {
    			pausing=true;
    			rb.MovePosition(target);
    			Invoke("changeTarget",pauseTime);
    		}
    	}
    }

    void changeTarget() {
    	pausing=false;
    	if (targetID == 1) {
			target = startPoint.position;
			targetID = 0;
		} else {
			target = endPoint.position;
			targetID = 1;
		}
    }
}
