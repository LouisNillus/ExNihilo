using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave_", menuName = "New Wave")]
public class WaveData : ScriptableObject
{
    public int id;
    
    public string waveName;

    //public TimeLine timeLine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class TimeLine
{
    public string name;
    public bool test;
}
