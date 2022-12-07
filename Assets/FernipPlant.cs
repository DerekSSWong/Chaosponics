using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FernipPlant : Plant
{
	public override Invoice generatePrimeIntake(Nutrient soilN)
	{
		Invoice intake = PrimeInvoice;
		intake.mult(soilN.getSaltWeight());
		return intake;
	}
}
