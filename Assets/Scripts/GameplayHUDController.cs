using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayHUDController : MonoBehaviour {

    public List<PlayerHUD> playerHUDList;
    private bool PARTIDADONE;

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
        PARTIDADONE = true;
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

                //CARGAR ESCENA RANDOM
                if (PARTIDADONE)
                {
                    GameController.characterWinsList.Insert(i, GameController.characterWinsList[i] + 1);
                    Debug.Log("VICTORIAS: " + GameController.characterWinsList[0].ToString() + GameController.characterWinsList[1].ToString() + GameController.characterWinsList[2].ToString() + GameController.characterWinsList[3].ToString());
                    Invoke("DelayChargeScene", 3f);
                    PARTIDADONE = false;
                }
            }
        }

       


        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
            GameController.ResetPlayers();
            GameController.RESETEARWINS();
        }
    }


    //Delay de scene manager
    void DelayChargeScene()
    {
        SceneManager.LoadScene(Random.Range(0,3));
        GameController.ResetPlayers();
    }
}
