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
		Invoice toCompost = calcDecomp();
		//Debug.Log(string.Format("Purity: {0}, Decomp: {1}", currP, toCompost.getVal(Chaos)));
		toCompost = FruitNutrient.withdraw(toCompost);
		toCompost = corrupt(toCompost);
		
		newSoilN.deposit(toCompost);
		return newSoilN;
	}
	
	
	Invoice calcDecomp() {
		Invoice decompInvoice = new Invoice();
		//1.03^155-x  -4
		float portion = (Mathf.Pow(1.03f, 155f-getCurrPPercent()) - 4f) / 100f;
		//Debug.Log(portion);
		float integ = getIntegPercent();
		if (integ >= UnityEngine.Random.Range(0f, 100f)) {
			foreach (Element e in decompInvoice) {
				decompInvoice.setVal(e, FruitNutrient.getVal(e) * portion);
			}
		}
		
		return decompInvoice;
	}
	
	Invoice corrupt(Invoice decomp) {
		foreach (Element e in decomp) {
			decomp.setVal(e, decomp.getVal(e) * currP);
		}
		return decomp;
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
	
	public float getCurrPPercent() {
		float pper = currP * 100f;
		pper = Mathf.Round(pper);
		return pper;
	}
	
	public float getIntegPercent() {
		
		float curr = FruitNutrient.getVal(Chaos);
		float cap = FruitNutrient.getCap(Chaos);
	
		float integrity = (curr/cap) * 100f;
		integrity = Mathf.Round(integrity);
		return integrity;
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
