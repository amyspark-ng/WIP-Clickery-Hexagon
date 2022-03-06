using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// # Scrip that rotates the hexagon trough code and not animations (cool)
public class Rotator : MonoBehaviour
{
	// Z rotation value and its speed
	float rotZ;
	float rotSpeed = 5f;		
	
	// !clockwise = L | clockwise = R;
	bool clockwise = true; 

	// # Update is called once per frame
	void Update()
	{
		// if it's going left
		if (!clockwise) {
			rotZ += Time.deltaTime * rotSpeed;
			// Rotation will be equal to its value plus Time.deltaTime multiplied by its speed
		}

		else {
			rotZ += -Time.deltaTime * rotSpeed;
			// Rotation will be equal to its value plus negative Time.deltaTime multiplied by its speed
		}
	
		// # Actually rotates the object with its Z rotation value (very cool)
		transform.rotation = Quaternion.Euler(0, 0, rotZ);
	}
}
