using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private playerDataSO user1;
    [SerializeField] private playerDataSO user2;
    [SerializeField] private playerDataSO user3;
    [SerializeField] public gameManagerSO gm;
    void Start()
    {
        user1.currentMoney = PlayerPrefs.GetInt("currentMoneyUser1", 0);
        user1.completedOrders = PlayerPrefs.GetInt("completedOrdersUser1", 0);
        user1.rejectedOrders = PlayerPrefs.GetInt("rejectedOrdersUser1", 0);
        user1.cancelledOrders = PlayerPrefs.GetInt("cancelledOrdersUser1", 0);
        user1.avgTimePerOrder = PlayerPrefs.GetFloat("avgTimePerOrderUser1", 0);

        user2.currentMoney = PlayerPrefs.GetInt("currentMoneyUser2", 0);
        user2.completedOrders = PlayerPrefs.GetInt("completedOrdersUser2", 0);
        user2.rejectedOrders = PlayerPrefs.GetInt("rejectedOrdersUser2", 0);
        user2.cancelledOrders = PlayerPrefs.GetInt("cancelledOrdersUser2", 0);
        user2.avgTimePerOrder = PlayerPrefs.GetFloat("avgTimePerOrderUser2", 0);

        user3.currentMoney = PlayerPrefs.GetInt("currentMoneyUser3", 0);
        user3.completedOrders = PlayerPrefs.GetInt("completedOrdersUser3", 0);
        user3.rejectedOrders = PlayerPrefs.GetInt("rejectedOrdersUser3", 0);
        user3.cancelledOrders = PlayerPrefs.GetInt("cancelledOrdersUser3", 0);
        user3.avgTimePerOrder = PlayerPrefs.GetFloat("avgTimePerOrderUser3", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("currentMoneyUser1", user1.currentMoney);
        PlayerPrefs.SetInt("completedOrdersUser1", user1.completedOrders);
        PlayerPrefs.SetInt("rejectedOrdersUser1", user1.rejectedOrders);
        PlayerPrefs.SetInt("cancelledOrdersUser1", user1.cancelledOrders);
        PlayerPrefs.SetFloat("avgTimePerOrderUser1", user1.avgTimePerOrder);

        PlayerPrefs.SetInt("currentMoneyUser2", user2.currentMoney);
        PlayerPrefs.SetInt("completedOrdersUser2", user2.completedOrders);
        PlayerPrefs.SetInt("rejectedOrdersUser2", user2.rejectedOrders);
        PlayerPrefs.SetInt("cancelledOrdersUser2", user2.cancelledOrders);
        PlayerPrefs.SetFloat("avgTimePerOrderUser2", user2.avgTimePerOrder);

        PlayerPrefs.SetInt("currentMoneyUser3", user3.currentMoney);
        PlayerPrefs.SetInt("completedOrdersUser3", user3.completedOrders);
        PlayerPrefs.SetInt("rejectedOrdersUser3", user3.rejectedOrders);
        PlayerPrefs.SetInt("cancelledOrdersUser3", user3.cancelledOrders);
        PlayerPrefs.SetFloat("avgTimePerOrderUser3", user3.avgTimePerOrder);
    }
}
