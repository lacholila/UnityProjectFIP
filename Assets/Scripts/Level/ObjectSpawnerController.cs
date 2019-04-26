using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnerController : MonoBehaviour {

    private GameController gameController;

    public GameObject[] itemsToSpawn;
    public GameObject[] spawners;
        
    private void Awake()
    {
        gameController = GetComponent<GameController>();

        float timeToSpawn = Random.Range(1f, 10f);
        Invoke("SpawnObject", timeToSpawn);
    }

    public void SpawnObject()
    {
        int rnd = Random.Range(0, spawners.Length);
        int rnd2 = Random.Range(0, itemsToSpawn.Length);

        Instantiate(itemsToSpawn[rnd2], spawners[rnd].transform.position, Quaternion.identity);

        float timeToSpawn = Random.Range(1f, 10f);
        Invoke("SpawnObject", timeToSpawn);
    }
}