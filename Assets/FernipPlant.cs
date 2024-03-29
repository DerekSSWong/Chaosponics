﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Elements;

public class FernipPlant : Plant
{	
	
	//Prime -> Idle
	//Agent -> Ask fruit node
	//Catalyst -> Ask fruit node
	
	public override void InheritanceTest()
	{
		//Debug.Log("Inheritance Working");
	}
	
	public override Invoice generatePrimeIntake(Nutrient soilN)
	{	
		Invoice intake = new Invoice(PrimeInvoice);
		intake.mult(soilN.getSaltWeight());
		return intake;
	}
	
	
	public override Invoice generatePrimeCost(Nutrient soilN) {
		Invoice cost = new Invoice(PrimeInvoice);
		//cost.mult(soilN.getBrimstoneWeight());
		return cost;
	}
	
	public override Invoice generateAgentIntake(Nutrient soilN) {
		FruitNode fruitNode = this.GetComponent<FruitNode>();
		Invoice ingredients = fruitNode.getTotalInvoice();
		return ingredients;
	}
	
	public override Invoice generateAgentCost(Nutrient soilN) {
		FruitNode fruitNode = this.GetComponent<FruitNode>();
		Invoice ingredients = fruitNode.getTotalInvoice();
		return ingredients;
	}
	
	public override Nutrient consume(Nutrient soilN) {
		
		Nutrient newNutrient = Nutrient;
		//Debug.Log("Original Value: " + newNutrient.getVal(Chaos));
		Invoice invoice = newNutrient.withdraw(generatePrimeCost(soilN));
		//Debug.Log("Chaos taken: " + invoice.getVal(Chaos) + " Current Value: " + newNutrient.getVal(Chaos));
		if (invoice.isFlagged()) {
			//Take damage here
		}
		
		return newNutrient;
	}
	public override Nutrient relayToNode(Nutrient soilN)
	{	
		if (roll(FruitGrowChancePerTick)) {
			FruitNode fruitNode = this.GetComponent<FruitNode>();
			Invoice ingredients = generateAgentCost(soilN); //WIP
			fruitNode.receive(ingredients);
		}
		return soilN;
	}
}
