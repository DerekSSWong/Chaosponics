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

	public Invoice FruitInvoice;
	Invoice SigmaInvoice;
	Invoice currentInvoice;
	
	float currPurity;
	
	void Start() {
		fruit = fruitObj.GetComponent<Fruit>();
		//currChaos = 0;
		currPurity = fruit.getBaseP();
		FruitInvoice = fruit.getInvoice();
		SigmaInvoice = new Invoice();
		currentInvoice = new Invoice();
	}
	
	void Update() {
		
	}
	
	public void supply (Invoice inv) {
		if (inv.getVal(Chaos) < currentInvoice.getVal(Chaos)) {
			//Debug.Log("Demand: " + currentInvoice.getVal(Chaos) + " Supply: " + inv.getVal(Chaos));
			currPurity *= 0.9f;
		}
		SigmaInvoice.add(inv);
		if (SigmaInvoice.getVal(Chaos) >= FruitInvoice.getVal(Chaos)) {
			spawnFruit(currPurity);
		}
		
	}
	
	public void spawnFruit(float p) {
		foreach (Element e in FruitInvoice) {
			if (SigmaInvoice.getVal(e) >= FruitInvoice.getVal(e) && e != Chaos && FruitInvoice.getVal(e) != 0) {
				p *= 1.1f;
			}
		}
		if (p > fruit.getMaxP()) {
			p = fruit.getMaxP();
		}

		fruit.spawn(this.transform, p);
		
		SigmaInvoice = new Invoice();
		currPurity = fruit.getBaseP();
		
	}
	
	public float getProgress() {
		return SigmaInvoice.getVal(Chaos) / FruitInvoice.getVal(Chaos);
	}
	
	public Invoice getDemand() {
		//fruit = fruitObj.GetComponent<Fruit>();
		FruitInvoice = fruitObj.GetComponent<Fruit>().getInvoice();
		//Debug.Log(fruit.FruitInvoice.getVal(Chaos));
		currentInvoice = new Invoice();
		float rate = fruit.getRate();
		foreach (Element e in FruitInvoice) {
			float amount = FruitInvoice.getVal(e) * rate;
			float diff = FruitInvoice.getVal(e) - SigmaInvoice.getVal(e);
			float outVal = Mathf.Min(amount,diff);
			currentInvoice.setVal(e, outVal);
		}
		//Debug.Log("Chaos Demand: " + currentInvoice.getVal(Chaos));
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
