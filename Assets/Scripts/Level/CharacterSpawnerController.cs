using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawnerController : MonoBehaviour {
            
    public GameObject[] charactersArray;
    public GameObject[] spawners;

    private List<int> playerIndexArray = new List<int>();
    private List<GameObject> playerToSpawn = new List<GameObject>();
    private List<bool> selected = new List<bool>();

    private void Awake()
    {
        playerIndexArray = GameController.playerIndexList;

        selected.Clear();

        for (int i = 0; i < spawners.Length; i++)
        {
            selected.Add(false);
        }
        
        if (playerIndexArray.Count <= spawners.Length)
        {
            SetPlayers();
        }
        else
        {
            Debug.LogError("Illo, ¿ereh tonto? El número de players ha de ser igual o menor al número de spawners");
        }
    }

    public void SetPlayers()
    {
        GameController.charactersNameList.Clear();
        playerToSpawn.Clear();

        for (int i = 0; i < playerIndexArray.Count; i++)
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
            CharacterModel characterModel = characterController.characterModel;

            characterController.playerIndex = i + 1;

            
            

            GameController.charactersNameList.Add(characterModel.characterName);
            GameController.characterIconList.Add(characterModel.characterIcon);
            GameController.charactersColorList.Add(characterModel.characterColor);
        }
    }
}
