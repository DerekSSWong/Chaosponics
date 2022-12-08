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
	
	public override void relayIngredients(Nutrient soilN)
	{	
		FruitNode fruitNode = this.GetComponent<FruitNode>();
		Invoice ingredients = fruitNode.getTotalInvoice();
		ingredients.mult(soilN.getSaltWeight());
		
		fruitNode.receive(ingredients);
	}
}
