using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carManager : MonoBehaviour
{
    public List<ScriptableObject> carPartStatisticGeneralList;
    public List<ScriptableObject> colorPartStatisticGeneralList;

    public static carManager instance;

    public carManager()
    {
        instance = this;
    }
}
