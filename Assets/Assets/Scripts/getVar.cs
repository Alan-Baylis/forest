using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getVar : MonoBehaviour {



	// Use this for initialization
	void Start () {
        GameObject terrain = GameObject.Find("proceduralTerrain");
        terrainScript density = terrain.GetComponent<terrainScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
