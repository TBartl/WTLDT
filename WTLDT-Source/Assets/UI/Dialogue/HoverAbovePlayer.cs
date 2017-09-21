using UnityEngine;
using System.Collections;

public class HoverAbovePlayer : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (PlayerMovement.S)
		{
			this.transform.position = PlayerMovement.S.transform.position + Vector3.up * 1.5f;
		}
	}
}
