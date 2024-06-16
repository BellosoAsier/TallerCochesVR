using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public playerDataSO user;
    [SerializeField] public gameManagerSO gm;
    void Start()
    {
        user.currentMoney = PlayerPrefs.GetInt("currentMoneyUser1", 0);
        user.completedOrders = PlayerPrefs.GetInt("completedOrdersUser1", 0);
        user.rejectedOrders = PlayerPrefs.GetInt("rejectedOrdersUser1", 0);
        user.cancelledOrders = PlayerPrefs.GetInt("cancelledOrdersUser1", 0);
        user.avgTimePerOrder = PlayerPrefs.GetFloat("avgTimePerOrderUser1", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnApplicationQuit()
    //{
    //    PlayerPrefs.SetInt("currentMoneyUser1", user.currentMoney);
    //    PlayerPrefs.SetInt("completedOrdersUser1", user.completedOrders);
    //    PlayerPrefs.SetInt("rejectedOrdersUser1", user.rejectedOrders);
    //    PlayerPrefs.SetInt("cancelledOrdersUser1", user.cancelledOrders);
    //    PlayerPrefs.SetFloat("avgTimePerOrderUser1", user.avgTimePerOrder);
    //}
}
