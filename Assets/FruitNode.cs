using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Elements;

public class FruitNode : MonoBehaviour
{	
	[SerializeField] GameObject fruitObj;
	[SerializeField] GameObject spawnNode;
	Fruit fruit;
	Nutrient FruitNutrient;
	float chaosProg;
	Invoice FruitInvoice;
	Invoice SigmaInvoice;
	Invoice currentInvoice;
	
	float currChaos;
	float currPurity;
	
	void Start() {
		fruit = fruitObj.GetComponent<Fruit>();
		currChaos = 0;
		currPurity = fruit.getBasePurity();
		FruitInvoice = fruit.getInvoice();
		SigmaInvoice = new Invoice();
		currentInvoice = new Invoice();
	}
	
	public float getChaosCost() {
		float diff = fruit.getChaos() - currChaos;
		float demand = fruit.getChaos() * fruit.getBaseRate();
		return Mathf.Min(diff, demand);
	}
	
	public void supply(float chaosGiven) {
		currChaos += chaosGiven;
		if (currChaos >= fruit.getChaos()) {
			Instantiate(fruitObj, spawnNode.transform.position, Quaternion.identity);
			currChaos = 0;
		}
	}
	
	public void supply (Invoice inv) {
		
	}
	
	public float getProgress() {
		return currChaos/fruit.getChaos();
	}
	
	public Invoice getDemand() {
		currentInvoice = new Invoice();
		float rate = fruit.getRate();
		foreach (Element e in FruitInvoice) {
			float amount = FruitInvoice.getVal(e) * rate;
			float diff = FruitInvoice.getVal(e) - SigmaInvoice.getVal(e);
			float outVal = Mathf.Min(amount,diff);
			currentInvoice.setVal(e, outVal);
		}
		
		return currentInvoice;
	}
	
	public bool roll() {
		bool outcome = true;
		float rate =  fruit.getRNG();
		rate *= 100f;
		int rateInt = (int) rate;
		int p = rateInt % 100;
		int output = Random.Range(0,100);
		if (output >= rate) {
			outcome = false;
		}
		return outcome;
	}
	
}
