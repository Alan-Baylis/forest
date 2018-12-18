using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(populateScript))]
public class editorSaveButton : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        //GameObject objwithplacer = GameObject.Find("Forest");
        //var loadedData = objwithplacer.GetComponent<populateScript>().loadMethod();
        //var loadedData = loadMethod();
        //populateScript myscript = (populateScript)target;
        //List<populateScript.objectplaced> objlist = loadedData.wraplist;

        populateScript myScript = (populateScript)target;
        if (GUILayout.Button("Save Placements"))
        {
            myScript.saveMethod();
        }
    }
}