using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class profileStatisticsScript : MonoBehaviour
{
    [SerializeField] private TMP_Text money;
    [SerializeField] private TMP_Text coOrder;
    [SerializeField] private TMP_Text reOrder;
    [SerializeField] private TMP_Text caOrder;
    [SerializeField] private TMP_Text timeOrder;

    private void Update()
    {
        playerDataSO x = GameObject.Find("GameManager").GetComponent<gameManagerScript>().user;
        money.text ="Dinero actual: "+ x.currentMoney + "$";
        coOrder.text = "Pedidos completados: " + x.completedOrders + "";
        reOrder.text = "Pedidos rechazados: " + x.rejectedOrders + "";
        caOrder.text = "Pedidos cancelados: " + x.cancelledOrders + "";
        timeOrder.text = "Avg de tiempo por Pedido: " + x.avgTimePerOrder + "";
    }
}
