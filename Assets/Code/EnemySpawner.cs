using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    List<Vector3> spawnPoints;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        Transform sp = transform.Find("SpawnPositions");
        spawnPoints = new List<Vector3>();
        foreach (Transform child in sp)
        {
            spawnPoints.Add(child.position);
        }
        InvokeRepeating("Spawn", 0, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }


    void Spawn()
    {
        int index = Random.Range(0, spawnPoints.Count);
        Instantiate(enemy, spawnPoints[index], Quaternion.identity);
    }
}
