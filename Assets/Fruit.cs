using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{	
	[SerializeField] float maxChaos; //Theoretical chaos value if purity is 100%
	[SerializeField] float baseRate; //Percentage increase per tick
	                 float decompRate;
	[SerializeField] float basePurity;
	[SerializeField] float maxPurity;
	                 float purity;
    
	[SerializeField] Rigidbody2D rigidFruit;
	[SerializeField] float dropSpeed;
	
	
	// Start is called before the first frame update
    void Start()
	{
		
    }

    // Update is called once per frame
    void Update()
	{
    	
	}
    
    
	void FixedUpdate() {
		if (rigidFruit.IsAwake()) {
			rigidFruit.velocity = new Vector2(0, -dropSpeed * Time.fixedDeltaTime);
		}
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		
	}
    
    
    
	public float exchange(float total) {
		
		return total;
	}
	
	public void setPurity(float p) {
		if (p < maxPurity) {
			purity = p;
		} else {
			p = maxPurity;
		}
		Debug.Log("Purity set to " + purity);
	}
	
	public float getChaos() {
		return maxChaos;
	}
	
	public float getBaseRate() {
		return baseRate;
	}
	
	public float getBasePurity() {
		return basePurity;
	}
	
	public float getMaxPurity() {
		return maxPurity;
	}
	
}
