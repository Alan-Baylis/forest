using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class populateScript : MonoBehaviour {


	//Saving 
	public bool save = false;
	public string saveFilepath = "/Save";

	//Prefab
    public Transform prefab;
    Vector3 posran;

    //Placing
	public float density;
	public bool rotation = false;
    public float range = 50;

    string objectserialized;

    List<Vector3> poslist = new List<Vector3>();
    List<Quaternion> rotlist = new List<Quaternion>();

    [SerializeField]
    List<objectplaced> objlist = new List<objectplaced>();


    [System.Serializable]
    public class objectplaced
    {

    	public int ID;
    	public Vector3 position;
    	public Quaternion rotation;
    	public string prefabname;

    	//[SerializableField]
        public objectplaced(string prefabname,int idnum,Vector3 pos, Quaternion rot)
        {
        	this.prefabname = prefabname;
        	this.ID = idnum;
        	this.position = pos;
        	this.rotation = rot;
         }

    }


    Quaternion rot = Quaternion.identity;
    
   
    void SaveMethod(List<objectplaced> whattosave){

    	Debug.Log(Application.persistentDataPath);
        string savepath = (Application.persistentDataPath + saveFilepath);

        string dataAsJson = JsonUtility.ToJson(whattosave);

        File.WriteAllText(savepath + "data.json", dataAsJson);
    }

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
                //Debug.Log(i + "didn't hit");
            }

            //Updating list
            objectplaced objgen = new objectplaced(prefab.name,i,defpos, rot);
			objlist.Add(objgen);

			//Debug.Log(objlist[1]);
            //SaveMethod(objlist);

			//Debug.Log(defpos);
			//Instantiating prefab
            Instantiate(prefab, defpos, rot);


        }

        

        SaveMethod(objlist);
         
        //objectserialized = JsonUtility.ToJson(objlist);
        

    }

    // Update is called once per frame
    void Update() {
    }

}
