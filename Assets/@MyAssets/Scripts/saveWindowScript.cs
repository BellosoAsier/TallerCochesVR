using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class saveWindowScript : MonoBehaviour
{
    [SerializeField] private Button b1;
    [SerializeField] private Button b2;
    [SerializeField] private TMP_Text lastSave;

    [SerializeField] private GameObject monitor;

    public List<containerManager.AlmacenItem> listaAlmacen;

    private playerDataSO user;
    void Start()
    {
        user = GameObject.Find("GameManager").GetComponent<gameManagerScript>().user;
        lastSave.text ="Último guardado: " + PlayerPrefs.GetString("lastSaveDate","No hay ultimo guardado.");
    }
    private void Update()
    {
        lastSave.text = "Último guardado: " + PlayerPrefs.GetString("lastSaveDate", "No hay ultimo guardado.");
    }

    public void guardarPartida()
    {
        listaAlmacen.Clear();
        containerManager[] listaJ = FindObjectsOfType<containerManager>();
        foreach (containerManager cm in listaJ)
        {
            containerManager.AlmacenItem x = cm.getItemAlmacen();
            listaAlmacen.Add(x);
        }

        DateTime fechaYHoraActual = DateTime.Now;
        string stringFormatDate = fechaYHoraActual.ToString();
        PlayerPrefs.SetString("lastSaveDate",stringFormatDate);
        PlayerPrefs.SetInt("firstTimePlaying", 1);
        PlayerPrefs.Save();

        this.GetComponent<filesManager>().SaveOrderListJSON(monitor.GetComponent<clientOrders>().listClientOrders);
        this.GetComponent<filesManager>().SaveAlmacenItemsList(listaAlmacen);

        PlayerPrefs.SetInt("currentMoneyUser1", user.currentMoney);
        PlayerPrefs.SetInt("completedOrdersUser1", user.completedOrders);
        PlayerPrefs.SetInt("rejectedOrdersUser1", user.rejectedOrders);
        PlayerPrefs.SetInt("cancelledOrdersUser1", user.cancelledOrders);
        PlayerPrefs.SetFloat("avgTimePerOrderUser1", user.avgTimePerOrder);
    }
}
