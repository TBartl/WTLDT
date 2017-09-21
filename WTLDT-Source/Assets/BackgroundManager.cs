using UnityEngine;
using System.Collections;

public class BackgroundManager : MonoBehaviour {
	static bool useAlt = true;
	public GameObject background1;
	public GameObject background2;

	void Start()
	{
		if (useAlt)
		{
			background1.SetActive(!useAlt);
			background2.SetActive(useAlt);
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.T))
		{
			useAlt = !useAlt;
			if (useAlt)
			{
				background1.SetActive(false);
				background2.SetActive(true);
			} else
			{
				background1.SetActive(true);
				background2.SetActive(false);
			}
		}
	}
}
