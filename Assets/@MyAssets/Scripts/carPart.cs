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

    public int ownEcoValue;
    public int ownVelocityValue;
    public int ownManejoValue;
    public int ownWeightValue;
    public int ownQualityValue;
    public int ownBeautyValue;

    private void Awake()
    {
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
                string nameMaterial = material.name.Replace(" (Instance)", "");
                string path = "Assets/@MyAssets/ScriptableObjects/Color Parts/TintsAndLights/" + nameMaterial + ".asset";
                color_ps = UnityEditor.AssetDatabase.LoadAssetAtPath<colorPartStatistic>(path);
                break;
            default:
                for (int i = 0; i < GetComponent<Renderer>().materials.Length; i++)
                {
                    Material material2 = GetComponent<Renderer>().materials[i];
                    string nameMaterial2 = material2.name.Replace(" (Instance)", "");

                    if (i == 0)
                    {
                        string path3 = "Assets/@MyAssets/ScriptableObjects/Color Parts/" + nameMaterial2 + "_Color.asset";
                        color_ps = UnityEditor.AssetDatabase.LoadAssetAtPath<colorPartStatistic>(path3);
                    }
                    if (i == 1)
                    {
                        string path5 = "Assets/@MyAssets/ScriptableObjects/Color Parts/TintsAndLights/" + nameMaterial2 + ".asset";
                        tintOrLight_ps = UnityEditor.AssetDatabase.LoadAssetAtPath<colorPartStatistic>(path5);
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
}
