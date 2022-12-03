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
	
	void Start() {
	}
	
	Nutrient getPlantN(){
		return gameObject.GetComponent<Plant>().getNutrient();
	}
	
	Nutrient getSoilNutrient(){
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
    
}

public class Recipe {
	
	private Invoice InputInvoice;
	private Invoice OutputInvoice;
	
	public Recipe(){}
	
	public Recipe(Invoice input, Invoice output) {
		InputInvoice = input;
		OutputInvoice = output;
	}
	
	public void setInput(Element e, float amount) {
		InputInvoice.setVal(e, amount);
	}
	
	public void setInput(Invoice input) {
		InputInvoice = input;
	}
	
	public void setOutput(Invoice output) {
		OutputInvoice = output;
	}
	
	public void setOutput(Element e, float amount) {
		OutputInvoice.setVal(e, amount);
	}
	
	public Invoice getInput() {
		return InputInvoice;
	}
	
	public Invoice getOutput() {
		return OutputInvoice;
	}
	
}
