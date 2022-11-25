﻿using System;
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
	float purity;
	
	[SerializeField]
	float PortionPerTick;
	
	[SerializeField]
	float ChancePerTick;
	
	
	// Start is called before the first frame update
    void Start()
	{
		
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
		//Check for fruit to merge with
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll(this.transform.position, 10f);
		foreach (var hitCollider in hitColliders)
		{
			if (hitCollider.gameObject.tag == "Fruit" && hitCollider.gameObject != this.gameObject) {
				//Merge with fruit here
				Debug.Log(hitCollider.gameObject.name);
			}
		}
	}
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(this.transform.position, 1);
	}
    
	public float exchange(float total) {
		
		return total;
	}
	
	public void setPurity(float p) {
		if (p < maxP) {
			purity = p;
		} else {
			p = maxP;
		}
		//Debug.Log("Purity set to " + purity);
	}
	public float getBasePurity() {
		return baseP;
	}
	
	public float getMaxPurity() {
		return maxP;
	}
	
	public Nutrient getNutrient() {
		foreach (fruitContent f in NutritionalYield) {
			FruitNutrient.setCap(f.element, f.Amount);
			FruitNutrient.setVal(f.element, f.Amount);
		}
		return FruitNutrient;
	}
	
	public Invoice getInvoice() {
		foreach (fruitContent f in RequiredIngredients) {
			FruitInvoice.setVal(f.element, f.Amount);
		}
		return FruitInvoice;
	}
	
	public float getBaseP() {
		return BaseAndMaxPurity[0];
	}
	
	public float getMaxP() {
		return BaseAndMaxPurity[1];
	}
	
	public float getRate() {
		return PortionPerTick;
	}
	
	public float getRNG() {
		return ChancePerTick;
	}
	
	public void spawn(Transform rootnode) {
		Instantiate(gameObject, rootnode.position, Quaternion.identity);
	}
	
	public void merge() {
		
	}
}
