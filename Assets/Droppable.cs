using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droppable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
	float speed = -30f;
	[SerializeField] Rigidbody2D rigidFruit;
	private void fall() {
		rigidFruit.velocity = new Vector2(0, speed * Time.fixedDeltaTime);
	}
	
	void FixedUpdate() {
		if (rigidFruit.IsAwake()) {
			fall();
		}
		
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		rigidFruit.Sleep();
	}
	
	
}
