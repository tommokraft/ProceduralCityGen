using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public int gridX = 4;
    public int gridZ = 4;
    public GameObject prefabToSpawn;
    public Vector3 gridOrigin = Vector3.zero;
    public float gridOffset = 2f;
    public bool generateOnEnable;
    public int XGridIndex = 0;
    public int ZGridIndex = 0;

    void OnEnable()
    {
        if (generateOnEnable)
        {
            Generate();
        }
    }

    public void Generate()
    {
        SpawnGrid();
    }


    void SpawnGrid()
    {
        for (int x = 0; x < gridX; x++)
        {
            XGridIndex = x;
            int xSize = 15;
            for (int z = 0; z < gridZ; z++)
            {
                ZGridIndex = z; 
                GameObject clone = Instantiate(prefabToSpawn,transform.position + gridOrigin + new Vector3(gridOffset * x, 0, gridOffset * z), Quaternion.identity);
                clone.transform.SetParent(this.transform);
                Vector3 scale = new Vector3(xSize, 0, 15);
                clone.transform.localScale = scale;
            }
        }
    }
}
