using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantInfo : MonoBehaviour
{	
	[SerializeField] GameObject nameLabel;
	[SerializeField] GameObject maxVitLabel;
	[SerializeField] GameObject currVitLabel;
	[SerializeField] GameObject InLabel;
	[SerializeField] GameObject maxInternalChaos;
	[SerializeField] GameObject currInternalChaos;
	[SerializeField] GameObject chaosRate;
	[SerializeField] GameObject fruitRate;
	
	GameObject plant;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    if (plant != null && plant.tag == "Plant") {
	    	Plant data = plant.GetComponent<Plant>();
	    	setText(nameLabel, plant.name);
	    	setText(maxVitLabel, data.getMaxVit().ToString());
	    	setText(currVitLabel, data.getCurrVit().ToString());
	    	setText(InLabel, data.getCurrChaosIn().ToString());
	    	setText(maxInternalChaos, data.getMaxChaos().ToString());
	    	setText(currInternalChaos, data.getCurrChaos().ToString());
	    	setText(chaosRate, data.getCurrChaosCost().ToString());
	    	setText(fruitRate, data.getFruitProgress().ToString());
	    }
    }
    
	public void setPlant(GameObject obj) {
		plant = obj;
	}
	
	void setText(GameObject UI, string text) {
		UI.GetComponent<TMPro.TextMeshProUGUI>().text = text;
	}
}
