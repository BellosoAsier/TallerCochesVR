using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initialWindow : MonoBehaviour
{
    [SerializeField] private GameObject order;
    [SerializeField] private GameObject purchase;
    [SerializeField] private GameObject assistant;

    public void initialToOrder()
    {
        order.SetActive(true);
        this.gameObject.SetActive(false);
    }
    public void initialToPurchase()
    {
        purchase.SetActive(true);
        this.gameObject.SetActive(false);
    }
    public void initialToAssistant()
    {
        //TODO: Implementar el codigo cuando el asistente este hecho.
    }
}
