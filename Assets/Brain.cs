using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using static Elements;

public class Brain : MonoBehaviour
{	
	protected List<Recipe> RecipeList;
	[SerializeField] string Name;
	
	protected Nutrient SoilN;
	
	protected Invoice PrimeInvoice;
	protected Invoice AgentInvoice;
	protected Invoice CatalystInvoice;
	protected Invoice OutputInvoice;

	
	void Start() {
		setInvoices();
	}
	
	protected Nutrient getPlantN(){
		return gameObject.GetComponent<Plant>().getNutrient();
	}
	
	protected Nutrient getSoilN(){
		return SoilN;
	}
	
	void getFruitReq(){
		
	}
	
	void pipeToFruit() {
		
	}
	
	public string getName() {
		return Name;
	}
	
	public void updateSoilN(Nutrient n) {
		SoilN = n;
	}
	
	public virtual Invoice calcIdleCost() {
		Invoice IdleInvoice = new Invoice();
		
		return IdleInvoice;
	}
	
	public virtual Invoice calcIdleBuff() {
		Invoice BuffAmount = new Invoice();
		
		return BuffAmount;
	}
	
	public virtual void setInvoices() {
		PrimeInvoice = new Invoice();
		AgentInvoice = new Invoice();
		CatalystInvoice = new Invoice();
	}
    
}

public class Recipe {
	
	private Invoice InputInvoice;
	
	private Invoice PrimeInvoice;
	private Invoice AgentInvoice;
	private Invoice CatalystInvoice;
	
	private Invoice OutputInvoice;
	
	public Recipe(){}
	
}
