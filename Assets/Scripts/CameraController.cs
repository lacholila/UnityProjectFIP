using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject background;

    public BackgroundController backgroundController;

    public List<GameObject> characterList = new List<GameObject>();
    public List<Vector3> characterPosition = new List<Vector3>();

    public Vector3 totalCharacterPosition;


    private void Start ()
    {
        characterList.Clear();
        characterPosition.Clear();

        backgroundController = background.GetComponent<BackgroundController>();

        characterList = GameController.charactersObjectList;

        totalCharacterPosition = Vector3.zero;

        for (int i = 0; i < characterList.Count; i++)
        {
            characterPosition.Add(characterList[i].gameObject.transform.position);
        }
    }

    private void Update()
    {
        for (int i = 0; i < characterList.Count; i++)
        {
            characterPosition[i] = characterList[i].gameObject.transform.position;
            totalCharacterPosition += characterPosition[i];
        }

        totalCharacterPosition /= characterList.Count;
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < characterList.Count; i++)
        {
            Gizmos.DrawWireSphere(characterPosition[i], 1f);
            Gizmos.DrawWireSphere(totalCharacterPosition, 1f);
        }
        
    }
}