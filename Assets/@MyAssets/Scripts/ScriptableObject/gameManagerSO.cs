using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameManager", menuName = "ScriptableObjects/CreateGameManager", order = 4)]
public class gameManagerSO : ScriptableObject
{
    [SerializeField] public List<ScriptableObject> carPartStatisticGeneralList;
    [SerializeField] public List<ScriptableObject> colorPartStatisticGeneralList;
    [SerializeField] public List<GameObject> purchasableUniquePartsGeneralList;
    [SerializeField] public List<Sprite> maleAvatarImagesGeneralList;
    [SerializeField] public List<Sprite> femaleAvatarImagesGeneralList;
}
