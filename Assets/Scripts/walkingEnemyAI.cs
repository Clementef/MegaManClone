using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkingEnemyAI : MonoBehaviour
{
	public Transform leftBound;
	public Transform rightBound;
	public Transform walkerLeftBound;
	public Transform walkerRightBound;
	public float speed;
	public float waitTime;
	enemyHealth eh;
	Rigidbody2D rb;
	float target;
	Transform seekingPosition;
	bool pausing;
	bool targetRight;
	float currentWalkerPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        eh = GetComponent<enemyHealth>();
        target = rightBound.position.x;
        seekingPosition = walkerRightBound;
        targetRight=true;
    }

    void FixedUpdate()
    {
    	if (!eh.dead) {
	    	currentWalkerPosition = seekingPosition.position.x;
	    	Vector2 moveVec = new Vector2(target-currentWalkerPosition,0);
	    	if (Mathf.Abs(moveVec.x) > speed * Time.fixedDeltaTime) {
	    		rb.MovePosition(rb.position + moveVec.normalized * speed * Time.fixedDeltaTime);
	    	} else {
	    		if (!pausing) {
	    			pausing=true;
	    			rb.MovePosition(new Vector2(target+(rb.position.x-currentWalkerPosition),rb.position.y));
	    			Invoke("changeTarget",waitTime);
	    		}
	    	}
    	}
    }

    void changeTarget() {
    	pausing = false;
    	if (targetRight==true) {
		    target = leftBound.position.x;
		    seekingPosition = walkerLeftBound;
		    targetRight=false;
    	} else {
		    target = rightBound.position.x;
		    seekingPosition = walkerRightBound;
		    targetRight=true;
    	}
    }
}
