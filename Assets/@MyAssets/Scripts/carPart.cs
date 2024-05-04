using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class carPart : MonoBehaviour
{
    [SerializeField] private carPartStatistics car_ps;
    [SerializeField] private colorPartStatistic color_ps;

    [SerializeField] private bool hasExtraPart;
    [SerializeField] private colorPartStatistic tintOrLight_ps;

    [SerializeField] private GameObject carManager;

    public int ownEcoValue;
    public int ownVelocityValue;
    public int ownManejoValue;
    public int ownWeightValue;
    public int ownQualityValue;
    public int ownBeautyValue;

    private void Awake()
    {
        carManager = GameObject.Find("CarManager");
        addColorLightAndTintStatistics();
    }
    
    private void Update()
    {
        addColorLightAndTintStatistics();
        calculateCarPartValues();
    }

    private void addColorLightAndTintStatistics()
    {
        switch (this.name)
        {
            case "Windshield":
            case "SideWindow":
            case "RearWindow":
                Material material = GetComponent<Renderer>().material;
                color_ps = selectColorPartStatistic(material);
                break;
            default:
                for (int i = 0; i < GetComponent<Renderer>().materials.Length; i++)
                {
                    Material material2 = GetComponent<Renderer>().materials[i];

                    if (i == 0)
                    {
                        color_ps = selectColorPartStatistic(material2);
                    }

                    else if (i == 1)
                    {
                        tintOrLight_ps = selectColorPartStatistic(material2);
                    }
                }
                break;
        }
    }
    
    private void calculateCarPartValues()
    {
        if (hasExtraPart)
        {
            ownEcoValue = car_ps.ecoValue + color_ps.extraEcoValue + tintOrLight_ps.extraEcoValue;
            ownVelocityValue = car_ps.velocityValue + color_ps.extraVelocityValue + tintOrLight_ps.extraVelocityValue;
            ownManejoValue = car_ps.manejoValue + color_ps.extraManejoValue + tintOrLight_ps.extraManejoValue;
            ownWeightValue = car_ps.weightValue + color_ps.extraWeightValue + tintOrLight_ps.extraWeightValue;
            ownQualityValue = car_ps.qualityValue + color_ps.extraQualityValue + tintOrLight_ps.extraQualityValue;
            ownBeautyValue = car_ps.beautyValue + color_ps.extraBeautyValue + tintOrLight_ps.extraBeautyValue;
        }
        else
        {
            ownEcoValue = car_ps.ecoValue + color_ps.extraEcoValue;
            ownVelocityValue = car_ps.velocityValue + color_ps.extraVelocityValue;
            ownManejoValue = car_ps.manejoValue + color_ps.extraManejoValue;
            ownWeightValue = car_ps.weightValue + color_ps.extraWeightValue;
            ownQualityValue = car_ps.qualityValue + color_ps.extraQualityValue;
            ownBeautyValue = car_ps.beautyValue + color_ps.extraBeautyValue;
        }
    }

    private colorPartStatistic selectColorPartStatistic(Material material)
    {
        colorPartStatistic cps = ScriptableObject.CreateInstance<colorPartStatistic>();
        string nameMaterial = material.name.Replace(" (Instance)", "");

        foreach (ScriptableObject so in carManager.GetComponent<carManager>().colorPartStatisticGeneralList)
        {
            if (so.name.Contains(nameMaterial))
            {
                cps = (colorPartStatistic) so;
                break;
            }
        }
        return cps;
    }
}
