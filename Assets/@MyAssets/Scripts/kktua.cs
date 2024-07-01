using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kktua : MonoBehaviour
{
    [SerializeField] private GameObject go1;
    [SerializeField] private GameObject go2;

    public void changejej()
    {
        go1.SetActive(!go1.activeSelf);
        go2.SetActive(!go2.activeSelf);
    }
}
