using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponEngine : MonoBehaviour
{
	public bool semiAuto = false;
    public int roundsPerMinute = 120;
    public int magazineSize = 60;
    public float reloadTime = 3f;
    public int roundsInMagazine = 0;
    public GameObject bullet;
    public Transform shootPoint;
    float roundTime;
    float roundCooldown;
    int rounds;
    bool reloading;
    float reloadCooldown;

    void Start() {
    	if (roundsPerMinute != 0) {
			roundTime = 60f/roundsPerMinute;
		} else {
			roundTime = .5f;
		}
    	roundsInMagazine = magazineSize;
    }

    void Update()
    {
    	if (reloading) {
    		reloadCooldown -= Time.deltaTime;
    		if (reloadCooldown <=0) {
    			reloadWeapon();
    		}
    	}
    	if (Input.GetButton("Reload")) {
    		reloadWeapon();
    	}
        if (roundsInMagazine>0) {
        	if (roundCooldown <= 0) {
        		//shoot
        		if (semiAuto) {
        			if (Input.GetButtonDown("Fire1")) {
						Instantiate(bullet, shootPoint.position, shootPoint.rotation);
						roundCooldown = roundTime;
						roundsInMagazine -= 1;
	        		}
        		} else {
        			//full auto
        			if (Input.GetButton("Fire1")) {
						Instantiate(bullet, shootPoint.position, shootPoint.rotation);
						roundCooldown = roundTime;
						roundsInMagazine -= 1;
	        		}

        		}
        		
        	} else {
        		roundCooldown -= Time.deltaTime;
        	}
        } else {
			if (semiAuto) {
    			if (Input.GetButtonDown("Fire1")) {
					reloadWeapon();
        		}
    		} else {
    			//full auto
    			if (Input.GetButton("Fire1")) {
    				reloadWeapon();
        		}

    		}
        }
    }

    void reloadWeapon() {
    	if (reloading) {
    		if (reloadCooldown <= 0) {
    			//done reloading
    			reloading = false;
    			roundsInMagazine = magazineSize;
    			reloadCooldown = 0;
    		}
    	} else {
    		//starting reload
    		reloading=true;
    		roundsInMagazine=-1;
    		reloadCooldown=reloadTime;
    	}
    }
}
