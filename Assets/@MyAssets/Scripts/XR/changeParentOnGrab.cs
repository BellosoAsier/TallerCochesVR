using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class changeParentOnGrab : MonoBehaviour
{
    [SerializeField] private Transform t;
    [SerializeField] private PossibleChanges lm;

    private int usos = 10;

    private void Awake()
    {
        t = GameObject.Find("Garbage").transform;
    }
    private void Update()
    {
        if (usos == 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void grabItem(SelectEnterEventArgs args)
    {
        // Cambia el padre del objeto al interactor que lo agarra
        //transform.SetParent(args.interactorObject.transform);
        transform.GetChild(1).GetComponent<MeshCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (usos > 0)
        {
            if (other.CompareTag("Coche"))
            {
                if (other.GetComponent<carPartChangesPossibleID>().possibleChanges.Contains(PossibleChanges.Color) && lm == PossibleChanges.Color)
                {
                    string nameMaterialWithoutInstance = other.gameObject.GetComponent<Renderer>().material.name.Replace(" (Instance)", "");
                    string nameSprayWithoutInstance = this.transform.GetChild(1).GetComponent<Renderer>().material.name.Replace(" (Instance)", "");
                    other.gameObject.GetComponent<Renderer>().material = this.transform.GetChild(1).GetComponent<Renderer>().material;
                    Debug.Log(nameMaterialWithoutInstance + " - " + nameSprayWithoutInstance);
                    if (nameMaterialWithoutInstance != nameSprayWithoutInstance)
                    {
                        usos--;
                    }
                }

                if (other.GetComponent<carPartChangesPossibleID>().possibleChanges.Contains(PossibleChanges.Tinte) && lm == PossibleChanges.Tinte)
                {
                    if (other.gameObject.name.StartsWith("Door"))
                    {
                        string nameMaterialWithoutInstance = other.gameObject.GetComponent<Renderer>().materials[1].name.Replace(" (Instance)", "");
                        string nameSprayWithoutInstance = this.transform.GetChild(1).GetComponent<Renderer>().material.name.Replace(" (Instance)", "");
                        //other.gameObject.GetComponent<Renderer>().materials[1] = this.transform.GetChild(1).GetComponent<Renderer>().material;
                        Debug.Log(nameMaterialWithoutInstance + " - " + nameSprayWithoutInstance);
                        Material[] nuevosMateriales = other.gameObject.GetComponent<Renderer>().materials;
                        nuevosMateriales[1] = this.transform.GetChild(1).GetComponent<Renderer>().material;
                        other.gameObject.GetComponent<Renderer>().materials = nuevosMateriales;
                        if (nameMaterialWithoutInstance != nameSprayWithoutInstance)
                        {
                            usos--;
                        }
                    }
                    else
                    {
                        string nameMaterialWithoutInstance = other.gameObject.GetComponent<Renderer>().material.name.Replace(" (Instance)", "");
                        string nameSprayWithoutInstance = this.transform.GetChild(1).GetComponent<Renderer>().material.name.Replace(" (Instance)", "");
                        other.gameObject.GetComponent<Renderer>().material = this.transform.GetChild(1).GetComponent<Renderer>().material;
                        Debug.Log(nameMaterialWithoutInstance + " - " + nameSprayWithoutInstance);
                        if (nameMaterialWithoutInstance != nameSprayWithoutInstance)
                        {
                            usos--;
                        }
                    }
                }
            }
        }
        
    }
    public void noGrabItem()
    {
        transform.SetParent(t);
        this.transform.GetChild(1).GetComponent<MeshCollider>().isTrigger = false;
    }
}
