using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class terrainScript : MonoBehaviour {

    

    public float frecuency = 2;
    private float density = 1;

    public int xSize;
    public int zSize;
    
    Vector3[] vertices;
    Vector2[] uvs;

    private void Start()
    {
    }

    void Awake() {
    #if UNITY_EDITOR
        UnityEditor.SceneView.FocusWindowIfItsOpen(typeof(UnityEditor.SceneView));
    #endif
    }


    private Vector3 displacement (int x, int z, Texture2D dispMap)
    {
        Vector3 dspVector = new Vector3();

        var color =  dispMap.GetPixel(x,z);
        dspVector.x = color.r - 0.5f;
        dspVector.y = color.g - 0.5f;
        dspVector.z = color.b - 0.5f;


        Debug.Log(dspVector.x);
        return dspVector;
    }

    private float ynoise(float x, float z) {

        return Mathf.PerlinNoise(x / xSize * frecuency, z / zSize * frecuency) * 1f;
        //return Random.Range(0,1);
    }


    public Vector3[] vertexrqt(Vector3[] vertexarray)
    {
        for (int i = 0;i <= vertexarray.Length;i++)
        {
            for (int j = 0; j <= vertexarray.Length; j++)
            {
                
            }
        }


        Vector3[] finalvertex = vertexarray;
        return finalvertex;
    }

    [ExecuteInEditMode]
    public void Generate(Texture2D dspmap,int chunks,int xinit = 0, int zinit = 0, int terrainID = 0)
    {


        //Debug.Log(dspmap.width);
        xSize = 64;// dspmap.width/chunks;
        zSize = 64;//dspmap.height/chunks;
        //Debug.Log(xSize);
        //dspmap.Resize(dspmap.width / 16 * chunks, dspmap.width / 10 * chunks);
        //dspmap.Resize(64*chunks,64*chunks);
        dspmap.Resize(256, 256);
        Debug.Log(dspmap.width);

        GameObject tile = new GameObject();
        tile.name = "Terrain Tile " + xinit +" " + zinit;        
        tile.AddComponent <MeshRenderer>();

        Mesh terrainGeo = new Mesh();
        terrainGeo = new Mesh();
        terrainGeo.name = "Terrain tile " + terrainID;

        var tilepos = tile.transform.position;

        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                Vector3 startpos = new Vector3(x,0,z);
                float height = ynoise(xinit+x, zinit+z);
                //float height = Random.Range(0,3);
                //vertices[i] = new Vector3(x + xinit, height * 10, z + zinit);
                //vertices[i] = new Vector3(x + xinit, 0, z + zinit);
                Vector3 dspResult = displacement(x+xinit, z+zinit, dspmap);
                vertices[i] = startpos + dspResult*10;
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
                uvs[i] = new Vector2((float)x / xSize, (float)z / zSize);
            }
        }

        tile.transform.position = new Vector3(xinit,0,zinit);

        //Assigning mesh data
        terrainGeo.uv = uvs;
        terrainGeo.triangles = triangles;
        terrainGeo.RecalculateBounds();
        terrainGeo.RecalculateNormals();

        //Assigning components to GO.
        MeshFilter tilecomplete = tile.AddComponent<MeshFilter>();
        tilecomplete.mesh = terrainGeo;
        MeshCollider meshc = tile.AddComponent<MeshCollider>();
        meshc.sharedMesh = terrainGeo;

        Material mat = Resources.Load("terrainMat", typeof(Material)) as Material;
        tile.GetComponent<Renderer>().material = mat;
 }


    void Update () {
		
	}
}
