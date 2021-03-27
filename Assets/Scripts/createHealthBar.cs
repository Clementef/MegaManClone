using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class createHealthBar : MonoBehaviour
{
	public GameObject sliderPrefab;
	GameObject canvas;
	GameObject slider;
	enemyHealth eh;
	healthBar hb;

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        slider = Instantiate(sliderPrefab);
        slider.transform.SetParent(canvas.transform,false);
        eh = transform.GetChild(0).GetComponent<enemyHealth>();
        hb = slider.GetComponent<healthBar>();
        eh.healthBar = hb;
        hb.follow = eh.healthPos;
        print(eh.healthPos);
    }
}
