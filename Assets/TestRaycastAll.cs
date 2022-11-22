using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRaycastAll : MonoBehaviour
{
    // Start is called before the first frame update
	[SerializeField]  string layerToCheck = "Default";

	void Update()
	{
		Vector2 mousePosWorld2D = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

		GameObject result = GetHighestRaycastTarget(mousePosWorld2D);


		// Do Stuff example:
		Debug.Log($"Highest Layer: {result}");

		if (Input.GetMouseButtonDown(0)
			&& result != null)
		{
			Destroy(result);
		}
	}


	// Get highest RaycastTarget based on the Sortinglayer
	// Note: If multiple Objects have the same SortingLayer (e.g. 42) and this is also the highest SortingLayer, then the Function will return the last one it found
	private GameObject GetHighestRaycastTarget(Vector2 mousePos)
	{
		GameObject topLayer = null;
		RaycastHit2D[] hit = Physics2D.RaycastAll(mousePos, Vector2.zero);
		
		if (Input.GetMouseButtonDown(0)) {
			Debug.Log(hit.Length);
		}
		
		foreach (RaycastHit2D ray in hit)
		{
			SpriteRenderer spriteRenderer = ray.transform.GetComponent<SpriteRenderer>();

			// Check if RaycastTarget has a SpriteRenderer and
			// Check if the found SpriteRenderer uses the relevant SortingLayer
			if (spriteRenderer != null
				&& spriteRenderer.sortingLayerName == layerToCheck)
			{
				if (topLayer == null)
				{
					topLayer = spriteRenderer.transform.gameObject;
				}

				if (spriteRenderer.sortingOrder >= topLayer.GetComponent<SpriteRenderer>().sortingOrder)
				{
					topLayer = ray.transform.gameObject;
				}
			}

		}

		return topLayer;
	}
	
}
