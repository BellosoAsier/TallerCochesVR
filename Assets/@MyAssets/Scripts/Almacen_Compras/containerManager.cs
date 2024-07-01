using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static clientOrders;

public class containerManager : MonoBehaviour
{
    [SerializeField] public CarPartEnum typeItem; 
    [SerializeField] public int numberOfItems;
    private GameObject itemPallet;
    private gameManagerSO gameDataSO;
    private bool isPalletEmpty;
    private bool isCoroutineRunning = false;
    private List<GameObject> totalPurchasableUniqueParts = new List<GameObject>();

    [System.Serializable]
    public class AlmacenItem
    {
        public CarPartEnum typeItem;
        public string name;
        public int quantity;

        public AlmacenItem(CarPartEnum cpe, string n, int q)
        {
            this.typeItem = cpe;
            this.name = n;
            this.quantity = q;
        }
    }
    private AlmacenItem itemAlmacen;

    private void Awake()
    {
        this.AddComponent<filesManager>();
        List<AlmacenItem> x = this.GetComponent<filesManager>().LoadAlmacenItemsListJSON();
        foreach (AlmacenItem u in x)
        {
            if (u.typeItem.Equals(typeItem) && u.name.Equals(this.name))
            {
                numberOfItems = u.quantity;
            }
        }
        gameDataSO = GameObject.Find("GameManager").GetComponent<gameManagerScript>().gm;
        foreach (gameManagerSO.UniqueObject gmsouo in gameDataSO.purchasableUniquePartsObjectGeneralList)
        {
            totalPurchasableUniqueParts.Add(gmsouo.item);
        }
        itemPallet = totalPurchasableUniqueParts.FirstOrDefault(x => x.name == (typeItem.ToString() + "_" + this.name));
        isPalletEmpty = true;
        itemAlmacen = new AlmacenItem(typeItem,this.name,numberOfItems);
    }

    private void Update()
    {
        if (this.transform.GetChild(0).childCount > 0)
        {
            itemAlmacen.quantity = numberOfItems + 1;
        }
        else
        {
            itemAlmacen.quantity = numberOfItems;
        }
        
        if (numberOfItems<=0)
        {
            this.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<TMP_Text>().color = Color.red;
            this.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = "Sin Stock";
        }
        else
        {
            this.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<TMP_Text>().color = Color.white;

            if (this.transform.GetChild(0).childCount <= 1 && !(transform.name.StartsWith("Color")))
            {
                isPalletEmpty = true;
                if (!isCoroutineRunning)
                {
                    StartCoroutine(ExecuteAfterDelay(3f));
                }
            }
            else if (this.transform.GetChild(0).childCount <= 0 && transform.name.StartsWith("Color"))
            {
                isPalletEmpty = true;
                if (!isCoroutineRunning)
                {
                    StartCoroutine(ExecuteAfterDelay(3f));
                }
            }
            else
            {
                isPalletEmpty = false;
            }

            this.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = numberOfItems + "";
        }
        
    }

    IEnumerator ExecuteAfterDelay(float delay)
    {
        isCoroutineRunning = true;

        yield return new WaitForSeconds(delay);

        placeItemOnPallet();

        isCoroutineRunning = false;
    }

    private void placeItemOnPallet()
    {
        if (isPalletEmpty)
        {
            isPalletEmpty = false;
            GameObject item = Instantiate(itemPallet,transform.GetChild(0));
            item.name.Replace("(Clone)", "");
            if (!(item.name.StartsWith("Paint")))
            {
                item.transform.position = transform.GetChild(0).GetChild(0).position;
                item.transform.localScale = Vector3.one;
            }
            numberOfItems--;
        }
    }

    public AlmacenItem getItemAlmacen()
    {
        return itemAlmacen;
    }
}
