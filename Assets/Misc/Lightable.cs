using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lightable : MonoBehaviour {
	Color originalColor;
	MeshRenderer mr;
	public List<Lightable> children;

	public void Awake()
	{
		mr = this.GetComponent<MeshRenderer>();
		originalColor = mr.material.color;

		UpdateLight(LightAmount.black, false);
	}

	public void UpdateLight(LightAmount l, bool andChildrenLight)
	{
		if (andChildrenLight == true)
		{
			foreach (Lightable c in children)
			{
				c.UpdateLight(l, andChildrenLight);
			}
		}

		if (l == LightAmount.black)
			mr.material.color = Color.black;
		else if (l == LightAmount.revealed)
			mr.material.color = originalColor * .2f;
		else if (l == LightAmount.litOutskirts)
			mr.material.color = originalColor * .5f;
		else if (l == LightAmount.lit)
			mr.material.color = originalColor;
	}
}
