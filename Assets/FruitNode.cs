using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
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
	
	protected Nutrient Nutrient;
	protected Invoice PrimeInvoice;
	protected Invoice AgentInvoice;
	protected Invoice CatalystInvoice;
	protected Invoice OutputInvoice;
	
	[TableList]
	public List<Ingredient>  Prime = new List<Ingredient>();
	
	[TableList]
	public List<Ingredient>  Agent = new List<Ingredient>();
	
	[TableList]
	public List<Ingredient>  Catalyst = new List<Ingredient>();
	
	[TableList]
	public List<Ingredient>  Output = new List<Ingredient>();
	
	[Serializable]
	public class Ingredient {
		
		[TableColumnWidth(60)]
		
		public Element element;
		public float rate;
		public float capacity;
	}
	
	Invoice toInvoice(List<Ingredient> list) {
		Invoice outInvoice = new Invoice();
		foreach (Ingredient i in list) {
			outInvoice.setVal(i.element, i.rate);
		}
		return outInvoice;
	}
	
	//Not right
	Nutrient setNCap(Nutrient n, List<Ingredient> list) {
		Nutrient outNutrient = n;
		foreach (Ingredient i in list) {
			outNutrient.setCap(i.element, outNutrient.getCap(i.element) + i.capacity);
		}
		return outNutrient;
	}
	
	void compileInput() {
		
		PrimeInvoice = new Invoice();
		PrimeInvoice = toInvoice(Prime);
		
		AgentInvoice = new Invoice();
		AgentInvoice = toInvoice(Agent);
		
		CatalystInvoice = new Invoice();
		CatalystInvoice = toInvoice(Catalyst);
		
		OutputInvoice = new Invoice();
		OutputInvoice = toInvoice(Output);
		
		Nutrient = new Nutrient();
		Nutrient = setNCap(Nutrient, Prime);
		Nutrient = setNCap(Nutrient, Agent);
		Nutrient = setNCap(Nutrient, Catalyst);
		
	}
	
	
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
	
	//Invoices are the rates
	//If prime cap is reached, spawn fruit
	//If prime invoice is not met, reduce output purity
	//Output of fruit depends on total agent avaiable
	
	public virtual void receive(Invoice ingredients) {
		Nutrient.deposit(ingredients);
	}
	
	public virtual Invoice getPrimeInvoice() {
		return PrimeInvoice;
	}
	
	public virtual Invoice getAgentInvoice() {
		return AgentInvoice;
	}
	
	public virtual Invoice getCatalystInvoice() {
		return CatalystInvoice;
	}
	
	public virtual Invoice getTotalInvoice() {
		Invoice total = PrimeInvoice;
		total.add(AgentInvoice);
		total.add(CatalystInvoice);
		
		return total;
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
		int output = UnityEngine.Random.Range(0,100);
		if (output >= rate) {
			outcome = false;
		}
		return outcome;
	}
	
}
