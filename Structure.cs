using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    //public GameObject building;
    public BuildingType[] buildingTypes;
    public Dictionary<Vector3Int, GameObject> structures = new Dictionary<Vector3Int, GameObject>();
    public Material largeMat;
    public Material largeRoof;
    public Material largeMat1;
    public Material largeRoof1;
    public Material smallMat;
    public Material smallMat1;
    public GameObject Quad;
    private const float repeatEvery = 1.0f;

    public void Place(List<Vector3Int> roadPos)
    {
        Dictionary<Vector3Int, Direction> freePlaces = findFreePlaces(roadPos);
        List<Vector3Int> blockedPos = new List<Vector3Int>();
        Vector3 randomNewPoint = new Vector3(UnityEngine.Random.Range(-30, 30), 0, UnityEngine.Random.Range(-30, 30));
        foreach (var position in freePlaces)
        {
            var rotation = Quaternion.identity;
            var quadRotation = Quaternion.Euler(90, 0, 0);
            switch (position.Value)
            {
                case Direction.Up:
                    rotation = Quaternion.Euler(0,90,0);
                    quadRotation = Quaternion.Euler(90, 90, 0);
                    break;
                case Direction.Down:
                    rotation = Quaternion.Euler(0, -90, 0);
                    quadRotation = Quaternion.Euler(90, -90, 0);
                    break;
                case Direction.Right:
                    rotation = Quaternion.Euler(0, 180, 0);
                    quadRotation = Quaternion.Euler(90, 180, 0);
                    break;
                default:
                    break;
            }
            if (blockedPos.Contains(position.Key))
            {
                continue;
            }
            
            float distanceFromOrigin = (float) Math.Sqrt((position.Key.x * position.Key.x) + (position.Key.z * position.Key.z));
            float distanceFromNewPoint = (float)Math.Sqrt(((position.Key.x - randomNewPoint.x) * (position.Key.x - randomNewPoint.x)) + ((position.Key.z - randomNewPoint.z) * (position.Key.z - randomNewPoint.z)));
            float maxHeight = 0;
            if (distanceFromOrigin < distanceFromNewPoint)
            {
                maxHeight = 20 - (distanceFromOrigin);
            }
            else if (distanceFromNewPoint < distanceFromOrigin)
            {
                maxHeight = 20 - (distanceFromNewPoint);
            }
            float minHeight = maxHeight - 2;
            if (maxHeight < 2)
            {
                maxHeight = 2;
                minHeight = 1;
            }
            for (int i = 0; i < buildingTypes.Length; i++)
            {
                if (buildingTypes[i].quantity == -1)
                {
                    var building = SpawnPrefab(buildingTypes[i].GetPrefab(), position.Key, rotation);
                    structures.Add(position.Key, building);
                    Vector3 scale = new Vector3(0.75f, UnityEngine.Random.Range(minHeight, maxHeight), 0.75f);
                    building.transform.localScale = scale;
                    building.transform.position = new Vector3(building.transform.position.x,building.transform.position.y + scale.y/2, building.transform.position.z);
                    MeshFilter mf = building.GetComponent<MeshFilter>();
                    if (mf != null)
                    {
                        var bounds = mf.mesh.bounds;
                        var size = Vector3.Scale(bounds.size, building.transform.localScale) * repeatEvery;
                        if (scale.y > 2.5)
                        {
                            int materialPicker = UnityEngine.Random.Range(0, 2);
                            GameObject quad = Instantiate(Quad, transform);
                            if (materialPicker == 1)
                            {
                                building.GetComponent<Renderer>().material = largeMat;
                                quad.GetComponent<Renderer>().material = largeRoof;
                            }
                            else
                            {
                                building.GetComponent<Renderer>().material = largeMat1;
                                quad.GetComponent<Renderer>().material = largeRoof1;
                            }
                            quad.transform.position = new Vector3(building.transform.position.x, building.transform.position.y + (scale.y / 2) + 0.001f, building.transform.position.z);
                            quad.transform.localScale = new Vector3(scale.x, scale.z, scale.y);
                            quad.transform.rotation = quadRotation;
                        }
                        else
                        {
                            int materialPicker = UnityEngine.Random.Range(0, 2);
                            if (materialPicker == 1)
                            {
                                building.GetComponent<Renderer>().material = smallMat;
                            }
                            else
                            {
                                building.GetComponent<Renderer>().material = smallMat1;
                            }
                            
                        }
                        building.GetComponent<Renderer>().material.mainTextureScale = size;
                    }
                    
                    break;
                }
                if (buildingTypes[i].buildingAvailable())
                {
                    if (buildingTypes[i].sizeNeeded > 1)
                    {
                        var halfSize = Mathf.FloorToInt(buildingTypes[i].sizeNeeded / 2.0f);
                        List<Vector3Int> tempPosBlocked = new List<Vector3Int>();
                        if(DoesBuildingFit(halfSize,freePlaces,position,blockedPos, ref tempPosBlocked))
                        {
                            blockedPos.AddRange(tempPosBlocked);
                            var building = SpawnPrefab(buildingTypes[i].GetPrefab(), position.Key, rotation);
                            structures.Add(position.Key, building);
                            Vector3 scale = new Vector3(building.transform.localScale.x, UnityEngine.Random.Range(minHeight, maxHeight), building.transform.localScale.z);
                            building.transform.localScale = scale;
                            
                            building.transform.position = new Vector3(building.transform.position.x, building.transform.position.y + scale.y / 2, building.transform.position.z);
                            MeshFilter mf = building.GetComponent<MeshFilter>();
                            if (mf != null)
                            {
                                var bounds = mf.mesh.bounds;
                                var size = Vector3.Scale(bounds.size, building.transform.localScale) * repeatEvery;
                                if (scale.y > 5)
                                {
                                    int materialPicker = UnityEngine.Random.Range(0, 2);
                                    GameObject quad = Instantiate(Quad, transform);
                                    if (materialPicker == 1)
                                    {
                                        building.GetComponent<Renderer>().material = largeMat;
                                        quad.GetComponent<Renderer>().material = largeRoof;
                                    }
                                    else
                                    {
                                        building.GetComponent<Renderer>().material = largeMat1;
                                        quad.GetComponent<Renderer>().material = largeRoof1;
                                    }
                                    quad.transform.position = new Vector3(building.transform.position.x, building.transform.position.y + (scale.y / 2) + 0.001f, building.transform.position.z);
                                    quad.transform.localScale = new Vector3(scale.x, scale.z, scale.y);
                                    quad.transform.rotation = quadRotation;
                                }
                                else
                                {
                                    building.GetComponent<Renderer>().material = smallMat;
                                }
                                building.GetComponent<Renderer>().material.mainTextureScale = size;
                            }
                            /*foreach(var pos in tempPosBlocked)
                            {
                                structures.Add(pos, building);
                            }*/
                        }
                    }
                    else
                    {
                        var building = SpawnPrefab(buildingTypes[i].GetPrefab(), position.Key, rotation);
                        structures.Add(position.Key, building);
                        Vector3 scale = new Vector3(0.5f, UnityEngine.Random.Range(minHeight, maxHeight), 0.5f);
                        building.transform.localScale = scale;
                        building.transform.position = new Vector3(building.transform.position.x, building.transform.position.y + scale.y / 2, building.transform.position.z);
                        MeshFilter mf = building.GetComponent<MeshFilter>();
                        if (mf != null)
                        {
                            var bounds = mf.mesh.bounds;
                            var size = Vector3.Scale(bounds.size, building.transform.localScale) * repeatEvery;
                            if (scale.y > 15)
                            {
                                int materialPicker = UnityEngine.Random.Range(0, 2);
                                GameObject quad = Instantiate(Quad, transform);
                                if (materialPicker == 1)
                                {
                                    building.GetComponent<Renderer>().material = largeMat;
                                    quad.GetComponent<Renderer>().material = largeRoof;
                                }
                                else
                                {
                                    building.GetComponent<Renderer>().material = largeMat1;
                                    quad.GetComponent<Renderer>().material = largeRoof1;
                                }
                                quad.transform.position = new Vector3(building.transform.position.x, building.transform.position.y + (scale.y / 2) + 0.001f, building.transform.position.z);
                                quad.transform.localScale = new Vector3(scale.x, scale.z, scale.y);
                                quad.transform.rotation = quadRotation;

                            }
                            else
                            {
                                building.GetComponent<Renderer>().material = smallMat;
                            }
                            building.GetComponent<Renderer>().material.mainTextureScale = size;
                        }
                    }
                    break;
                }
            }
            
            //GameObject build = Instantiate(building, position, Quaternion.identity, transform);
            //Vector3 scale = new Vector3(0.5f, UnityEngine.Random.Range(minHeight, maxHeight), 0.5f);
           // build.transform.localScale = scale;

        }
    }

    private bool DoesBuildingFit(int halfSize, Dictionary<Vector3Int, Direction> freePlaces, KeyValuePair<Vector3Int, Direction> position, List<Vector3Int> blockedPos, ref List<Vector3Int> tempPosBlocked)
    {
        Vector3Int direction = Vector3Int.zero;
        if(position.Value == Direction.Down || position.Value == Direction.Up)
        {
            direction = Vector3Int.right;
        }
        else
        {
            direction = new Vector3Int(0, 0, 1);
        }
        for (int i =1; i <= halfSize; i++)
        {
            var pos1 = position.Key + direction * i;
            var pos2 = position.Key - direction * i;
            if(!freePlaces.ContainsKey(pos1) || !freePlaces.ContainsKey(pos2)||blockedPos.Contains(pos1)||blockedPos.Contains(pos2))
            {
                return false;
            }
            tempPosBlocked.Add(pos1);
            tempPosBlocked.Add(pos2);
        }
        return true;
    }

    private GameObject SpawnPrefab(GameObject prefab, Vector3Int position, Quaternion rotation)
    {
        var newBuilding = Instantiate(prefab, position, rotation, transform);
        return newBuilding;
    }

    private Dictionary<Vector3Int, Direction> findFreePlaces(List<Vector3Int> roadPos)
    {
        Dictionary<Vector3Int, Direction> freePlaces = new Dictionary<Vector3Int, Direction>();
        foreach (var postions in roadPos)
        {
            var neighbours = Placement.neighbours(postions, roadPos);
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                if (neighbours.Contains(direction) == false)
                {
                    var newPos = postions + Placement.getOffset(direction);
                    if (freePlaces.ContainsKey(newPos))
                    {
                        continue;
                    }
                    freePlaces.Add(newPos, Placement.GetDirection(direction));
                }
            }
        }
        return freePlaces;
    }
}
