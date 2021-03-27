using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
	public int health;
	int currentHealth;
	public healthBar healthBar;
	public float deathDelay = .5f;
	public bool dead;
	public Transform healthPos;

	void Awake() {
		healthPos = transform.GetChild(2);
	}
	
    void Start()
    {
    	dead=false;
        currentHealth = health;
        if (healthBar) {
			healthBar.SetHealth(currentHealth,health);
        }
    }

    public void Damage(int damage) {
    	if (currentHealth-damage>0) {
    		currentHealth-=damage;
    	} else {
    		currentHealth=0;
    		dead = true;
    	}
    	healthBar.SetHealth(currentHealth,health);

    	if (dead) {
    		Invoke("KillEnemy",deathDelay);
    	}
    }

    void KillEnemy() {
    	Destroy(gameObject);
    }
}