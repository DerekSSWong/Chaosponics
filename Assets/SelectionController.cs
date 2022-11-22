using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TMPro;

public class SelectionController : MonoBehaviour
{	
	[SerializeField] GameObject textField;
	[SerializeField] GameObject plantInfo;
	bool isOn;
	//TODO: Remember to edit the prefabs
	string[] layers = {"Fruit", "TopPlant", "MidPlant", "BotPlant"};
	List<GameObject> oldSample;
	List<GameObject> sample;
	int selIndex;
	bool inFocus;
	// Start is called before the first frame update
    void Start()
	{	
		selIndex = 0;
		isOn = true;
		inFocus = false;
    }
	
	Vector2 mousePos;
    // Update is called once per frame
    void Update()
	{	
		if (isOn) {
			mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
			sample = sampleAt(mousePos);
			
			if (sample.Count > 0) {
				
				//On Hover
				if (!compare(oldSample, sample)) {
					textField.GetComponent<TMPro.TextMeshProUGUI>().text = string.Join(',' , toStringList(sample, false));
				}
				
				//On Click
				if (Input.GetMouseButtonDown(0)) {
					
					cycleSel(sample);
					GameObject focus = sample[selIndex];
					
					//Interacts with PlantHighLighter here
					this.GetComponent<PlantHighLighter>().focusOn(focus);
					
					//Interacts with TextField here
					textField.GetComponent<TMPro.TextMeshProUGUI>().text = string.Join(',' , toStringList(sample, true));
					plantInfo.GetComponent<PlantInfo>().setPlant(focus);
					
					//Create window here
				}
			} else {
				textField.GetComponent<TMPro.TextMeshProUGUI>().text = "";
			}
			oldSample = sample;
		}
    }
    
	private List<GameObject> sampleAt(Vector2 mousePos) {
		
		//Sample
		RaycastHit2D[] hit = Physics2D.RaycastAll(mousePos, Vector2.zero);
		List<GameObject> sampled = new List<GameObject>();
		foreach (RaycastHit2D ray in hit) {
			sampled.Add(ray.transform.gameObject);
		}
		
		//Sort sample by layer
		List<GameObject> sortedSample = new List<GameObject>();
		foreach (string layer in layers) {
			foreach (GameObject obj in sampled) {
				if (obj.GetComponent<SpriteRenderer>().sortingLayerName == layer) {
					sortedSample.Add(obj);
				}
			}
		}
		
		return sortedSample;
	}
	
	public List<string> toStringList(List<GameObject> s, bool focus) {
		List<string> stringList = new List<string>();
		foreach(GameObject obj in s) {
			stringList.Add(obj.name);
		}
		if (focus) {
			stringList[selIndex] = stringList[selIndex].ToUpper();
		}
		return stringList;
	}
	
	//Only called on click
	//Either begins or cycle through focus selection
	List<GameObject> internalSample = new List<GameObject>();
	private void cycleSel(List<GameObject> newSample) {
		if (compare(internalSample, newSample) && selIndex < internalSample.Count - 1) {
			selIndex++;
		} else {
			selIndex = 0;
		}
		internalSample = newSample;
		
	}
	
	private bool compare(List<GameObject> list1, List<GameObject> list2) {
		bool same = false;
		if (list1.Count == list2.Count) {
			for (int i = 0; i < list1.Count; i++) {
				if (list1[i] != list2[i]) {
					break;
				}
				if (i == list1.Count - 1) {
					same = true;
				}
			}
		}
		return same;
	}
	
	public List<GameObject> getSample() {
		return sample;
	}
	
	public bool isInFocus() {
		return inFocus;
	}
}
