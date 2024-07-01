using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class returnPage : MonoBehaviour
{
    [SerializeField] private GameObject go;
    public void yesButton()
    {
        this.gameObject.SetActive(false);
        go.SetActive(true);
    }
    public void noButton()
    {
        this.gameObject.SetActive(false);
        go.SetActive(true);
    }
}
