using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TMPCurrentTime : MonoBehaviour
{
    [SerializeField] TMP_Text currentTime;
    // Update is called once per frame
    void Update()
    {
        currentTime.text = DateTime.Now.ToLongTimeString();
    }
}
