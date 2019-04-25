using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public int[] characterIndex;
    public GameObject[] charactersArray;
    public GameObject[] itemsArray;

    public List<GameObject> spawners;
    public List<GameObject> selectedCharacters;

    public List<GameObject> SetPlayers()
    {
        for (int i = 0; i < characterIndex.Length; i ++)
        {
            selectedCharacters.Add(charactersArray[characterIndex[i]]);
        }

        return selectedCharacters;
    }
}
