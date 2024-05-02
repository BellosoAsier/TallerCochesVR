using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class packageScript : MonoBehaviour
{
    [System.Serializable]
    public class PurchasePackageItem
    {
        public GameObject item;
        public int quantity;
        public PurchasePackageItem(GameObject item, int quantity)
        {
            this.item = item;
            this.quantity = quantity;
        }
    }
    public List<PurchasePackageItem> listPurchasePackageItem;
    
}
