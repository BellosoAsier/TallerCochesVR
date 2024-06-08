using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class containerManager : MonoBehaviour
{
    [SerializeField] public CarPartEnum typeItem; 
    [SerializeField] public int numberOfItems;
    private GameObject itemPallet;
    private gameManagerSO gameDataSO;
    private bool isPalletEmpty;
    private bool isCoroutineRunning = false;
    private List<GameObject> totalPurchasableUniqueParts = new List<GameObject>();

    private void Awake()
    {
        gameDataSO = GameObject.Find("GameManager").GetComponent<gameManagerScript>().gm;
        foreach (gameManagerSO.UniqueObject gmsouo in gameDataSO.purchasableUniquePartsObjectGeneralList)
        {
            totalPurchasableUniqueParts.Add(gmsouo.item);
        }
        itemPallet = totalPurchasableUniqueParts.FirstOrDefault(x => x.name == (typeItem.ToString() + "_" + this.name));
        isPalletEmpty = true;
    }

    private void Update()
    {
        if (numberOfItems<=0)
        {
            this.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<TMP_Text>().color = Color.red;
            this.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = "Sin Stock";
        }
        else
        {
            this.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<TMP_Text>().color = Color.white;
            if (this.transform.GetChild(0).childCount <= 0)
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
            GameObject item = Instantiate(itemPallet,this.transform.GetChild(0));
            item.transform.localPosition = Vector3.zero;
            numberOfItems--;
        }
    }
}
