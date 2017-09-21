using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UseRandomMesh : MonoBehaviour {

	public List<Mesh> meshes;

	// Use this for initialization
	void Start () {
		this.GetComponent<MeshFilter>().mesh = meshes[Random.Range(0, meshes.Count)];
	}
}
