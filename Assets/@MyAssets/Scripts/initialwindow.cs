using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initialWindow : MonoBehaviour
{
    [SerializeField] private GameObject b1;
    [SerializeField] private GameObject b2;
    [SerializeField] private GameObject b3;

    public void initialToOrder()
    {
        b1.SetActive(true);
        this.gameObject.SetActive(false);
    }
    public void initialToPurchase()
    {
        b2.SetActive(true);
        this.gameObject.SetActive(false);
    }
    public void initialToAssistant()
    {
        b3.SetActive(true);
        this.gameObject.SetActive(false);
    }
    public void returnToHome()
    {
        b1.SetActive(false);
        b2.SetActive(false);
        b3.SetActive(false);
        this.gameObject.SetActive(true);
    }
}
