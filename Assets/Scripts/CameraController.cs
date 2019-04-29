using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject background;

    private BackgroundController backgroundController;

    private List<GameObject> characterList = new List<GameObject>();
    private List<Vector2> characterPosition = new List<Vector2>();

    private Vector2 totalCharacterPosition;
    private Vector2 backgroundCurrentPosition, backgroundNewPosition, backgroundLerpPosition, cameraCurrentPosition, cameraNewPosition, cameraLerpPosition;



    [Range(0f, 1f)] public float cameraLerp;

    private void Start ()
    {
        characterList.Clear();
        characterPosition.Clear();

        backgroundController = background.GetComponent<BackgroundController>();

        characterList = GameController.charactersObjectList;

        totalCharacterPosition = Vector2.zero;

        for (int i = 0; i < characterList.Count; i++)
        {
            characterPosition.Add(characterList[i].gameObject.transform.position);
        }
    }

    private void Update()
    {
        totalCharacterPosition = Vector2.zero;

        for (int i = 0; i < characterList.Count; i++)
        {
            characterPosition[i] = characterList[i].gameObject.transform.position;
            totalCharacterPosition += characterPosition[i];
        }

        totalCharacterPosition /= characterPosition.Count;

        cameraCurrentPosition = transform.position;
        cameraNewPosition = totalCharacterPosition;
        cameraLerpPosition = Vector3.Lerp(cameraCurrentPosition, cameraNewPosition, cameraLerp);
        
        transform.position = new Vector3(cameraLerpPosition.x, cameraLerpPosition.y, transform.position.z);
        background.transform.position = new Vector3(transform.position.x, transform.position.y, background.transform.position.z);
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < characterList.Count; i++)
        {
            Gizmos.DrawWireSphere(characterPosition[i], 0.1f);
            Gizmos.DrawWireSphere(totalCharacterPosition, 0.1f);
        }
    }
}