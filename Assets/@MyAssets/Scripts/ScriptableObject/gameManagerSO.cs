using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameManager", menuName = "ScriptableObjects/CreateGameManager", order = 4)]
public class gameManagerSO : ScriptableObject
{
    [System.Serializable]
    public class UniqueObject
    {
        public string name;
        public GameObject item;
        public int value;
        public Sprite sprite;
        public UniqueObject(string name, GameObject item, int value, Sprite sprite)
        {
            this.name = name;
            this.item = item;
            this.value = value;
            this.sprite = sprite;
        }
    }

    [SerializeField] public List<ScriptableObject> carPartStatisticGeneralList;
    [SerializeField] public List<ScriptableObject> colorPartStatisticGeneralList;
    //[SerializeField] public List<GameObject> purchasableUniquePartsGeneralList;
    [SerializeField] public List<Sprite> maleAvatarImagesGeneralList;
    [SerializeField] public List<Sprite> femaleAvatarImagesGeneralList;
    [SerializeField] public List<GameObject> carModelsGeneralList;
    [SerializeField] public List<Material> colorMaterialGeneralList;
    [SerializeField] public List<Material> tintsMaterialGeneralList;
    [SerializeField] public List<Material> lightsMaterialGeneralList;
    //[SerializeField] public List<Sprite> purchaseableObjectImageGeneralList;

    [SerializeField] public List<UniqueObject> purchasableUniquePartsObjectGeneralList;
}
