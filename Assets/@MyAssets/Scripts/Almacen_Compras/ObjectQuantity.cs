using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using static purchaseScript;

public class ObjectQuantity : MonoBehaviour
{
    [SerializeField] private TMP_Text quantity;

    private int result;
    [SerializeField] private purchaseScript ps;

    [SerializeField] public string objectName;

    [SerializeField] public bool objectActive;

    [SerializeField] private Image image;

    [SerializeField] private TMP_Text text_Value;

    [SerializeField] private TMP_Text item_Name;

    private gameManagerSO gameDataSO;

    private int valueOfItem;

    private void Awake()
    {
        result = int.Parse(quantity.text);
        if (!objectActive)
        {
            this.gameObject.SetActive(false);
        }

        gameDataSO = GameObject.Find("GameManager").GetComponent<gameManagerScript>().gm;
    }

    private void Update()
    {
        foreach (gameManagerSO.UniqueObject gmsouo in gameDataSO.purchasableUniquePartsObjectGeneralList)
        {
            if (gmsouo.item.name.Equals(objectName))
            {
                item_Name.text = objectName;
                valueOfItem = gmsouo.value;
                text_Value.text = valueOfItem + "$"; 
                image.sprite = gmsouo.sprite;
            }
        }
        //item_Name.text = objectName;
        //image.sprite = gameDataSO.purchaseableObjectImageGeneralList.FirstOrDefault(sprite => sprite.name == objectName);
        if (!objectActive)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
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
        ps.listaPurchaseOrders.Add(new PurchaseOrder(objectName, result));
        ps.addTotalAmount(valueOfItem*result);
        result = 0;
        quantity.text = result + "";
    }
}
