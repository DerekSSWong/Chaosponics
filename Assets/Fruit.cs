﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using static Elements;

public class Fruit : MonoBehaviour
{	
	[SerializeField] float maxChaos; //Theoretical chaos value if purity is 100%
	[SerializeField] float baseRate; //Percentage increase per tick
	                 float decompRate;
	[SerializeField] float basePurity;
	[SerializeField] float maxPurity;
	                 float purity;
    
	[SerializeField] Rigidbody2D rigidFruit;
	[SerializeField] float dropSpeed;
	
	//Fruit nutrient
	Nutrient FruitNutrient = new Nutrient();
	
	Invoice FruitInvoice = new Invoice();
	
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
	
	[SerializeField]
	float PortionPerTick;
	
	[SerializeField]
	float ChancePerTick;
	
	
	// Start is called before the first frame update
    void Start()
	{
		foreach (fruitContent f in NutritionalYield) {
			FruitNutrient.setCap(f.element, f.Amount);
			FruitNutrient.setVal(f.element, f.Amount);
		}
		
		foreach (fruitContent f in RequiredIngredients) {
			FruitInvoice.setVal(f.element, f.Amount);
		}
		
		baseP = BaseAndMaxPurity[0];
		maxP = BaseAndMaxPurity[1];
    }

    // Update is called once per frame
    void Update()
	{
    	
	}
    
    
	void FixedUpdate() {
		if (rigidFruit.IsAwake()) {
			rigidFruit.velocity = new Vector2(0, -dropSpeed * Time.fixedDeltaTime);
		}
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		
	}
    
    
    
	public float exchange(float total) {
		
		return total;
	}
	
	public void setPurity(float p) {
		if (p < maxPurity) {
			purity = p;
		} else {
			p = maxPurity;
		}
		Debug.Log("Purity set to " + purity);
	}
	
	public float getChaos() {
		return maxChaos;
	}
	
	public float getBaseRate() {
		return baseRate;
	}
	
	public float getBasePurity() {
		return basePurity;
	}
	
	public float getMaxPurity() {
		return maxPurity;
	}
	
	public Nutrient getNutrient() {
		return FruitNutrient;
	}
	
	public Invoice getInvoice() {
		return FruitInvoice;
	}
	
	public float getRate() {
		return PortionPerTick;
	}
	
	public float getRNG() {
		return ChancePerTick;
	}
	
}
