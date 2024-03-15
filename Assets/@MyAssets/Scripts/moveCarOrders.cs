using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCarOrders : MonoBehaviour
{
    [SerializeField] private GameObject repairOrderWindow;
    [SerializeField] private GameObject l;
    [SerializeField] private GameObject r;

    private void Awake()
    {
        if(repairOrderWindow.GetComponent<clientOrders>().pointerClients == 0)
        {
            l.SetActive(false);
        }
        if (repairOrderWindow.GetComponent<clientOrders>().pointerClients == 4)
        {
            r.SetActive(false);
        }
    }

    public void moveRight()
    {
        if (repairOrderWindow.GetComponent<clientOrders>().pointerClients != 4)
        {
            repairOrderWindow.GetComponent<clientOrders>().pointerClients++;
            if (!l.activeSelf)
            {
                l.SetActive(true);
            }
        }
        if (repairOrderWindow.GetComponent<clientOrders>().pointerClients == 4)
        {
            r.SetActive(false);
        }
    }

    public void moveLeft()
    {
        if (repairOrderWindow.GetComponent<clientOrders>().pointerClients != 0)
        {
            repairOrderWindow.GetComponent<clientOrders>().pointerClients--;
            if (!r.activeSelf)
            {
                r.SetActive(true);
            }

        }
        if (repairOrderWindow.GetComponent<clientOrders>().pointerClients == 0)
        {
            l.SetActive(false);
        }
    }
}
