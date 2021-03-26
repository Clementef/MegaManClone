using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkingEnemyAI : MonoBehaviour
{
	public Transform leftBound;
	public Transform rightBound;
	public float speed;
	public float waitTime;
	Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }
}
