using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CreatePlayerData", order = 3)]
public class playerDataSO : ScriptableObject
{
    [SerializeField] private string namePlayer;
    [SerializeField] private int currentMoney;
    [SerializeField] private int completedOrders;
}
