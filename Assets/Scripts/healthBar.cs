using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{

	public Slider slider;
	public Transform follow;

	public void SetHealth(float health, float maxHealth) {
		if (health==0) {
			slider.gameObject.SetActive(false);

		}
		slider.maxValue = maxHealth;
		slider.value = health;
		}

    void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(follow.position);
    }
}
