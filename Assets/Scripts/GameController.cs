using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class GameController {

    //Variables que se deciden en el mapa
    public static List<int> playerIndexList = new List<int>() { Random.Range(0, 4), Random.Range(0, 4), Random.Range(0, 4), Random.Range(0, 4) };

    //Variables que se inicializan al generar los characters (se accede al modelo)
    public static List<string> charactersNameList = new List<string>();
    public static List<Sprite> characterIconList = new List<Sprite>();
    public static List<Color> charactersColorList = new List<Color>();

    public static void ResetPlayers()
    {
        playerIndexList.Clear();
        charactersNameList.Clear();
        characterIconList.Clear();
        charactersColorList.Clear();

        playerIndexList = new List<int>() { Random.Range(0, 4), Random.Range(0, 4), Random.Range(0, 4), Random.Range(0, 4) };
    }
}
