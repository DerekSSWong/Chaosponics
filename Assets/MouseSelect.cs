using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSelect : MonoBehaviour
{	
	GameObject prevOutlined;
	GameObject prevHighlighted;
	Material material;
	
    void Start()
    {
	    prevOutlined = null;
	    prevHighlighted = null;
    }

	
    void Update()
    {
	    
	    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	    Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
	    RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
	    
	    if (Input.GetMouseButtonDown(0)) {
	    	if (hit.collider != null) {
	    		outlineObj(hit.collider.gameObject);
	    	}
	    	else {
	    		deOutline();
	    	}
	    }
	    
	    if (hit.collider != null) {
	    	highlightObj(hit.collider.gameObject);
	    }
	    else {
	    	deHighlight();
	    }
    }
	
	void deOutline() {
		if (prevOutlined != null) {
			prevOutlined.GetComponent<SpriteRenderer>().material.SetFloat("_Outline", 0f);
		}
	}
	
	void outlineObj(GameObject go) {
		if (prevOutlined != null && prevOutlined != go) {
			deOutline();
		}
		if (go.tag == "Plant") {
			go.GetComponent<SpriteRenderer>().material.SetFloat("_Outline", 1f);
			prevOutlined = go;
		}
		
	}
	
	void deHighlight() {
		if (prevHighlighted != null) {
			prevHighlighted.GetComponent<SpriteRenderer>().material.SetFloat("_Contrast", 1f);
		}
	}
	
	void highlightObj(GameObject go) {
		if (prevHighlighted != null && prevHighlighted != go) {
			deHighlight();
		}
		if (go.tag == "Plant") {
			go.GetComponent<SpriteRenderer>().material.SetFloat("_Contrast", 1.1f);
			prevHighlighted = go;
		}
	}
}
