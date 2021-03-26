using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float runSpeed = 1;
    public Vector2 velocity;
    public Vector2 accel;
    public float g;
    public float jumpHeight;
    float jumpHeightInit;
    float jumpVel;
    Vector2 gravity;
    Vector2 jumpVec;
    bool grounded;
    Vector2 perp;
    public groundChecker groundChecker;
    Rigidbody2D rb;
    Vector2 moveInput;
    bool left,right,up,down;
    bool oldJumpInput = false;
    bool jumping = false;
    public Transform followPlatform = null;
    Vector3 oldPlatPosition;
    Vector3 newPlatPosition;

    void Start() {
    	rb = GetComponent<Rigidbody2D>();
    	velocity = Vector2.zero;
    	accel = Vector2.zero;
    	jumpVec = Vector2.zero;
    	gravity = new Vector2(0,-g);
    	grounded = groundChecker.grounded;
        perp = groundChecker.perp;

    	jumpHeightInit = jumpHeight;
    	jumpVel = Mathf.Sqrt(2*g*jumpHeight);

        oldPlatPosition = Vector3.zero;
        newPlatPosition = Vector3.zero;
    }

    void Update()
    {
    	if (Input.GetButton("Left")) {
    		left=true;
    	} else {
    		left=false;
    	}
    	if (Input.GetButton("Right")) {
    		right=true;
    	} else {
    		right=false;
    	}

        grounded = groundChecker.grounded;
        if (grounded && !oldJumpInput) {
            if (Input.GetButtonDown("Up")) {
                oldJumpInput = true;
                jumping = true;
            }
        } else if (grounded && oldJumpInput && !jumping) {
            oldJumpInput=false;
        }

    	moveInput = Vector2.zero;

    	if (left==true||right==true) {
    		if (left==false) {
    			//right only
    			moveInput.x = 1;
			} else if (right==false) {
				//left only
				moveInput.x = -1;
			} else {
				//both left and right
				moveInput.x = 0;
			}
    	}

    	//normalize
    	moveInput = moveInput.normalized;
    }

    void FixedUpdate() {
        //platform movement
        rb.velocity = new Vector2(0,0);
        // Vector3 differenceVector = Vector3.zero;
        // if (followPlatform != null) {
        //     if (oldPlatPosition == Vector3.zero) {
        //         oldPlatPosition = followPlatform.position;
        //     } else {
        //         newPlatPosition = followPlatform.position;
        //         differenceVector = newPlatPosition-oldPlatPosition;
        //         oldPlatPosition = newPlatPosition;
        //         newPlatPosition = Vector3.zero;
        //         print("generated diference vec" + differenceVector.ToString("F4"));
        //     }
        // } else {
        //     oldPlatPosition = Vector3.zero;
        //     newPlatPosition = Vector3.zero;
        // }

        // if (differenceVector != Vector3.zero) {
        //     //apply difference to player position;
        //     Vector2 diffVec2D = new Vector2(differenceVector.x,differenceVector.y);
        //     if (diffVec2D.y > 0) {
        //         diffVec2D.y = 0;
        //     }
        //     print("applying " + diffVec2D.ToString("F4"));
        //     rb.velocity += diffVec2D*100;
        // }

    	grounded = groundChecker.grounded;
    	if (jumpHeight != jumpHeightInit) {
    		jumpVel = Mathf.Sqrt(2*g*jumpHeight);
    	}

    	jumpVec.y = 0;
    	if (!grounded) {
    		accel += gravity;
    	} else {
    		if (velocity.y<0) {
    			velocity.y = 0;
    		}

    		if (jumping) {
    			velocity.y += jumpVel;
                jumping = false;
    		}
    	}
    	
    	velocity += accel;
    	accel = Vector2.zero;
        
        if (grounded) {
            perp = groundChecker.perp;
            rb.velocity += (velocity + perp*moveInput.x) * runSpeed;
        } else {
            rb.velocity += (velocity + moveInput) * runSpeed;
        }
    }
}
