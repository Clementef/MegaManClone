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

    void Start() {
    	rb = GetComponent<Rigidbody2D>();
    	velocity = new Vector2(0,0);
    	accel = new Vector2(0,0);
    	jumpVec = new Vector2(0,0);
    	gravity = new Vector2(0,-g);
    	grounded = groundChecker.grounded;
        perp = groundChecker.perp;

    	jumpHeightInit = jumpHeight;
    	jumpVel = Mathf.Sqrt(2*g*jumpHeight);
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

    	moveInput = new Vector2(0,0);

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
    	accel = new Vector2(0,0);
        
        if (grounded) {
            perp = groundChecker.perp;
            rb.velocity = (velocity + perp*moveInput.x) * runSpeed;
        } else {
            rb.velocity = (velocity + moveInput) * runSpeed;
        }
    }
}
