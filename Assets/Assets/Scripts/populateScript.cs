using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class populateScript : MonoBehaviour {

	//Saving 
	public bool save = false;
	public bool load = false;
	public string saveFilepath = "/Save/";

	//Prefab
    public Transform prefab;


    //Placing
	public float density;
	public bool rotation = false;
    public float range = 50;

    string objectserialized;

    //List<Vector3> poslist = new List<Vector3>();
    //List<Quaternion> rotlist = new List<Quaternion>();

    [SerializeField]
    public List<objectplaced> objlist = new List<objectplaced>();


    [System.Serializable]
    public class objectplaced
    {

    	public int ID;
    	public Vector3 position;
    	public Quaternion rotation;
    	public string prefabname;
        public Transform prefab;

    	//[SerializableField]
        public objectplaced(string prefabname,int idnum,Vector3 pos, Quaternion rot,Transform pfab)
        {
        	this.prefabname = prefabname;
        	this.ID = idnum;
        	this.position = pos;
        	this.rotation = rot;
            this.prefab = pfab;
         }

    }

    public class WrappingClass {
    	public List<objectplaced> wraplist;
	}
    
    Quaternion rot = Quaternion.identity;
    
    void Start () {

		Placer();
		if (save == true){
            //saveMethod(objlist);
            saveMethod();
        }
        
    }
   
   	WrappingClass desjson = new WrappingClass();

   	public WrappingClass loadMethod() {

        

        var variable = new WrappingClass();
        string savepath = (Application.persistentDataPath + saveFilepath);
       
       	var inputString = File.ReadAllText(savepath + prefab.name + ".json");

		//var desjson = JsonUtility.FromJson<WrappingClass>(inputString);
		WrappingClass desjson = JsonUtility.FromJson<WrappingClass>(inputString);

   		return desjson;
   	}

    public void saveMethod(){// (List<objectplaced> whattosave){

        List<objectplaced> whattosave = objlist;
    	//Debug.Log(Application.persistentDataPath);
        string savepath = (Application.persistentDataPath + saveFilepath);

        var variable = new WrappingClass() { wraplist = whattosave};

        string dataAsJson = JsonUtility.ToJson(variable);
        var folder = Directory.CreateDirectory(savepath);

        File.WriteAllText(savepath + prefab.name + ".json", dataAsJson);
    }

    void Placer(){

    	if (load == true){
    		var loadedData = loadMethod();

    		for (int j = 1; j <= loadedData.wraplist.Count;j++){
    			//Debug.Log(loadedData.wraplist[j-1].ID);
                Instantiate(loadedData.wraplist[j - 1].prefab, loadedData.wraplist[j - 1].position, loadedData.wraplist[j - 1].rotation);
            }


    	}
    	else{

            Vector3 posran;
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
	                    rot = Quaternion.FromToRotation(Vector3.up, hit.normal);
	                }
	            }
	            else
	            {
	            }

	            //Updating list
	            objectplaced objgen = new objectplaced(prefab.name,i,defpos, rot, prefab);
				objlist.Add(objgen);

				//Instantiating prefab
	            Instantiate(prefab, defpos, rot);

	            //if (objlist.Contains(objgen)){
	            //	Debug.Log("YES" + i );
	            //}

	        }
        } 
    }


   

}
