using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeRandomizer : MonoBehaviour {

    Vector3 randscale;
    public int topHeight = 20;
    public int minimunHeight = 10;

    //public GameObject parentForest;
    

	// Use this for initialization
	void OnEnable () {


        GameObject parent = GameObject.Find("Forest");

        transform.SetParent(parent.transform);

        int angleRan = Random.Range(0, 360);
        transform.Rotate(Vector3.up, angleRan);
      

        float scaley = Random.Range(minimunHeight, topHeight);
        float scalexz = Random.Range(5, 15);

        randscale = new Vector3(scalexz,scaley, scalexz);
        transform.localScale = randscale;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
