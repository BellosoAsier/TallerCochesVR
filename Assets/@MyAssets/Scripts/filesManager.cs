using Newtonsoft.Json;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static clientOrders;
using static containerManager;

public class filesManager : MonoBehaviour
{
    [System.Serializable]
    public class SerializableOrderList
    {
        public List<ClientOrder> OrderList;

        public SerializableOrderList(List<ClientOrder> orderList)
        {
            OrderList = orderList;
        }
    }

    [System.Serializable]
    public class SerializableAlmacenList
    {
        public List<AlmacenItem> AlmacenList;

        public SerializableAlmacenList(List<AlmacenItem> aList)
        {
            AlmacenList = aList;
        }
    }
    public List<string> ReadTXTFile(TextAsset file, bool isName)
    {
        string[] data = file.text.Split(new string[] { "\n" }, System.StringSplitOptions.None);
        List<string> newdata = new List<string>();
        foreach (string arrItem in data)
        {
            newdata.Add(arrItem);
        }
        newdata = Shuffle(newdata);
        List<string> listaFinal = new List<string>();
        for (int i = 0; i < newdata.Count; i++)
        {
            if (isName)
            {
                string nameLower = newdata[i].ToLower();
                if (!string.IsNullOrEmpty(nameLower))
                {
                    nameLower = char.ToUpper(nameLower[0]) + nameLower.Substring(1);
                }
                listaFinal.Add(nameLower);
            }
            else
            {
                listaFinal.Add(newdata[i]);
            }
            

        }
        return listaFinal;
    }

    public List<string> Shuffle(List<string> items)
    {
        return items.Distinct().OrderBy(x => System.Guid.NewGuid().ToString()).ToList();
    }

    public void SaveOrderListJSON(List<ClientOrder> listObject)
    {
        string json = JsonUtility.ToJson(new SerializableOrderList(listObject));
        string path = Path.Combine(Application.persistentDataPath + "/orderList.json");
        File.WriteAllText(path, json);

        Debug.Log(path);
    }

    public List<ClientOrder> LoadOrderListJSON()
    {
        string path = Path.Combine(Application.persistentDataPath + "/orderList.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SerializableOrderList serializedList = JsonUtility.FromJson<SerializableOrderList>(json);
            return serializedList.OrderList;
        }
        else
        {
            Debug.LogWarning("No se encontró el archivo de lista de pedidos.");
            PlayerPrefs.SetInt("ppap", 0);
            return new List<ClientOrder>();
        }
    }

    public void SaveAlmacenItemsList(List<AlmacenItem> aList)
    {
        string json = JsonUtility.ToJson(new SerializableAlmacenList(aList));
        string path = Path.Combine(Application.persistentDataPath + "/almacenItemsList.json");
        File.WriteAllText(path, json);

        Debug.Log(path);
    }

    public List<AlmacenItem> LoadAlmacenItemsListJSON()
    {
        string path = Path.Combine(Application.persistentDataPath + "/almacenItemsList.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SerializableAlmacenList serializedList = JsonUtility.FromJson<SerializableAlmacenList>(json);
            return serializedList.AlmacenList;
        }
        else
        {
            Debug.LogWarning("No se encontró el archivo de lista de items de almacen.");
            return new List<AlmacenItem>();
        }
    }

}
