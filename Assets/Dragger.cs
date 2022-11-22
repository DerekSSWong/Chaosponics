using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
	Vector3 dragOffset;
	void OnMouseDown() {
		//Debug.Log("test");
		dragOffset = transform.position - GetMousePos();
	}
	
	void OnMouseDrag() {
		transform.position = GetMousePos() + dragOffset;
	}
	
	Vector3 GetMousePos() {
		var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePos.z = 0;
		return mousePos;
	}
}
