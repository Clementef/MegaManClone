using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float bulletVelocity = 10f;
	public float lifeTime = 5f;
	float life;
    Rigidbody2D rb;

	void Start() {
		life = lifeTime;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletVelocity;
	}

    void Update()
    {
        life -= Time.deltaTime;

        if (life <= 0) {
        	Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        enemyHealth eh = col.transform.gameObject.GetComponent<enemyHealth>();
        if (eh) {
            eh.Damage(1);
        }
    	Destroy(gameObject);
    }
}
