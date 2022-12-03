using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FernipBrain : Brain
{
	
	//Delegate Example
	//delegate void DeleExample(int a);
	//DeleExample myDelegate = Foo;
	//myDelegate.Invoke(4);
	//void Foo(int a) {}
	
	public override Invoice calcIdleCost() {
		Invoice IdleInvoice = new Invoice();
		Nutrient PlantN = getPlantN();
		Nutrient SoilN = getSoilN();
		
		Debug.Log(SoilN.getPortion(Elements.Iron));
		
		return IdleInvoice;
	}
}
