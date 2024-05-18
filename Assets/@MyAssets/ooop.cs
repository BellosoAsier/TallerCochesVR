using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ooop : MonoBehaviour
{
    [SerializeField] private GameObject instanceIdABuscar;
    void Start()
    {
        
        Debug.Log(instanceIdABuscar.GetInstanceID());
    }
}
