using UnityEngine;
using System.Collections;

public class LaserPointer : MonoBehaviour {

	GameObject hitObject;
	RaycastHit hit;
	
	
	void Update () 
	{
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
		{
			hitObject = hit.transform.gameObject;
			Interactable interactable = hitObject.GetComponent<Interactable>();
			if(interactable != null)
			{
				interactable.targeted = true;
			}
		}
	}
}
