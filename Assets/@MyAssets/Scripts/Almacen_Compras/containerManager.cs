using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class containerManager : MonoBehaviour
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


        return new List<Transform> { listaPosiciones[contador].GetChild(0), listaPosiciones[contador].GetChild(1), listaPosiciones[contador].GetChild(2) };
    }
}
