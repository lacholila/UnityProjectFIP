using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawnerController : MonoBehaviour {

    private GameController gameController;
        
    public GameObject[] charactersArray;
    public GameObject[] spawners;

    public int[] playerIndexArray;
    private List<GameObject> playerToSpawn = new List<GameObject>();
    private List<bool> selected = new List<bool>();

    private void Awake()
    {
        gameController = GetComponent<GameController>();
        playerIndexArray = gameController.playerIndexArray;

        for (int i = 0; i < spawners.Length; i++)
        {
            selected.Add(false);
        }
        
        if (playerIndexArray.Length <= spawners.Length)
        {
            SetPlayers();
        }
        else
        {
            print("Hola, soy un print que evita un loop infinito :D");
        }
    }

    private void SetPlayers()
    {
        for (int i = 0; i < playerIndexArray.Length; i++)
        {
            playerToSpawn.Add(charactersArray[playerIndexArray[i]]);

            int rnd = Random.Range(0, spawners.Length);
            while (selected[rnd] == true)
            {
                rnd = Random.Range(0, spawners.Length);
            }

            selected[rnd] = true;
            print("Player " + i + ": Spawn: " + rnd);

            GameObject player = Instantiate(playerToSpawn[i], spawners[rnd].transform.position, Quaternion.identity) as GameObject;
            Character_Controller characterController = player.GetComponent<Character_Controller>();
            characterController.playerIndex = i + 1;
        }
    }
}
