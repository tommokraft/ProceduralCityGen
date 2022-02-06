using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Road : MonoBehaviour
{
    public GameObject roadObject;
    public LSystemGen lSystem;
    readonly Dictionary<Vector3Int, GameObject> roads = new Dictionary<Vector3Int, GameObject>();

    public List<Vector3Int> getRoadsPos()
    {
        return roads.Keys.ToList();
    }
    
    public void Place(Vector3 startPosition, Vector3Int direction, int length)
    {
        var rotation = Quaternion.identity;
        if (lSystem.Radial)
        {
            rotation = Quaternion.Euler(direction);
            Debug.Log(rotation);
        }
        for (int i = 0; i < length; i++)
        {
            var position = Vector3Int.RoundToInt(startPosition + direction * i);
            if (roads.ContainsKey(position))
            {
                continue;
            }
            var road = Instantiate(roadObject, position, rotation, transform);
            roads.Add(position, road);
        }
        
    }
}
