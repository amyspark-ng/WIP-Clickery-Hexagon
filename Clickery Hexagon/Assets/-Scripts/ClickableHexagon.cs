using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableHexagon : MonoBehaviour
{

	[SerializeField] ScrScripter scripterRef;

	void OnMouseEnter()
	{
		scripterRef.Clickable = true;
	}

	void OnMouseExit()
	{
		scripterRef.Clickable = false;	
	}

}
