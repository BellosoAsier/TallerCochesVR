using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectItemCatalog : MonoBehaviour
{
    [SerializeField] private List<GameObject> listaSlotsPurchase;
    [SerializeField] private List<List<GameObject>> listaCatalogo;

    public static GameObject objetoActual;

    [System.Serializable]
    public class KeyValueEntry
    {
        public string key;
        public List<GameObject> value;
    }

    public List<KeyValueEntry> hashmap;
    
    private bool updateBool;
    public static int pageCount;
    public static int pagePointer;

    [SerializeField] private GameObject buttonLeft;
    [SerializeField] private GameObject buttonRight;
    [SerializeField] private GameObject buttonComprar;

    [SerializeField] private gameManagerSO gameDataSO;
    private List<GameObject> totalPurchasableUniqueParts;
    // Start is called before the first frame update
    void Start()
    {
        listaCatalogo = new List<List<GameObject>>();
        updateBool = false;
        pageCount = 0;
        totalPurchasableUniqueParts = gameDataSO.purchasableUniquePartsGeneralList;

        foreach (GameObject go in totalPurchasableUniqueParts)
        {
            foreach(KeyValueEntry kve in hashmap)
            {
                if (go.name.Contains(kve.key))
                {
                    kve.value.Add(go);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    { 
        if (updateBool)
        {
            updateBool = !updateBool;

            for (int i = 0; i < 6; i++)
            {
                try
                {
                    listaSlotsPurchase[i].SetActive(true);
                    listaSlotsPurchase[i].GetComponent<ObjectQuantity>().objectName = listaCatalogo[pagePointer][i].name;
                    listaSlotsPurchase[i].GetComponent<ObjectQuantity>().objectActive = true;
                }
                catch (ArgumentOutOfRangeException)
                {
                    listaSlotsPurchase[i].GetComponent<ObjectQuantity>().objectActive = false;
                }
            }
        }
    }

    public void selectCatalogItemtype()
    {
        listaCatalogo = new List<List<GameObject>>();
        foreach (GameObject go in listaSlotsPurchase)
        {
            go.SetActive(true);
        }
        foreach (KeyValueEntry kve in hashmap)
        {
            if (objetoActual.name.Contains(kve.key))
            {
                buttonComprar.SetActive(true);
                listaCatalogo = dividirLista(kve.value,6);
                pageCount = listaCatalogo.Count-1;
                pagePointer = 0;
                updateBool = true;
                if (pageCount > 0)
                {
                    buttonLeft.SetActive(false);
                    buttonRight.SetActive(true);
                }
                else if (pageCount == 0)
                {
                    buttonLeft.SetActive(false);
                    buttonRight.SetActive(false);
                }
            }
        }
    }

    public List<List<GameObject>> dividirLista(List<GameObject> objetos, int elementosPorLista)
    {
        List<List<GameObject>> listasDivididas = new List<List<GameObject>>();

        for (int i = 0; i < objetos.Count; i += elementosPorLista)
        {
            listasDivididas.Add(objetos.GetRange(i, Mathf.Min(elementosPorLista, objetos.Count - i)));
        }

        return listasDivididas;
    }

    public void moveRight()
    {
        updateBool = true;
        if (pagePointer != pageCount)
        {
            pagePointer++;
            buttonRight.SetActive(pagePointer != pageCount);
            buttonLeft.SetActive(true);
        }
    }

    public void moveLeft()
    {
        updateBool = true;
        if (pagePointer != 0)
        {
            pagePointer--;
            buttonLeft.SetActive(pagePointer != 0);
            buttonRight.SetActive(true);
        }
    }
}
