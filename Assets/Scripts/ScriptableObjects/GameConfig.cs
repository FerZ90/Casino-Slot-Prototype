using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/GameConfig", order = 3)]
public class GameConfig : ScriptableObject
{
    public List<RowValuesConfig> rowIDsOrder;
}

[Serializable]
public class RowValuesConfig
{
    public List<SlotsIDs> rowIDs;
}
