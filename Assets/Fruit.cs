using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using static Elements;

public class Fruit : MonoBehaviour
{
    
	[SerializeField] Rigidbody2D rigidFruit;
	[SerializeField] float dropSpeed;
	
	//Fruit nutrient
	Nutrient FruitNutrient = new Nutrient();
	
	public Invoice FruitInvoice = new Invoice();
	
	[TableList]
	public List<fruitContent> NutritionalYield = new List<fruitContent>();
	
	[TableList]
	public List<fruitContent> RequiredIngredients = new List<fruitContent>();
	
	[Serializable]
	public class fruitContent {
		[TableColumnWidth(10)]
		public Element element;
		public float Amount;
	}
	
	[MinMaxSlider(0.01f, 1f, true)]
	public Vector2 BaseAndMaxPurity = new Vector2(0.5f, 0.9f);
	float baseP;
	float maxP;
	[SerializeField] float currP;
	
	[SerializeField]
	float PortionPerTick;
	
	[SerializeField]
	float ChancePerTick;
	
	[Range(0.0f, 2.0f)]
	public float DetectorRange;
	
	bool onGround;
	
	[SerializeField] float internalChaos;
	
	// Start is called before the first frame update
    void Start()
	{	
		maxP = getMaxP();
		baseP = getBaseP();
		FruitNutrient = getNutrient();
		FruitInvoice = getInvoice();
		onGround = false;
    }

    // Update is called once per frame
    void Update()
	{
		internalChaos = FruitNutrient.getVal(Chaos);
	}
    
    
	void FixedUpdate() {
		if (rigidFruit.IsAwake()) {
			rigidFruit.velocity = new Vector2(0, -dropSpeed * Time.fixedDeltaTime);
		}
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		//Check for fruit to merge with
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll(this.transform.position, DetectorRange);
		GameObject fruitToMerge = findFirstValidFruit(hitColliders);
		if (fruitToMerge != null) {
			mergeInto(fruitToMerge);
		}
		onGround = true;
		
	}
	
	GameObject findFirstValidFruit(Collider2D[] array) {
		GameObject fruit = null;
		foreach (var hitCollider in array) {
			GameObject obj = hitCollider.gameObject;
			if (obj.GetComponent<SpriteRenderer>().sprite == gameObject.GetComponent<SpriteRenderer>().sprite && obj != this.gameObject) {
				Fruit objF = obj.GetComponent<Fruit>();
				if (Mathf.Abs(objF.getCurrP() - currP) <= 0.4f && objF.isOnGround()) {
					fruit = obj;
					break;
				}
			}
		}
		return fruit;
	}
	
	void mergeInto(GameObject otherFruit) {
		otherFruit.GetComponent<Fruit>().accept(FruitNutrient, currP);
		Destroy(gameObject);
	}
	
	public void accept(Nutrient n, float p) {
		float effectiveN = FruitNutrient.getVal(Chaos)*currP + n.getVal(Chaos)*p;
		float theoreticalN = FruitNutrient.getVal(Chaos) + n.getVal(Chaos);
		float newP = effectiveN / theoreticalN;
		FruitNutrient.combine(n);
		currP = newP;
		Debug.Log("Nutrient: " + FruitNutrient.getVal(Chaos) + " Purity: " + newP);
	}
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(this.transform.position, DetectorRange);
	}
    
	public Nutrient exchange(Nutrient soilN) {
		Nutrient newSoilN = soilN;
		float integrity = FruitNutrient.getVal(Chaos) / FruitNutrient.getCap(Chaos);
		float decompRate = (1 - currP)*(1/(integrity+0.01f));
		Debug.Log(integrity + " , " + decompRate);
		Invoice compost = new Invoice();
			//foreach (Element e in FruitNutrient) {
			//	if (FruitNutrient.getVal(e) > 0) {
			//		float toCompost = FruitNutrient.getVal(e) * decompRate;
			//		compost.setVal(e, toCompost);
			//	}
			//}
			//compost = FruitNutrient.withdraw(compost);
			//foreach (Element e in compost) {
			//	if (compost.getVal(e) > 0) {
			//		float postP = compost.getVal(e) * currP;
			//		compost.setVal(e, postP);
			//	}
			//}
		compost.setVal(Chaos, FruitNutrient.getVal(Chaos) * decompRate);
		compost = FruitNutrient.withdraw(compost);
		Debug.Log("Composted: " + compost.getVal(Chaos));
		newSoilN.deposit(compost);
		
		return newSoilN;
	}
	
	public void setPurity(float p) {
		currP = p;
	}
	public float getBaseP() {
		return BaseAndMaxPurity[0];
	}
	
	public float getMaxP() {
		return BaseAndMaxPurity[1];
	}
	
	public float getCurrP() {
		return currP;
	}
	
	public Nutrient getNutrient() {
		foreach (fruitContent f in NutritionalYield) {
			FruitNutrient.setCap(f.element, f.Amount);
			FruitNutrient.setVal(f.element, f.Amount);
		}
		return FruitNutrient;
	}
	
	public Nutrient getCurrN() {
		return FruitNutrient;
	}
	
	public Invoice getInvoice() {
		foreach (fruitContent f in RequiredIngredients) {
			FruitInvoice.setVal(f.element, f.Amount);
		}
		return FruitInvoice;
	}
	
	public float getRate() {
		return PortionPerTick;
	}
	
	public float getRNG() {
		return ChancePerTick;
	}
	
	public void spawn(Transform rootnode, float purity) {
		setPurity(purity);
		Debug.Log("Fruit spawn with " + currP + " purity.");
		Instantiate(gameObject, rootnode.position, Quaternion.identity);
	}
	
	public bool isOnGround() {
		return onGround;
	}
}
