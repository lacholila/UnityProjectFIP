using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayHUDController : MonoBehaviour {

    public List<PlayerHUD> playerHUDList;

    private void Start()
    {
        for (int i = 0; i < GameController.charactersNameList.Count; i ++)
        {
            playerHUDList[i].characterName = GameController.charactersNameList[i];
            playerHUDList[i].characterColor = GameController.charactersColorList[i];
            playerHUDList[i].characterIconSprite = GameController.characterIconList[i];

            playerHUDList[i].SetPlayerHUDColor();
        }
    }

    private void Update()
    {
        for (int i = 0; i < GameController.charactersNameList.Count; i++)
        {
            for (int j = 0; j < playerHUDList[i].characterHearts.Count; j++)
            {
                if (GameController.charactersObjectList[i].GetComponent<Character_Controller>().characterCurrentHits > j)
                {
                    playerHUDList[i].characterHearts[j].color = Color.black;
                }
                else
                {
                    playerHUDList[i].characterHearts[j].color = GameController.charactersColorList[i];
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
            GameController.ResetPlayers();
        }
    }
}
