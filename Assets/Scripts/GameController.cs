using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class GameController {

    //Variables que se deciden en el mapa
    public static List<int> playerIndexList = new List<int>() { 0, 1, 2, 3 };

    //Variables que se inicializan al generar los characters (se accede al modelo)
    public static List<string> charactersNameList = new List<string>();
    public static List<Sprite> characterIconList = new List<Sprite>();
    public static List<Color> charactersColorList = new List<Color>();
    public static List<GameObject> charactersObjectList = new List<GameObject>();

    public static void ResetPlayers()
    {
        playerIndexList.Clear();
        charactersNameList.Clear();
        characterIconList.Clear();
        charactersColorList.Clear();
        charactersObjectList.Clear();

        playerIndexList = new List<int>() { 2, 3, 0, 1 }; //Random.Range(0, 4), Random.Range(0, 4), Random.Range(0, 4), Random.Range(0, 4) };
    }

    public static void Update()
    {

    }
}
