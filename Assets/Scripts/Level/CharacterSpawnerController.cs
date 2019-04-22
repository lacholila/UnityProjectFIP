using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawnerController : MonoBehaviour {

    public int playersNum;
    public GameObject[] spawners;
    public GameObject[] playerToSpawn;

    public List<bool> selected;

    private void Awake()
    {
        for (int i = 0; i < spawners.Length; i++)
        {
            selected.Add(false);
        }

        
        if (playersNum <= spawners.Length)
        {
            SetSpawns();
        }
        else
        {
            print("Hola, soy un print que evita un loop infinito :D");
        }
    }

    public void SetSpawns()
    {
        for(int i = 0; i < playersNum; i++)
        {
            int rnd = Random.Range(0, spawners.Length);
            while(selected[rnd] == true)
            {
                rnd = Random.Range(0, spawners.Length);
            }

            selected[rnd] = true;
            print("player " + i + ": Spawn: " + rnd);
        }
    }
}
