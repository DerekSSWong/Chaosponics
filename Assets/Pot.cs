using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using static Elements;

public class Pot : MonoBehaviour
{	
	float chaos;
	GameObject[] plantArray;
	GameObject[] fruitArray;
	
	Nutrient nutrient;
	
	
	//Inspector values
	[TableList]
	public List<soilCont>  ListOfNutrients = new List<soilCont>();
	
	[Serializable]
	public class soilCont {
		
		[TableColumnWidth(60)]
		
		public Element element;
		public float current;
		public float capacity;
	}
	
    // Start is called before the first frame update
    void Start()
	{	
		Ticker ticker = GameObject.Find("Watcher").GetComponent<Ticker>();
		ticker.exchangeTick += performExchange;
		ticker.exchangeTick += exchangeNutrient;
		
		//Puts inspector values into internal storage
		nutrient = new Nutrient();
		foreach (soilCont s in ListOfNutrients) {
			nutrient.setNutrient(s.element, new float[] {s.current, s.capacity});
		}
		
		chaos = nutrient.getVal(Chaos);
	    
    }

    // Update is called once per frame
    void Update()
	{	
		plantArray = GameObject.FindGameObjectsWithTag("Plant");
		fruitArray = GameObject.FindGameObjectsWithTag("Fruit");
    }
	
	void performExchange(object sender, EventArgs e) {
		if (plantArray != null && plantArray.Length > 0) {
			foreach (GameObject plant in plantArray) {
				chaos = plant.GetComponent<Plant>().exchange(chaos);
			}
		}
		
	}
	
	void exchangeNutrient(object sender, EventArgs e) {
		if (plantArray != null && plantArray.Length > 0) {
			foreach (GameObject plant in plantArray) {
				nutrient = plant.GetComponent<Plant>().exchange(nutrient);
			}
		}
		
		//Updates inspector value with internal storage
		foreach (soilCont item in ListOfNutrients) {
			item.current = nutrient.getVal(item.element);
			item.capacity = nutrient.getCap(item.element);
		}
	}
	
}
