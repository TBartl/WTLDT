using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lightable : MonoBehaviour {
	Color originalColor;
	Renderer mr;
	public List<Lightable> children;

	public void Awake()
	{
		mr = this.GetComponent<Renderer>();
		if (mr == null)
			return;

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

		if (mr == null)
			return;

		Color newColor = Color.black;
		mr.enabled = true;
		if (l == LightAmount.black)
		{
			newColor = Color.black;
			mr.enabled = false;
		}
		else if (l == LightAmount.revealed)
			newColor = originalColor * .2f;
		else if (l == LightAmount.litOutskirts)
			newColor = originalColor * .5f;
		else if (l == LightAmount.lit)
			newColor = originalColor;

		//StartCoroutine(SmoothGoBetweenColors(mr.material.color ,newColor));
		mr.material.color = newColor;
	}

	IEnumerator SmoothGoBetweenColors(Color fromCol, Color toCol)
	{
		mr.material.color = fromCol;
		float length = .1f;
		for (float f = 0; f < length; f += Time.deltaTime)
		{
			mr.material.color = Color.Lerp(fromCol, toCol, f / length);
			yield return null;
		}
		mr.material.color = toCol;
	}

	public void HideAfterAFrame()
	{
		StartCoroutine(Hide());

	}
	IEnumerator Hide()
	{
		yield return null;
		foreach (Lightable c in children)
		{
			if (c.mr)
				c.mr.enabled = false;
		}
		if (mr)
			mr.enabled = false;
	}
}
