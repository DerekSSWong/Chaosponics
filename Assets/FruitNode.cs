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
	public Invoice FruitInvoice;
	Invoice SigmaInvoice;
	Invoice currentInvoice;
	
	float currChaos;
	float currPurity;
	
	void Start() {
		fruit = fruitObj.GetComponent<Fruit>();
		//currChaos = 0;
		currPurity = fruit.getBasePurity();
		FruitInvoice = fruit.getInvoice();
		SigmaInvoice = new Invoice();
		currentInvoice = new Invoice();
	}
	
	void Update() {
		//fruit = fruitObj.GetComponent<Fruit>();
		//FruitInvoice = fruit.getInvoice();
		Debug.Log(FruitInvoice.getVal(Chaos));
	}
	
	public void supply (Invoice inv) {
		if (inv.getVal(Chaos) < FruitInvoice.getVal(Chaos) * fruit.getRate()) {
			currPurity *= 0.9f;
		}
		SigmaInvoice.add(inv);
		if (SigmaInvoice.getVal(Chaos) >= FruitInvoice.getVal(Chaos)) {
			spawnFruit();
		}
	}
	
	public void spawnFruit() {
		foreach (Element e in FruitInvoice) {
			if (e == Chaos) {
				continue;
			} else if (SigmaInvoice.getVal(e) >= FruitInvoice.getVal(e)) {
				currPurity *= 1.1f;
			}
		}
		if (currPurity > fruit.getMaxPurity()) {
			currPurity = fruit.getMaxPurity();
		}
		fruit.setPurity(currPurity);
		fruit.spawn(this.transform);
		SigmaInvoice = new Invoice();
		currPurity = fruit.getBasePurity();
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
