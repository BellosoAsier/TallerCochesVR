using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ooop : MonoBehaviour
{
    public void jji()
    {
        GameObject objetoActual = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        Debug.Log(objetoActual.name);

    }
}
