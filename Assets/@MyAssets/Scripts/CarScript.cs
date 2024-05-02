using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    public int CarEcoValue;
    public int CarVelocityValue;
    public int CarManejoValue;
    public int CarWeightValue;
    public int CarQualityValue;
    public int CarBeautyValue;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in transform)
        {
            carPart carStat = child.GetComponentInChildren<carPart>();

            if (carStat != null)
            {
                CarEcoValue += carStat.ownEcoValue;
                CarVelocityValue += carStat.ownVelocityValue;
                CarManejoValue += carStat.ownManejoValue;
                CarWeightValue += carStat.ownWeightValue;
                CarQualityValue += carStat.ownQualityValue;
                CarBeautyValue += carStat.ownBeautyValue;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CarEcoValue = 0; CarVelocityValue = 0; CarManejoValue = 0; CarWeightValue = 0; CarQualityValue = 0; CarBeautyValue = 0;
        foreach (Transform child in transform)
        {
            carPart carStat = child.GetComponentInChildren<carPart>();

            if (carStat != null)
            {
                CarEcoValue += carStat.ownEcoValue;
                CarVelocityValue += carStat.ownVelocityValue;
                CarManejoValue += carStat.ownManejoValue;
                CarWeightValue += carStat.ownWeightValue;
                CarQualityValue += carStat.ownQualityValue;
                CarBeautyValue += carStat.ownBeautyValue;
            }
        }
    }
}
