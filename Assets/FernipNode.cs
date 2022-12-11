using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FernipNode : FruitNode
{
	public override void crystallise()
	{
		//Determine yield based on agent
		//Modify purity and yield based on salt/brimstone
		float concentration = getAgentConcentration();
		float purityBuff = sigmoidConcentration(concentration);
		Purity += BasePurity*purityBuff;
		Purity = Mathf.Max(Purity, MaxPurity);
	}
		
}
