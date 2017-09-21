using UnityEngine;
using System.Collections;

public class MoveBackground : MonoBehaviour {
	public float resetMod;
	float distanceTravelled = 0;
	public float speed = 5;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		distanceTravelled = (distanceTravelled + speed * Time.deltaTime) % resetMod;
		this.transform.localPosition = new Vector3(distanceTravelled, distanceTravelled, 20);
	}
}
