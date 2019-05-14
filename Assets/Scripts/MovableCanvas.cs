using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovableCanvas : MonoBehaviour
{
    private void EnableButtons(bool _bool)
    {
        Button[] buttons = FindObjectsOfType<Button>();

        foreach (Button button in buttons)
        {
            button.interactable = _bool;
        }
    }

    public IEnumerator MoveCanvas(Transform targetTransform, float time, bool enable)
    {
        EnableButtons(false);

        float totalTime = 0;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = targetTransform.position;

        while(totalTime < time)
        {
            transform.position = Vector3.Slerp(startPosition, targetPosition, totalTime / time);
            totalTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transform.position = targetPosition;
        
        EnableButtons(enable);
        gameObject.SetActive(enable);
    }
}
