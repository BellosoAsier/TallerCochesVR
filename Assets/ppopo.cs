using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ppopo : MonoBehaviour
{
    [SerializeField] private TMP_Text texto;

    public void gg()
    {
        texto.text = this.name;
    }
}
