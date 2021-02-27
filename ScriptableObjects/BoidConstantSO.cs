using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoidConstant", menuName = "ScriptableObjects/BoidConstant")]
public class BoidConstantSO : ScriptableObject
{
    public float Min;
    public float Range;
    public bool IsInverted;

    public float GetValue(float attributeValue)
    {
        float modifier = IsInverted ? (1 - attributeValue) : attributeValue;
        return Range * modifier + Min;
    }
}
