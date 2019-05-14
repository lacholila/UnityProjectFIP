using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayHUDController : MonoBehaviour {

    public List<PlayerHUD> playerHUDList;
    private bool roundEnded;

    public GameObject winParticles;

    public GameObject fadeIn, fadeOut;

    private void Start()
    {
        //Instantiate(fadeIn, transform);

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

                    if (GameController.characterWinsList[i] < GameController.roundsToWin)
                    {
                        StartCoroutine(ApplyFadeOut(4f));
                    }
                    else
                    {
                        StartCoroutine(MoveWinnerHUD(playerHUDList[i].gameObject, playerHUDList[i].gameObject.transform.localPosition + Vector3.up, 1f));
                    }

                    roundEnded = true;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Nivel3");
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

    private IEnumerator ApplyFadeOut(float time)
    {
        yield return new WaitForSeconds(time);

        Instantiate(fadeOut, transform);
    }

    public IEnumerator MoveWinnerHUD(GameObject objectToMove, Vector3 targetPosition, float time)
    {
        float totalTime = 0;
        Vector3 startPosition = objectToMove.transform.localPosition;

        yield return new WaitForSeconds(3f);

        while (totalTime < time)
        {
            objectToMove.transform.localPosition = Vector3.Slerp(startPosition, targetPosition, totalTime / time);
            objectToMove.transform.localScale += new Vector3(1,1,0) * 0.005f;
            totalTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        objectToMove.transform.localPosition = targetPosition;

        yield return new WaitForSeconds(6f);

        Instantiate(fadeOut, transform);

        yield return new WaitForSeconds(1f);

        GameController.GoToMenu();
    }
}
