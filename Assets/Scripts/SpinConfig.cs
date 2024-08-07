using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SpinConfig", menuName = "ScriptableObjects/SpinConfig", order = 1)]
public class SpinConfig : ScriptableObject
{
    public SlotAnimationConfig animationConfig;
    public float delayBetweenRows = 2f;
}

[Serializable]
public class SlotAnimationConfig
{
    public float initialMinVelocity = 5f;
    public float initialMaxVelocity = 9f;
    public float spinMinTime = 4.2f;
    public float spinMaxTime = 7f;
}
