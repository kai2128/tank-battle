using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
 
public class MapCreation : MonoBehaviour
{
    // init map object
    // 0 base 
    // 1 wall
    // 2 barrier
    // 3 born effect
    // 4 river
    // 5 grass
    // 6 AirWall
    public GameObject[] item;
    
    // position occupied by an object
    private readonly List<Vector3> _itemPositionList = new();

    private void Awake()
    {
        InitBase();
        InitMapBoundary();
        GenerateMapItem();
        SpawnPlayer();
        InitSpawnEnemy();
        InvokeRepeating(nameof(SpawnEnemy), 4, 5);
    }

    private void SpawnEnemy()
    {
        var num = Random.Range(0, 3);
        var enemyPos = num switch
        {
            0 => new Vector3(-10, 8, 0),
            1 => new Vector3(0, 8, 0),
            _ => new Vector3(10, 8, 0)
        };
        CreateItem(item[3], enemyPos, Quaternion.identity);
    }

    private void InitSpawnEnemy()
    {
        CreateItem(item[3], new Vector3(-10, 8, 0), Quaternion.identity);
        CreateItem(item[3], new Vector3(0, 8, 0), Quaternion.identity);
        CreateItem(item[3], new Vector3(10, 8, 0), Quaternion.identity);
    }

    private void SpawnPlayer()
    {
        var playerBornObject = Instantiate(item[3], new Vector3(-2, -8, 0), Quaternion.identity);
        playerBornObject.GetComponent<Born>().createPlayer = true;
    }

    private void GenerateMapItem()
    {
        GenerateItem(1, 60);
        GenerateItem(2, 20);
        GenerateItem(4, 20);
        GenerateItem(5, 20);
    }

    private void GenerateItem(int itemIndex, int number)
    {
        for (int i = 0; i < number; i++)
        {
            CreateItem(item[itemIndex], CreateRandomPosition(), Quaternion.identity);
        }
    }
    
    private void InitMapBoundary()
    {
        for (int i = -11; i < 12; i++)
        {
            CreateItem(item[6], new Vector3(i, 9, 0), quaternion.identity);
        }

        for (int i = -11; i < 12; i++)
        {
            CreateItem(item[6], new Vector3(i, -9, 0), quaternion.identity);
        }

        for (int i = -8; i < 9; i++)
        {
            CreateItem(item[6], new Vector3(-11, i, 0), quaternion.identity);
        }

        for (int i = -8; i < 9; i++)
        {
            CreateItem(item[6], new Vector3(11, i, 0), quaternion.identity);
        }
    }

    private void InitBase()
    {
        // init base
        CreateItem(item[0], new Vector3(0, -8, 0), Quaternion.identity);
        // protect base using wall
        CreateItem(item[1], new Vector3(-1, -8, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(1, -8, 0), Quaternion.identity);
        for (var i = -1; i < 2; i++)
        {
            CreateItem(item[1], new Vector3(i, -7, 0), Quaternion.identity);
        }
    }

    private void CreateItem(GameObject createGameObject, Vector3 createPosition, Quaternion createRotation)
    {
        GameObject itemGo = Instantiate(createGameObject, createPosition, createRotation);
        itemGo.transform.SetParent(gameObject.transform);
        _itemPositionList.Add(createPosition);
    }

    private Vector3 CreateRandomPosition()
    {
        while (true)
        {
            var createPosition = new Vector3(Random.Range(-9, 10), Random.Range(-7, 8), 0);
            if(_itemPositionList.Contains(createPosition))
                continue;
            return createPosition;
        }
    }
}   
