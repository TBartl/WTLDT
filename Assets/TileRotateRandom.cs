using UnityEngine;
using System.Collections;

public class TileRotateRandom : MonoBehaviour {

	// Use this for initialization
	void Start () {
		float val = Random.value;
		if (val <= .25f)
			this.transform.rotation = Quaternion.Euler(0, 0, 0);
		else if (val <= .5f)
			this.transform.rotation = Quaternion.Euler(0, 90, 0);
		else if (val <= .75f)
			this.transform.rotation = Quaternion.Euler(0, 180, 0);
		else
			this.transform.rotation = Quaternion.Euler(0, 270, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
