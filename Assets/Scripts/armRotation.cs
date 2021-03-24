using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armRotation : MonoBehaviour
{
	SpriteRenderer sp;
	float angle;
	int mode = 1;

	public Transform shootPoint;
	public Transform sp1;
	public Transform sp2;

	void Start() {
		sp = GetComponent<SpriteRenderer>();
	}

     void Update () {
	     Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
	     Vector3 dir = Input.mousePosition - pos;
	     angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
	     if (Mathf.Abs(angle)>90) {
	     	sp.flipY=true;
	     	if (mode == 1) {
	     		mode = 2;
	     		shootPoint.position = sp2.position;
	     	}
	     } else {
	     	sp.flipY=false;
	     	if (mode == 2) {
	     		mode = 1;
	     		shootPoint.position = sp1.position;
	     	}
	     }
	     transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); 
	 }
}
