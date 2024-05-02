using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectItemCatalog : MonoBehaviour
{
    [SerializeField] private List<GameObject> listaSlotsPurchase;

    [System.Serializable]
    public class KeyValueEntry
    {
        public string key;
        public List<GameObject> value;
    }

    public List<KeyValueEntry> hashmap;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void selectCatalogItemtype()
    {
        GameObject objetoActual = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        List<GameObject> listaCatalogo = new List<GameObject>();

        foreach (KeyValueEntry kve in hashmap)
        {
            if (objetoActual.name.Contains(kve.key))
            {
                listaCatalogo = kve.value;
            }
        }
    }
}
