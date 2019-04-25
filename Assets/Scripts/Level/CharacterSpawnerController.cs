using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawnerController : MonoBehaviour {

    private GameController gameController;
    private int playersNum;
    public GameObject[] spawners;

    public List<GameObject> playerToSpawn;
    public List<bool> selected;

    private void Awake()
    {
        gameController = GetComponent<GameController>();

        playersNum = gameController.characterIndex.Length;
        playerToSpawn = gameController.SetPlayers();

        for (int i = 0; i < spawners.Length; i++)
        {
            selected.Add(false);
            gameController.spawners.Add(spawners[i]);
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
            print("Player " + i + ": Spawn: " + rnd);

            Instantiate(playerToSpawn[i], spawners[rnd].transform.position, Quaternion.identity);
        }
    }
}
