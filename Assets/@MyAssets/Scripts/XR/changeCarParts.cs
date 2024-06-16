using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class changeCarParts : MonoBehaviour
{
    [SerializeField] private Transform t;
    private Vector3 posicionX;

    private void Awake()
    {
        t = GameObject.Find("Garbage").transform;
    }

    private void Update()
    {
        if (transform.parent==null /*|| /*transform.parent.name == "Garbage"*/)
        {
            transform.GetComponent<Rigidbody>().useGravity = true;
            transform.GetComponent<Rigidbody>().isKinematic = false;
        } else
        {
            transform.GetComponent<Rigidbody>().useGravity = false;
            transform.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
