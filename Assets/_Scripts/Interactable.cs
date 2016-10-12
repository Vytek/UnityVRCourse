using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {
	private Color startColor;
	public bool targeted = false;
	private Material material;
	
	void Start () 
	{
		material = GetComponent<Renderer>().material;
		startColor = material.color;
	}
	
	
	void Update () 
	{
		if (targeted)
		{
			Target();
			targeted = false;
		} else
		{
			Untarget();
		}
	}

	public void Target()
	{
		material.color = Color.green;
	}

	public void Untarget()
	{
		material.color = startColor;
	}
}
