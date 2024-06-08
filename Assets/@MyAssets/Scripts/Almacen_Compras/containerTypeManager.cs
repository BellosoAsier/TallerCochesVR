using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class containerTypeManager : MonoBehaviour
{
    [SerializeField] private List<Transform> listaPosiciones;


    public List<Transform> getContainerPosition(string word)
    {
        //if (position >= listaPosiciones.Count)
        //{
        //    return null;
        //}
        int contador = 0;
        foreach (Transform t in listaPosiciones)
        {
            if (t.name.Contains(word))
            {
                break;
            }
            contador++;
        }

        //[Contenedor, posicion de robot para dejar cosas]
        return new List<Transform> { listaPosiciones[contador], listaPosiciones[contador].GetChild(2) };
    }
}
