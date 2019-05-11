using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameController {

    //Variables que se deciden en el menu
    public static List<int> playerIndexList = new List<int>() { Random.Range(0, 4), Random.Range(0, 4), Random.Range(0, 4), Random.Range(0, 4) };
    public static int roundsToWin = 3;

    //Variables que se inicializan al generar los characters (se accede al modelo)
    public static List<string> charactersNameList = new List<string>();
    public static List<Sprite> characterIconList = new List<Sprite>();
    public static List<Color> charactersColorList = new List<Color>();
    public static List<GameObject> charactersObjectList = new List<GameObject>();
    public static List<GameObject> charactersAliveList = new List<GameObject>();
    public static List<bool> characterIsAliveList = new List<bool>();
    public static List<int> characterWinsList = new List<int>();

    //Resetea la lista de jugadores al reiniciar la escena
    public static void ResetPlayers()
    {
        //playerIndexList.Clear();
        charactersNameList.Clear();
        characterIconList.Clear();
        charactersColorList.Clear();
        charactersObjectList.Clear();
        charactersAliveList.Clear();
        characterIsAliveList.Clear();

        //playerIndexList = new List<int>() { Random.Range(0, 4), Random.Range(0, 4), Random.Range(0, 4), Random.Range(0, 4) };
    }

    //Reiniciar las wins
    public static void ResetWins()
    {
        characterWinsList.Clear();
        characterWinsList = new List<int>() { 0, 0, 0, 0 };
}

    //Detectar si ha acabado el juego
    public static IEnumerator CheckForNextRound()
    {
        bool gameEnded = false;

        for(int i = 0; i < characterWinsList.Count; i++)
        {
            if (characterWinsList[i] >= roundsToWin)
            {
                gameEnded = true;
                Debug.Log("Jugador " + (i + 1) + " ha ganado la partida!");
            }
        }

        yield return new WaitForSeconds(5f);

        if (!gameEnded)
        {
            StartNextRound();
        }
    }

    //Cargar el siguiente mapa
    public static void StartNextRound()
    {
        SceneManager.LoadScene(3);
        ResetPlayers();
    }
}
