//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Plant_bkp : MonoBehaviour
//{	
	
//	//Vitality: Decreases by one when requirements are not met
//	[SerializeField] float maxVit; //Max health of plant
//	                 float currentVit; //Current health of plant
//	                 bool  reqMet;
						
//	//Base chaos costs
//	[SerializeField] float baseChaosIn;
//	         private float currentChaosIn;
//	         private bool  isWorking;
//	[SerializeField] float baseCost;
//	                 float currentCost;
	                 
//	//Base chaos production for generator
//	[SerializeField] float chaosProd;
	
	
//	[SerializeField] float maxInternalChaos;
//	float internalChaos; //How much chaos is stored within
	
//	//Percentage, spawn fruit when progression hits 100
//	                 float fruitProgress;
//	[SerializeField] GameObject fruitObj;
//	[SerializeField] GameObject fruitNode;
//	                 float fruitPercent;
	
//	//IO
//	[SerializeField] GameObject IO;
	
	
	
	
//    void Start()
//	{	
//		reqMet = true;
//		internalChaos = 0;
//		currentVit = maxVit;
//		fruitProgress = 0;
//    }

//    // Update is called once per frame
//    void Update()
//	{
    	
//    }
	
//	//Check for fruits or environmental
//	void checkExt() {
//		if (IO != null) {
//			//GameObject[] nearbyFruits = IO.GetComponent<Scanner>().scanForFruits();
//		}
//	}
	
//	//Figures out all the rates
//	void checkInt() {

//		currentCost = baseCost;
		
//		//currentCost = baseCost + fruitCost() + processCost()
//	}
	
//	float calcIntake() {
//		float intake = baseChaosIn;
//		float diff = maxInternalChaos - internalChaos;
//		if (diff < baseChaosIn) {
//			intake = diff;
//		}
//		return intake;
//	}
	
//	//Performs the exchange
//	public float exchange(float total) {
		
//		checkInt();
//		currentChaosIn = calcIntake();
//		if (total <= currentChaosIn) {
//			currentChaosIn = total;
//			total = 0;
//		} else {
//			total -= currentChaosIn;
//		}
//		internalChaos += currentChaosIn;
		
//		grow();
//		return total;
//	}
	
//	//Grow, heal, or die
//	void grow() {
//		//either idle or produce
		
//		//if (reqMet) {
//		//	if (currentVit < maxVit) {
//		//		currentVit += 1;
//		//	}
			
//		//	if (fruitProgress >= 100) {
//		//		Instantiate(fruit, fruitNode.transform.position, Quaternion.identity).GetComponent<Fruit>().setPurity(12f);
//		//		fruitProgress = 0;
//		//	}
//		//	fruitProgress += fruitRate;
			
//		//} else {
//		//	currentVit -= 1;
//		//}
		
//		//if (currentVit <= 0) {
//		//	Destroy(gameObject);
//		//}
		
//		produce();
//		sustain();
//		//refine();
//	}
	
//	void produce() {
//		if (fruitObj != null) {
			
//			Fruit fruit = fruitObj.GetComponent<Fruit>();
//			float fruitCost = fruit.getBaseDemand();
			
//			//Check how much  is needed
//			if (fruitCost > fruit.getChaosVal() - fruitProgress) {
//				fruitCost = fruit.getChaosVal() - fruitProgress;
//			}
			
//			//Check how much is avaiable
//			if (fruitCost > internalChaos) {
//				fruitCost = internalChaos;
//			}
			
//			fruitProgress += fruitCost;
//			internalChaos -= fruitCost;
			
//			if (fruitProgress >= fruit.getChaosVal()) {
//				Instantiate(fruit, fruitNode.transform.position, Quaternion.identity).GetComponent<Fruit>().setPurity(fruit.getBasePurity());
//				fruitProgress = 0;
//			}
			
//			//fruitPercent = fruitProgress  / fruit.getChaosVal();
//		}
//	}
	
//	void sustain() {
//		if (currentCost < internalChaos) {
//			internalChaos = 0;
//			currentVit -= 1;
//		} else {
//			internalChaos -= currentCost;
//			if (currentVit < maxVit) {
//				currentVit += 1;
//			}
//		}
//		if (currentVit <= 0) {
//			kill();
//		}
//	}
	
//	public float getVit() {
//		return currentVit;
//	}
	
//	public override string ToString() {
//		return gameObject.name;
//	}
	
//	public float getMaxVit() {
//		return maxVit;
//	}
	
//	public float getCurrVit() {
//		return currentVit;
//	}
	
//	public bool getState() {
//		return isWorking;
//	}
	
//	public float getMaxInternalChaos() {
//		return maxInternalChaos;
//	}
	
//	public float getCurrInternalChaos() {
//		return internalChaos;
//	}
	
//	public float getChaosRate() {
//		return currentCost;
//	}
	
//	public float getIntake() {
//		return currentChaosIn;
//	}
	
//	public float getFruitPercent() {
//		return fruitPercent;
//	}
	
//	public void kill() {
//		Destroy(gameObject);
//	}
	
//	//Contains everything contained within a plant
//	//Growth requirement
//	//Nutrient stat
//	//Health
//	//Individual values
//	//Modifier
	
//	//Fernip
//	//Grows iron in bursts
//	//Chaos -> Iron
//	//If low chaos -> no production -> death
//	//If too much iron -> slow production
//	//Too much chaos -> damage
	
	
//	//AcidMelon
//	//Constantly converts iron to chaos as long as there is iron
//	//Iron -> Chaos
//	//No iron -> idle mode
//	//No chaos -> no production -> death
//	//Too much chaos -> take damage
	
//}
