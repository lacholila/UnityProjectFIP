using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayHUDController : MonoBehaviour {

    public List<PlayerHUD> playerHUDList;
    private bool roundEnded;

    public GameObject winParticles;

    private void Start()
    {
        for (int i = 0; i < GameController.charactersNameList.Count; i ++)
        {
            playerHUDList[i].characterName = GameController.charactersNameList[i];
            playerHUDList[i].characterColor = GameController.charactersColorList[i];
            playerHUDList[i].characterIconSprite = GameController.characterIconList[i];

            playerHUDList[i].SetPlayerHUDColor();
        }

        roundEnded = false;
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

            playerHUDList[i].characterWins.text = "x" + GameController.characterWinsList[i].ToString();
            playerHUDList[i].characterWinsOutline.text = "x" + GameController.characterWinsList[i].ToString();

            //Detectar ganador
            Character_Controller characterController = GameController.charactersObjectList[i].gameObject.GetComponent<Character_Controller>();
            if (characterController.characterCurrentHits >= characterController.characterTotalHits && GameController.characterIsAliveList[i])
            {
                GameController.characterIsAliveList[i] = false;
                GameController.charactersAliveList.RemoveAt(0);
            }

            if (GameController.charactersAliveList.Count == 1 && GameController.characterIsAliveList[i])
            {
                Debug.Log("Ha ganado el jugador " + (i + 1));

                playerHUDList[i].characterWinner.gameObject.SetActive(true);
                playerHUDList[i].characterWinnerOutline.gameObject.SetActive(true);

                winParticles.gameObject.transform.position = GameController.charactersObjectList[i].transform.position;
                winParticles.gameObject.SetActive(true);

                //Cargar escena random
                if (!roundEnded)
                {
                    GameController.characterWinsList[i] = GameController.characterWinsList[i] + 1;
                    Debug.Log("VICTORIAS: " + GameController.characterWinsList[0].ToString() + GameController.characterWinsList[1].ToString() + GameController.characterWinsList[2].ToString() + GameController.characterWinsList[3].ToString());

                    StartCoroutine(GameController.CheckForNextRound());
                    roundEnded = true;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(3);
            GameController.ResetPlayers();
            GameController.ResetWins();
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            GameController.playerIndexList.Clear();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameController.playerIndexList.Add(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameController.playerIndexList.Add(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameController.playerIndexList.Add(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            GameController.playerIndexList.Add(3);
        }
    }
}
