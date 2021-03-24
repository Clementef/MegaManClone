using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cameraTransition : MonoBehaviour
{
	public CinemachineVirtualCamera vm;
	public PolygonCollider2D transitionArea;
    
    void OnTriggerEnter2D(Collider2D collider) {
    	vm.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = transitionArea;
    	vm.GetComponent<CinemachineConfiner2D>().InvalidateCache();
    }
}
