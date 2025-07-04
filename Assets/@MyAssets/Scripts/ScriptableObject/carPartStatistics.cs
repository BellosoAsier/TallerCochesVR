using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CarPartEnum {Wheel, SteeringWheel, Body, Door, Window, Seat, Paint}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CreateCarPart", order = 1)]
public class carPartStatistics : ScriptableObject
{
    [SerializeField] public CarPartEnum carPart;

    [SerializeField] public int ecoValue;
    [SerializeField] public int velocityValue;
    [SerializeField] public int manejoValue;
    [SerializeField] public int weightValue;
    [SerializeField] public int qualityValue;
    [SerializeField] public int beautyValue;
}
