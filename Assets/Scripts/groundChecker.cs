using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundChecker : MonoBehaviour
{
	public bool grounded;
	public LayerMask groundMask;
	public Vector2 normal;
	public Vector2 perp;
	public Vector2 contactPoint;
	GameObject player;
	CircleCollider2D feetCollider;

	void Start () {
		normal = new Vector2(0,0);
		perp = new Vector2(0,0);
		feetCollider = GetComponent<CircleCollider2D>();
		player = transform.parent.gameObject;
	}

	void OnCollisionEnter2D(Collision2D collider)
	{
        CheckIfGrounded(collider);
	}


    void OnCollisionStay2D(Collision2D collider)
	{
	    CheckIfGrounded (collider);
	}

	void OnCollisionExit2D(Collision2D collider)
	{
	    grounded = false;
	    player.transform.parent = null;
	    normal = new Vector2(0,0);
	    contactPoint = new Vector2(0,0);
	    perp = new Vector2(0,0);
	}

	private void CheckIfGrounded(Collision2D collider)
	{
		Vector2 sumContactNormals = new Vector2(0,0);
		Vector2 sumContactPoints = new Vector2(0,0);

        foreach (ContactPoint2D item in collider.contacts) {
            sumContactNormals += item.normal;
            sumContactPoints += item.point;
        }
        contactPoint = sumContactPoints/collider.contacts.Length;
        normal = sumContactNormals/collider.contacts.Length;
        perp = -1*Vector2.Perpendicular(normal);
		RaycastHit2D[] hits;
		if (contactPoint != new Vector2(0,0)) {
			Vector2 circleCenter = new Vector2(transform.position.x,transform.position.y) + feetCollider.offset;
			Vector2 castDirection = (contactPoint - circleCenter).normalized;
			Vector2 radiusVector = castDirection * feetCollider.radius;
			hits = Physics2D.RaycastAll(circleCenter + radiusVector, castDirection, 0.05f, groundMask);
			foreach (RaycastHit2D hit in hits) {
				if (hit.transform.parent.tag == "MovingPlatform") {
					//do something with the moving pla
				}
			}
			
			//draw vectors
			//Debug.DrawRay(circleCenter + radiusVector, castDirection*.05f, Color.blue, .1f);
			//Debug.DrawRay(contactPoint, normal/10, Color.red, .1f);
			//Debug.DrawRay(contactPoint, perp/10, Color.red, .1f);
		} else {
			hits = null;
			player = null;
		}

	    if (hits.Length>0) {
	        grounded = true;
	    }
	}
}
