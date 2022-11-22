using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subscriber : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
	    Ticker ticker = GetComponent<Ticker>();
	    ticker.exchangeTick += testExchange;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
	private void testExchange(object sender, EventArgs e) {
		Debug.Log("Space");
	}
}
