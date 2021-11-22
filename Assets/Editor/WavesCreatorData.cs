using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesCreatorData : ScriptableObject
{
    public float second;
    public GameObject prefab;

    public List<WaveData> waves = new List<WaveData>();
}
