using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using static packageScript;

public class purchaseScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> totalPurchasableUniqueParts = new List<GameObject>();
    [SerializeField] private GameObject purchasePackage;
    [SerializeField] private RobotAIBehaviour raib;

    [SerializeField] private gameManagerSO gameDataSO;

    [SerializeField] private GameObject mainPage;
    [SerializeField] private GameObject confButton;
    [SerializeField] private GameObject cancButton;

    [SerializeField] private TMP_Text confirmacionText;

    [SerializeField] private int totalAmount = 0;

    [System.Serializable]
    public class PurchaseOrder
    {
        public string item;
        public int quantity;
        public PurchaseOrder(string item, int quantity)
        {
            this.item = item;
            this.quantity = quantity;
        }
    }
    public List<PurchaseOrder> listaPurchaseOrders;

    private Dictionary<string, GameObject> itemDictionary = new Dictionary<string, GameObject>();

    [SerializeField] private Transform positionPackage;

    // Start is called before the first frame update
    private void Awake()
    {
        foreach (gameManagerSO.UniqueObject gmsouo in gameDataSO.purchasableUniquePartsObjectGeneralList)
        {
            totalPurchasableUniqueParts.Add(gmsouo.item);
        }
        //totalPurchasableUniqueParts = gameDataSO.purchasableUniquePartsGeneralList;
        //totalPurchasableUniqueParts = LoadGameObjectFromFolder("Assets/@MyAssets/Provisional");
        InitializeDictionary();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void InitializeDictionary()
    {
        // Itera sobre la lista de GameObjects
        foreach (GameObject obj in totalPurchasableUniqueParts)
        {
            // Añade el nombre del objeto y su GameObject al diccionario
            itemDictionary.Add(obj.name, obj);
        }
    }

    public void continueToPurchase()
    {
        mainPage.SetActive(false);
        confButton.SetActive(true);
    }

    public void continueToCancelOrder()
    {
        mainPage.SetActive(false);
        cancButton.SetActive(true);
    }

    public void cancelOrder()
    {
        listaPurchaseOrders.Clear();
        returnBack();
        totalAmount = 0;
    }

    public void returnBack()
    {
        mainPage.SetActive(true);
        cancButton.SetActive(false);
        confButton.SetActive(false);
    }

    public void allSelectedAndPurchase()
    {
        GameObject newPackage = Instantiate(purchasePackage);
        newPackage.transform.position = positionPackage.position;
        newPackage.AddComponent<packageScript>();
        foreach (PurchaseOrder order in listaPurchaseOrders)
        {
            // Verifica si el nombre del elemento está en el diccionario
            if (itemDictionary.ContainsKey(order.item))
            {
                GameObject item = itemDictionary[order.item];
                int quantity = order.quantity;

                packageScript package = newPackage.GetComponent<packageScript>();
                if (package != null)
                {
                    if (package.listPurchasePackageItem == null)
                    {
                        package.listPurchasePackageItem = new List<PurchasePackageItem>(); 
                    }

                    PurchasePackageItem existingItem = package.listPurchasePackageItem.Find(x => x.item.name == item.name);
                    if (existingItem != null)
                    {
                        // Si el elemento ya está en la lista, actualiza la cantidad
                        existingItem.quantity += quantity;
                    }
                    else
                    {
                        // Si no está en la lista, agrega un nuevo elemento
                        PurchasePackageItem newPPI = new PurchasePackageItem(item, quantity);
                        newPackage.GetComponent<packageScript>().listPurchasePackageItem.Add(newPPI);
                    }
                }
                else
                {
                    Debug.LogWarning("El objeto newPackage no tiene un componente packageScript adjunto.");
                }
            }
            else
            {
                Debug.LogWarning("El elemento " + order.item + " no se encontró en el diccionario.");
            }
        }
        raib.assignPackage(newPackage);
        raib.changeStateRobot(robotState.Recogida);
        cancelOrder();
    }

    public void addTotalAmount(int x)
    {
        totalAmount += x;
        confirmacionText.text = totalAmount + "$";
    }
}
