using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "L-System City/Rule")]
public class Rule : ScriptableObject
{
    public string ruleLetter;
    [SerializeField]
    private string[] outputs = null;
    [SerializeField]
    private bool random = false;

    public string GetOutput()
    {
        if (random)
        {
            int randIndex = UnityEngine.Random.Range(0, outputs.Length);
            return outputs[randIndex];
        }
        return outputs[0];
    }
}

    
