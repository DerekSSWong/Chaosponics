using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOverlap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    Collider2D[] hitColliders = Physics2D.OverlapCircleAll(this.transform.position, 100f);
	    foreach (var hitCollider in hitColliders)
	    {
		    Debug.Log(hitCollider.gameObject.name);
	    }
    }
    
	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(this.transform.position, 1);
	}
}
