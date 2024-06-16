using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class catalogVisualizeScript : MonoBehaviour
{
    private gameManagerSO.UniqueObject actualCatalogItem;
    private gameManagerSO gameDataSO;
    [SerializeField] private GameObject panel;

    [SerializeField] private TMP_Text texto;
    [SerializeField] private Button botonIzq;
    [SerializeField] private Button botonDer;
    [SerializeField] private Image imagen;

    [SerializeField] private int maxValue;

    private int pointer = 0;
    private List<gameManagerSO.UniqueObject> lista = new List<gameManagerSO.UniqueObject>();

    private void Awake()
    {
        lista.Clear();
        gameDataSO = GameObject.Find("GameManager").GetComponent<gameManagerScript>().gm;
        lista = gameDataSO.purchasableUniquePartsObjectGeneralList;
        //foreach (gameManagerSO.UniqueObject uo in gameDataSO.purchasableUniquePartsObjectGeneralList)
        //{
        //    if (!(uo.item.name.StartsWith("Paint")))
        //    {
        //        lista.Add(uo);
        //    }
        //}
        //lista = gameDataSO.purchasableUniquePartsObjectGeneralList;
        actualCatalogItem = lista[pointer];
    }

    private void Update()
    {
        actualCatalogItem = lista[pointer];
        if (pointer == 0) 
        {
            botonIzq.gameObject.SetActive(false);
        }
        else
        {
            botonIzq.gameObject.SetActive(true);
        }

        if (pointer == lista.Count - 1)
        {
            botonDer.gameObject.SetActive(false);
        }
        else
        {
            botonDer.gameObject.SetActive(true);
        }

        if (actualCatalogItem.name.StartsWith("Pintura"))
        {
            colorPartStatistic x = null;
            foreach(colorPartStatistic cps in gameDataSO.colorPartStatisticGeneralList)
            {
                string color = actualCatalogItem.item.name.Replace("Paint_Color","");
                if (cps.name.StartsWith(color))
                {
                    x = cps;
                    
                }
            }
            updateColorStatisticsOfPart(panel, x);
        }
        else
        {
            updateStatisticsOfPart(panel);
        }
        imagen.sprite = actualCatalogItem.sprite;
        texto.text = actualCatalogItem.name;
    }

    private void updateStatisticsOfPart(GameObject panel)
    {
        //panel.GetComponent<Slider>().maxValue = maxValue;
        panel.transform.GetChild(0).GetComponent<Slider>().value = actualCatalogItem.item.GetComponent<carPart>().GetCarPartStatistics().ecoValue;
        if (actualCatalogItem.item.GetComponent<carPart>().GetCarPartStatistics().ecoValue < 0)
        {
            panel.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Image>().color = Color.red;
        }
        else
        {
            panel.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Image>().color = Color.white;
        }
        panel.transform.GetChild(0).GetChild(5).GetComponent<TMP_Text>().text = actualCatalogItem.item.GetComponent<carPart>().GetCarPartStatistics().ecoValue + "";

        panel.transform.GetChild(1).GetComponent<Slider>().value = actualCatalogItem.item.GetComponent<carPart>().GetCarPartStatistics().velocityValue;
        if (actualCatalogItem.item.GetComponent<carPart>().GetCarPartStatistics().velocityValue < 0)
        {
            panel.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<Image>().color = Color.red;
        }
        else
        {
            panel.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<Image>().color = Color.white;
        }
        panel.transform.GetChild(1).GetChild(5).GetComponent<TMP_Text>().text = actualCatalogItem.item.GetComponent<carPart>().GetCarPartStatistics().velocityValue + "";

        panel.transform.GetChild(2).GetComponent<Slider>().value = actualCatalogItem.item.GetComponent<carPart>().GetCarPartStatistics().manejoValue;
        if (actualCatalogItem.item.GetComponent<carPart>().GetCarPartStatistics().manejoValue < 0)
        {
            panel.transform.GetChild(2).GetChild(2).GetChild(0).GetComponent<Image>().color = Color.red;
        }
        else
        {
            panel.transform.GetChild(2).GetChild(2).GetChild(0).GetComponent<Image>().color = Color.white;
        }
        panel.transform.GetChild(2).GetChild(5).GetComponent<TMP_Text>().text = actualCatalogItem.item.GetComponent<carPart>().GetCarPartStatistics().manejoValue + "";

        panel.transform.GetChild(3).GetComponent<Slider>().value = actualCatalogItem.item.GetComponent<carPart>().GetCarPartStatistics().weightValue;
        if (actualCatalogItem.item.GetComponent<carPart>().GetCarPartStatistics().weightValue < 0)
        {
            panel.transform.GetChild(3).GetChild(2).GetChild(0).GetComponent<Image>().color = Color.red;
        }
        else
        {
            panel.transform.GetChild(3).GetChild(2).GetChild(0).GetComponent<Image>().color = Color.white;
        }
        panel.transform.GetChild(3).GetChild(5).GetComponent<TMP_Text>().text = actualCatalogItem.item.GetComponent<carPart>().GetCarPartStatistics().weightValue + "";

        panel.transform.GetChild(4).GetComponent<Slider>().value = actualCatalogItem.item.GetComponent<carPart>().GetCarPartStatistics().qualityValue;
        if (actualCatalogItem.item.GetComponent<carPart>().GetCarPartStatistics().qualityValue < 0)
        {
            panel.transform.GetChild(4).GetChild(2).GetChild(0).GetComponent<Image>().color = Color.red;
        }
        else
        {
            panel.transform.GetChild(4).GetChild(2).GetChild(0).GetComponent<Image>().color = Color.white;
        }
        panel.transform.GetChild(4).GetChild(5).GetComponent<TMP_Text>().text = actualCatalogItem.item.GetComponent<carPart>().GetCarPartStatistics().qualityValue + "";

        panel.transform.GetChild(5).GetComponent<Slider>().value = actualCatalogItem.item.GetComponent<carPart>().GetCarPartStatistics().beautyValue;
        if (actualCatalogItem.item.GetComponent<carPart>().GetCarPartStatistics().beautyValue < 0)
        {
            panel.transform.GetChild(5).GetChild(2).GetChild(0).GetComponent<Image>().color = Color.red;
        }
        else
        {
            panel.transform.GetChild(5).GetChild(2).GetChild(0).GetComponent<Image>().color = Color.white;
        }
        panel.transform.GetChild(5).GetChild(5).GetComponent<TMP_Text>().text = actualCatalogItem.item.GetComponent<carPart>().GetCarPartStatistics().beautyValue + "";

    }

    private void updateColorStatisticsOfPart(GameObject panel, colorPartStatistic cps)
    {
        //panel.GetComponent<Slider>().maxValue = maxValue;
        panel.transform.GetChild(0).GetComponent<Slider>().value = cps.extraEcoValue;
        if (cps.extraEcoValue < 0)
        {
            panel.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Image>().color = Color.red;
        }
        else
        {
            panel.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Image>().color = Color.white;
        }
        panel.transform.GetChild(0).GetChild(5).GetComponent<TMP_Text>().text = cps.extraEcoValue + "";

        panel.transform.GetChild(1).GetComponent<Slider>().value = cps.extraVelocityValue;
        if (cps.extraVelocityValue < 0)
        {
            panel.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<Image>().color = Color.red;
        }
        else
        {
            panel.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<Image>().color = Color.white;
        }
        panel.transform.GetChild(1).GetChild(5).GetComponent<TMP_Text>().text = cps.extraVelocityValue + "";

        panel.transform.GetChild(2).GetComponent<Slider>().value = cps.extraManejoValue;
        if (cps.extraManejoValue < 0)
        {
            panel.transform.GetChild(2).GetChild(2).GetChild(0).GetComponent<Image>().color = Color.red;
        }
        else
        {
            panel.transform.GetChild(2).GetChild(2).GetChild(0).GetComponent<Image>().color = Color.white;
        }
        panel.transform.GetChild(2).GetChild(5).GetComponent<TMP_Text>().text = cps.extraManejoValue + "";

        panel.transform.GetChild(3).GetComponent<Slider>().value = cps.extraWeightValue;
        if (cps.extraWeightValue < 0)
        {
            panel.transform.GetChild(3).GetChild(2).GetChild(0).GetComponent<Image>().color = Color.red;
        }
        else
        {
            panel.transform.GetChild(3).GetChild(2).GetChild(0).GetComponent<Image>().color = Color.white;
        }
        panel.transform.GetChild(3).GetChild(5).GetComponent<TMP_Text>().text = cps.extraWeightValue + "";

        panel.transform.GetChild(4).GetComponent<Slider>().value = cps.extraQualityValue;
        if (cps.extraQualityValue < 0)
        {
            panel.transform.GetChild(4).GetChild(2).GetChild(0).GetComponent<Image>().color = Color.red;
        }
        else
        {
            panel.transform.GetChild(4).GetChild(2).GetChild(0).GetComponent<Image>().color = Color.white;
        }
        panel.transform.GetChild(4).GetChild(5).GetComponent<TMP_Text>().text = cps.extraQualityValue + "";

        panel.transform.GetChild(5).GetComponent<Slider>().value = cps.extraBeautyValue;
        if (cps.extraBeautyValue < 0)
        {
            panel.transform.GetChild(5).GetChild(2).GetChild(0).GetComponent<Image>().color = Color.red;
        }
        else
        {
            panel.transform.GetChild(5).GetChild(2).GetChild(0).GetComponent<Image>().color = Color.white;
        }
        panel.transform.GetChild(5).GetChild(5).GetComponent<TMP_Text>().text = cps.extraBeautyValue + "";

    }

    public void goRight()
    {
        pointer++;
    }

    public void goLeft()
    {
        pointer--;
    }
}
