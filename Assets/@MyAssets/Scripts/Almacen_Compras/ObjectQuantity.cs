using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static purchaseScript;

public class ObjectQuantity : MonoBehaviour
{
    [SerializeField] private TMP_Text quantity;

    private int result;
    [SerializeField] private purchaseScript ps;

    [SerializeField] private string objectName;

    [SerializeField] private bool objectActive;

    private void Awake()
    {
        result = int.Parse(quantity.text);
        if (!objectActive)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void funcionSumar()
    {
        result = result + 1;
        quantity.text = result + "";
    }

    public void funcionRestar()
    {
        if (result>0)
        {
            result = result - 1;
            quantity.text = result + "";
        }
    }
    
    public void AddCarPartPurchase()
    {
        //string newName = this.name.Replace("Purchase","");
        ps.listaPurchaseOrders.Add(new PurchaseOrder(objectName, result));
        result = 0;
        quantity.text = result + "";
    }
}
