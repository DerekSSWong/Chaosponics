using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitNode : MonoBehaviour
{	
	[SerializeField] GameObject fruitObj;
	[SerializeField] GameObject spawnNode;
	Fruit fruit;
	
	float currChaos;
	float currPurity;
	
	void Start() {
		fruit = fruitObj.GetComponent<Fruit>();
		currChaos = 0;
		currPurity = fruit.getBasePurity();
	}
	
	public float getChaosCost() {
		float diff = fruit.getChaos() - currChaos;
		float demand = fruit.getChaos() * fruit.getBaseRate();
		return Mathf.Min(diff, demand);
	}
	
	public void supply(float chaosGiven) {
		currChaos += chaosGiven;
		if (currChaos >= fruit.getChaos()) {
			Instantiate(fruitObj, spawnNode.transform.position, Quaternion.identity);
			currChaos = 0;
		}
	}
	
	public float getProgress() {
		return currChaos/fruit.getChaos();
	}
	
	public void confirm() {
		Debug.Log("I am here");
	}
	
}
