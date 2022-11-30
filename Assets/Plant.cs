using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using static Elements;

public class Plant : SerializedMonoBehaviour
{
	Nutrient internalNutrient = new Nutrient();
	//NutrientRates currentRates = new NutrientRates();
	
	[SerializeField] float maxVit;
	
	float maxChaos;
	float baseChaosIn;
	float baseChaosCost;
	
	
	float currVit;
	float currChaos;
	float currChaosIn;
	float currChaosCost;
	
	Invoice SigmaInvoice;
	Invoice DeltaInvoice;
	Invoice IntakeInvoice;
	Invoice IdleInvoice;
	Invoice CultivateInvoice;
	
	FruitNode fruitNode;
	bool hasFruitNode;
	
	[SerializeField] bool ProducesFruit;
	[EnableIf("ProducesFruit")]
	[SerializeField] FruitNode FruitNode;
	
	
	public struct Capacitor {
		[TableColumnWidth(5)]
		public float Capacity;
		public float ChargeRate;
		public float DischargeRate;
	}
	
	[DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.Foldout)]
	public Dictionary<Element, Capacitor> Capacitors = new Dictionary<Element, Capacitor>();
	
	
	
	void Start()
	{	
		currVit = maxVit;
		currChaos = baseChaosIn;
		
		if (TryGetComponent<FruitNode>(out fruitNode)) {
			hasFruitNode = true;
		} else {hasFruitNode = false;}
		
		//Loads capacity data into nutrients
		foreach (var item in Capacitors) {
			internalNutrient.setNutrient(item.Key, new float[] {item.Value.ChargeRate, item.Value.Capacity});
			//currentRates.setRates(item.Key, new float[] {item.Value.ChargeRate, item.Value.DischargeRate});
			//Debug.Log(internalNutrient.getVal(index.Key) + " , " + internalNutrient.getCap(index.Key));
		}
		
		maxChaos = Capacitors[Chaos].Capacity;
		baseChaosIn = Capacitors[Chaos].ChargeRate;
		baseChaosCost = Capacitors[Chaos].DischargeRate;
		currChaos = Capacitors[Chaos].DischargeRate;
		
		SigmaInvoice = new Invoice();
		DeltaInvoice = new Invoice();
		IntakeInvoice = new Invoice();
		IdleInvoice = new Invoice();
	}

	// Update is called once per frame
	void Update()
	{
		
	}
	
	public Nutrient exchange(Nutrient soilN) {
		
		Brain testBrain = this.GetComponent<Brain>();
		testBrain.ping();
		Nutrient newSoilN = soilN;
		SigmaInvoice = new Invoice();
		DeltaInvoice = new Invoice();
		
		//Generate initial invoice
		IntakeInvoice = calcIntake(newSoilN);
		//Submit invoice, updated to actual values taken from soil
		IntakeInvoice = newSoilN.withdraw(IntakeInvoice);
		//Deposits final value to internal nutrient
		internalNutrient.deposit(IntakeInvoice);
		DeltaInvoice.add(IntakeInvoice);
		
		//Idle cost
		IdleInvoice = calcIdle(newSoilN);
		IdleInvoice = internalNutrient.withdraw(IdleInvoice);
		SigmaInvoice.add(IdleInvoice);
		
		if (IdleInvoice.isFlagged()){
			currVit -= 1;
		} else if (currVit < maxVit) {
			currVit += 1;
		}
		
		//Cultivate cost
		if (hasFruitNode) {
			if (FruitNode.roll()) {
				Invoice fruitInvoice = FruitNode.getDemand();
				CultivateInvoice = internalNutrient.withdraw(fruitInvoice);
				FruitNode.supply(CultivateInvoice);
				SigmaInvoice.add(CultivateInvoice);
			}
			
		}
		//Ask for invoice
		//Withdraw from internal nutrient
		//Return invoice
		
		//Refine cost
		
		
		return newSoilN;
	}
	
	private Invoice calcIntake(Nutrient newSoilN) {
		Invoice intake = new Invoice();
		
		foreach (var item in Capacitors) {
			Element e = item.Key;
			float intakeVal = item.Value.ChargeRate;
			float diff = internalNutrient.getCap(e) - internalNutrient.getVal(e);
			intakeVal = Mathf.Min(intakeVal, diff);
			intake.setVal(e, intakeVal);
		}
		return intake;
	}
	
	private Invoice calcIdle(Nutrient newSoilN) {
		Invoice idle = new Invoice();
		
		foreach (var item in Capacitors) {
			Element e = item.Key;
			float idleVal = item.Value.DischargeRate;
			
			idle.setVal(e, idleVal);
		}
		
		return idle;
	}
	
	public void kill() {
		Destroy(gameObject);
	}
	
	public float getMaxVit() {return maxVit;}
	public float getMaxChaos() {return internalNutrient.getCap(Chaos);}
	public float getBaseChaosIn() {return Capacitors[Chaos].ChargeRate;}
	public float getBaseChaosCost() {return Capacitors[Chaos].DischargeRate;}
	
	public float getCurrVit() {return currVit;}
	public float getCurrChaos() {return internalNutrient.getVal(Chaos);}
	public float getCurrChaosIn() {return IntakeInvoice.getVal(Chaos);}
	public float getCurrChaosCost() {return SigmaInvoice.getVal(Chaos);}
	
	public float getFruitProgress() {
		float progress = 0;
		if (hasFruitNode) {
			progress = fruitNode.getProgress();
		}
		return progress;
	}

	
}
