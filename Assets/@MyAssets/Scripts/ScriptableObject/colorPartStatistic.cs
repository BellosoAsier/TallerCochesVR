using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Color", menuName = "ScriptableObjects/CreateColorPart", order = 2)]
public class colorPartStatistic : ScriptableObject
{
    [SerializeField] public int extraEcoValue;
    [SerializeField] public int extraVelocityValue;
    [SerializeField] public int extraManejoValue;
    [SerializeField] public int extraWeightValue;
    [SerializeField] public int extraQualityValue;
    [SerializeField] public int extraBeautyValue;
}
