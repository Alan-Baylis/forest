using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class populateScript : MonoBehaviour {


    public Transform prefab;
    Vector3 posran;

    public float density;

    public int population = 100;
    public float range = 50;
    

	// Use this for initialization
	void Start () {

        Vector3 defpos = new Vector3();
        int layerMask = 9;

        for (int i=1; i<=range*density; i++) {

            posran = new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range));

            RaycastHit hit;
            Ray upRay = new Ray(posran, Vector3.up*50);


            if (Physics.Raycast(upRay, out hit, Mathf.Infinity, layerMask))
            {
                Debug.Log("Hit");
                defpos = posran + new Vector3(0, hit.distance, 0);
                Debug.Log(hit.point);
            }
            else
            {
                //Debug.Log("Didnt");
                //hit.distance = 5;
            }


            Debug.DrawRay(posran, Vector3.up*50, Color.green, 15f);

            defpos = posran + new Vector3(0, hit.distance, 0);
            

            Instantiate(prefab, defpos, Quaternion.identity);
        }

    }
	
	// Update is called once per frame
	void Update () {


	}
}
