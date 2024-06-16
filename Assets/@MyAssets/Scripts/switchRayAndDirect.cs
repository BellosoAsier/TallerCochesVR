using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchRayAndDirect : MonoBehaviour
{
    [SerializeField] private GameObject hijo1;
    [SerializeField] private GameObject hijo2;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hijo1.SetActive(true);
            hijo2.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hijo1.gameObject.SetActive(false);
            hijo2.gameObject.SetActive(true);
        }
    }
}
