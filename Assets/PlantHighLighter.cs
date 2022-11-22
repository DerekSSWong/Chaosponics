using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantHighLighter : MonoBehaviour
{	
	GameObject prevHighlighted;
	GameObject prevOutlined;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
	{	
		GameObject currHighlighted;
		
		List<GameObject> sample = this.GetComponent<SelectionController>().getSample();
		bool inFocus = this.GetComponent<SelectionController>().isInFocus();
		
		//Highlght handling
		if (sample != null && sample.Count > 0) {
			currHighlighted = sample[0];
			if (currHighlighted != prevHighlighted) {
				deHighlight(prevHighlighted);
			}
			highlight(currHighlighted);
			prevHighlighted = currHighlighted;
		} else {
			deHighlight(prevHighlighted);
		}
	}
    
    
    
    
	private void highlight(GameObject obj) {
		if (obj != null) {
			obj.GetComponent<SpriteRenderer>().material.SetFloat("_Contrast", 1.2f);
		}
	}
	private void deHighlight(GameObject obj){
		if (obj != null) {
			obj.GetComponent<SpriteRenderer>().material.SetFloat("_Contrast", 1f);
		}
	}
	
	private void outline(GameObject obj) {
		if (obj != null) {
			obj.GetComponent<SpriteRenderer>().material.SetFloat("_Outline", 1f);
		}
	}
	private void deOutline(GameObject obj) {
		if (obj != null) {
			obj.GetComponent<SpriteRenderer>().material.SetFloat("_Outline", 0f);
		}
	}
	
	public void focusOn(GameObject obj) {
		deOutline(prevOutlined);
		outline(obj);
		prevOutlined = obj;
	}
}
