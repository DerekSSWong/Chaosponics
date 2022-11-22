using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ticker : MonoBehaviour
{	
	private bool canExchange;
	[SerializeField] int exchangesPerSec;
	public event EventHandler exchangeTick;
	
    // Start is called before the first frame update
    void Start()
	{
		canExchange = true;
    }

    // Update is called once per frame
    void Update()
    {
	    if (canExchange) {
	    	StartCoroutine(startExchange());
	    	
	    }
    }
    
	private IEnumerator startExchange() {
		canExchange = false;
		
		exchangeTick?.Invoke(this, EventArgs.Empty);
		yield return new WaitForSeconds(1f/exchangesPerSec);
		canExchange = true;
	}
    
}
