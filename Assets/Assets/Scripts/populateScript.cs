﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class populateScript : MonoBehaviour {


    public Transform prefab;
    Vector3 posran;

    public float density;

    public bool rotation = false;
    public int population = 100;
    public float range = 50;

    List<Vector3> poslist = new List<Vector3>();
    List<Quaternion> rotlist = new List<Quaternion>();
    List<objectplaced> objlist = new List<objectplaced>();

    public class objectplaced
    {
        public objectplaced(int _ID,Vector3 _position, Quaternion _rotation)
        {
        }
    }


    Quaternion rot = Quaternion.identity;
    
   
	// Use this for initialization
	void Start () {

        Vector3 defpos = new Vector3();
        int layerMask = 9;

        string name = this.name;
        
        for (int i = 1; i <= range * density; i++)
        {

            posran = new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range));

            RaycastHit hit;
            Ray downRay = new Ray(posran + new Vector3(0,10,0), Vector3.down*50);


            if (Physics.Raycast(downRay, out hit, Mathf.Infinity, layerMask))
            {
                //Debug.Log("Hit");
                defpos = posran + new Vector3(0,(10-hit.distance), 0);
                //Debug.Log(hit.point);

                if (rotation == true)
                {
                    //Vector3 newDir = Vector3.RotateTowards(Vector3.up, hit.normal,2.4f,0.0f);
                    //transform.rotation = Quaternion.Euler(newDir);
                    rot = Quaternion.FromToRotation(Vector3.up, hit.normal);
                }
            }
            else
            {
                //Debug.Log("Didnt");
                //hit.distance = 5;
            }


            //Debug.DrawRay(posran+Vector3.up*40, Vector3.down * 50, Color.green, 15f);

            //defpos = posran + new Vector3(0, 10-hit.distance, 0);

            objectplaced objgen = new objectplaced(i,defpos, rot);
            //objgen.position = defpos;

            objlist.Add(objgen);

            //Debug.Log(i);
            //poslist.Add(defpos);
            //rotlist.Add(rot);

            Instantiate(prefab, defpos, rot);

        }

    }

    // Update is called once per frame
    void Update() {
    }

}
