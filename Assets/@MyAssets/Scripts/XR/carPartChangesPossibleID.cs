using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PossibleChanges {Color, Tinte}
public class carPartChangesPossibleID : MonoBehaviour
{
    [SerializeField] public List<PossibleChanges> possibleChanges;
}
