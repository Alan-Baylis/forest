using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starter : MonoBehaviour {

    terrainScript customTerrain = new terrainScript();

    public int detail = 1;

    // Use this for initialization

    void Start()
    {
        int chunks = detail*4;
        //Texture2D white = new Texture2D(180,180);
        //terrainScript customTerrain = GetComponent<terrainScript>();
        Texture2D terratex = Resources.Load("dspmap", typeof(Texture2D)) as Texture2D;
        //Debug.Log(white.GetPixel(1, 1));
        int id = 1;

        for (int i = 0; (i + 1) <= chunks; i++)
        {
            for (int j = 0; (j + 1) <= chunks; j++)
            {
                customTerrain.Generate(terratex,chunks,(i) * 64 + 0, (j) * 64 + 0, id);
                id++;
            }
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
