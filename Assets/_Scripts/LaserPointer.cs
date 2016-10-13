using UnityEngine;
using System.Collections;

public class LaserPointer : MonoBehaviour {

	GameObject hitObject;
	RaycastHit hit;
	public Reticle reticle;
	
	void Update () 
	{
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
		{
			reticle.SetPosition(hit);
			hitObject = hit.transform.gameObject;
			Interactable interactable = hitObject.GetComponent<Interactable>();
			if(interactable != null)
			{
				interactable.targeted = true;
			}
		} else
		{
			reticle.SetPosition();
		}
	}
}
