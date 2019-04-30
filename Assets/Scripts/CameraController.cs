using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject background;
    public float cameraXMin, cameraXMax, cameraYMin, cameraYMax;

    private BackgroundController backgroundController;

    private List<GameObject> characterList = new List<GameObject>();
    private List<Vector2> characterPosition = new List<Vector2>();

    private Vector2 totalCharacterPosition;
    private Vector2 cameraCurrentPosition, cameraNewPosition;

    private float cameraSize;
    public float minCameraSize, maxCameraSize;

    [Range(0f, 1f)] public float cameraMovementLerp, cameraSizeLerp;

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
        float cameraWidth = Camera.main.orthographicSize * Camera.main.aspect;
        float cameraHeight = Camera.main.orthographicSize;

        //Resetear valores para recalcular
        totalCharacterPosition = Vector2.zero;

        float characterMinX = Mathf.Infinity;
        float characterMaxX = -Mathf.Infinity;
        float characterMinY = Mathf.Infinity;
        float characterMaxY = -Mathf.Infinity;

        for (int i = 0; i < characterList.Count; i++)
        {
            if (characterList[i].gameObject.activeSelf)
            {
                characterPosition[i] = characterList[i].gameObject.transform.position;
            }
            else
            {
                characterPosition[i] = Vector3.zero;
            }
            
            totalCharacterPosition += characterPosition[i];

            //Detectar distancia personajes
            if (characterPosition[i].x < characterMinX)
            {
                characterMinX = characterPosition[i].x;
            }

            if (characterPosition[i].x > characterMaxX)
            {
                characterMaxX = characterPosition[i].x;
            }

            if (characterPosition[i].y < characterMinY)
            {
                characterMinY = characterPosition[i].y;
            }

            if (characterPosition[i].y > characterMaxY)
            {
                characterMaxY = characterPosition[i].y;
            }
        }

        totalCharacterPosition /= characterPosition.Count;

        //Mover cámara
        cameraCurrentPosition = transform.position;
        cameraNewPosition = totalCharacterPosition;

        //Ampliar o reducir cámara
        cameraSize = Mathf.Max(((characterMaxX - characterMinX) / 1.5f / Camera.main.aspect + 1f), ((characterMaxY - characterMinY) / 1.5f + 1f));

        //Lerp y clamp
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, cameraSize, cameraSizeLerp);
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minCameraSize, maxCameraSize);

        transform.position = new Vector3 (Mathf.Lerp(cameraCurrentPosition.x, cameraNewPosition.x, cameraMovementLerp), Mathf.Lerp(cameraCurrentPosition.y, cameraNewPosition.y, cameraMovementLerp), transform.position.z);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, cameraXMin, cameraXMax), Mathf.Clamp(transform.position.y, cameraYMin, cameraYMax), transform.position.z);

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