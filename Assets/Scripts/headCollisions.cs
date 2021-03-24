using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headCollisions : MonoBehaviour
{
	playerController pc;

	void Start () {
		pc = transform.parent.GetComponent<playerController>();
	}

    void OnTriggerEnter2D(Collider2D collider)
	{
        if (pc.velocity.y>0) {
        	pc.velocity.y=0;
        }
	}
}
