using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CreatePlayerData", order = 3)]
public class playerDataSO : ScriptableObject
{
    [SerializeField] public string namePlayer;
    [SerializeField] public int currentMoney;
    [SerializeField] public int completedOrders;
    [SerializeField] public int rejectedOrders;
    [SerializeField] public int cancelledOrders;
    [SerializeField] public float avgTimePerOrder;
}
