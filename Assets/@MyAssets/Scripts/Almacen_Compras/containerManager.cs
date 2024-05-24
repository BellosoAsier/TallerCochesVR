using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class containerManager : MonoBehaviour
{
    [SerializeField] private List<Transform> listaPosiciones;

    public List<Transform> getContainerPosition(int position)
    {
        if (position >= listaPosiciones.Count)
        {
            return null;
        }

        return new List<Transform> { listaPosiciones[position].GetChild(0), listaPosiciones[position].GetChild(1), listaPosiciones[position].GetChild(2) };
    }
}
