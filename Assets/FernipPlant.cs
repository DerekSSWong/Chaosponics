using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FernipPlant : Plant
{	
	
	//Prime -> Idle
	//Agent -> Ask fruit node
	//Catalyst -> Ask fruit node
	
	public override Invoice generatePrimeIntake(Nutrient soilN)
	{
		Invoice intake = PrimeInvoice;
		intake.mult(soilN.getSaltWeight());
		return intake;
	}
	
	
	public override Invoice generatePrimeCost(Nutrient soilN) {
		Invoice cost = PrimeInvoice;
		cost.mult(soilN.getBrimstoneWeight());
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
		Invoice invoice = newNutrient.withdraw(generatePrimeCost(soilN));
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
