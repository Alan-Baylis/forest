using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terrainScript : MonoBehaviour {



    public float frecuency = 2;
    
    public int xSize = 60;
    public int zSize = 60;
    public float density = 1;
    Vector3[] vertices;
    Vector2[] uvs;

    // Use this for initialization
    void Awake() {


        populateScript.objec

    #if UNITY_EDITOR
        UnityEditor.SceneView.FocusWindowIfItsOpen(typeof(UnityEditor.SceneView));
#endif

        

        //gameObject.layer = 9;

        //transform.Rotate((Vector3.right * 90f));
        transform.position = new Vector3((-xSize / 2), transform.position.y,(- zSize / 2));
        Generate();

    }


    private float ynoise(float x, float z) {

        return Mathf.PerlinNoise(x/xSize* frecuency, z/zSize*frecuency)*1f;
        //return Random.Range(0,1);
    }
        

    
    //private void Awake()
    //{        
    //    Generate();
    //}


    [ExecuteInEditMode]
    private void Generate()
    {
        Mesh terrainGeo = new Mesh();

        

        GetComponent<MeshFilter>().mesh = terrainGeo= new Mesh();
        terrainGeo.name = "Terrain";

        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                float height = ynoise(x,z);
                vertices[i] = new Vector3(x,height*10, z);
            }
        }
        terrainGeo.vertices = vertices;

        int[] triangles = new int[xSize * zSize * 6];
        for (int ti = 0, vi = 0, z = 0; z < zSize; z++, vi++)
        {
            for (int x = 0; x < xSize; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
            }
        }

        uvs = new Vector2[vertices.Length];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                uvs[i] = new Vector2((float)x / xSize,(float) z / zSize);
            }
        }

        terrainGeo.uv = uvs;
        terrainGeo.triangles = triangles;
        terrainGeo.RecalculateBounds();
        terrainGeo.RecalculateNormals();



        //Assigning MeshCollider
        MeshCollider meshc = gameObject.AddComponent<MeshCollider>();
        meshc.sharedMesh = terrainGeo;
        


    }

   
    void Update () {


		
	}
}
