using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddNewPartTest : MonoBehaviour
{
    [SerializeField] private bool activate;
    [SerializeField] private GameObject coche;
    public string parteString;
    [SerializeField] private GameObject parteCoche;
    // Update is called once per frame
    void Update()
    {
        if (activate)
        {
            activate = !activate;
            foreach (Transform go in coche.transform)
            {
                if (go.gameObject.name.Contains(parteString))
                {
                    if (go.childCount > 0)
                    {
                        Debug.Log("Tiene una parte");
                    }
                    else
                    {
                        GameObject parte = Instantiate(parteCoche);
                        parte.transform.parent = go.transform;
                        parte.transform.localPosition = Vector3.zero;
                    }
                    
                }
            }
            


        }
    }
}
