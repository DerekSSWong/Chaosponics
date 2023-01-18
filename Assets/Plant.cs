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
	
	[SerializeField] bool ProducesFruit;
	[EnableIf("ProducesFruit")]
	[SerializeField] protected FruitNode FruitNode;
	
	
	public struct Capacitor {
		[TableColumnWidth(5)]
		public float Capacity;
		public float ChargeRate;
		public float DischargeRate;
	}
	
	//Where the real fun begins
	[DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.Foldout)]
	public Dictionary<Element, Capacitor> Capacitors = new Dictionary<Element, Capacitor>();
	
	
	[TableList]
	public List<Ingredient>  Prime = new List<Ingredient>();
	
	[TableList]
	public List<Ingredient>  Agent = new List<Ingredient>();
	
	[TableList]
	public List<Ingredient>  Catalyst = new List<Ingredient>();
	
	[Serializable]
	public class Ingredient {
		
		[TableColumnWidth(60)]
		
		public Element element;
		public float rate;
		public float capacity;
	}
	
	protected Nutrient Nutrient;
	protected Invoice PrimeInvoice;
	protected Invoice AgentInvoice;
	protected Invoice CatalystInvoice;
	
	protected GameObject Fruit;
	[SerializeField] protected float FruitGrowChancePerTick;
	
	void Start()
	{	
		currVit = maxVit;
		//currChaos = baseChaosIn;
		
		////Loads capacity data into nutrients
		//foreach (var item in Capacitors) {
		//	internalNutrient.setNutrient(item.Key, new float[] {item.Value.ChargeRate, item.Value.Capacity});
		//	//currentRates.setRates(item.Key, new float[] {item.Value.ChargeRate, item.Value.DischargeRate});
		//	//Debug.Log(internalNutrient.getVal(index.Key) + " , " + internalNutrient.getCap(index.Key));
		//}
		
		
		//maxChaos = Capacitors[Chaos].Capacity;
		//baseChaosIn = Capacitors[Chaos].ChargeRate;
		//baseChaosCost = Capacitors[Chaos].DischargeRate;
		//currChaos = Capacitors[Chaos].DischargeRate;
		
		
		//SigmaInvoice = new Invoice();
		//DeltaInvoice = new Invoice();
		//IntakeInvoice = new Invoice();
		//IdleInvoice = new Invoice();
		
		compileInput();
	}

	// Update is called once per frame
	void Update()
	{
		
	}
	
	Invoice toInvoice(List<Ingredient> list) {
		Invoice outInvoice = new Invoice();
		foreach (Ingredient i in list) {
			outInvoice.setVal(i.element, i.rate);
		}
		return outInvoice;
	}
	
	//Maybe right
	Nutrient setNCap(Nutrient n, List<Ingredient> list) {
		Nutrient outNutrient = n;
		foreach (Ingredient i in list) {
			float currentCap = n.getCap(i.element);
			float newCap = currentCap + i.capacity;
			outNutrient.setCap(i.element, newCap);
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
		
		Nutrient = new Nutrient();
		Nutrient = setNCap(Nutrient, Prime);
		Nutrient = setNCap(Nutrient, Agent);
		Nutrient = setNCap(Nutrient, Catalyst);
	}
	
	//Salt -> Suck more -> Purer but slower
	//Brimstone -> Use more -> Less pure but faster
	//Heat -> Custom effect
	
	public virtual void InheritanceTest(){
		Debug.Log("Not working");
	}
	
	public virtual Invoice generatePrimeIntake(Nutrient soilN) {
		Invoice intake = PrimeInvoice;
		return intake;
	}
	
	public virtual Invoice generateAgentIntake(Nutrient soilN) {
		Invoice intake = AgentInvoice;
		
		return intake;
	}
	
	public virtual Invoice generateCatalystIntake(Nutrient soilN) {
		Invoice intake = CatalystInvoice;
		
		return intake;
	}
	
	public static Invoice clamp(Invoice invoice, Nutrient nutrient) {
		foreach(Element e in invoice) {
			float invoiceVal = invoice.getVal(e);
			float nutrientVal = nutrient.getCap(e) - nutrient.getVal(e);
			invoice.setVal(e, Mathf.Min(invoiceVal, nutrientVal));
		}
		return invoice;
	}
	
	public virtual Invoice generatePrimeCost(Nutrient soilN) {
		Invoice cost = PrimeInvoice;
		
		return cost;
	}
	
	public virtual Invoice generateAgentCost(Nutrient soilN) {
		Invoice cost = AgentInvoice;
		
		return cost;
	}
	
	public virtual Invoice generateCatalystCost(Nutrient soilN) {
		Invoice cost = CatalystInvoice;
		
		return cost;
	}
	
	public virtual Nutrient consume(Nutrient nutrient) {
		return nutrient;
	}
	
	public virtual Nutrient relayToNode(Nutrient soilN) {
		return soilN;
	}
	
	public Nutrient exchange(Nutrient soilN) {
		
		Nutrient newSoilN = soilN;
		InheritanceTest();
		
		//SigmaInvoice = new Invoice();
		//DeltaInvoice = new Invoice();
		
		////Generate initial invoice
		//IntakeInvoice = calcIntake(newSoilN);
		////Submit invoice, updated to actual values taken from soil
		//IntakeInvoice = newSoilN.withdraw(IntakeInvoice);
		////Deposits final value to internal nutrient
		//internalNutrient.deposit(IntakeInvoice);
		//DeltaInvoice.add(IntakeInvoice);
		
		//Idle cost
		//IdleInvoice = calcIdle(newSoilN);
		//IdleInvoice = internalNutrient.withdraw(IdleInvoice);
		//SigmaInvoice.add(IdleInvoice);
		
		//if (IdleInvoice.isFlagged()){
		//	currVit -= 1;
		//} else if (currVit < maxVit) {
		//	currVit += 1;
		//}
		
		//Cultivate cost
		//if (hasFruitNode) {
		//	if (FruitNode.roll()) {
		//		Invoice fruitInvoice = FruitNode.getDemand();
		//		CultivateInvoice = internalNutrient.withdraw(fruitInvoice);
		//		FruitNode.supply(CultivateInvoice);
		//		SigmaInvoice.add(CultivateInvoice);
		//	}
		//}
		
		Invoice TotalIntake = new Invoice();
		Invoice PrimeIntake = generatePrimeIntake(soilN);
		TotalIntake.add(PrimeInvoice);
		//TotalIntake.add(generateAgentIntake(soilN));
		//TotalIntake.add(generateCatalystIntake(soilN));
		
		TotalIntake = clamp(TotalIntake, Nutrient);
		
		Invoice TotalIntook = newSoilN.withdraw(TotalIntake);
		//Debug.Log("Chaos taken: " + TotalIntook.getVal(Chaos));
		Nutrient.deposit(TotalIntook);
		
		//Nutrient = consume(soilN);
		//Nutrient = relayToNode(Nutrient);
		
		return newSoilN;
	}
	
	//private Invoice calcIntake(Nutrient newSoilN) {
	//	Invoice intake = new Invoice();
		
	//	foreach (var item in Capacitors) {
	//		Element e = item.Key;
	//		float intakeVal = item.Value.ChargeRate;
	//		float diff = internalNutrient.getCap(e) - internalNutrient.getVal(e);
	//		intakeVal = Mathf.Min(intakeVal, diff);
	//		intake.setVal(e, intakeVal);
	//	}
	//	return intake;
	//}
	
	//private Invoice calcIdle(Nutrient newSoilN) {
	//	Invoice idle = new Invoice();
		
	//	foreach (var item in Capacitors) {
	//		Element e = item.Key;
	//		float idleVal = item.Value.DischargeRate;
			
	//		idle.setVal(e, idleVal);
	//	}
		
	//	return idle;
	//}
	
	public void kill() {
		Destroy(gameObject);
	}
	
	public static bool roll(float p) {
		bool outcome = false;
		while (p > 1f) {
			p /= 100f;
		}
		float r = UnityEngine.Random.Range(0f, 1f);
		Debug.Log(r);
		if (r <= p && p != 0f) {
			outcome = true;
		}
		return outcome;
	}
	
	public float getMaxVit() {return maxVit;}
	public float getMaxChaos() {return internalNutrient.getCap(Chaos);}
	public float getBaseChaosIn() {return Capacitors[Chaos].ChargeRate;}
	public float getBaseChaosCost() {return Capacitors[Chaos].DischargeRate;}
	
	public float getCurrVit() {return currVit;}
	public float getCurrChaos() {return internalNutrient.getVal(Chaos);}
	public float getCurrChaosIn() {return IntakeInvoice.getVal(Chaos);}
	public float getCurrChaosCost() {return SigmaInvoice.getVal(Chaos);}
	
	public Nutrient getNutrient() {return internalNutrient;}
	
	
	public float getFruitProgress() {
		float progress = 0;
		return progress;
	}

	
}
