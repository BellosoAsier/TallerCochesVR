using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ooop : MonoBehaviour
{
    public ScriptableObject myScriptableObject;

    void Start()
    {
        string path = AssetDatabase.GetAssetPath(myScriptableObject);
        Debug.Log("Ruta del Scriptable Object: " + path);
    }
}
