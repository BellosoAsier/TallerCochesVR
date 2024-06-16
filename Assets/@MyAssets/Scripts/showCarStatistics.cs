using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class showCarStatistics : MonoBehaviour
{
    private GameObject car;


    private void Update()
    {
        GameObject[] todosLosObjetos = FindObjectsOfType<GameObject>();

        car = null;
        foreach (GameObject obj in todosLosObjetos)
        {
            // Comprueba si el nombre empieza con "car1" o "car2"
            if (obj.name.Equals("car1") || obj.name.Equals("car2"))
            {
                car = obj;
            }
        }
        changeStatistics(0);
        
    }

    private void changeStatistics(int defaultV)
    {
        for(int i = 0; i < this.transform.childCount; i++)
        {
            if (car != null)
            {
                this.transform.GetChild(i).GetComponent<Slider>().value = car.GetComponent<CarScript>().getCarStatistics()[i];
                this.transform.GetChild(i).GetChild(5).GetComponent<TMP_Text>().text = car.GetComponent<CarScript>().getCarStatistics()[i] + "";
            }
            else
            {
                this.transform.GetChild(i).GetComponent<Slider>().value = 0;
                this.transform.GetChild(i).GetChild(5).GetComponent<TMP_Text>().text = "0";
            }
            
        }
    }
}
