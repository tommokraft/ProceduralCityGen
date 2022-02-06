
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class SizeRand : MonoBehaviour
{
    Renderer rend;
    Vector3 center;
    Vector3 size;
    public GameObject Building;
    public GameObject Quad;
    public float cellX;
    public float cellZ;
    public int[,] grid = new int[5, 5];
    public string[] districts = { "urban", "resedential", "industrial" };
    public int XGridIndex = 0;
    public int ZGridIndex = 0;
    public int noOfCells = 16;
    bool firstRing = false;
    bool secondRing = false;
    bool thirdRing = false;
    public Material UrbanMaterial1;
    public Material UrbanRoof1;
    public Material UrbanMaterial2;
    public Material UrbanRoof2;
    public Material UrbanMaterial3;
    public Material UrbanRoof3;
    public Material IndRoof;
    public Material ResRoof;
    public Material ResMaterial;
    public Material IndMaterial;
    private const float repeatEvery = 0.2f;


    
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        PlaceCube();
    }
    public void PlaceCube()
    {
        XGridIndex = Mathf.CeilToInt(rend.bounds.center.x / 20);
        ZGridIndex = Mathf.CeilToInt(rend.bounds.center.z / 20);

        int centreRingMin = (noOfCells - 1) / 3;
        int centreRingMax = (noOfCells-1)- ((noOfCells - 1) / 3);
        int outerRingMin = 0;
        int outerRingMax = (noOfCells - 1);
        int centreRingOffset = 0;
        if (noOfCells == 10)
        {
            centreRingOffset = 1;
        }
        else if (noOfCells == 16)
        {
            centreRingOffset = 2;
        }
        int district;
        while (1 == 1)
        {

            if (XGridIndex <= centreRingMax && XGridIndex >= centreRingMin && ZGridIndex <= centreRingMax && ZGridIndex >= centreRingMin)
            {
                district = 0;
              
                if((XGridIndex == centreRingMin + centreRingOffset  || XGridIndex ==centreRingMax - centreRingOffset ) && (ZGridIndex == centreRingMin +centreRingOffset  || ZGridIndex == centreRingMax -centreRingOffset))
                { 
                    firstRing = true;
                }
                else if (noOfCells > 10)
                {
                    if ((XGridIndex == centreRingMin + 1 || XGridIndex == centreRingMax - 1) && (ZGridIndex == centreRingMin + 1 || ZGridIndex == centreRingMax - 1))
                    {
                        secondRing = true;
                    }
                    else
                    {
                        thirdRing = true;
                    }
                }
                else
                {
                    secondRing = true;
                }
                break;
            }
            else if (XGridIndex == outerRingMin || XGridIndex == outerRingMax || ZGridIndex == outerRingMin || ZGridIndex == outerRingMax)
            {
                district = 1;
                break;
            }
            else if (XGridIndex == outerRingMin + 1  && (ZGridIndex != outerRingMin || ZGridIndex != outerRingMin))
            {
                int num = UnityEngine.Random.Range(0, 100);
                if (num <= 50)
                {
                    district = 1;
                }
                else
                {
                    district = 2;
                }
                break;
            }
            else if (XGridIndex == outerRingMax-1 && (ZGridIndex != outerRingMin || ZGridIndex != outerRingMax))
            {
                int num = UnityEngine.Random.Range(0, 100);
                if (num <= 50)
                {
                    district = 1;
                }
                else
                {
                    district = 2;
                }
                break;
            }
            else if (ZGridIndex == outerRingMin +1  && (XGridIndex != outerRingMin || XGridIndex != outerRingMax))
            {
                int num = UnityEngine.Random.Range(0, 100);
                if (num <= 50)
                {
                    district = 1;
                }
                else
                {
                    district = 2;
                }
                break;
            }
            else if (ZGridIndex == outerRingMax-1 && (XGridIndex != outerRingMin || XGridIndex != outerRingMax))
            {
                int num = UnityEngine.Random.Range(0, 100);
                if (num <= 50)
                {
                    district = 1;
                }
                else
                {
                    district = 2;
                }
                break;
            }
            else
            {
                int num = UnityEngine.Random.Range(0, 100);
                if (num <= 50)
                {
                    district = 0;
                }
                else
                {
                    district = 2;
                }
                break;
            }
        }
        if (districts[district] == "urban")
        {
            SpawnUrb();
        }
        else if(districts[district] == "resedential")
        {
            SpawnRes();
        }
        else if (districts[district]== "industrial")
        {
            SpawnInd();
        }
    }
    public void SpawnUrb()
    {
        
        Vector3 pos = new Vector3(0.0f, 0.0f, 0.0f);
        center = rend.bounds.center;
        size = rend.bounds.size;
        cellX = (size.x) / 5;
        cellZ = (size.z) / 5;
        int minY = 15;
        int maxY = 25;
        if (firstRing)
        {
            minY = 40;
            maxY = 60;
        }
        else if (secondRing)
        {
            minY = 30;
            maxY = 50;
        }
        else if (thirdRing)
        {
            minY = 20;
            maxY = 40;
        }
        int minX = 1;
        int maxX = 3;
        int maxZ = 3;
        int minZ = 1;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                grid[i, j] = 0;
            }
        }
        while (grid[4, 4] == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (i == 0 || i == 4 || j == 0 || j == 4)
                    {
                        if (grid[i, j] == 0)
                        {
                            for (int checkX = 1; checkX + i < 5; checkX++)
                            {

                                if (i == 4)
                                {
                                    maxX = 1;
                                    break;
                                }
                                else if (grid[checkX + i, j] == 1)
                                {
                                    maxX = checkX;
                                    break;
                                }
                                else if (5 - i < 3)
                                {
                                    maxX = 5 - (i + 1);

                                    break;
                                }
                                else
                                {
                                    maxX = 3;
                                }
                            }
                            for (int checkZ = 1; checkZ + j < 5; checkZ++)
                            {
                                if (j == 4)
                                {
                                    maxZ = 1;
                                    break;
                                }
                                else if (grid[i, checkZ + j] == 1)
                                {
                                    maxZ = checkZ;
                                    break;
                                }
                                else if (5 - j < 3)
                                {
                                    maxZ = 5 - (j + 1);
                                    break;
                                }
                                else
                                {
                                    maxZ = 3;
                                }
                            }
                            if (j == 4) { maxZ = 1; }
                            if (i == 4) { maxX = 1; }
                            int randY = UnityEngine.Random.Range(minY, maxY);
                            int randX = UnityEngine.Random.Range(minX, maxX);
                            int randZ = UnityEngine.Random.Range(minZ, maxZ);

                            float y = randY / 2;
                            for (int xCount = 0; xCount < randX; xCount++)
                            {
                                for (int zCount = 0; zCount < randZ; zCount++)
                                {
                                    grid[i + xCount, j + zCount] = 1;
                                }
                            }
                            Vector3 scale = new Vector3(cellX * randX, randY, cellZ * randZ);
                            pos = center + new Vector3(-size.x / 2 + (i * cellX) + ((cellX * randX)) / 2, y, -size.z / 2 + (j * cellZ + ((cellZ * randZ)) / 2));
                            GameObject cube = Instantiate(Building);
                            GameObject quad = Instantiate(Quad);

                            cube.transform.position = pos;
                            cube.transform.localScale = scale;
                            quad.transform.position = new Vector3(pos.x, pos.y + (scale.y / 2) + 0.001f, pos.z);
                            quad.transform.localScale = new Vector3(scale.x, scale.z, scale.y);
                            int materialPicker = UnityEngine.Random.Range(0, 3);
                            if (materialPicker == 1)
                            {
                                quad.GetComponent<Renderer>().material = UrbanRoof1;
                            }
                            else if (materialPicker == 2)
                            {
                                quad.GetComponent<Renderer>().material = UrbanRoof2;
                            }
                            else
                            {
                                quad.GetComponent<Renderer>().material = UrbanRoof3;
                            }
                            MeshFilter mf = cube.GetComponent<MeshFilter>();
                            if (mf != null)
                            {
                                var bounds = mf.mesh.bounds;
                                var size = Vector3.Scale(bounds.size, cube.transform.localScale) * repeatEvery;
                                if (materialPicker == 1)
                                {
                                    cube.GetComponent<Renderer>().material = UrbanMaterial1;
                                }
                                else if (materialPicker == 2)
                                {
                                    cube.GetComponent<Renderer>().material = UrbanMaterial2;
                                }
                                else
                                {
                                    cube.GetComponent<Renderer>().material = UrbanMaterial3;
                                }


                                cube.GetComponent<Renderer>().material.mainTextureScale = size;

                            }
                        }
                        //cube.GetComponent<Renderer>().material.mainTextureScale = new Vector2(cube.transform.localScale.y / repeatEvery, 1f);

                    }
                }
                
            }
        }
    }
    public void SpawnRes()
    {
        Vector3 pos = new Vector3(0.0f, 0.0f, 0.0f);
        center = rend.bounds.center;
        size = rend.bounds.size;
        cellX = (size.x) / 5;
        cellZ = (size.z) / 5;
        float minY = 1;
        float maxY = 5;
        float minX = 1;
        float maxX = 2;
        float maxZ = 1;
        float minZ = 2;
        for (int i = 0; i <= 4; i++)
        {
            for (int j = 0; j <= 4; j++)
            {
                if (i == 0 || i == 4 || j == 0 || j == 4)
                {
                    float y;
                    float randY = UnityEngine.Random.Range(minY, maxY);
                    float randX = UnityEngine.Random.Range(minX, maxX);
                    float randZ = UnityEngine.Random.Range(minZ, maxZ);
                    y = randY / 2;
                    Vector3 scale = new Vector3(randX, randY, randZ);
                    pos = center + new Vector3(UnityEngine.Random.Range((-size.x / 2 + (i * cellX)) + randX, (-size.x / 2 + ((i + 1) * cellX)) - randX), y, UnityEngine.Random.Range((-size.z / 2 + (j * cellZ)) + randZ, (-size.z / 2 + ((j + 1) * cellZ)) - randZ));
                    GameObject cube = Instantiate(Building);
                    cube.transform.position = pos;
                    cube.transform.localScale = scale;
                    MeshFilter mf = cube.GetComponent<MeshFilter>();
                    if (mf != null)
                    {
                        var bounds = mf.mesh.bounds;
                        var size = Vector3.Scale(bounds.size, cube.transform.localScale) * repeatEvery;
                        cube.GetComponent<Renderer>().material = ResMaterial;
                        cube.GetComponent<Renderer>().material.mainTextureScale = size;
                    }
                }
            }
        }
    }
    public void SpawnInd()
    {
        Vector3 pos = new Vector3(0.0f, 0.0f, 0.0f);
        center = rend.bounds.center;
        size = rend.bounds.size;
        cellX = (size.x) / 5;
        cellZ = (size.z) / 5;
        float minY = 5;
        float maxY = 15;
        float minX = 1;
        float maxX = 4;
        float maxZ = 4;
        float minZ = 1;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                grid[i, j] = 0;
            }
        }
        while (grid[4, 4] == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (i == 0 || i == 4 || j == 0 || j == 4)
                    {
                        if (grid[i, j] == 0)
                        {
                            for (int checkX = 1; checkX + i < 5; checkX++)
                            {

                                if (i == 4)
                                {
                                    maxX = 1;
                                    break;
                                }
                                else if (grid[checkX + i, j] == 1)
                                {
                                    maxX = checkX;
                                    break;
                                }
                                else if (5 - i < 4)
                                {
                                    maxX = 5 - (i + 1);
                                    break;
                                }
                                else
                                {
                                    maxX = 4;
                                }
                            }
                            for (int checkZ = 1; checkZ + j < 5; checkZ++)
                            {
                                if (j == 4)
                                {
                                    maxZ = 1;
                                    break;
                                }
                                else if (grid[i, checkZ + j] == 1)
                                {
                                    maxZ = checkZ;
                                    break;
                                }
                                else if (5 - j < 4)
                                {

                                    maxZ = 5 - (j + 1);
                                    break;
                                }
                                else
                                {
                                    maxZ = 4;
                                }
                            }
                            if (j == 4) { maxZ = 1; }
                            if (i == 4) { maxX = 1; }
                            float randY = UnityEngine.Random.Range(minY, maxY);
                            float randX = UnityEngine.Random.Range(minX, maxX);
                            float randZ = UnityEngine.Random.Range(minZ, maxZ);

                            float y = randY / 2;
                            Debug.Log(i);
                            Debug.Log(j);
                            Debug.Log(randX);
                            Debug.Log(randZ);
                            for (int xCount = 0; xCount < randX; xCount++)
                            {
                                for (int zCount = 0; zCount < randZ; zCount++)
                                {
                                    grid[i + xCount, j + zCount] = 1;
                                }
                            }
                            Vector3 scale = new Vector3(cellX * randX, randY, cellZ * randZ);
                            pos = center + new Vector3(Mathf.Ceil(-size.x / 2 + (i * cellX) + ((cellX * randX) / 2)), y, Mathf.Ceil(-size.z / 2 + (j * cellZ) + ((cellZ * randZ) / 2)));
                            GameObject cube = Instantiate(Building);
                            cube.transform.position = pos;
                            cube.transform.localScale = scale;
                            MeshFilter mf = cube.GetComponent<MeshFilter>();
                            if (mf != null)
                            {
                                var bounds = mf.mesh.bounds;
                                var size = Vector3.Scale(bounds.size, cube.transform.localScale) * repeatEvery;
                                cube.GetComponent<Renderer>().material = IndMaterial;
                                cube.GetComponent<Renderer>().material.mainTextureScale = size;
                            }
                        }
                    }
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
